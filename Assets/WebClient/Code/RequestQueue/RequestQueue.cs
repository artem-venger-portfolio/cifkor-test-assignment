using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace WebClient
{
    [UsedImplicitly]
    public class RequestQueue : IRequestQueue
    {
        private readonly MonoBehaviourFunctions _monoBehaviourFunctions;
        private readonly Queue<IWebRequest> _requests = new();
        private readonly Queue<IWebRequest> _interimQueue = new();
        private Coroutine _handleRequestsCoroutine;
        private IWebRequest _currentRequest;

        public RequestQueue(MonoBehaviourFunctions monoBehaviourFunctions)
        {
            _monoBehaviourFunctions = monoBehaviourFunctions;
        }

        public void Add(IWebRequest request)
        {
            _requests.Enqueue(request);
        }

        public void Interrupt(RequestType requestType)
        {
            InterruptCurrentRequestWithType(requestType);
            RemoveFromQueueRequestsWithType(requestType);
        }

        public void Start()
        {
            _handleRequestsCoroutine = _monoBehaviourFunctions.RunCoroutine(GetHandeRequestsInterfaceEnumerator());
        }

        private IEnumerator GetHandeRequestsInterfaceEnumerator()
        {
            while (true)
            {
                yield return new WaitUntil(HasRequests);

                _currentRequest = _requests.Dequeue();
                _currentRequest.Send();

                while (_currentRequest !=null && _currentRequest.IsInProgress())
                {
                    yield return null;
                }

                _currentRequest = null;
            }
        }

        private bool HasRequests()
        {
            return _requests.Count > 0;
        }

        private void InterruptCurrentRequestWithType(RequestType requestType)
        {
            if (_currentRequest != null && _currentRequest.Type == requestType)
            {
                _currentRequest.Interrupt();
                _currentRequest = null;
            }
        }

        private void RemoveFromQueueRequestsWithType(RequestType requestType)
        {
            while (_requests.TryDequeue(out var currentRequest))
            {
                if (currentRequest.Type == requestType)
                {
                    continue;
                }

                _interimQueue.Enqueue(currentRequest);
            }

            while (_interimQueue.TryDequeue(out var currentRequest))
            {
                _requests.Enqueue(currentRequest);
            }
        }
    }
}