using BiblioGest.Commands;
using BiblioGest.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace BiblioGest.ViewModels
{
    public class EmpruntViewModel : ViewModelBase
    {
        private readonly BiblioDbContext _dbContext;

        public ObservableCollection<Emprunt> Emprunts { get; private set; }
        public ObservableCollection<Adherent> Adherents { get; private set; }
        public ObservableCollection<Livre> LivresDisponibles { get; private set; }

        private Emprunt? _selectedEmprunt;
        public Emprunt? SelectedEmprunt
        {
            get => _selectedEmprunt;
            set => SetProperty(ref _selectedEmprunt, value);
        }

        private Adherent? _selectedAdherent;
        public Adherent? SelectedAdherent
        {
            get => _selectedAdherent;
            set
            {
                if (SetProperty(ref _selectedAdherent, value) && value != null)
                {
                    FormEmprunt.AdherentId = value.Id;
                }
            }
        }

        private Livre? _selectedLivre;
        public Livre? SelectedLivre
        {
            get => _selectedLivre;
            set
            {
                if (SetProperty(ref _selectedLivre, value) && value != null)
                {
                    FormEmprunt.LivreISBN = value.ISBN;
                }
            }
        }

        private string _searchText = string.Empty;
        public string SearchText
        {
            get => _searchText;
            set => SetProperty(ref _searchText, value);
        }

        private bool _isFormVisible;
        public bool IsFormVisible
        {
            get => _isFormVisible;
            set => SetProperty(ref _isFormVisible, value);
        }

        private bool _isReturnVisible;
        public bool IsReturnVisible
        {
            get => _isReturnVisible;
            set => SetProperty(ref _isReturnVisible, value);
        }

        private Emprunt _formEmprunt = new();
        public Emprunt FormEmprunt
        {
            get => _formEmprunt;
            set => SetProperty(ref _formEmprunt, value);
        }

        private string _formTitle = "Ajouter un Emprunt";
        public string FormTitle
        {
            get => _formTitle;
            set => SetProperty(ref _formTitle, value);
        }

        private bool _isEditMode = false;

        // Commands
        public ICommand ShowAddFormCommand { get; }
        public ICommand ShowEditFormCommand { get; }
        public ICommand SubmitFormCommand { get; }
        public ICommand CancelFormCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand ReturnBookCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand SearchCommand { get; }

        public EmpruntViewModel(BiblioDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

            // Initialize collections
            LoadEmprunts();
            LoadAdherents();
            LoadLivresDisponibles();

            // Initialize commands
            ShowAddFormCommand = new RelayCommand(_ => ShowAddForm());
            ShowEditFormCommand = new RelayCommand(_ => ShowEditForm(), _ => SelectedEmprunt != null);
            SubmitFormCommand = new RelayCommand(_ => SubmitForm());
            CancelFormCommand = new RelayCommand(_ => CancelForm());
            DeleteCommand = new RelayCommand(_ => DeleteEmprunt(), _ => SelectedEmprunt != null);
            ReturnBookCommand = new RelayCommand(_ => ReturnBook(), _ => CanReturnBook());
            SaveCommand = new RelayCommand(_ => SaveChanges());
            SearchCommand = new RelayCommand(_ => Search());
        }

        private void LoadEmprunts()
        {
            try
            {
                var emprunts = _dbContext.Emprunts
                    .Include(e => e.Adherent)
                    .Include(e => e.Livre)
                    .ToList();
                Emprunts = new ObservableCollection<Emprunt>(emprunts);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement des emprunts: {ex.Message}");
                Emprunts = new ObservableCollection<Emprunt>();
            }
        }

        private void LoadAdherents()
        {
            try
            {
                var adherents = _dbContext.Adherents.ToList();
                Adherents = new ObservableCollection<Adherent>(adherents);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement des adhérents: {ex.Message}");
                Adherents = new ObservableCollection<Adherent>();
            }
        }

        private void LoadLivresDisponibles()
        {
            try
            {
                // Get books that are either not loaned or have been returned
                var livresDisponibles = _dbContext.Livres
                    .Where(l => l.NombreExemplaires > 0)
                    .Include(l => l.Categorie)
                    .ToList();

                // Filter out books that are currently on loan
                var livresEmpruntesNonRetournes = _dbContext.Emprunts
                    .Where(e => e.DateRetourEffective == null)
                    .Select(e => e.LivreISBN)
                    .Distinct()
                    .ToList();

                // If editing an existing loan, include the current book
                if (_isEditMode && SelectedEmprunt != null)
                {
                    var currentBookISBN = SelectedEmprunt.LivreISBN;
                    if (!livresDisponibles.Any(l => l.ISBN == currentBookISBN))
                    {
                        var bookToAdd = _dbContext.Livres
                            .Include(l => l.Categorie)
                            .FirstOrDefault(l => l.ISBN == currentBookISBN);
                        if (bookToAdd != null)
                        {
                            livresDisponibles.Add(bookToAdd);
                        }
                    }
                }

                // Add books where there are still available copies after considering current loans
                LivresDisponibles = new ObservableCollection<Livre>(
                    livresDisponibles.Where(l => !livresEmpruntesNonRetournes.Contains(l.ISBN) ||
                        (SelectedEmprunt != null && l.ISBN == SelectedEmprunt.LivreISBN))
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement des livres disponibles: {ex.Message}");
                LivresDisponibles = new ObservableCollection<Livre>();
            }
        }

        private void ShowAddForm()
        {
            FormEmprunt = new Emprunt
            {
                DateEmprunt = DateTime.Today,
                DateRetourPrevue = DateTime.Today.AddDays(14) // Default loan period: 2 weeks
            };
            FormTitle = "Ajouter un Emprunt";
            _isEditMode = false;
            IsReturnVisible = false;

            // Default to first adherent if available
            SelectedAdherent = Adherents.FirstOrDefault();
            if (SelectedAdherent != null)
            {
                FormEmprunt.AdherentId = SelectedAdherent.Id;
            }

            // Default to first available book if available
            SelectedLivre = LivresDisponibles.FirstOrDefault();
            if (SelectedLivre != null)
            {
                FormEmprunt.LivreISBN = SelectedLivre.ISBN;
            }

            IsFormVisible = true;
        }

        private void ShowEditForm()
        {
            if (SelectedEmprunt == null) return;

            // Create a new instance to avoid directly modifying the selected item
            FormEmprunt = new Emprunt
            {
                Id = SelectedEmprunt.Id,
                AdherentId = SelectedEmprunt.AdherentId,
                LivreISBN = SelectedEmprunt.LivreISBN,
                DateEmprunt = SelectedEmprunt.DateEmprunt,
                DateRetourPrevue = SelectedEmprunt.DateRetourPrevue,
                DateRetourEffective = SelectedEmprunt.DateRetourEffective
            };

            // Set the selected adherent and book
            SelectedAdherent = Adherents.FirstOrDefault(a => a.Id == SelectedEmprunt.AdherentId);
            SelectedLivre = LivresDisponibles.FirstOrDefault(l => l.ISBN == SelectedEmprunt.LivreISBN);

            // If the book is not in LivresDisponibles, reload available books to include it
            if (SelectedLivre == null)
            {
                LoadLivresDisponibles();
                SelectedLivre = LivresDisponibles.FirstOrDefault(l => l.ISBN == SelectedEmprunt.LivreISBN);
            }

            FormTitle = "Modifier l'Emprunt";
            _isEditMode = true;
            IsReturnVisible = true; // Show return date field in edit mode
            IsFormVisible = true;
        }

        private void SubmitForm()
        {
            try
            {
                // Validate required fields
                if (FormEmprunt.AdherentId <= 0)
                {
                    MessageBox.Show("Veuillez sélectionner un adhérent");
                    return;
                }

                if (string.IsNullOrWhiteSpace(FormEmprunt.LivreISBN))
                {
                    MessageBox.Show("Veuillez sélectionner un livre");
                    return;
                }

                if (FormEmprunt.DateEmprunt == DateTime.MinValue)
                {
                    MessageBox.Show("Date d'emprunt invalide");
                    return;
                }

                if (FormEmprunt.DateRetourPrevue == DateTime.MinValue)
                {
                    MessageBox.Show("Date de retour prévue invalide");
                    return;
                }

                if (_isEditMode)
                {
                    // Update existing loan
                    var emprunt = _dbContext.Emprunts.Find(FormEmprunt.Id);
                    if (emprunt != null)
                    {
                        emprunt.AdherentId = FormEmprunt.AdherentId;
                        emprunt.LivreISBN = FormEmprunt.LivreISBN;
                        emprunt.DateEmprunt = FormEmprunt.DateEmprunt;
                        emprunt.DateRetourPrevue = FormEmprunt.DateRetourPrevue;
                        emprunt.DateRetourEffective = FormEmprunt.DateRetourEffective;

                        // Update the status based on return date
                        UpdateEmpruntStatus(emprunt);

                        _dbContext.Emprunts.Update(emprunt);
                        var result = _dbContext.SaveChanges();

                        if (result > 0)
                        {
                            // Update the item in the observable collection
                            var item = Emprunts.FirstOrDefault(e => e.Id == FormEmprunt.Id);
                            if (item != null)
                            {
                                item.AdherentId = FormEmprunt.AdherentId;
                                item.LivreISBN = FormEmprunt.LivreISBN;
                                item.DateEmprunt = FormEmprunt.DateEmprunt;
                                item.DateRetourPrevue = FormEmprunt.DateRetourPrevue;
                                item.DateRetourEffective = FormEmprunt.DateRetourEffective;
                                item.Statut = emprunt.Statut;
                                item.Adherent = Adherents.FirstOrDefault(a => a.Id == FormEmprunt.AdherentId);
                                item.Livre = LivresDisponibles.FirstOrDefault(l => l.ISBN == FormEmprunt.LivreISBN);
                            }

                            MessageBox.Show("Emprunt modifié avec succès");
                        }
                        else
                        {
                            MessageBox.Show("L'emprunt n'a pas été modifié, aucun changement détecté");
                        }
                    }
                    else
                    {
                        MessageBox.Show("L'emprunt n'a pas été trouvé dans la base de données");
                    }
                }
                else
                {
                    // Check if book is already loaned and not returned
                    bool isBookLoaned = _dbContext.Emprunts
                        .Any(e => e.LivreISBN == FormEmprunt.LivreISBN && e.DateRetourEffective == null);

                    if (isBookLoaned)
                    {
                        MessageBox.Show("Ce livre est déjà emprunté et n'a pas été retourné.");
                        return;
                    }

                    // Check if the book and adherent exist
                    var livre = _dbContext.Livres.Find(FormEmprunt.LivreISBN);
                    var adherent = _dbContext.Adherents.Find(FormEmprunt.AdherentId);

                    if (livre == null)
                    {
                        MessageBox.Show("Le livre sélectionné n'existe pas dans la base de données.");
                        return;
                    }

                    if (adherent == null)
                    {
                        MessageBox.Show("L'adhérent sélectionné n'existe pas dans la base de données.");
                        return;
                    }

                    // Set the status as "En cours"
                    FormEmprunt.Statut = "En cours";

                    // Add to database context and save
                    _dbContext.Emprunts.Add(FormEmprunt);
                    var result = _dbContext.SaveChanges();

                    if (result > 0)
                    {
                        // Set the navigation properties
                        FormEmprunt.Adherent = adherent;
                        FormEmprunt.Livre = livre;

                        // Add to observable collection
                        Emprunts.Add(FormEmprunt);

                        // Refresh available books
                        LoadLivresDisponibles();

                        MessageBox.Show("Emprunt ajouté avec succès");
                    }
                    else
                    {
                        MessageBox.Show("Erreur lors de l'ajout de l'emprunt");
                    }
                }

                IsFormVisible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur: {ex.Message}");
            }
        }

        private void CancelForm()
        {
            IsFormVisible = false;
        }

        private void DeleteEmprunt()
        {
            if (SelectedEmprunt == null) return;

            try
            {
                if (MessageBox.Show("Confirmer la suppression ?", "Suppression", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    // Find the entity in the context
                    var empruntToDelete = _dbContext.Emprunts.Find(SelectedEmprunt.Id);
                    if (empruntToDelete != null)
                    {
                        _dbContext.Emprunts.Remove(empruntToDelete);
                        var result = _dbContext.SaveChanges();

                        if (result > 0)
                        {
                            Emprunts.Remove(SelectedEmprunt);
                            SelectedEmprunt = null;

                            // Refresh available books
                            LoadLivresDisponibles();

                            MessageBox.Show("Emprunt supprimé avec succès");
                        }
                        else
                        {
                            MessageBox.Show("Erreur lors de la suppression de l'emprunt");
                        }
                    }
                    else
                    {
                        MessageBox.Show("L'emprunt n'a pas été trouvé dans la base de données");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la suppression: {ex.Message}");
            }
        }

        private bool CanReturnBook()
        {
            return SelectedEmprunt != null && SelectedEmprunt.DateRetourEffective == null;
        }

        private void ReturnBook()
        {
            if (SelectedEmprunt == null || SelectedEmprunt.DateRetourEffective != null) return;

            try
            {
                var emprunt = _dbContext.Emprunts.Find(SelectedEmprunt.Id);
                if (emprunt != null)
                {
                    // Set return date to today
                    emprunt.DateRetourEffective = DateTime.Today;

                    // Update status based on return date
                    UpdateEmpruntStatus(emprunt);

                    _dbContext.Emprunts.Update(emprunt);
                    var result = _dbContext.SaveChanges();

                    if (result > 0)
                    {
                        // Update the item in the observable collection
                        SelectedEmprunt.DateRetourEffective = emprunt.DateRetourEffective;
                        SelectedEmprunt.Statut = emprunt.Statut;

                        // Refresh the view
                        OnPropertyChanged(nameof(SelectedEmprunt));

                        // Refresh available books
                        LoadLivresDisponibles();

                        MessageBox.Show("Livre retourné avec succès");
                    }
                    else
                    {
                        MessageBox.Show("Erreur lors de l'enregistrement du retour");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur: {ex.Message}");
            }
        }

        private void UpdateEmpruntStatus(Emprunt emprunt)
        {
            if (emprunt.DateRetourEffective == null)
            {
                // Not returned yet
                if (DateTime.Today > emprunt.DateRetourPrevue)
                {
                    emprunt.Statut = "En retard";
                }
                else
                {
                    emprunt.Statut = "En cours";
                }
            }
            else
            {
                // Returned
                if (emprunt.DateRetourEffective > emprunt.DateRetourPrevue)
                {
                    emprunt.Statut = "Retourné en retard";
                }
                else
                {
                    emprunt.Statut = "Retourné";
                }
            }
        }

        private void SaveChanges()
        {
            try
            {
                var result = _dbContext.SaveChanges();
                if (result > 0)
                {
                    MessageBox.Show("Changements enregistrés avec succès");
                }
                else
                {
                    MessageBox.Show("Aucun changement à enregistrer");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'enregistrement: {ex.Message}");
            }
        }

        private void Search()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(SearchText))
                {
                    LoadEmprunts();
                }
                else
                {
                    var lowerSearchText = SearchText.ToLower();
                    var query = _dbContext.Emprunts
                        .Include(e => e.Adherent)
                        .Include(e => e.Livre)
                        .Where(e => e.Livre.Titre.ToLower().Contains(lowerSearchText) ||
                                   e.Adherent.Nom.ToLower().Contains(lowerSearchText) ||
                                   e.Adherent.Prenom.ToLower().Contains(lowerSearchText) ||
                                   e.Statut.ToLower().Contains(lowerSearchText))
                        .ToList();
                    Emprunts = new ObservableCollection<Emprunt>(query);
                }

                OnPropertyChanged(nameof(Emprunts));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la recherche: {ex.Message}");
            }
        }
    }
}