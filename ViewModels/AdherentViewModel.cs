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
    public class AdherentViewModel : ViewModelBase
    {
        private readonly BiblioDbContext _dbContext;

        public ObservableCollection<Adherent> Adherents { get; private set; }

        private Adherent? _selectedAdherent;
        public Adherent? SelectedAdherent
        {
            get => _selectedAdherent;
            set => SetProperty(ref _selectedAdherent, value);
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

        private Adherent _formAdherent = new();
        public Adherent FormAdherent
        {
            get => _formAdherent;
            set => SetProperty(ref _formAdherent, value);
        }

        private string _formTitle = "Ajouter un Adhérent";
        public string FormTitle
        {
            get => _formTitle;
            set => SetProperty(ref _formTitle, value);
        }

        private bool _isEditMode = false;

        private string _passwordInput;
        public string PasswordInput
        {
            get => _passwordInput;
            set => SetProperty(ref _passwordInput, value);
        }

        // Commands
        public ICommand ShowAddFormCommand { get; }
        public ICommand ShowEditFormCommand { get; }
        public ICommand SubmitFormCommand { get; }
        public ICommand CancelFormCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand SearchCommand { get; }

        public AdherentViewModel(BiblioDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

            LoadAdherents();

            ShowAddFormCommand = new RelayCommand(_ => ShowAddForm());
            ShowEditFormCommand = new RelayCommand(_ => ShowEditForm(), _ => SelectedAdherent != null);
            SubmitFormCommand = new RelayCommand(_ => SubmitForm());
            CancelFormCommand = new RelayCommand(_ => CancelForm());
            DeleteCommand = new RelayCommand(_ => DeleteAdherent(), _ => SelectedAdherent != null);
            SaveCommand = new RelayCommand(_ => SaveChanges());
            SearchCommand = new RelayCommand(_ => Search());
        }

        private void LoadAdherents()
        {
            try
            {
                var adherents = _dbContext.Adherents
                    .Include(a => a.Emprunts)
                    .ToList();

                Adherents = new ObservableCollection<Adherent>(adherents);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement des adhérents: {ex.Message}");
                Adherents = new ObservableCollection<Adherent>();
            }
        }

        private void ShowAddForm()
        {
            FormAdherent = new Adherent
            {
                DateInscription = DateTime.Now,
                EstActif = true
            };
            FormTitle = "Ajouter un Adhérent";
            _isEditMode = false;
            IsFormVisible = true;
        }

        private void ShowEditForm()
        {
            if (SelectedAdherent == null) return;

            // Create a new instance to avoid directly modifying the selected item
            FormAdherent = new Adherent
            {
                Id = SelectedAdherent.Id,
                Nom = SelectedAdherent.Nom,
                Prenom = SelectedAdherent.Prenom,
                Adresse = SelectedAdherent.Adresse,
                Email = SelectedAdherent.Email,
                Telephone = SelectedAdherent.Telephone,
                DateInscription = SelectedAdherent.DateInscription,
                EstActif = SelectedAdherent.EstActif
            };

            FormTitle = "Modifier l'Adhérent";
            _isEditMode = true;
            IsFormVisible = true;
        }

        private void SubmitForm()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(FormAdherent.Nom) ||
                    string.IsNullOrWhiteSpace(FormAdherent.Prenom) ||
                    string.IsNullOrWhiteSpace(FormAdherent.Email))
                {
                    MessageBox.Show("Nom, prénom et email sont obligatoires");
                    return;
                }

                if (!FormAdherent.Email.Contains("@") || !FormAdherent.Email.Contains("."))
                {
                    MessageBox.Show("Format d'email invalide");
                    return;
                }

                if (!_isEditMode)
                {
                    if (string.IsNullOrWhiteSpace(PasswordInput))
                    {
                        MessageBox.Show("Le mot de passe est obligatoire");
                        return;
                    }
                    FormAdherent.PasswordHash = BCrypt.Net.BCrypt.HashPassword(PasswordInput);
                    _dbContext.Adherents.Add(FormAdherent);
                }
                else
                {
                    var adherent = _dbContext.Adherents.Find(FormAdherent.Id);
                    if (adherent != null)
                    {
                        adherent.Nom = FormAdherent.Nom;
                        adherent.Prenom = FormAdherent.Prenom;
                        adherent.Adresse = FormAdherent.Adresse;
                        adherent.Email = FormAdherent.Email;
                        adherent.Telephone = FormAdherent.Telephone;
                        adherent.DateInscription = FormAdherent.DateInscription;
                        adherent.EstActif = FormAdherent.EstActif;
                        _dbContext.Adherents.Update(adherent);
                    }
                }

                var result = _dbContext.SaveChanges();

                if (result > 0)
                {
                    if (!_isEditMode)
                        Adherents.Add(FormAdherent);

                    MessageBox.Show(_isEditMode ? "Adhérent modifié avec succès" : "Adhérent ajouté avec succès");
                }

                IsFormVisible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur: {ex.Message}\n\nStack: {ex.StackTrace}");
            }
        }

        //private void SubmitForm()
        //{
        //    try
        //    {
        //        // Validate required fields
        //        if (string.IsNullOrWhiteSpace(FormAdherent.Nom))
        //        {
        //            MessageBox.Show("Le nom est obligatoire");
        //            return;
        //        }

        //        if (string.IsNullOrWhiteSpace(FormAdherent.Prenom))
        //        {
        //            MessageBox.Show("Le prénom est obligatoire");
        //            return;
        //        }

        //        if (string.IsNullOrWhiteSpace(FormAdherent.Email))
        //        {
        //            MessageBox.Show("L'email est obligatoire");
        //            return;
        //        }

        //        // Simple email validation
        //        if (!FormAdherent.Email.Contains("@") || !FormAdherent.Email.Contains("."))
        //        {
        //            MessageBox.Show("Format d'email invalide");
        //            return;
        //        }

        //        if (_isEditMode)
        //        {
        //            // Update existing - find the actual entity in the DbContext
        //            var adherent = _dbContext.Adherents.Find(FormAdherent.Id);
        //            if (adherent != null)
        //            {
        //                adherent.Nom = FormAdherent.Nom;
        //                adherent.Prenom = FormAdherent.Prenom;
        //                adherent.Adresse = FormAdherent.Adresse;
        //                adherent.Email = FormAdherent.Email;
        //                adherent.Telephone = FormAdherent.Telephone;
        //                adherent.DateInscription = FormAdherent.DateInscription;
        //                adherent.EstActif = FormAdherent.EstActif;

        //                _dbContext.Adherents.Update(adherent);
        //                var result = _dbContext.SaveChanges();

        //                if (result > 0)
        //                {
        //                    // Update the item in the observable collection
        //                    var item = Adherents.FirstOrDefault(a => a.Id == FormAdherent.Id);
        //                    if (item != null)
        //                    {
        //                        item.Nom = FormAdherent.Nom;
        //                        item.Prenom = FormAdherent.Prenom;
        //                        item.Adresse = FormAdherent.Adresse;
        //                        item.Email = FormAdherent.Email;
        //                        item.Telephone = FormAdherent.Telephone;
        //                        item.DateInscription = FormAdherent.DateInscription;
        //                        item.EstActif = FormAdherent.EstActif;
        //                    }

        //                    MessageBox.Show("Adhérent modifié avec succès");
        //                }
        //                else
        //                {
        //                    MessageBox.Show("L'adhérent n'a pas été modifié, aucun changement détecté");
        //                }
        //            }
        //            else
        //            {
        //                MessageBox.Show("L'adhérent n'a pas été trouvé dans la base de données");
        //            }
        //        }
        //        else
        //        {
        //            // Add new
        //            _dbContext.Adherents.Add(FormAdherent);
        //            var result = _dbContext.SaveChanges();

        //            if (result > 0)
        //            {
        //                // Add to observable collection only if successfully saved to database
        //                Adherents.Add(FormAdherent);
        //                MessageBox.Show("Adhérent ajouté avec succès");
        //            }
        //            else
        //            {
        //                MessageBox.Show("Erreur lors de l'ajout de l'adhérent: Aucune ligne affectée");
        //            }
        //        }

        //        IsFormVisible = false;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Erreur: {ex.Message}\n\nStack: {ex.StackTrace}");
        //    }
        //    FormAdherent.PasswordHash = BCrypt.Net.BCrypt.HashPassword("defaultPassword123"); // Or from input
        //}

        private void CancelForm()
        {
            IsFormVisible = false;
        }

        private void DeleteAdherent()
        {
            if (SelectedAdherent == null) return;

            try
            {
                // Check if member has active loans
                bool hasActiveLoans = _dbContext.Emprunts
                    .Any(e => e.AdherentId == SelectedAdherent.Id && e.DateRetourPrevue == null);

                if (hasActiveLoans)
                {
                    MessageBox.Show("Cet adhérent a des emprunts en cours. Impossible de le supprimer.");
                    return;
                }

                if (MessageBox.Show("Confirmer la suppression ?", "Suppression", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    // Find the entity in the context
                    var adherentToDelete = _dbContext.Adherents.Find(SelectedAdherent.Id);
                    if (adherentToDelete != null)
                    {
                        _dbContext.Adherents.Remove(adherentToDelete);
                        var result = _dbContext.SaveChanges();

                        if (result > 0)
                        {
                            Adherents.Remove(SelectedAdherent);
                            SelectedAdherent = null;
                            MessageBox.Show("Adhérent supprimé avec succès");
                        }
                        else
                        {
                            MessageBox.Show("Erreur lors de la suppression de l'adhérent");
                        }
                    }
                    else
                    {
                        MessageBox.Show("L'adhérent n'a pas été trouvé dans la base de données");
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
                    LoadAdherents();
                }
                else
                {
                    var lowerSearchText = SearchText.ToLower();
                    var query = _dbContext.Adherents
                        .Include(a => a.Emprunts)
                        .Where(a => a.Nom.ToLower().Contains(lowerSearchText) ||
                                    a.Prenom.ToLower().Contains(lowerSearchText) ||
                                    a.Email.ToLower().Contains(lowerSearchText))
                        .ToList();
                    Adherents = new ObservableCollection<Adherent>(query);
                }

                OnPropertyChanged(nameof(Adherents));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la recherche: {ex.Message}");
            }
        }
    }
}