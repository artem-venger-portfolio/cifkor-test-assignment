using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine.Networking;

namespace WebClient
{
    [UsedImplicitly]
    public class RequestQueue : IRequestQueue
    {
        private Queue<UnityWebRequest> _requests = new();
    }
}