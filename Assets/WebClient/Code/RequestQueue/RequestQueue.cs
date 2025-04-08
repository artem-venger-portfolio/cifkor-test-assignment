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
        private readonly IProjectLogger _logger;
        private Coroutine _handleRequestsCoroutine;
        private IWebRequest _currentRequest;

        public RequestQueue(MonoBehaviourFunctions monoBehaviourFunctions, IProjectLogger logger)
        {
            _monoBehaviourFunctions = monoBehaviourFunctions;
            _logger = logger;
        }

        public void Add(IWebRequest request)
        {
            _requests.Enqueue(request);
        }

        public void Start()
        {
            _handleRequestsCoroutine = _monoBehaviourFunctions.RunCoroutine(GetHandeRequestsInterfaceEnumerator());
        }

        private IEnumerator GetHandeRequestsInterfaceEnumerator()
        {
            while (true)
            {
                yield return new WaitUntil(HasRequestsInterfaces);

                _currentRequest = _requests.Dequeue();
                _currentRequest.Send();

                while (_currentRequest.IsInProgress())
                {
                    yield return null;
                }

                _currentRequest = null;
            }
        }

        private bool HasRequestsInterfaces()
        {
            return _requests.Count > 0;
        }
    }
}