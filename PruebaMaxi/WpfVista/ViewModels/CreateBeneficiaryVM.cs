using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfVista.CommandHandlers;
using WpfVista.Models;
using WpfVista.RequestService;

namespace WpfVista.ViewModels
{
    public class CreateBeneficiaryVM
    {

        public ICommand CancelCommand { get; set; }
        public ICommand CreateBeneficiaryCommand { get; set; }

        public Action closeModal { get; set; }
        public Action reloadData { get; set; }

        public BeneficiaryModel beneficiary { get; set; }
        public CreateBeneficiaryVM(EmployeeModel arg, Action _closeModal, Action _reloadData)
        {
            closeModal = _closeModal;
            reloadData = _reloadData;
            CancelCommand = new CommandHandler((arg) => cancel(), true);
            CreateBeneficiaryCommand = new CommandHandler(async(arg) => await createBeneficiary(), true);

            beneficiary = new BeneficiaryModel() { IdEmployee = arg.Id, Fecha = DateTime.Now };
        }

        private void cancel()
        {
            closeModal.Invoke();
        }

        private async Task createBeneficiary()
        {
            if (beneficiary.IsValid)
            {
                IRequestServices service = new RequestServices();
                await service.RequestMethod("CrearBeneficiario", beneficiary, HttpMethod.Post, success =>
                {
                    switch (success.Code)
                    {
                        case 100:
                            reloadData.Invoke();
                            break;
                        case 200:
                            break;
                    }
                    cancel();

                }, () =>
                {
                    cancel();
                });
            }
        }
    }
}
