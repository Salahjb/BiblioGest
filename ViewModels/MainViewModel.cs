using System;
using System.Windows.Input;
using BiblioGest.Commands;
using BiblioGest.Models;

namespace BiblioGest.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly BiblioDbContext _dbContext;
        private ViewModelBase? _currentViewModel;
        private Adherent? _loggedInAdherent;
        private UserRole _userRole;

        public ViewModelBase? CurrentViewModel
        {
            get => _currentViewModel;
            private set => SetProperty(ref _currentViewModel, value);
        }

        public Adherent? LoggedInAdherent
        {
            get => _loggedInAdherent;
            private set => SetProperty(ref _loggedInAdherent, value);
        }

        public UserRole UserRole
        {
            get => _userRole;
            private set => SetProperty(ref _userRole, value);
        }

        public ICommand ShowBooksCommand { get; private set; }
        public ICommand ShowMembersCommand { get; private set; }
        public ICommand ShowLoansCommand { get; private set; }

        public MainViewModel()
        {
            _dbContext = new BiblioDbContext();
            var loginVM = new LoginViewModel(_dbContext);

            // Update the login succeeded handler to handle different roles
            loginVM.LoginSucceeded += (user, role) =>
            {
                LoggedInAdherent = user;
                UserRole = role;

                Console.WriteLine($"Login success: {user.Email} - Role: {role}"); // Debug line

                if (role == UserRole.Admin)
                {
                    // Admin gets access to all functionality
                    InitializeAdminNavigation();
                    CurrentViewModel = new LivreViewModel(_dbContext);
                }
                else
                {
                    // Regular member only gets access to the member dashboard
                    InitializeMemberNavigation();
                    CurrentViewModel = new MemberDashboardViewModel(_dbContext);
                }
            };

            CurrentViewModel = loginVM;

            // Create initial commands
            ShowBooksCommand = new RelayCommand(_ => { });
            ShowMembersCommand = new RelayCommand(_ => { });
            ShowLoansCommand = new RelayCommand(_ => { });
        }

        private void InitializeAdminNavigation()
        {
            // Full navigation for admin users
            ShowBooksCommand = new RelayCommand(_ => CurrentViewModel = new LivreViewModel(_dbContext));
            ShowMembersCommand = new RelayCommand(_ => CurrentViewModel = new AdherentViewModel(_dbContext));
            ShowLoansCommand = new RelayCommand(_ => CurrentViewModel = new EmpruntViewModel(_dbContext));

            // Notify UI that commands have changed
            OnPropertyChanged(nameof(ShowBooksCommand));
            OnPropertyChanged(nameof(ShowMembersCommand));
            OnPropertyChanged(nameof(ShowLoansCommand));
        }

        private void InitializeMemberNavigation()
        {
            // Limited navigation for regular members
            ShowBooksCommand = new RelayCommand(_ => CurrentViewModel = new MemberDashboardViewModel(_dbContext));
            
            // No-op commands for members
            ShowMembersCommand = new RelayCommand(_ => { }); 
            ShowLoansCommand = new RelayCommand(_ => { });   

            // Notify UI that commands have changed
            OnPropertyChanged(nameof(ShowBooksCommand));
            OnPropertyChanged(nameof(ShowMembersCommand));
            OnPropertyChanged(nameof(ShowLoansCommand));
        }
    }
}