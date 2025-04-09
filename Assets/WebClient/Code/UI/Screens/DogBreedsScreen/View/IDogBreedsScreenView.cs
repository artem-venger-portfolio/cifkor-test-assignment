using System;
using System.Collections.Generic;

namespace WebClient
{
    public interface IDogBreedsScreenView
    {
        public event Action Shown;
        public event Action Hidden;
        void DisplayBreeds(IReadOnlyList<DogBreedShortInfo> breeds);
    }
}