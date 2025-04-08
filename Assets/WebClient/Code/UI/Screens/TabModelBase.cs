using System;

namespace WebClient
{
    public abstract class TabModelBase
    {
        private bool _isOpen;

        public bool IsOpen
        {
            get => _isOpen;
            private set
            {
                if (_isOpen == value)
                {
                    return;
                }

                _isOpen = value;
                IsOpenChanged?.Invoke(_isOpen);
            }
        }

        public event Action<bool> IsOpenChanged;

        public void Open()
        {
            IsOpen = true;
        }

        public void Close()
        {
            IsOpen = false;
        }
    }
}