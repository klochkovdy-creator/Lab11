using System.Collections.Generic;
using System.Linq;
using PhoneBook.Models;

namespace PhoneBook.Services
{
    public class ContactService : IContactService
    {
        private readonly List<Contact> _contacts = new List<Contact>
        {
            new Contact { Id = 1, Name = "Иван Иванов", Phone = "+79001234567" },
            new Contact { Id = 2, Name = "Мария Петрова", Phone = "+79007654321" }
        };
        
        private int _nextId = 3;

        public List<Contact> GetAllContacts() => _contacts.ToList();

        public void UpdateContact(Contact contact)
        {
            // Ищем по Id, а не по имени
            var existing = _contacts.FirstOrDefault(c => c.Id == contact.Id);
            if (existing != null)
            {
                existing.Name = contact.Name;
                existing.Phone = contact.Phone;
            }
        }
        
        public void AddContact(Contact contact)
        {
            contact.Id = _nextId++;
            _contacts.Add(contact);
        }
        
        public void DeleteContact(int id)
        {
            var contact = _contacts.FirstOrDefault(c => c.Id == id);
            if (contact != null)
                _contacts.Remove(contact);
        }
    }
}
