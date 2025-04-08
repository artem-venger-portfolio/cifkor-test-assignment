using TMPro;
using UnityEngine;

namespace WebClient
{
    public class NavigationPanelToggle : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _pageNameField;

        public void SetPageName(string pageName)
        {
            _pageNameField.text = pageName;
        }
    }
}