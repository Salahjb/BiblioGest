using BiblioGest.ViewModels;
using System.Windows;
using System.Windows.Controls;


namespace BiblioGest.Views
{
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
