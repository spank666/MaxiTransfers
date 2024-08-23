using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfVista.NotificationProperty;

namespace WpfVista.Models.ContentPresenter
{
    public class ContentPresenterModel : NotifyPropertyBase
    {
        #region Variables
        private FrameworkElement _contentControlView;
        #endregion

        #region Properties
        public FrameworkElement ContentControlView
        {
            get { return _contentControlView; }
            set
            {
                _contentControlView = value;
                OnPropertyChanged(nameof(ContentControlView));
            }
        }
        #endregion
    }
}
