﻿using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace WebClient
{
    public class UnityUINavigationPanelToggle : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _pageNameField;

        [SerializeField]
        private Toggle _toggle;

        private UnityUINavigationPanelTabViewBase _tab;
        private bool _lastValue;

        public void SetPageName(string pageName)
        {
            name = pageName;
            _pageNameField.text = pageName;
        }

        public void SetToggleGroup(ToggleGroup toggleGroup)
        {
            _toggle.group = toggleGroup;
        }

        public void SetTab(UnityUINavigationPanelTabViewBase tab)
        {
            _tab = tab;
        }

        public void SubscribeToValueChange()
        {
            _lastValue = _toggle.isOn;
            InvokeShowOrHide();
            _toggle.onValueChanged.AddListener(ValueChangedEventHandler);
        }

        private void ValueChangedEventHandler(bool isOn)
        {
            if (_lastValue == isOn)
            {
                return;
            }
            _lastValue = isOn;

            InvokeShowOrHide();
        }

        private void InvokeShowOrHide()
        {
            if (_lastValue)
            {
                _tab.Show();
            }
            else
            {
                _tab.Hide();
            }
        }
    }
}