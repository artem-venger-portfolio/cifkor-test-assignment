using System;
using System.Collections;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Networking;
using Zenject;

namespace WebClient
{
    [UsedImplicitly]
    public class DogBreedDescriptionRequest : IWebRequest
    {
        private const string URL_TEMPLATE = "https://dogapi.dog/api/v2/breeds/{0}";
        private readonly MonoBehaviourFunctions _monoBehaviourFunctions;
        private readonly Action<DogBreedDescriptionRequest> _completed;
        private readonly IProjectLogger _logger;
        private readonly string _id;
        private DogBreedDescriptionResponse _response;
        private Coroutine _requestCoroutine;
        private UnityWebRequest _request;
        private bool _isInProgress;

        public DogBreedDescriptionRequest(MonoBehaviourFunctions monoBehaviourFunctions, IProjectLogger logger,
                                          string id, Action<DogBreedDescriptionRequest> completed)
        {
            _monoBehaviourFunctions = monoBehaviourFunctions;
            _completed = completed;
            _logger = logger;
            _id = id;
        }

        public DogBreedDescription Result { get; private set; }

        public RequestType Type => RequestType.DogBreedDescription;

        public void Send()
        {
            _requestCoroutine = _monoBehaviourFunctions.RunCoroutine(GetRequestEnumerator());
        }

        public bool IsInProgress()
        {
            return _isInProgress;
        }

        public void Interrupt()
        {
            if (IsInProgress() == false)
            {
                return;
            }

            if (_requestCoroutine != null)
            {
                _monoBehaviourFunctions.KillCoroutine(_requestCoroutine);
                _requestCoroutine = null;
            }

            _request.Dispose();
            _request = null;
        }

        private IEnumerator GetRequestEnumerator()
        {
            _isInProgress = true;

            var url = string.Format(URL_TEMPLATE, _id);
            _request = UnityWebRequest.Get(url);

            yield return _request.SendWebRequest();

            if (_request.result != UnityWebRequest.Result.Success)
            {
                _logger.LogError(message: "DogBreedDescriptionRequest failed!");
                _requestCoroutine = null;
                _isInProgress = false;
                yield break;
            }

            var json = _request.downloadHandler.text;
            _response = JsonUtility.FromJson<DogBreedDescriptionResponse>(json);

            FillInfo();

            _requestCoroutine = null;
            _isInProgress = false;

            _completed(this);
        }

        private void FillInfo()
        {
            var attributes = _response.data.attributes;
            var name = attributes.name;
            var description = attributes.description;
            Result = new DogBreedDescription(name, description);
        }

        [UsedImplicitly]
        public class Factory : PlaceholderFactory<string, Action<DogBreedDescriptionRequest>,
            DogBreedDescriptionRequest>
        {
        }
    }
}