using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Networking;

namespace WebClient
{
    [UsedImplicitly]
    public class TextureLoader : ITextureLoader
    {
        private readonly MonoBehaviourFunctions _monoBehaviourFunctions;
        private readonly List<UnityWebRequest> _requests;
        private readonly ITextureCache _textureCache;
        private Coroutine _workingCoroutine;

        public TextureLoader(MonoBehaviourFunctions monoBehaviourFunctions, ITextureCache textureCache)
        {
            _monoBehaviourFunctions = monoBehaviourFunctions;
            _textureCache = textureCache;
            _requests = new List<UnityWebRequest>();
        }

        public void Start()
        {
            _workingCoroutine = _monoBehaviourFunctions.RunCoroutine(GetWorkingEnumerator());
        }

        public void Stop()
        {
            _monoBehaviourFunctions.KillCoroutine(_workingCoroutine);
            _workingCoroutine = null;

            foreach (var currentRequest in _requests)
            {
                currentRequest.Dispose();
            }
            _requests.Clear();
        }

        public void Load(string url)
        {
            var textureLoadingRequest = UnityWebRequestTexture.GetTexture(url);
            textureLoadingRequest.SendWebRequest();
            _requests.Add(textureLoadingRequest);
        }

        public bool IsLoading()
        {
            return _requests.Count > 0;
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

        public bool IsOperating()
        {
            return _workingCoroutine != null;
        }

        private IEnumerator GetWorkingEnumerator()
        {
            while (true)
            {
                yield return new WaitUntil(IsLoading);

                for (var i = 0; i < _requests.Count; i++)
                {
                    var currentRequest = _requests[i];
                    if (currentRequest.isDone == false)
                    {
                        continue;
                    }

                    _requests.RemoveAt(i--);
                    FinishRequest(currentRequest);
                }
            }
        }

        private void FinishRequest(UnityWebRequest request)
        {
            if (request.result == UnityWebRequest.Result.Success)
            {
                var key = request.url;
                var texture = DownloadHandlerTexture.GetContent(request);
                _textureCache.Save(key, texture);
            }

            request.Dispose();
        }
    }
}