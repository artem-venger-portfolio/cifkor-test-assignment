using System;
using System.Collections.Generic;

namespace WebClient
{
    public sealed class UnityUIDogBreedsScreenView : UnityUINavigationPanelTabViewBase, IDogBreedsScreenView
    {
        public event Action Shown;
        public event Action Hidden;

        public void DisplayBreeds(IReadOnlyList<DogBreedShortInfo> breeds)
        {
        }

        protected override void OnShow()
        {
            Shown?.Invoke();
        }

        protected override void OnHide()
        {
            Hidden?.Invoke();
        }
    }
}