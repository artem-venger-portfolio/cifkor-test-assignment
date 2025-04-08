using System;

namespace WebClient
{
    public interface IDogBreedsScreenView
    {
        public event Action Shown;
        public event Action Hidden;
    }
}