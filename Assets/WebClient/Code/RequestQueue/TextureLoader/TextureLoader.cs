using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine.Networking;

namespace WebClient
{
    [UsedImplicitly]
    public class TextureLoader : ITextureLoader
    {
        private readonly List<UnityWebRequest> _requests = new();

        public void StartNew(string url)
        {
            var textureLoadingRequest = UnityWebRequestTexture.GetTexture(url);
            textureLoadingRequest.SendWebRequest();
            _requests.Add(textureLoadingRequest);
        }

        public bool IsAllLoaded()
        {
            for (var i = 0; i < _requests.Count; i++)
            {
                if (_requests[i].isDone)
                {
                    continue;
                }

                return false;
            }

            return true;
        }

        public void Finish()
        {
            foreach (var loadingRequest in _requests)
            {
                loadingRequest.Dispose();
            }
            _requests.Clear();
        }
    }
}