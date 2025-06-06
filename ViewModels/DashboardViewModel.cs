// BiblioGest/ViewModels/DashboardViewModel.cs

using BiblioGest.Commands;
using BiblioGest.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

// --- ADD THESE USING STATEMENTS ---
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
// ------------------------------------

namespace BiblioGest.ViewModels
{
    public class DashboardViewModel : ViewModelBase
    {
        private readonly BiblioDbContext _dbContext;

        // --- Statistic Properties ---
        private int _totalLivres;
        public int TotalLivres { get => _totalLivres; set => SetProperty(ref _totalLivres, value); }
        
        private int _totalExemplaires;
        public int TotalExemplaires { get => _totalExemplaires; set => SetProperty(ref _totalExemplaires, value); }
        
        private int _totalAdherents;
        public int TotalAdherents { get => _totalAdherents; set => SetProperty(ref _totalAdherents, value); }

        private int _adherentsActifs;
        public int AdherentsActifs { get => _adherentsActifs; set => SetProperty(ref _adherentsActifs, value); }

        private int _empruntsEnCours;
        public int EmpruntsEnCours { get => _empruntsEnCours; set => SetProperty(ref _empruntsEnCours, value); }
        
        private int _empruntsEnRetard;
        public int EmpruntsEnRetard { get => _empruntsEnRetard; set => SetProperty(ref _empruntsEnRetard, value); }
        
        // --- Chart Properties ---

        // For the "Books by Category" Pie Chart
        private ISeries[] _booksByCategorySeries = Array.Empty<ISeries>();
        public ISeries[] BooksByCategorySeries
        {
            get => _booksByCategorySeries;
            set => SetProperty(ref _booksByCategorySeries, value);
        }

        // For the "Loan Activity" Column Chart
        private ISeries[] _loanActivitySeries = Array.Empty<ISeries>();
        public ISeries[] LoanActivitySeries
        {
            get => _loanActivitySeries;
            set => SetProperty(ref _loanActivitySeries, value);
        }

        private Axis[] _loanActivityXAxis = Array.Empty<Axis>();
        public Axis[] LoanActivityXAxis
        {
            get => _loanActivityXAxis;
            set => SetProperty(ref _loanActivityXAxis, value);
        }

        // --- Commands ---
        public ICommand RefreshCommand { get; }

        public DashboardViewModel(BiblioDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            RefreshCommand = new RelayCommand(_ => LoadData());
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                LoadStatistics();
                LoadChartData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement des données du tableau de bord: {ex.Message}");
            }
        }

        private void LoadStatistics()
        {
            TotalLivres = _dbContext.Livres.Count();
            TotalExemplaires = _dbContext.Livres.Sum(l => l.NombreExemplaires);
            TotalAdherents = _dbContext.Adherents.Count();
            AdherentsActifs = _dbContext.Adherents.Count(a => a.EstActif);
            EmpruntsEnCours = _dbContext.Emprunts.Count(e => e.DateRetourEffective == null);
            EmpruntsEnRetard = _dbContext.Emprunts.Count(e => e.DateRetourEffective == null && e.DateRetourPrevue < DateTime.Today);
        }

        private void LoadChartData()
        {
            // 1. Data for "Books by Category" Pie Chart
            var booksByCategory = _dbContext.Livres
                .Include(l => l.Categorie)
                .Where(l => l.Categorie != null)
                .GroupBy(l => l.Categorie.Nom)
                .Select(g => new { CategoryName = g.Key, Count = g.Count() })
                .ToList();

            BooksByCategorySeries = booksByCategory.Select(c => new PieSeries<int>
            {
                Name = c.CategoryName,
                Values = new[] { c.Count }
            }).ToArray();

            // 2. Data for "Loan Activity" Column Chart (Last 15 days)
            var startDate = DateTime.Today.AddDays(-14);
            
            var recentLoans = _dbContext.Emprunts
                .Where(e => e.DateEmprunt >= startDate)
                .GroupBy(e => e.DateEmprunt.Date)
                .Select(g => new { Date = g.Key, Count = g.Count() })
                .ToDictionary(r => r.Date, r => r.Count);

            var allDates = Enumerable.Range(0, 15).Select(i => startDate.AddDays(i));
            
            var loanCounts = allDates.Select(date => recentLoans.ContainsKey(date) ? recentLoans[date] : 0).ToList();
            
            LoanActivitySeries = new ISeries[]
            {
                new ColumnSeries<int>
                {
                    Name = "Nouveaux Emprunts",
                    Values = loanCounts,
                    Fill = new SolidColorPaint(SKColors.CornflowerBlue)
                }
            };
            
            LoanActivityXAxis = new Axis[]
            {
                new Axis
                {
                    Name = "Date",
                    Labels = allDates.Select(d => d.ToString("dd/MM")).ToList(),
                    LabelsRotation = -45
                }
            };
        }
    }
}