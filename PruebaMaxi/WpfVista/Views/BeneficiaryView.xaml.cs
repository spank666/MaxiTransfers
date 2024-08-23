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

namespace WpfVista.Views
{
    /// <summary>
    /// Lógica de interacción para BeneficiaryView.xaml
    /// </summary>
    public partial class BeneficiaryView : UserControl
    {
        public BeneficiaryView(EmployeeModel arg, Action closeModal)
        {
            InitializeComponent();
            BeneficiaryVM vm = new BeneficiaryVM(arg, closeModal);
            this.DataContext = vm;
        }
    }
}
