using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfVista.NotificationProperty;

namespace WpfVista.Models
{
    public class MainWindowModel : NotifyPropertyBase
    {
        #region Private
        private bool _enableButton;
        private bool _IsVisible;
        private bool _ShowError;
        private string _ErrorText;
        #endregion

        #region Public
        public bool EnableButton
        {
            get { return _enableButton; }
            set
            {
                _enableButton = value;
                OnPropertyChanged(nameof(EnableButton));
            }
        }
        public bool IsVisible
        {
            get { return _IsVisible; }
            set
            {
                _IsVisible = value;
                OnPropertyChanged(nameof(IsVisible));
            }
        }
        public bool ShowError
        {
            get { return _ShowError; }
            set
            {
                _ShowError = value;
                OnPropertyChanged(nameof(ShowError));
            }
        }
        public string ErrorText
        {
            get { return _ErrorText; }
            set
            {
                _ErrorText = value;
                OnPropertyChanged(nameof(ErrorText));
            }
        }
        #endregion
    }
}
