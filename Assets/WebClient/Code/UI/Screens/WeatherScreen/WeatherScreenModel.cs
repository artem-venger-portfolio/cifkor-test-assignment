using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Networking;

namespace WebClient
{
    [UsedImplicitly]
    public class WeatherScreenModel : TabModelBase
    {
        private readonly MonoBehaviourFunctions _monoBehaviourFunctions;
        private readonly List<UnityWebRequest> _textureLoadingRequest;
        private readonly Dictionary<string, Texture2D> _urlToTexture;
        private readonly List<WeatherPeriod> _periods;

        public WeatherScreenModel(MonoBehaviourFunctions monoBehaviourFunctions)
        {
            _monoBehaviourFunctions = monoBehaviourFunctions;
            _textureLoadingRequest = new List<UnityWebRequest>();
            _urlToTexture = new Dictionary<string, Texture2D>();
            _periods = new List<WeatherPeriod>();
        }
    }
}