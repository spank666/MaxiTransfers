using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfVista.CommandHandlers;
using WpfVista.Models;
using WpfVista.NotificationProperty;
using WpfVista.RequestService;

namespace WpfVista.ViewModels
{
    public class DistributePercentageVM : NotifyPropertyBase
    {
        private List<BeneficiaryModel> _items;
        public List<BeneficiaryModel> items
        {
            get { return _items; }
            set
            {
                _items = value;
                OnPropertyChanged(nameof(items));
            }
        }
        public Action closeModalAction { get; set; }
        public Action reloadData { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand DistributePercentageCommand { get; set; }

        private IRequestServices service;

        public DistributePercentageVM(List<BeneficiaryModel> beneficiaries, Action _closeModalAction, Action _reloadData)
        {
            items = beneficiaries;
            closeModalAction = _closeModalAction;
            reloadData = _reloadData;

            CancelCommand = new CommandHandler((arg) => cancel(), true);
            DistributePercentageCommand = new CommandHandler(async(arg) => await distributePercentage(), true);
            service = new RequestServices();
        }

        public void cancel()
        {
            closeModalAction.Invoke();
        }

        public async Task distributePercentage()
        {
            if (validatePercentage()==100)
            {
                await service.RequestMethod("PorcentajeParticipacionBeneficiario", items, HttpMethod.Put, success =>
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
            else
            {
                MessageBox.Show("El porcentaje de todos los beneficiarios debe ser de 100%", "Confirmation");
            }
        }

        private int validatePercentage()
        {
            return items.Sum(x => x.Porcentaje);
        }
    }
}
