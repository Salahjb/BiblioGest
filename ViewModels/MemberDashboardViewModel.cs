using System.Collections.ObjectModel;
using System.Linq;
using BiblioGest.Models;

namespace BiblioGest.ViewModels
{
    public class MemberDashboardViewModel : ViewModelBase
    {
        private readonly BiblioDbContext _dbContext;
        private ObservableCollection<Livre> _livres;
        private string _searchText = string.Empty;

        public ObservableCollection<Livre> Livres
        {
            get => _livres;
            set => SetProperty(ref _livres, value);
        }

        public string SearchText
        {
            get => _searchText;
            set
            {
                if (SetProperty(ref _searchText, value))
                {
                    FilterLivres();
                }
            }
        }

        public MemberDashboardViewModel(BiblioDbContext dbContext)
        {
            _dbContext = dbContext;
            LoadLivres();
        }

        private void LoadLivres()
        {
            var livres = _dbContext.Livres
                .OrderBy(l => l.Titre)
                .ToList();

            Livres = new ObservableCollection<Livre>(livres);
        }

        private void FilterLivres()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                LoadLivres();
                return;
            }

            var filteredLivres = _dbContext.Livres
                .Where(l => l.Titre.Contains(SearchText) ||
                           l.Auteur.Contains(SearchText) ||
                           l.ISBN.Contains(SearchText))
                .OrderBy(l => l.Titre)
                .ToList();

            Livres = new ObservableCollection<Livre>(filteredLivres);
        }
    }
}