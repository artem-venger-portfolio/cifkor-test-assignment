using System;

namespace WebClient
{
    public sealed class UnityUIDogBreedsScreenView : UnityUINavigationPanelTabViewBase, IDogBreedsScreenView
    {
        public event Action Shown;
        public event Action Hidden;

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