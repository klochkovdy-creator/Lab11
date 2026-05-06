namespace PhoneBook.Services
{
    // Контракт сервиса навигации.
    // Позволяет ViewModels инициировать переход на другой экран,
    // не зная ничего о конкретных View — соблюдается принцип разделения ответственности.
    public interface INavigationService
    {
        // Текущая активная ViewModel; ContentControl привязан к этому свойству
        object CurrentViewModel { get; }

        // Переход на экран TViewModel с опциональной передачей параметра.
        // Если TViewModel реализует INavigationAware, параметр будет передан через OnNavigatedTo.
        void NavigateTo<TViewModel>(object parameter = null) where TViewModel : class;
    }
}
