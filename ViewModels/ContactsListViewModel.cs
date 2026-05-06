using System.Collections.ObjectModel;
using System.Windows.Input;
using PhoneBook.Models;
using PhoneBook.Services;

namespace PhoneBook.ViewModels
{
    // ViewModel экрана списка контактов.
    // Реализует INavigationAware для перезагрузки списка при возврате с экрана редактирования.
    public class ContactsListViewModel : ObservableObject, INavigationAware
    {
        private readonly INavigationService _navigation;
        private readonly IContactService _contactService;

        private Contact _selectedContact;
        public Contact SelectedContact
        {
            get => _selectedContact;
            set => Set(ref _selectedContact, value);
        }

        public ObservableCollection<Contact> Contacts { get; } = new ObservableCollection<Contact>();

        // Команда редактирования — переходит на экран ContactEditViewModel,
        // передавая выбранный контакт в качестве параметра навигации
        public ICommand EditContactCommand { get; }

        public ContactsListViewModel(INavigationService navigation, IContactService contactService)
        {
            _navigation = navigation;
            _contactService = contactService;

            EditContactCommand = new RelayCommand(
                _ =>
                {
                    if (SelectedContact != null)
                        _navigation.NavigateTo<ContactEditViewModel>(SelectedContact);
                },
                _ => SelectedContact != null
            );

            LoadContacts();
        }

        // Вызывается NavigationService при каждом переходе на этот экран.
        // Перезагружаем список, чтобы отобразить изменения после редактирования.
        public void OnNavigatedTo(object parameter)
        {
            LoadContacts();
        }

        private void LoadContacts()
        {
            var selected = SelectedContact;
            Contacts.Clear();
            foreach (var c in _contactService.GetAllContacts())
                Contacts.Add(c);

            // Восстанавливаем выделение по имени после перезагрузки
            if (selected != null)
            {
                foreach (var c in Contacts)
                {
                    if (c.Name == selected.Name)
                    {
                        SelectedContact = c;
                        break;
                    }
                }
            }
        }
    }
}
