using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace ContactManager
{
    class DBHandler
    {
        readonly string ConString = ConfigurationManager.ConnectionStrings["Contacts"].ConnectionString;


        public List<Person> ReadAllPersons()
        {
            List<Person> listPerson = new List<Person>();

            using (SqlConnection con = new SqlConnection(ConString))
            {
                con.Open();
                SqlCommand cm = new SqlCommand("Select * from Person", con);
                using (SqlDataReader reader = cm.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Person person = new Person();

                        person.FirstName = reader["FirstName"].ToString();
                        person.LastName = reader["LastName"].ToString();
                        person.Email = reader["Email"].ToString();
                        person.Phone = reader["Phone"].ToString();

                        listPerson.Add(person);
                    }
                    return listPerson;
                }
            }
        }
        public int AddPerson(Person person)
        {
            int newID = 0;
            int row = -1;

            using (SqlConnection con = new SqlConnection(ConString))
            {
                con.Open();

                string query = "INSERT INTO Person (FirstName, LastName, Email, Phone) VALUES (@FirstName, @LastName, @Email, @Phone)";

                SqlCommand command = new SqlCommand(query, con);

                command.Parameters.AddWithValue("@FirstName", person.FirstName);
                command.Parameters.AddWithValue("@LastName", person.LastName);
                command.Parameters.AddWithValue("@City", person.Email);
                command.Parameters.AddWithValue("@Age", person.Phone);

                try
                {
                    row = command.ExecuteNonQuery();
                    string query2 = "Select @@Identity as newID from Person";
                    command.CommandText = query2;
                    command.Connection = con;
                    newID = Convert.ToInt32(command.ExecuteScalar());
                }
                catch (SqlException e)
                {
                    Console.WriteLine("Error Generated. Details: " + e.ToString());
                }
            }
            return newID;
        }
    }
}
