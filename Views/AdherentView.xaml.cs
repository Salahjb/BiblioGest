using BiblioGest.ViewModels;
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

namespace BiblioGest.Views
{
    /// <summary>
    /// Interaction logic for AdherentView.xaml
    /// </summary>
    public partial class AdherentView : UserControl
    {
        public AdherentView()
        {
            InitializeComponent();
        }

        private void PasswordBoxInput_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is AdherentViewModel vm && sender is PasswordBox pb)
            {
                vm.PasswordInput = pb.Password;
            }
        }
    }
}
