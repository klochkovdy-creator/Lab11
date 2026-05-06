using System.Windows.Input;
using PhoneBook.Models;
using PhoneBook.Services;

namespace PhoneBook.ViewModels
{
    // ViewModel экрана редактирования контакта.
    // Реализует INavigationAware: получает редактируемый контакт через OnNavigatedTo.
    public class ContactEditViewModel : ObservableObject, INavigationAware
    {
        private readonly INavigationService _navigation;
        private readonly IContactService _contactService;

        // Ссылка на оригинальный объект Contact из хранилища;
        // изменения вносятся в копию (EditName/EditPhone) и применяются только при сохранении
        private Contact _contact = new Contact();
        private string _editName = string.Empty;
        private string _editPhone = string.Empty;

        public string EditName
        {
            get => _editName;
            set => Set(ref _editName, value);
        }

        public string EditPhone
        {
            get => _editPhone;
            set => Set(ref _editPhone, value);
        }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public ContactEditViewModel(INavigationService navigation, IContactService contactService)
        {
            _navigation = navigation;
            _contactService = contactService;

            // Сохраняем изменения в оригинальный объект и возвращаемся к списку
            SaveCommand = new RelayCommand(_ =>
            {
                _contact.Name = EditName;
                _contact.Phone = EditPhone;
                _contactService.UpdateContact(_contact);
                _navigation.NavigateTo<ContactsListViewModel>();
            });

            // Отмена — возвращаемся к списку без сохранения
            CancelCommand = new RelayCommand(_ =>
                _navigation.NavigateTo<ContactsListViewModel>());
        }

        // Вызывается NavigationService перед отображением экрана.
        // Инициализируем поля редактирования данными выбранного контакта.
        public void OnNavigatedTo(object parameter)
        {
            if (parameter is Contact contact)
            {
                _contact = contact;
                EditName = contact.Name;
                EditPhone = contact.Phone;
            }
        }
    }
}
