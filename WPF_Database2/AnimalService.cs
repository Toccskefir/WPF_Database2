using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Database2
{
    public class AnimalService
    {
        private MySqlConnection connection;
        public AnimalService()
        {
            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
            builder.Server = "localhost";
            builder.Port = 3306;
            builder.UserID = "root";
            builder.Password = "";
            builder.Database = "zoo_test";
            this.connection = new MySqlConnection(builder.ConnectionString);
        }

        //CRUD

        //Read
        public List<Animal> GetAll()
        {
            List<Animal> animals = new List<Animal>();

            OpenConnection();
            string sql = "SELECT * FROM animals";
            MySqlCommand command = this.connection.CreateCommand();
            command.CommandText = sql;
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Animal item = new Animal();
                    item.Id = reader.GetInt32("id");
                    item.Name = reader.GetString("name");
                    item.Age = reader.GetInt32("age");
                    item.Species = reader.GetString("species");
                    animals.Add(item);
                }
            }
            CloseConnection();

            return animals;
        }

        //Create
        public bool Create(Animal animal)
        {
            OpenConnection();
            string sql = "INSERT INTO animals (name, age, species) VALUES (@name, @age, @species)";
            MySqlCommand command = this.connection.CreateCommand();
            command.CommandText = sql;
            command.Parameters.AddWithValue("@name", animal.Name);
            command.Parameters.AddWithValue("@age", animal.Age);
            command.Parameters.AddWithValue("@species", animal.Species);
            int affectedRows = command.ExecuteNonQuery();
            CloseConnection();

            return affectedRows == 1;
        }

        //Update
        public bool Update(int id, Animal animal)
        {
            OpenConnection();
            string sql = "UPDATE animals SET name = @name, age = @age, species = @species WHERE id = @id";
            MySqlCommand command = this.connection.CreateCommand();
            command.CommandText = sql;
            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@name", animal.Name);
            command.Parameters.AddWithValue("@age", animal.Age);
            command.Parameters.AddWithValue("@species", animal.Species);
            int affectedRows = command.ExecuteNonQuery();
            CloseConnection();

            return affectedRows == 1;
        }

        //Delete
        public bool Delete(int id)
        {
            OpenConnection();
            string sql = "DELETE FROM animals WHERE id = @id";
            MySqlCommand command = this.connection.CreateCommand();
            command.CommandText = sql;
            command.Parameters.AddWithValue("@id", id);
            int affectedRows = command.ExecuteNonQuery();
            CloseConnection();

            return affectedRows == 1;
        }

        //Method for opening and closing connection
        private void OpenConnection()
        {
            if (this.connection.State != System.Data.ConnectionState.Open)
            {
                this.connection.Open();
            }
        }

        private void CloseConnection()
        {
            if (this.connection.State != System.Data.ConnectionState.Closed)
            {
                this.connection.Close();
            }
        }
    }
}
