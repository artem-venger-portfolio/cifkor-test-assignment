using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Networking;
using Zenject;

namespace WebClient
{
    [UsedImplicitly]
    public class DogBreedsRequest : IWebRequest
    {
        private const string URL = "https://dogapi.dog/api/v2/breeds";
        private readonly MonoBehaviourFunctions _monoBehaviourFunctions;
        private readonly Action<DogBreedsRequest> _completed;
        private readonly IProjectLogger _logger;
        private DogBreedsResponse _response;
        private Coroutine _requestCoroutine;
        private UnityWebRequest _request;
        private bool _isInProgress;

        public DogBreedsRequest(MonoBehaviourFunctions monoBehaviourFunctions, Action<DogBreedsRequest> completed,
                                IProjectLogger logger)
        {
            _monoBehaviourFunctions = monoBehaviourFunctions;
            _completed = completed;
            _logger = logger;
            Result = new List<DogBreedShortInfo>();
        }

        public List<DogBreedShortInfo> Result { get; }

        public RequestType Type => RequestType.DogBreeds;

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

            _request = UnityWebRequest.Get(URL);

            yield return _request;

            if (_request.result != UnityWebRequest.Result.Success)
            {
                _logger.LogError(message: "DogBreedsRequest failed!");
                _requestCoroutine = null;
                _isInProgress = false;
                yield break;
            }

            var json = _request.downloadHandler.text;
            _response = JsonUtility.FromJson<DogBreedsResponse>(json);

            FillInfo();

            _requestCoroutine = null;
            _isInProgress = false;

            _completed(this);
        }

        private void FillInfo()
        {
            Result.Clear();
            foreach (var currentData in _response.data)
            {
                var id = currentData.id;
                var name = currentData.attributes.name;
                Result.Add(new DogBreedShortInfo(id, name));
            }
        }

        [UsedImplicitly]
        public class Factory : PlaceholderFactory<Action<DogBreedsRequest>, DogBreedsRequest>
        {
        }
    }
}