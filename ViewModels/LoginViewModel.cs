using System.Windows.Input;
using System.Linq;
using System.Windows;
using BiblioGest.Commands;
using BiblioGest.Models;

namespace BiblioGest.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly BiblioDbContext _dbContext;
        private string _email = "";
        private string _password = "";

        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public ICommand LoginCommand { get; }

        public event Action<Adherent>? LoginSucceeded;

        public LoginViewModel(BiblioDbContext dbContext)
        {
            _dbContext = dbContext;
            LoginCommand = new RelayCommand(_ => Login());
        }

        private void Login()
        {
            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
            {
                MessageBox.Show("Email et mot de passe sont obligatoires.");
                return;
            }

            var user = _dbContext.Adherents.FirstOrDefault(a => a.Email == Email);
            if (user != null && BCrypt.Net.BCrypt.Verify(Password, user.PasswordHash))
            {
                LoginSucceeded?.Invoke(user);
            }
            else
            {
                MessageBox.Show("Identifiants invalides.");
            }
        }
    }
}
