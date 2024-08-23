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
    public class ConfirmDeleteEmployeeVM
    {
        #region Commands
        public ICommand NoCommand { get; set; }
        public ICommand SiCommand { get; set; }

        #endregion

        private IRequestServices service;

        public Action closeModal { get; set; }
        public Action reloadData { get; set; }
        private EmployeeModel employee;

        public ConfirmDeleteEmployeeVM(EmployeeModel _model, Action _closeModal, Action _reloadData)
        {
            SiCommand = new CommandHandler((arg) => Si(), true);
            NoCommand = new CommandHandler((arg) => No(), true);
            closeModal = _closeModal;
            service = new RequestServices();
            employee = _model;
            reloadData = _reloadData;
        }

        public void No()
        {
            closeModal.Invoke();
        }

        public void Si()
        {
            service.RequestMethod("BorrarEmpleado", employee.Id, HttpMethod.Delete, success =>
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
