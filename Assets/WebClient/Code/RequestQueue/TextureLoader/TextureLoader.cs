using System;
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

        private IEnumerator GetWorkingEnumerator()
        {
            while (true)
            {
                yield return new WaitUntil(IsLoading);
            }
        }
    }
}