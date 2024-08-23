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
    public class EditBeneficiaryVM
    {
        public Action closeModalAction { get; set; }
        public Action reloadData { get; set; }
        public BeneficiaryModel beneficiary { get; set; }

        #region Commands
        public ICommand CancelCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        #endregion

        private IRequestServices service;

        public EditBeneficiaryVM(BeneficiaryModel arg, Action _closeModalAction, Action _reloadData)
        {
            closeModalAction = _closeModalAction;
            reloadData = _reloadData;
            beneficiary = arg;

            SaveCommand = new CommandHandler(async(arg) => await saveBeneficiary(), true);
            CancelCommand = new CommandHandler((arg) => cancel(), true);

            service = new RequestServices();
        }

        public void cancel()
        {
            closeModalAction.Invoke();
        }

        public async Task saveBeneficiary()
        {
            if (beneficiary.IsValid)
            {
                await service.RequestMethod("ActualizarBeneficiario", beneficiary, HttpMethod.Put, success =>
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
