using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleUsing_AJAX.data
{
    public class DbMAnager
    {
        private string _connectionString;
        public DbMAnager(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Person> GetAll()
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM People";
            connection.Open();
            var people = new List<Person>();
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                people.Add(new Person
                {
                    Id = (int)reader["Id"],
                    FirstName = (string)reader["FirstName"],
                    LastName = (string)reader["LastName"],
                    Age = (int)reader["Age"]
                });
            }

            return people;

        }

        public void AddPerson(Person p)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = connection.CreateCommand();
            command.CommandText = @"INSERT INTO People (FirstName, LastName, Age)
                            VALUES (@firstName, @lastName, @age)";
            command.Parameters.AddWithValue("@firstName", p.FirstName);
            command.Parameters.AddWithValue("@lastName", p.LastName);
            command.Parameters.AddWithValue("@age", p.Age);

            connection.Open();
            command.ExecuteNonQuery();
        }

        public void Update(Person person)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = connection.CreateCommand();
            command.CommandText = @"UPDATE People SET FirstName = @firstName, LastName = @LastName, Age = @Age
                            WHERE Id = @id";
            command.Parameters.AddWithValue("@id", person.Id);
            command.Parameters.AddWithValue("@firstName", person.FirstName);
            command.Parameters.AddWithValue("@lastName", person.LastName);
            command.Parameters.AddWithValue("@age", person.Age);

            connection.Open();
            command.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = connection.CreateCommand();
            command.CommandText = @"DELETE FROM People WHERE Id = @id";
            command.Parameters.AddWithValue("@id", id);
            connection.Open();
            command.ExecuteNonQuery();
        }

        public Person GetPersonById(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM People WHERE Id = @id";
            command.Parameters.AddWithValue("@id", id);
            connection.Open();
            var reader = command.ExecuteReader();

            if (!reader.Read())
            {
                return null;
            }

            return new Person
            {
                Id = (int)reader["Id"],
                FirstName = (string)reader["FirstName"],
                LastName = (string)reader["LastName"],
                Age = (int)reader["Age"]
            };

        }
    }
}
