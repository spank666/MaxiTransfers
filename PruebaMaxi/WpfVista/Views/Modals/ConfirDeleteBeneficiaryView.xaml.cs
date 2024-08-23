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
    /// Lógica de interacción para ConfirDeleteBeneficiaryView.xaml
    /// </summary>
    public partial class ConfirDeleteBeneficiaryView : UserControl
    {
        public ConfirDeleteBeneficiaryView(BeneficiaryModel arg, Action closeModalAction, Action reloadData)
        {
            InitializeComponent();
            ConfirmDeleteBeneficiaryVM vm = new ConfirmDeleteBeneficiaryVM(arg, closeModalAction, reloadData);
            this.DataContext = vm;
        }
    }
}
