using System.Windows.Input;
using PhoneBook.Services;

namespace PhoneBook.ViewModels
{
    // ViewModel главного окна-оболочки (Shell).
    // Управляет навигацией между экранами через команды меню.
    public class MainWindowViewModel : ObservableObject
    {
        private readonly INavigationService _navigation;

        // Публичное свойство для привязки ContentControl в XAML:
        // Content="{Binding NavigationService.CurrentViewModel}"
        public INavigationService NavigationService => _navigation;

        public ICommand ShowContactsCommand { get; }
        public ICommand ShowAboutCommand { get; }

        public MainWindowViewModel(INavigationService navigation)
        {
            _navigation = navigation;

            // При запуске сразу открываем экран списка контактов
            _navigation.NavigateTo<ContactsListViewModel>();

            // Команды меню — переключают активный экран через NavigationService
            ShowContactsCommand = new RelayCommand(_ => _navigation.NavigateTo<ContactsListViewModel>());
            ShowAboutCommand = new RelayCommand(_ => _navigation.NavigateTo<AboutViewModel>());
        }
    }
}
