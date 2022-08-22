using System;
using System.Collections.Generic;
using System.Text;

namespace WinFormsContacts
{
    public class BussinessLogicLayer
    {
        private DataAccessLayer _dataAccessLayer;
        public BussinessLogicLayer()
        {
            _dataAccessLayer = new DataAccessLayer();
        }
        public Contact SaveContact(Contact contact)
        {
            if (contact.Id == 0)
            {
                _dataAccessLayer.InsertContact(contact);
            } else
            {
                _dataAccessLayer.UpdateContact(contact);
            }
            return null;
        }

        public List<Contact> GetContacts(string searchText = null)
        {
            return _dataAccessLayer.GetContacts(searchText);
        }

        public void DeleteContact(int id)
        {
            _dataAccessLayer.DeleteContact(id);
        }
    }
}
