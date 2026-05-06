using System.Collections.Generic;
using System.Linq;
using PhoneBook.Models;

namespace PhoneBook.Services
{
    // Singleton-реализация хранилища контактов в памяти.
    // В реальном приложении здесь была бы работа с базой данных или файлом.
    public class ContactService : IContactService
    {
        // Внутренний список — единственный источник истины для всех контактов
        private readonly List<Contact> _contacts = new List<Contact>
        {
            new Contact { Name = "Иван Иванов", Phone = "+79001234567" },
            new Contact { Name = "Мария Петрова", Phone = "+79007654321" }
        };

        // Возвращаем копию списка, чтобы внешний код не мог изменить коллекцию напрямую
        public List<Contact> GetAllContacts() => _contacts.ToList();

        // Обновляем контакт по ссылке на объект (ReferenceEquals),
        // что корректно работает даже при изменении имени
        public void UpdateContact(Contact contact)
        {
            var existing = _contacts.FirstOrDefault(c => ReferenceEquals(c, contact));
            if (existing != null)
            {
                existing.Name = contact.Name;
                existing.Phone = contact.Phone;
            }
        }
    }
}
