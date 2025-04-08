using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Networking;

namespace WebClient
{
    [UsedImplicitly]
    public class RequestQueue : IRequestQueue
    {
        private readonly MonoBehaviourFunctions _monoBehaviourFunctions;
        private readonly Queue<UnityWebRequest> _requests = new();
        private readonly IProjectLogger _logger;
        private Coroutine _handleRequestsCoroutine;
        private UnityWebRequest _currentRequest;

        public RequestQueue(MonoBehaviourFunctions monoBehaviourFunctions, IProjectLogger logger)
        {
            _monoBehaviourFunctions = monoBehaviourFunctions;
            _logger = logger;
        }

        public void Add(UnityWebRequest request)
        {
            _requests.Enqueue(request);
        }

        public void Start()
        {
            _handleRequestsCoroutine = _monoBehaviourFunctions.RunCoroutine(GetHandleRequestsEnumerator());
        }

        private IEnumerator GetHandleRequestsEnumerator()
        {
            while (true)
            {
                yield return new WaitUntil(HasRequests);

                _currentRequest = _requests.Dequeue();

                yield return _currentRequest.SendWebRequest();

                var result = _currentRequest.result;
                switch (result)
                {
                    case UnityWebRequest.Result.ConnectionError:
                    case UnityWebRequest.Result.DataProcessingError:
                    case UnityWebRequest.Result.ProtocolError:
                        _logger.LogError(result.ToString());
                        break;
                    case UnityWebRequest.Result.Success:
                        _logger.LogInfo(result.ToString());
                        break;
                }
            }
        }

        private bool HasRequests()
        {
            return _requests.Count > 0;
        }
    }
}