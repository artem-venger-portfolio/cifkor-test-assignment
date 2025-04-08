using System;

namespace WebClient
{
    public abstract class TabModelBase
    {
        private bool _isOpen;

        public event Action<bool> IsOpenChanged;

        public void Open()
        {
            if (_isOpen)
            {
                return;
            }

            _isOpen = true;
            OnOpen();
            InvokeIsOpenChanged();
        }

        public void Close()
        {
            if (_isOpen == false)
            {
                return;
            }

            _isOpen = false;
            OnClose();
            InvokeIsOpenChanged();
        }

        protected virtual void OnOpen()
        {
        }

        protected virtual void OnClose()
        {
        }

        private void InvokeIsOpenChanged()
        {
            IsOpenChanged?.Invoke(_isOpen);
        }
    }
}