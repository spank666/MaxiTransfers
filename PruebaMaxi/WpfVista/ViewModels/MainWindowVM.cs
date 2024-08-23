using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfVista.CommandHandlers;
using WpfVista.Models;
using WpfVista.Models.ContentPresenter;
using WpfVista.Models.Credentials;
using WpfVista.NotificationProperty;
using WpfVista.RequestService;
using WpfVista.TokenHandler;
using WpfVista.Views;
using WpfVista.Views.Modals;

namespace WpfVista.ViewModels
{
    public class MainWindowVM : NotificationPropertyChange
    {
        #region Commands
        public ICommand LoginCommand { get; set; }
        #endregion

        #region Models
        public MainWindowModel ui { get; set; }
        public CredentialModel credential { get; set; }
        public ContentPresenterModel mainContentPresenter { get; set; }
        public ContentPresenterModalModel contentPresenterModal { get; set; }
        #endregion

        #region Contructor
        public MainWindowVM()
        {
            ui = new MainWindowModel() { EnableButton = true, IsVisible = true, ShowError = false };
            credential = new CredentialModel() { User = "maxi", Password = "123456" };
            contentPresenterModal = new ContentPresenterModalModel();
            mainContentPresenter = new ContentPresenterModel();

            LoginCommand = new CommandHandler(async(arg) => await login(), true);
        }
        #endregion

        #region Methods
        private async Task login()
        {
            ui.EnableButton = false;
            ui.ShowError = false;
            if (credential.IsValid)
            {
                IRequestServices service = new RequestServices();
                await service.RequestMethod("LoginController", credential, HttpMethod.Post, success =>
                {
                    switch (success.Code)
                    {
                        case 100:
                            TokenJWT.token = success.Result.ToString();
                            ui.IsVisible = false;
                            EmployeeView customer = new EmployeeView();
                            mainContentPresenter.ContentControlView = customer;
                            
                            break;
                        case 200:
                            ui.ShowError = true;
                            ui.ErrorText = success.Message;
                            break;
                    }
                    ui.EnableButton = true;

                }, () =>
                {
                    ui.EnableButton = true;
                });
                
            }
            else
            {
                ui.EnableButton = true;
            }
            //CreateCustomerView customer = new CreateCustomerView();
            //contentPresenterShell.ShowModal(customer);
        }
        #endregion
    }
}
