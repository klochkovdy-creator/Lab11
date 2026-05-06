using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using PhoneBook.Services;
using PhoneBook.ViewModels;

namespace PhoneBook
{
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var services = new ServiceCollection();

            // Singleton — единственный экземпляр на всё время жизни приложения.
            // NavigationService хранит текущую ViewModel и управляет переключением экранов.
            services.AddSingleton<IContactService, ContactService>();

            // Singleton — сервис навигации должен быть один, чтобы все ViewModels
            // обращались к одному и тому же ContentControl через единый CurrentViewModel.
            services.AddSingleton<INavigationService, NavigationService>();

            // Singleton — главная ViewModel окна-оболочки (Shell); создаётся один раз
            // и живёт столько же, сколько главное окно.
            services.AddSingleton<MainWindowViewModel>();

            // Transient — новый экземпляр при каждом навигационном переходе.
            // Это гарантирует, что список контактов всегда загружается заново при возврате.
            services.AddTransient<ContactsListViewModel>();

            // Transient — новый экземпляр при каждом открытии формы редактирования,
            // чтобы поля не содержали данные от предыдущего редактирования.
            services.AddTransient<ContactEditViewModel>();

            // Transient — экран «О программе» пересоздаётся при каждом переходе.
            services.AddTransient<AboutViewModel>();

            // Singleton — главное окно создаётся один раз; DataContext устанавливается
            // вручную, чтобы передать MainWindowViewModel из DI-контейнера.
            services.AddSingleton<MainWindow>(provider =>
            {
                var window = new MainWindow();
                window.DataContext = provider.GetRequiredService<MainWindowViewModel>();
                return window;
            });

            var serviceProvider = services.BuildServiceProvider();
            serviceProvider.GetRequiredService<MainWindow>().Show();
        }
    }
}
