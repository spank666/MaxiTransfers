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
using WpfVista.Views.Modals;

namespace WpfVista.ViewModels
{
    public class BeneficiaryVM : NotifyPropertyBase
    {
        public EmployeeModel employee { get; set; }
        private List<BeneficiaryModel> _beneficiaries;
        public List<BeneficiaryModel> beneficiaries
        {
            get { return _beneficiaries; }
            set
            {
                _beneficiaries = value;
                OnPropertyChanged(nameof(beneficiaries));
            }
        }

        public Action closeModal { get; set; }

        public Action closeModalAction { get; set; }
        public Action reloadData { get; set; }
        public ContentPresenterModalModel contentPresenterModal { get; set; }

        public ICommand RegresarCommand { get; set; }
        public ICommand CreateBeneficiaryCommand { get; set; }
        public ICommand DeleteBeneficiaryCommand { get; set; }
        public ICommand EditBeneficiaryCommand { get; set; }
        public ICommand PercentageCommand { get; set; }
        private IRequestServices service;

        public BeneficiaryVM(EmployeeModel arg, Action _closeModal)
        {
            employee = arg;
            closeModal = _closeModal;
            contentPresenterModal = new ContentPresenterModalModel();
            service = new RequestServices();

            RegresarCommand = new CommandHandler((arg) => regresar(), true);
            CreateBeneficiaryCommand = new CommandHandler((arg) => createBeneficiary(), true);
            DeleteBeneficiaryCommand = new CommandHandler((arg) => deleteBeneficiary(arg), true);
            EditBeneficiaryCommand = new CommandHandler((arg) => editBeneficiary(arg), true);
            PercentageCommand = new CommandHandler((arg) => distributePercentage(), true);

            closeModalAction = () =>
            {
                close();
            };

            reloadData = async () =>
            {
                await LoadAllBeneficiaries();
            };

            LoadAllBeneficiaries();
        }

        private async Task LoadAllBeneficiaries()
        {
            await service.RequestMethod("ListaBeneficiarios", employee.Id, HttpMethod.Get, success =>
            {
                switch (success.Code)
                {
                    case 100:
                        beneficiaries = Newtonsoft.Json.JsonConvert.DeserializeObject<List<BeneficiaryModel>>(success.Result.ToString());
                        break;
                    case 200:
                        break;
                }

            }, () =>
            {

            });
        }

        private void regresar()
        {
            closeModal.Invoke();
        }

        private void createBeneficiary()
        {
            CreateBeneficiaryView create = new CreateBeneficiaryView(employee, closeModalAction, reloadData);
            contentPresenterModal.ShowModal(create);
        }

        private void deleteBeneficiary(object arg)
        {
            ConfirDeleteBeneficiaryView delete = new ConfirDeleteBeneficiaryView((BeneficiaryModel)arg, closeModalAction, reloadData);
            contentPresenterModal.ShowModal(delete);
        }

        private void editBeneficiary(object arg)
        {
            EditBeneficiaryView edit = new EditBeneficiaryView((BeneficiaryModel)arg, closeModalAction, reloadData);
            contentPresenterModal.ShowModal(edit);
        }

        private void distributePercentage()
        {
            DistributePercentageView per = new DistributePercentageView(beneficiaries, closeModalAction, reloadData);
            contentPresenterModal.ShowModal(per);
        }

        public void close()
        {
            contentPresenterModal.CloseModal();
        }
    }
}
