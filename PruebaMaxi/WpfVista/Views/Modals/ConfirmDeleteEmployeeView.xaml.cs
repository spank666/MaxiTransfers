using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfVista.Models;
using WpfVista.ViewModels;

namespace WpfVista.Views.Modals
{
    /// <summary>
    /// Lógica de interacción para ConfirmDeleteEmployeeView.xaml
    /// </summary>
    public partial class ConfirmDeleteEmployeeView : UserControl
    {
        public ConfirmDeleteEmployeeView(EmployeeModel model, Action closeModal, Action reloadData)
        {
            InitializeComponent();

            ConfirmDeleteEmployeeVM delete = new ConfirmDeleteEmployeeVM(model, closeModal, reloadData);
            this.DataContext = delete;
        }
    }
}
