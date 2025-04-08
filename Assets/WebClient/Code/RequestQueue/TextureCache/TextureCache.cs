using System.Collections.Generic;
using UnityEngine;

namespace WebClient
{
    public class TextureCache : ITextureCache
    {
        private readonly Dictionary<string, Texture2D> _keyToTexture;

        public void Save(string key, Texture2D texture)
        {
            _keyToTexture.Add(key, texture);
        }

        public bool Contains(string key)
        {
            return _keyToTexture.ContainsKey(key);
        }
    }
}