using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows;
using WpfVista.NotificationProperty;

namespace WpfVista.Models.ContentPresenter
{
    public class ContentPresenterModalModel : NotifyPropertyBase
    {
        #region Variables
        private FrameworkElement? _contentControlView;
        private bool _isModalVisible;
        #endregion

        #region Properties
        public FrameworkElement? ContentControlView
        {
            get { return _contentControlView; }
            set
            {
                _contentControlView = value;
                OnPropertyChanged(nameof(ContentControlView));
            }
        }

        public bool IsModalVisible
        {
            get { return _isModalVisible; }
            set
            {
                _isModalVisible = value;
                OnPropertyChanged(nameof(IsModalVisible));
            }
        }

        public void ShowModal<T>(T param) where T : FrameworkElement
        {
            ContentControlView = param;
            IsModalVisible = true;
        }

        public void ShowModal<T>(T param, Action<object> closeAction) where T : FrameworkElement
        {
            ContentControlView = param;
            IsModalVisible = true;
        }

        //public void ShowMsg(string Title, string Msg, MessageBoxType TypeMessage, bool ShowImage = false, string? ImageUri = null, string? OkButtonCaption = null, string? CancelButtonCaption = null, Action<object>? YesAction = null, Action<object>? NoAction = null, int? width = null, int? height = null)
        //{
        //    var messageBoxVM = new MessageBoxVM();
        //    var model = messageBoxVM.MessageBoxModel = new MessageBoxModel();
        //    messageBoxVM.OkButtonVisibility = Visibility.Visible;
        //    messageBoxVM.CancelButtonVisibility = TypeMessage.Equals(MessageBoxType.Question) ? Visibility.Visible : Visibility.Collapsed;
        //    messageBoxVM.OkButtonContent = string.IsNullOrEmpty(OkButtonCaption) ? RecursoManager.GetString("OK") : OkButtonCaption;
        //    messageBoxVM.CancelButtonContent = string.IsNullOrEmpty(CancelButtonCaption) ? RecursoManager.GetString("Cancelar") : CancelButtonCaption;
        //    if (ShowImage || !string.IsNullOrEmpty(ImageUri))
        //    {
        //        var bitmap = new BitmapImage();
        //        bitmap.BeginInit();
        //        if (string.IsNullOrEmpty(ImageUri))
        //        {
        //            switch (TypeMessage)
        //            {
        //                case MessageBoxType.Question:
        //                    bitmap.UriSource = new Uri("/SigueLinkUI_NetCore;component/Assets/Images/InformativeMessage/question.png", UriKind.Relative);
        //                    break;
        //                case MessageBoxType.Warning:
        //                    bitmap.UriSource = new Uri("/SigueLinkUI_NetCore;component/Assets/Images/InformativeMessage/warning.png", UriKind.Relative);
        //                    break;
        //                default:
        //                    bitmap.UriSource = new Uri("/SigueLinkUI_NetCore;component/Assets/Images/InformativeMessage/informative.png", UriKind.Relative);
        //                    break;
        //            }
        //        }
        //        else
        //        {
        //            bitmap.UriSource = new Uri(ImageUri, UriKind.Relative);
        //        }
        //        bitmap.EndInit();
        //        messageBoxVM.ImageMessage = bitmap;
        //    }
        //    model.Title = Title;
        //    model.Message = Msg;
        //    model.Width = width ?? 700;
        //    model.Height = height ?? 400;
        //    messageBoxVM.CloseAction = NoAction += c => this.CloseModal();
        //    messageBoxVM.OkAction = YesAction += c => this.CloseModal();
        //    ContentControlView = new MessageBoxView(messageBoxVM);
        //    IsModalVisible = true;
        //}

        public void CloseModal()
        {
            IsModalVisible = false;
            ContentControlView = null;
        }
        #endregion
    }
}
