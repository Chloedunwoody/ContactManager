﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace ContactManager
{
    public sealed class DBHandler
    {
        DBHandler() { }
        static readonly DBHandler dBHandlerInstance = new DBHandler();
        readonly string ConString = ConfigurationManager.ConnectionStrings["Contacts"].ConnectionString;

        public static DBHandler Instance
        {
            get { return dBHandlerInstance; }
        }

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

                        if (Int32.TryParse(reader["Id"].ToString(), out int id))
                        {
                            person.Id = id;
                        }
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

        public Person GetPerson(int id)
        {
            Person person = new Person();

            using (SqlConnection con = new SqlConnection(ConString))
            {
                con.Open();

                SqlCommand command = new SqlCommand("Select * from Person WHERE Id = @Id", con);
                command.Parameters.AddWithValue("@Id", id);


                using (SqlDataReader reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        if (Int32.TryParse(reader["Id"].ToString(), out int id2))
                        {
                            person.Id = id2;
                        }
                        person.FirstName = reader["FirstName"].ToString();
                        person.LastName = reader["LastName"].ToString();
                        person.Email = reader["Email"].ToString();
                        person.Phone = reader["Phone"].ToString();
                    }
                }
                return person;
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
                command.Parameters.AddWithValue("@Email", person.Email);
                command.Parameters.AddWithValue("@Phone", person.Phone);

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

        public int UpdatePerson(Person person)
        {
            int row = -1;

            using (SqlConnection con = new SqlConnection(ConString))
            {
                con.Open();

                string query = "UPDATE Person SET FirstName = @FirstName, LastName = @LastName, Email = @Email, Phone = @Phone WHERE Id = @Id";

                SqlCommand command = new SqlCommand(query, con);
                command.Parameters.AddWithValue("@Id", person.Id);
                command.Parameters.AddWithValue("@FirstName", person.FirstName);
                command.Parameters.AddWithValue("@LastName", person.LastName);
                command.Parameters.AddWithValue("@Email", person.Email);
                command.Parameters.AddWithValue("@Phone", person.Phone);

                try
                {
                    row = command.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    Console.WriteLine("Error Generated. Details: " + e.ToString());
                }
                return row;
            }
        }

        public void DeletePerson(string listOfIds)
        {
            using (SqlConnection con = new SqlConnection(ConString))
            {
                con.Open();
                string query = $"DELETE FROM Person WHERE Id IN ({listOfIds}) ";

                SqlCommand command = new SqlCommand(query, con);

                try
                {
                    command.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    Console.WriteLine("Error Generated. Details: " + e.ToString());
                }
            }
        } 
    }
}
