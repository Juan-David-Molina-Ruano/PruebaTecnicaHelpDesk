using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PruebaTecnica.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.DataAccess
{
    public class UserDAL
    {

        private string connectionString;

        public UserDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task<string> createUser(User usuario)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                // Procede a crear el usuario
                SqlCommand command = new SqlCommand("CreateUser", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@UserName", usuario.UserName);
                var password = CalcularHashMD5(usuario.UserPassword);
                command.Parameters.AddWithValue("@UserPassword", password);

                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return reader.GetString(reader.GetOrdinal("Message"));
                    }
                    else
                    {
                        return "Unexpected error occurred";
                    }
                }
            }
        }

        public async Task<User> Login(User user)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                // Validate user
                SqlCommand command = new SqlCommand("ValidateUserLogin", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@UserName", user.UserName);
                var password = CalcularHashMD5(user.UserPassword);
                command.Parameters.AddWithValue("@UserPassword", password);

                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        // Assuming your User class has properties UserId, UserName, UserPassword, etc.
                        User loggedInUser = new User
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            UserName = reader.GetString(reader.GetOrdinal("UserName")),
                        };

                        return loggedInUser;
                    }
                    else
                    {
                        // Handle case when user is not found or password is incorrect
                        return null;
                    }
                }
            }
        }

        private string CalcularHashMD5(string texto)
        {
            using (MD5 md5 = MD5.Create())
            {
                // Convierte la cadena de texto a bytes
                byte[] inputBytes = Encoding.UTF8.GetBytes(texto);

                // Calcula el hash MD5 de los bytes
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convierte el hash a una cadena hexadecimal
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }

                return sb.ToString();
            }


        }
    }
}
