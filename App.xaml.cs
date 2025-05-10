//using BiblioGest.Models;
//using BiblioGest.ViewModels;
//using Microsoft.Extensions.DependencyInjection;
//using System;
//using System.Windows;

//namespace BiblioGest
//{
//    public partial class App : Application
//    {
//        public static BiblioDbContext DbContext { get; private set; } = null!;

//        // This needs to be a public property
//        public IServiceProvider Services { get; private set; } = null!;

//        protected override void OnStartup(StartupEventArgs e)
//        {
//            // Initialize DbContext first
//            DbContext = new BiblioDbContext();
//            DbContext.Database.EnsureCreated();

//            // Then configure services
//            Services = ConfigureServices();

//            // Start MainWindow
//            var mainWindow = new MainWindow();
//            mainWindow.Show();

//            base.OnStartup(e);
//        }

//        private IServiceProvider ConfigureServices()
//        {
//            var services = new ServiceCollection();

//            // Provide the initialized DbContext
//            services.AddSingleton(DbContext);

//            // Register ViewModels
//            services.AddSingleton<MainViewModel>();
//            services.AddTransient<LivreViewModel>();
//            //services.AddTransient<LoginViewModel>();
//            //services.AddTransient<AdherentViewModel>();
//            //services.AddTransient<EmpruntViewModel>();

//            return services.BuildServiceProvider();
//        }
//    }
//}

using BiblioGest.Models;
using BiblioGest.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace BiblioGest
{
    public partial class App : Application
    {
        public static BiblioDbContext DbContext { get; private set; } = null!;

        // This needs to be a public property
        public IServiceProvider Services { get; private set; } = null!;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Initialize DbContext first
            DbContext = new BiblioDbContext();

            // Ensure database is created
            DbContext.Database.EnsureCreated();

            // Then configure services
            Services = ConfigureServices();

            // Start MainWindow with proper DataContext
            var mainWindow = new MainWindow
            {
                DataContext = Services.GetRequiredService<MainViewModel>()
            };

            mainWindow.Show();
        }

        private IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            // Provide the initialized DbContext
            services.AddSingleton(DbContext);

            // Register ViewModels
            services.AddSingleton<MainViewModel>();
            services.AddTransient<LivreViewModel>();
            //services.AddTransient<LoginViewModel>();
            //services.AddTransient<AdherentViewModel>();
            //services.AddTransient<EmpruntViewModel>();

            return services.BuildServiceProvider();
        }
    }
}