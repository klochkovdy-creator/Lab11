using System.Collections.Generic;
using PhoneBook.Models;

namespace PhoneBook.Services
{
    // Контракт сервиса работы с контактами.
    // Абстракция позволяет легко заменить хранилище (in-memory → база данных)
    // без изменения ViewModels.
    public interface IContactService
    {
        List<Contact> GetAllContacts();
        void UpdateContact(Contact contact);
    }
}
