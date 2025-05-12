using BiblioGest.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using BiblioGest.Commands;

namespace BiblioGest.ViewModels
{
    public class CategorieViewModel : ViewModelBase
    {
        private readonly BiblioDbContext _dbContext;

        public ObservableCollection<Categorie> Categories { get; private set; }

        private Categorie? _selectedCategorie;
        public Categorie? SelectedCategorie
        {
            get => _selectedCategorie;
            set => SetProperty(ref _selectedCategorie, value);
        }

        private Categorie _formCategorie = new();
        public Categorie FormCategorie
        {
            get => _formCategorie;
            set => SetProperty(ref _formCategorie, value);
        }

        private string _formTitle = "Ajouter une Catégorie";
        public string FormTitle
        {
            get => _formTitle;
            set => SetProperty(ref _formTitle, value);
        }

        private bool _isEditMode = false;
        private bool _isFormVisible;
        public bool IsFormVisible
        {
            get => _isFormVisible;
            set => SetProperty(ref _isFormVisible, value);
        }
        
        private string _searchText = string.Empty;
        public string SearchText
        {
            get => _searchText;
            set => SetProperty(ref _searchText, value);
        }

        public ICommand ShowAddFormCommand { get; }
        public ICommand ShowEditFormCommand { get; }
        public ICommand SubmitFormCommand { get; }
        public ICommand CancelFormCommand { get; }
        public ICommand DeleteCommand { get; }
        
        public ICommand SearchCommand { get; }

        public CategorieViewModel(BiblioDbContext dbContext)
        {
            _dbContext = dbContext;
            LoadCategories();

            ShowAddFormCommand = new RelayCommand(_ => ShowAddForm());
            ShowEditFormCommand = new RelayCommand(_ => ShowEditForm(), _ => SelectedCategorie != null);
            SubmitFormCommand = new RelayCommand(_ => SubmitForm());
            CancelFormCommand = new RelayCommand(_ => CancelForm());
            DeleteCommand = new RelayCommand(_ => DeleteCategorie(), _ => SelectedCategorie != null);
            SearchCommand = new RelayCommand(_ => Search());
        }
        
        private void Search()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(SearchText))
                {
                    LoadCategories(); // Assumes you have a method to reload all categories
                }
                else
                {
                    var lowerSearchText = SearchText.ToLower();
                    var query = _dbContext.Categories
                        .Where(c => c.Nom.ToLower().Contains(lowerSearchText)).ToList();

                    Categories = new ObservableCollection<Categorie>(query);
                }

                OnPropertyChanged(nameof(Categories));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la recherche: {ex.Message}");
            }
        }


        private void LoadCategories()
        {
            try
            {
                var categories = _dbContext.Categories.ToList();
                Categories = new ObservableCollection<Categorie>(categories);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur de chargement des catégories : {ex.Message}");
                Categories = new ObservableCollection<Categorie>();
            }
        }

        private void ShowAddForm()
        {
            FormCategorie = new Categorie();
            FormTitle = "Ajouter une Catégorie";
            _isEditMode = false;
            IsFormVisible = true;
        }

        private void ShowEditForm()
        {
            if (SelectedCategorie == null) return;

            FormCategorie = new Categorie
            {
                Id = SelectedCategorie.Id,
                Nom = SelectedCategorie.Nom,
                Description = SelectedCategorie.Description
            };
            FormTitle = "Modifier la Catégorie";
            _isEditMode = true;
            IsFormVisible = true;
        }

        private void SubmitForm()
        {
            if (string.IsNullOrWhiteSpace(FormCategorie.Nom))
            {
                MessageBox.Show("Le nom de la catégorie est obligatoire.");
                return;
            }

            if (_isEditMode)
            {
                var existing = _dbContext.Categories.Find(FormCategorie.Id);
                if (existing != null)
                {
                    existing.Nom = FormCategorie.Nom;
                    existing.Description = FormCategorie.Description;
                    _dbContext.SaveChanges();

                    var item = Categories.FirstOrDefault(c => c.Id == existing.Id);
                    if (item != null)
                    {
                        item.Nom = existing.Nom;
                        item.Description = existing.Description;
                    }

                    MessageBox.Show("Catégorie modifiée avec succès");
                }
            }
            else
            {
                _dbContext.Categories.Add(FormCategorie);
                _dbContext.SaveChanges();
                Categories.Add(FormCategorie);
                MessageBox.Show("Catégorie ajoutée avec succès");
            }

            IsFormVisible = false;
        }

        private void CancelForm()
        {
            IsFormVisible = false;
        }

        private void DeleteCategorie()
        {
            if (SelectedCategorie == null) return;

            if (MessageBox.Show("Confirmer la suppression ?", "Suppression", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                var entity = _dbContext.Categories.Find(SelectedCategorie.Id);
                if (entity != null)
                {
                    _dbContext.Categories.Remove(entity);
                    _dbContext.SaveChanges();
                    Categories.Remove(SelectedCategorie);
                    SelectedCategorie = null;
                    MessageBox.Show("Catégorie supprimée avec succès");
                }
            }
        }
    }
}
