namespace PhoneBook.Services
{
    // Интерфейс для ViewModel, которым нужно получать параметры при навигации.
    // NavigationService вызывает OnNavigatedTo до установки CurrentViewModel,
    // чтобы данные были готовы к моменту отображения View.
    public interface INavigationAware
    {
        void OnNavigatedTo(object parameter);
    }
}
