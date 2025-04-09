using System;
using System.Collections.Generic;

namespace WebClient
{
    public interface IDogBreedsScreenView
    {
        public event Action Shown;
        public event Action Hidden;
        event Action<int> BreedClicked;
        void DisplayBreeds(IReadOnlyList<DogBreedShortInfo> breeds);
        void SetLoadingScreenActive(bool isActive);
        void ShowLoadingIndicator(int index);
        void HideLoadingIndicator(int index);
    }
}