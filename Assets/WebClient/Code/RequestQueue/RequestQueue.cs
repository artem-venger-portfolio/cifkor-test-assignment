using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine.Networking;

namespace WebClient
{
    [UsedImplicitly]
    public class RequestQueue : IRequestQueue
    {
        private readonly Queue<UnityWebRequest> _requests = new();

        public void Add(UnityWebRequest request)
        {
            _requests.Enqueue(request);
        }
    }
}