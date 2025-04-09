using System;
using System.Collections.Generic;
using UnityEngine;

namespace WebClient
{
    public sealed class UnityUIDogBreedsScreenView : UnityUINavigationPanelTabViewBase, IDogBreedsScreenView
    {
        [SerializeField]
        private RectTransform _content;

        [SerializeField]
        private DogBreedEntry _entryTemplate;
        
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