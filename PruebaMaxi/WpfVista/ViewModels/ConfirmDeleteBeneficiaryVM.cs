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
    public class ConfirmDeleteBeneficiaryVM
    {
        #region Commands
        public ICommand NoCommand { get; set; }
        public ICommand SiCommand { get; set; }

        #endregion

        public Action closeModalAction { get; set; }
        public Action reloadData { get; set; }
        private IRequestServices service;
        private BeneficiaryModel beneficiary;

        public ConfirmDeleteBeneficiaryVM(BeneficiaryModel arg, Action _closeModalAction, Action _reloadData)
        {
            closeModalAction = _closeModalAction;
            reloadData = _reloadData;

            SiCommand = new CommandHandler(async(arg) => await Si(), true);
            NoCommand = new CommandHandler((arg) => No(), true);

            service = new RequestServices();
            beneficiary = arg;
        }

        public void No()
        {
            closeModalAction.Invoke();
        }

        public async Task Si()
        {
            await service.RequestMethod("BorrarBeneficiario", beneficiary.Id, HttpMethod.Delete, success =>
            {
                switch (success.Code)
                {
                    case 100:
                        reloadData.Invoke();
                        break;
                    case 200:

                        break;
                }
                No();
            }, () =>
            {
                No();
            });
        }
    }
}
