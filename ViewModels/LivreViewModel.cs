
using BiblioGest.Commands;
using BiblioGest.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace BiblioGest.ViewModels
{
    public class LivreViewModel : ViewModelBase
    {
        private readonly BiblioDbContext _dbContext;

        public ObservableCollection<Livre> Livres { get; private set; }
        public ObservableCollection<Categorie> Categories { get; private set; }

        private Livre? _selectedLivre;
        public Livre? SelectedLivre
        {
            get => _selectedLivre;
            set => SetProperty(ref _selectedLivre, value);
        }

        private Categorie? _selectedCategorie;
        public Categorie? SelectedCategorie
        {
            get => _selectedCategorie;
            set
            {
                if (SetProperty(ref _selectedCategorie, value) && value != null)
                {
                    FormLivre.CategorieId = value.Id;
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

        private Livre _formLivre = new();
        public Livre FormLivre
        {
            get => _formLivre;
            set => SetProperty(ref _formLivre, value);
        }

        private string _formTitle = "Ajouter un Livre";
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
        public ICommand SaveCommand { get; }
        public ICommand SearchCommand { get; }

        public LivreViewModel(BiblioDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

            // Log connection state
            MessageBox.Show($"Database connection state: {_dbContext.Database.CanConnect()}");

            LoadBooks();
            LoadCategories();

            ShowAddFormCommand = new RelayCommand(_ => ShowAddForm());
            ShowEditFormCommand = new RelayCommand(_ => ShowEditForm(), _ => SelectedLivre != null);
            SubmitFormCommand = new RelayCommand(_ => SubmitForm());
            CancelFormCommand = new RelayCommand(_ => CancelForm());
            DeleteCommand = new RelayCommand(_ => DeleteLivre(), _ => SelectedLivre != null);
            SaveCommand = new RelayCommand(_ => SaveChanges());
            SearchCommand = new RelayCommand(_ => Search());
        }

        private void LoadBooks()
        {
            try
            {
                var books = _dbContext.Livres
                    .Include(l => l.Categorie) // Eager load the Categorie for each Livre
                    .ToList();
                MessageBox.Show($"Loaded {books.Count} books from database");
                Livres = new ObservableCollection<Livre>(books);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading books: {ex.Message}");
                Livres = new ObservableCollection<Livre>();
            }
        }

        private void LoadCategories()
        {
            try
            {
                var categories = _dbContext.Categories.ToList();
                MessageBox.Show($"Loaded {categories.Count} categories from database");
                Categories = new ObservableCollection<Categorie>(categories);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading categories: {ex.Message}");
                Categories = new ObservableCollection<Categorie>();
            }
        }

        private void ShowAddForm()
        {
            FormLivre = new Livre();
            FormTitle = "Ajouter un Livre";
            _isEditMode = false;
            IsFormVisible = true;

            // Default to first category if available
            SelectedCategorie = Categories.FirstOrDefault();
            if (SelectedCategorie != null)
            {
                FormLivre.CategorieId = SelectedCategorie.Id;
            }
        }

        private void ShowEditForm()
        {
            if (SelectedLivre == null) return;

            // Create a new instance to avoid directly modifying the selected item
            FormLivre = new Livre
            {
                ISBN = SelectedLivre.ISBN,
                Titre = SelectedLivre.Titre,
                Auteur = SelectedLivre.Auteur,
                Editeur = SelectedLivre.Editeur,
                AnneePublication = SelectedLivre.AnneePublication,
                NombreExemplaires = SelectedLivre.NombreExemplaires,
                CategorieId = SelectedLivre.CategorieId
            };

            // Set the selected category based on the book's category ID
            SelectedCategorie = Categories.FirstOrDefault(c => c.Id == SelectedLivre.CategorieId);

            FormTitle = "Modifier le Livre";
            _isEditMode = true;
            IsFormVisible = true;
        }

        private void SubmitForm()
        {
            try
            {
                // Validate required fields
                if (string.IsNullOrWhiteSpace(FormLivre.ISBN))
                {
                    MessageBox.Show("ISBN est obligatoire");
                    return;
                }

                if (string.IsNullOrWhiteSpace(FormLivre.Titre))
                {
                    MessageBox.Show("Titre est obligatoire");
                    return;
                }

                if (string.IsNullOrWhiteSpace(FormLivre.Auteur))
                {
                    MessageBox.Show("Auteur est obligatoire");
                    return;
                }

                // Ensure CategorieId is valid (greater than 0)
                if (FormLivre.CategorieId <= 0)
                {
                    MessageBox.Show("Veuillez sélectionner une catégorie valide");
                    return;
                }

                if (_isEditMode)
                {
                    // Update existing - find the actual entity in the DbContext
                    var livre = _dbContext.Livres.Find(FormLivre.ISBN);
                    if (livre != null)
                    {
                        livre.Titre = FormLivre.Titre;
                        livre.Auteur = FormLivre.Auteur;
                        livre.Editeur = FormLivre.Editeur;
                        livre.AnneePublication = FormLivre.AnneePublication;
                        livre.NombreExemplaires = FormLivre.NombreExemplaires;
                        livre.CategorieId = FormLivre.CategorieId;

                        _dbContext.Livres.Update(livre); // Explicitly tell EF that the entity has been modified
                        var result = _dbContext.SaveChanges();

                        if (result > 0)
                        {
                            // Update the item in the observable collection
                            var item = Livres.FirstOrDefault(l => l.ISBN == FormLivre.ISBN);
                            if (item != null)
                            {
                                item.Titre = FormLivre.Titre;
                                item.Auteur = FormLivre.Auteur;
                                item.Editeur = FormLivre.Editeur;
                                item.AnneePublication = FormLivre.AnneePublication;
                                item.NombreExemplaires = FormLivre.NombreExemplaires;
                                item.CategorieId = FormLivre.CategorieId;
                                item.Categorie = Categories.FirstOrDefault(c => c.Id == FormLivre.CategorieId);
                            }

                            MessageBox.Show("Livre modifié avec succès");
                        }
                        else
                        {
                            MessageBox.Show("Le livre n'a pas été modifié, aucun changement détecté");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Le livre n'a pas été trouvé dans la base de données");
                    }
                }
                else
                {
                    // Add new - check if ISBN already exists
                    if (_dbContext.Livres.Any(l => l.ISBN == FormLivre.ISBN))
                    {
                        MessageBox.Show("Un livre avec le même ISBN existe déjà.");
                        return;
                    }

                    // Debug information
                    string debugInfo = $"Adding book: ISBN={FormLivre.ISBN}, Title={FormLivre.Titre}, Author={FormLivre.Auteur}, " +
                                       $"CategoryId={FormLivre.CategorieId}, Copies={FormLivre.NombreExemplaires}";
                    MessageBox.Show(debugInfo);

                    try
                    {
                        // Check if the category exists
                        var category = _dbContext.Categories.Find(FormLivre.CategorieId);
                        if (category == null)
                        {
                            MessageBox.Show($"La catégorie avec ID {FormLivre.CategorieId} n'existe pas!");
                            return;
                        }

                        // Add to database context and save
                        _dbContext.Livres.Add(FormLivre);
                        var result = _dbContext.SaveChanges();

                        MessageBox.Show($"SaveChanges result: {result}");

                        if (result > 0)
                        {
                            // Set the category object for the new book
                            FormLivre.Categorie = category;

                            // Add to observable collection only if successfully saved to database
                            Livres.Add(FormLivre);
                            MessageBox.Show("Livre ajouté avec succès");
                        }
                        else
                        {
                            MessageBox.Show("Erreur lors de l'ajout du livre: Aucune ligne affectée");
                        }
                    }
                    catch (DbUpdateException dbEx)
                    {
                        MessageBox.Show($"Erreur de base de données: {dbEx.Message}\n\nInner: {dbEx.InnerException?.Message}");
                    }
                }

                IsFormVisible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur: {ex.Message}\n\nStack: {ex.StackTrace}");
            }
        }

        private void CancelForm()
        {
            IsFormVisible = false;
        }

        private void DeleteLivre()
        {
            if (SelectedLivre == null) return;

            try
            {
                if (MessageBox.Show("Confirmer la suppression ?", "Suppression", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    // Find the entity in the context
                    var livreToDelete = _dbContext.Livres.Find(SelectedLivre.ISBN);
                    if (livreToDelete != null)
                    {
                        _dbContext.Livres.Remove(livreToDelete);
                        var result = _dbContext.SaveChanges();

                        if (result > 0)
                        {
                            Livres.Remove(SelectedLivre);
                            SelectedLivre = null;
                            MessageBox.Show("Livre supprimé avec succès");
                        }
                        else
                        {
                            MessageBox.Show("Erreur lors de la suppression du livre");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Le livre n'a pas été trouvé dans la base de données");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la suppression: {ex.Message}");
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
                    LoadBooks();
                }
                else
                {
                    var lowerSearchText = SearchText.ToLower();
                    var query = _dbContext.Livres
                        .Include(l => l.Categorie) // Make sure to include the Categorie
                        .Where(l => l.Titre.ToLower().Contains(lowerSearchText) ||
                                    l.Auteur.ToLower().Contains(lowerSearchText) ||
                                    l.ISBN.ToLower().Contains(lowerSearchText))
                        .ToList();
                    Livres = new ObservableCollection<Livre>(query);
                }

                OnPropertyChanged(nameof(Livres));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la recherche: {ex.Message}");
            }
        }
    }
}