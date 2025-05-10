using BiblioGest.Commands;
using BiblioGest.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace BiblioGest.ViewModels
{
    public class LivreViewModel : ViewModelBase
    {
        private readonly BiblioDbContext _dbContext;
        private ObservableCollection<Livre> _livres;
        private Livre? _selectedLivre;
        private string _searchText = string.Empty;

        public LivreViewModel(BiblioDbContext dbContext)
        {
            _dbContext = dbContext;
            _livres = new ObservableCollection<Livre>(
                _dbContext.Livres.ToList());

            AddCommand = new RelayCommand(_ => AddLivre(), _ => CanAddLivre());
            EditCommand = new RelayCommand(_ => EditLivre(), _ => CanEditLivre());
            DeleteCommand = new RelayCommand(_ => DeleteLivre(), _ => CanDeleteLivre());
            SaveCommand = new RelayCommand(_ => SaveChanges(), _ => CanSaveChanges());
            SearchCommand = new RelayCommand(_ => Search());
        }

        public ObservableCollection<Livre> Livres
        {
            get => _livres;
            set => SetProperty(ref _livres, value);
        }

        public Livre? SelectedLivre
        {
            get => _selectedLivre;
            set => SetProperty(ref _selectedLivre, value);
        }

        public string SearchText
        {
            get => _searchText;
            set => SetProperty(ref _searchText, value);
        }

        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand SearchCommand { get; }

        private void AddLivre()
        {
            var newLivre = new Livre { Titre = "Nouveau livre" };
            _dbContext.Livres.Add(newLivre);
            Livres.Add(newLivre);
            SelectedLivre = newLivre;
        }

        private bool CanAddLivre() => true;

        private void EditLivre()
        {
            // Implementation for editing a book
        }

        private bool CanEditLivre() => SelectedLivre != null;

        private void DeleteLivre()
        {
            if (SelectedLivre == null) return;

            _dbContext.Livres.Remove(SelectedLivre);
            Livres.Remove(SelectedLivre);
            SelectedLivre = null;
        }

        private bool CanDeleteLivre() => SelectedLivre != null;

        private void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        private bool CanSaveChanges() => true;

        private void Search()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                Livres = new ObservableCollection<Livre>(_dbContext.Livres.ToList());
                return;
            }

            var query = _dbContext.Livres
                .Where(l => l.Titre.Contains(SearchText) ||
                            l.Auteur.Contains(SearchText) ||
                            l.ISBN.Contains(SearchText))
                .ToList();

            Livres = new ObservableCollection<Livre>(query);
        }
    }
}