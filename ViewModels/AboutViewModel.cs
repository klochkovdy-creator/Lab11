namespace PhoneBook.ViewModels
{

    public class AboutViewModel : ObservableObject
    {
        public string Title => "О программе";
        public string Version => "PhoneBook v1.0";
        public string Description => "Пример навигации ViewModel-First в WPF.";
    }
}
