using System.Windows.Input;
using PhoneBook.Models;
using PhoneBook.Services;

namespace PhoneBook.ViewModels
{

    public class ContactEditViewModel : ObservableObject, INavigationAware
    {
        private readonly INavigationService _navigation;
        private readonly IContactService _contactService;

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

            SaveCommand = new RelayCommand(_ =>
            {
                _contact.Name = EditName;
                _contact.Phone = EditPhone;
                _contactService.UpdateContact(_contact);
                _navigation.NavigateTo<ContactsListViewModel>();
            });

            CancelCommand = new RelayCommand(_ =>
                _navigation.NavigateTo<ContactsListViewModel>());
        }


        public void OnNavigatedTo(object parameter)
{
    if (parameter is Contact contact)
    {
        _contact = new Contact 
        { 
            Id = contact.Id, 
            Name = contact.Name, 
            Phone = contact.Phone 
        };
        EditName = _contact.Name;
        EditPhone = _contact.Phone;
    }
}
    }
}
