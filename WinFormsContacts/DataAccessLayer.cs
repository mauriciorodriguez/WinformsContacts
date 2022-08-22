using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Runtime.Remoting.Contexts;
using System.Text;

namespace WinFormsContacts
{
    public class DataAccessLayer
    {
        private SqlConnection _conn = new SqlConnection("Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=WinFormsContacts;Data Source=DESKTOP-I5OJ7KI");
        public DataAccessLayer()
        {

        }

        public void InsertContact(Contact contact)
        {
            try
            {
                _conn.Open();
                string query = @"
                                 INSERT INTO Contacts(firstName,LastName,Phone,Address)
                                 VALUES(@firstName, @LastName, @Phone, @Address)
                                ";
                SqlParameter firstName = new SqlParameter();
                firstName.ParameterName = "@firstName";
                firstName.Value = contact.FirstName;
                firstName.DbType = System.Data.DbType.String;

                SqlParameter lastName = new SqlParameter("@LastName", contact.LastName);
                SqlParameter phone = new SqlParameter("@Phone", contact.Phone);
                SqlParameter address = new SqlParameter("@Address", contact.Address);

                SqlCommand command = new SqlCommand(query, _conn);
                command.Parameters.Add(firstName);
                command.Parameters.Add(lastName);
                command.Parameters.Add(phone);
                command.Parameters.Add(address);

                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                _conn.Close();
            }
        }
        public List<Contact> GetContacts(string searchText = null)
        {
            List<Contact> contacts = new List<Contact>();
            try
            {
                _conn.Open();
                string query = @"SELECT Id, firstName, LastName, Phone, Address FROM Contacts";
                SqlCommand command = new SqlCommand();

                if (!string.IsNullOrEmpty(searchText))
                {
                    query += @" WHERE FirstName LIKE @Search OR LastName LIKE @Search OR Phone LIKE @Search OR Address LIKE @Search";
                    command.Parameters.Add(new SqlParameter("@Search", $"%{searchText}%"));
                }
                command.CommandText = query;
                command.Connection = _conn;
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    contacts.Add(new Contact
                    {
                        Id = int.Parse(reader["Id"].ToString()),
                        FirstName = reader["firstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        Phone = reader["Phone"].ToString(),
                        Address = reader["Address"].ToString(),
                    });
                }
            }
            catch (Exception e)
            {

                throw e;
            }
            finally { _conn.Close(); }
            return contacts;
        }

        public void UpdateContact(Contact c)
        {
            try
            {
                _conn.Open();
                string query = @"UPDATE Contacts SET FirstName = @FirstName, LastName = @LastName, Phone = @Phone, Address = @Address WHERE Id = @Id";
                SqlParameter id = new SqlParameter("@Id", c.Id);
                SqlParameter firstName = new SqlParameter("@FirstName", c.FirstName);
                SqlParameter lastName = new SqlParameter("@LastName", c.LastName);
                SqlParameter phone = new SqlParameter("@Phone", c.Phone);
                SqlParameter address = new SqlParameter("@Address", c.Address);
                SqlCommand command = new SqlCommand(query, _conn);
                command.Parameters.Add(firstName);
                command.Parameters.Add(lastName);
                command.Parameters.Add(phone);
                command.Parameters.Add(address);
                command.Parameters.Add(id);
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {

                throw e;
            }
            finally
            {
                _conn.Close();
            }
        }

        public void DeleteContact(int id)
        {
            try
            {
                _conn.Open();
                string query = @"DELETE FROM Contacts WHERE Id = @Id";
                SqlCommand command = new SqlCommand(query, _conn);
                command.Parameters.Add(new SqlParameter("Id", id));
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {

                throw e;
            }
            finally { _conn.Close(); }
        }
    }
}
