using System;
using System.Collections;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Networking;
using Zenject;

namespace WebClient
{
    [UsedImplicitly]
    public class DogBreedsRequest : IWebRequest
    {
        private readonly MonoBehaviourFunctions _monoBehaviourFunctions;
        private readonly Action<DogBreedsRequest> _completed;
        private Coroutine _requestCoroutine;
        private UnityWebRequest _request;
        private bool _isInProgress;

        public DogBreedsRequest(MonoBehaviourFunctions monoBehaviourFunctions, Action<DogBreedsRequest> completed)
        {
            _monoBehaviourFunctions = monoBehaviourFunctions;
            _completed = completed;
        }

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

            yield return null;

            _requestCoroutine = null;
            _isInProgress = false;

            _completed(this);
        }

        [UsedImplicitly]
        public class Factory : PlaceholderFactory<Action<DogBreedsRequest>, DogBreedsRequest>
        {
        }
    }
}