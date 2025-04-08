using UnityEngine;

namespace WebClient
{
    public interface ITextureCache
    {
        void Save(string key, Texture2D texture);
        Texture2D Get(string key);
        bool Contains(string key);
    }
}