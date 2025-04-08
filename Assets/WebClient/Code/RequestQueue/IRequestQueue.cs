using UnityEngine.Networking;

namespace WebClient
{
    public interface IRequestQueue
    {
        public void Add(UnityWebRequest request);
        public void Add(IWebRequest request);
        public void Start();
    }
}