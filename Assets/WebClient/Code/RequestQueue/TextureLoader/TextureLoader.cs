using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine.Networking;

namespace WebClient
{
    [UsedImplicitly]
    public class TextureLoader : ITextureLoader
    {
        private readonly List<UnityWebRequest> _requests;
        private readonly ITextureCache _textureCache;

        public TextureLoader(ITextureCache textureCache)
        {
            _textureCache = textureCache;
            _requests = new List<UnityWebRequest>();
        }

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
                if (loadingRequest.result == UnityWebRequest.Result.Success)
                {
                    var key = loadingRequest.url;
                    var texture = DownloadHandlerTexture.GetContent(loadingRequest);
                    _textureCache.Save(key, texture);
                }

                loadingRequest.Dispose();
            }
            _requests.Clear();
        }

        public bool IsLoading(string url)
        {
            foreach (var currentRequest in _requests)
            {
                if (currentRequest.url == url)
                {
                    return true;
                }
            }

            return false;
        }
    }
}