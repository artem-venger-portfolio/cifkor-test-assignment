using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace WebClient
{
    public class DogBreedEntry : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _numberField;

        [SerializeField]
        private TMP_Text _breedNameField;

        [SerializeField]
        private GameObject _loadingIndicator;

        [SerializeField]
        private Button _button;

        private int _index;

        private void Start()
        {
            _button.onClick.AddListener(ButtonClickedEventHandler);
        }

        public event Action<int> Clicked;

        public void SetIndex(int index)
        {
            _index = index;
            var number = _index + 1;
            _numberField.text = number.ToString();
        }

        public void SetName(string breedName)
        {
            _breedNameField.text = breedName;
        }

        public void SetLoadingIndicatorActive(bool isActive)
        {
            _loadingIndicator.SetActive(isActive);
        }

        private void ButtonClickedEventHandler()
        {
            Clicked?.Invoke(_index);
        }
    }
}