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

        private readonly List<DogBreedEntry> _entries = new();

        public event Action Shown;
        public event Action Hidden;

        public void DisplayBreeds(IReadOnlyList<DogBreedShortInfo> breeds)
        {
            for (var i = 0; i < breeds.Count; i++)
            {
                if (i >= _entries.Count)
                {
                    var newEntry = Instantiate(_entryTemplate, _content);
                    newEntry.Clicked += EntryClickedEventHandler;
                    _entries.Add(newEntry);
                }

                var currentBreed = breeds[i];
                var currentEntry = _entries[i];
                currentEntry.SetIndex(i);
                currentEntry.SetName(currentBreed.Name);
            }
        }

        protected override void OnShow()
        {
            Shown?.Invoke();
        }

        protected override void OnHide()
        {
            foreach (var currentEntry in _entries)
            {
                currentEntry.gameObject.SetActive(value: false);
            }

            Hidden?.Invoke();
        }

        private void EntryClickedEventHandler(int index)
        {
        }
    }
}