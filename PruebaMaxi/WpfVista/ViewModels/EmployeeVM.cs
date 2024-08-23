using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfVista.CommandHandlers;
using WpfVista.Models;
using WpfVista.Models.ContentPresenter;
using WpfVista.NotificationProperty;
using WpfVista.RequestService;
using WpfVista.Views;
using WpfVista.Views.Modals;

namespace WpfVista.ViewModels
{
    public class EmployeeVM : NotifyPropertyBase
    {
        #region Commands
        public ICommand CreateEmployeeCommand { get; set; }
        public ICommand EditEmployeeCommand { get; set; }
        public ICommand DeleteEmployeeCommand { get; set; }
        public ICommand BeneficiariesCommand { get; set; }
        public ICommand CloseModalCommand { get; set; }
        #endregion
        public Action closeModal { get; set; }
        public Action reloadData { get; set; }

        private ObservableCollection<EmployeeModel> _employees;
        //public ObservableCollection<EmployeeModel> Employees { get; set; }

        public ObservableCollection<EmployeeModel> Employees
        {
            get { return _employees; }
            set
            {
                _employees = value;
                OnPropertyChanged(nameof(Employees));
            }
        }


        public ContentPresenterModalModel contentPresenterModal { get; set; }

        private IRequestServices service;
        public EmployeeVM()
        {
            contentPresenterModal = new ContentPresenterModalModel();
            service = new RequestServices();

            CreateEmployeeCommand = new CommandHandler((arg) => createEmployee(), true);
            EditEmployeeCommand = new CommandHandler((arg) => editEmployee(arg), true);
            DeleteEmployeeCommand = new CommandHandler((arg) => deleteEmployee(arg), true);
            BeneficiariesCommand = new CommandHandler((arg) => beneficiaries(arg), true);
            CloseModalCommand = new CommandHandler((arg) => close(), true);

            closeModal = () =>
            {
                close();
            };

            reloadData = async() =>
            {
                await LoadAllEmployees();
            };


            LoadAllEmployees();
        }

        private async Task LoadAllEmployees()
        {
            await service.RequestMethod("ListaEmpleados", HttpMethod.Get, success =>
            {
                switch (success.Code)
                {
                    case 100:
                        Employees = Newtonsoft.Json.JsonConvert.DeserializeObject<ObservableCollection<EmployeeModel>>(success.Result.ToString());
                        break;
                    case 200:
                        break;
                }

            }, () =>
            {

            });
        }

        public void createEmployee()
        {
            CreateEmployeeView customer = new CreateEmployeeView(closeModal, reloadData);
            contentPresenterModal.ShowModal(customer);
        }

        public void editEmployee(object arg)
        {
            EditEmployeeView edit = new EditEmployeeView((EmployeeModel)arg, closeModal, reloadData);
            contentPresenterModal.ShowModal(edit);
        }

        public void deleteEmployee(object arg)
        {
            ConfirmDeleteEmployeeView delete = new ConfirmDeleteEmployeeView((EmployeeModel)arg, closeModal, reloadData);
            contentPresenterModal.ShowModal(delete);
        }

        public void beneficiaries(object arg)
        {
            BeneficiaryView bene = new BeneficiaryView((EmployeeModel)arg, closeModal);
            contentPresenterModal.ShowModal(bene);
        }

        public void close()
        {
            contentPresenterModal.CloseModal();
        }
    }
}
