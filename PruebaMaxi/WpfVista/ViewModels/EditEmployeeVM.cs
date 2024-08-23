using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Common;
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
    public class EditEmployeeVM
    {
        public EmployeeModel employee {  get; set; }
        public Action closeModal { get; set; }
        public Action reloadData { get; set; }

        #region Commands
        public ICommand CloseCommand { get; set; }
        public ICommand CreateEmployeeCommand { get; set; }

        #endregion

        private IRequestServices service;

        public EditEmployeeVM(EmployeeModel _model, Action _closeModal, Action _reloadData)
        {
            employee = _model;
            closeModal = _closeModal;
            reloadData = _reloadData;

            CreateEmployeeCommand = new CommandHandler(async(arg) => await editEmployee(), true);
            CloseCommand = new CommandHandler((arg) => close(), true);

            service = new RequestServices();
        }

        public void close()
        {
            closeModal.Invoke();
        }

        public async Task editEmployee()
        {
            if (employee.IsValid)
            {
                await service.RequestMethod("ActualizarEmpleado", employee, HttpMethod.Put, success =>
                {
                    switch (success.Code)
                    {
                        case 100:
                            reloadData.Invoke();
                            break;
                        case 200:

                            break;
                    }
                    close();
                }, () =>
                {
                    close();
                });
            }
        }
    }
}
