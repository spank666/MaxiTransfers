using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfVista.CommandHandlers;
using WpfVista.Models;
using WpfVista.Models.ContentPresenter;
using WpfVista.RequestService;
using WpfVista.Views.Modals;

namespace WpfVista.ViewModels
{
    public class CreateEmployeeVM
    {
        public ICommand CreateEmployeeCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public Action closeModal { get; set; }
        public Action reloadData { get; set; }
        public EmployeeModel employee { get; set; }
        public CreateEmployeeVM(Action _closeModal, Action _reloadData)
        {
            closeModal = _closeModal;
            reloadData = _reloadData;
            CreateEmployeeCommand = new CommandHandler(async (arg) => await createEmployee(), true);
            CloseCommand = new CommandHandler((arg) => close(), true);

            employee = new EmployeeModel() { Fecha = DateTime.Now };
        }

        private async Task createEmployee()
        {
            if (employee.IsValid)
            {
                IRequestServices service = new RequestServices();
                await service.RequestMethod("CrearEmpleado", employee, HttpMethod.Post, success =>
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

        public void close()
        {
            closeModal.Invoke();
        }
    }
}
