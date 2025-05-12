//using System;
//using System.Windows.Input;
//using System.Linq;
//using System.Windows;
//using BiblioGest.Commands;
//using BiblioGest.Models;

//namespace BiblioGest.ViewModels
//{
//    public class LoginViewModel : ViewModelBase
//    {
//        private readonly BiblioDbContext _dbContext;
//        private string _email = "";
//        private string _password = "";
//        private string _errorMessage = "";
//        private bool _hasError;

//        public string Email
//        {
//            get => _email;
//            set
//            {
//                if (SetProperty(ref _email, value))
//                {
//                    // Clear error when user types
//                    HasError = false;
//                    ErrorMessage = "";
//                }
//            }
//        }

//        public string Password
//        {
//            get => _password;
//            set
//            {
//                if (SetProperty(ref _password, value))
//                {
//                    // Clear error when user types
//                    HasError = false;
//                    ErrorMessage = "";
//                }
//            }
//        }

//        public string ErrorMessage
//        {
//            get => _errorMessage;
//            set => SetProperty(ref _errorMessage, value);
//        }

//        public bool HasError
//        {
//            get => _hasError;
//            set => SetProperty(ref _hasError, value);
//        }

//        public ICommand LoginCommand { get; }

//        // Event that will be raised when login is successful
//        public event Action<Adherent>? LoginSucceeded;

//        public LoginViewModel(BiblioDbContext dbContext)
//        {
//            _dbContext = dbContext;
//            LoginCommand = new RelayCommand(_ => Login(), _ => CanLogin());
//        }

//        private bool CanLogin()
//        {
//            return !string.IsNullOrWhiteSpace(Email) && !string.IsNullOrWhiteSpace(Password);
//        }

//        public void Login()
//        {
//            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
//            {
//                SetError("Email et mot de passe sont obligatoires.");
//                return;
//            }

//            var user = _dbContext.Adherents.FirstOrDefault(a => a.Email == Email);

//            if (user != null && BCrypt.Net.BCrypt.Verify(Password, user.PasswordHash))
//            {
//                // If we get here, login succeeded
//                LoginSucceeded?.Invoke(user);
//            }
//            else
//            {
//                SetError("Identifiants invalides.");
//            }
//        }

//        private void SetError(string message)
//        {
//            ErrorMessage = message;
//            HasError = true;
//        }
//    }
//}

using System;
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
        private string _errorMessage = "";
        private bool _hasError;

        public string Email
        {
            get => _email;
            set
            {
                if (SetProperty(ref _email, value))
                {
                    // Clear error when user types
                    HasError = false;
                    ErrorMessage = "";
                }
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                if (SetProperty(ref _password, value))
                {
                    // Clear error when user types
                    HasError = false;
                    ErrorMessage = "";
                }
            }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }

        public bool HasError
        {
            get => _hasError;
            set => SetProperty(ref _hasError, value);
        }

        public ICommand LoginCommand { get; }

        // Update event to include role information
        public event Action<Adherent, UserRole>? LoginSucceeded;

        public LoginViewModel(BiblioDbContext dbContext)
        {
            _dbContext = dbContext;
            LoginCommand = new RelayCommand(_ => Login(), _ => CanLogin());
        }

        private bool CanLogin()
        {
            return !string.IsNullOrWhiteSpace(Email) && !string.IsNullOrWhiteSpace(Password);
        }

        public void Login()
        {
            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
            {
                SetError("Email et mot de passe sont obligatoires.");
                return;
            }

            var user = _dbContext.Adherents.FirstOrDefault(a => a.Email == Email);

            if (user != null && BCrypt.Net.BCrypt.Verify(Password, user.PasswordHash))
            {
                // If we get here, login succeeded
                // Pass both the user and their role to the handler
                Console.WriteLine($"Login success: {user.Email} - Role: {user.Role}"); // Debug line
                LoginSucceeded?.Invoke(user, user.Role);
            }
            else
            {
                SetError("Identifiants invalides.");
            }
        }

        private void SetError(string message)
        {
            ErrorMessage = message;
            HasError = true;
        }
    }
}