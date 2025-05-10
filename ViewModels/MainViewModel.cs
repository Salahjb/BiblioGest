using System.Windows.Input;
using BiblioGest.Commands;
using BiblioGest.Models;

namespace BiblioGest.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly BiblioDbContext _dbContext;

        private ViewModelBase? _currentViewModel;
        public ViewModelBase? CurrentViewModel
        {
            get => _currentViewModel;
            private set => SetProperty(ref _currentViewModel, value);
        }

        public ICommand ShowBooksCommand { get; }
        public ICommand ShowMembersCommand { get; }
        public ICommand ShowLoansCommand { get; }

        public MainViewModel()
        {
            _dbContext = new BiblioDbContext(); // Or use dependency injection

            // Initialize commands
            ShowBooksCommand = new RelayCommand(_ => CurrentViewModel = new LivreViewModel(_dbContext));
            //ShowMembersCommand = new RelayCommand(_ => CurrentViewModel = new MembersViewModel(_dbContext));
            //ShowLoansCommand = new RelayCommand(_ => CurrentViewModel = new LoansViewModel(_dbContext));

            // Set default view
            CurrentViewModel = new LivreViewModel(_dbContext);
        }
    }
}
