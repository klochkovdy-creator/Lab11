using Microsoft.Extensions.DependencyInjection;
using System;

namespace PhoneBook.Services
{
    // Singleton-сервис навигации: единственный экземпляр на всё время жизни приложения.
    // Отвечает за переключение экранов путём замены CurrentViewModel,
    // к которому привязан ContentControl в главном окне.
    public class NavigationService : ObservableObject, INavigationService
    {
        // DI-контейнер для создания экземпляров ViewModel по запросу
        private readonly IServiceProvider _serviceProvider;
        private object _currentViewModel;

        public NavigationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        // Текущая активная ViewModel; при изменении ContentControl автоматически
        // подбирает соответствующий View через DataTemplate
        public object CurrentViewModel
        {
            get => _currentViewModel;
            private set => Set(ref _currentViewModel, value);
        }

        public void NavigateTo<TViewModel>(object parameter = null) where TViewModel : class
        {
            // 1. Получаем ViewModel из DI-контейнера
            var viewModel = _serviceProvider.GetRequiredService<TViewModel>();

            // 2. Если ViewModel поддерживает приём параметров — передаём их до отображения,
            //    чтобы данные были готовы к моменту рендеринга View
            if (viewModel is INavigationAware aware)
            {
                aware.OnNavigatedTo(parameter);
            }

            // 3. Обновляем CurrentViewModel — ContentControl подхватит изменение через привязку
            CurrentViewModel = viewModel;
        }
    }
}