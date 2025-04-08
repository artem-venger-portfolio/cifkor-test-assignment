using UnityEngine;

namespace WebClient
{
    public interface ITextureCache
    {
        void Save(string key, Texture2D texture);
        bool Contains(string key);
    }
}