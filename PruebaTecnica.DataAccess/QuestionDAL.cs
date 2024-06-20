using Microsoft.Data.SqlClient;
using PruebaTecnica.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.DataAccess
{
    public class QuestionDAL
    {
        private string connectionString;

        public QuestionDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        //public async Task<string> createQuestion(Question question)
        //{
        //    //using (SqlConnection connection = new SqlConnection(connectionString))
        //    //{
        //    //    await connection.OpenAsync();

        //    //    // Procede a crear el usuario
        //    //    SqlCommand command = new SqlCommand("CreateQuestion", connection);
        //    //    command.CommandType = CommandType.StoredProcedure;

        //    //    command.Parameters.AddWithValue("@UserName", usuario.UserName);
        //    //    command.Parameters.AddWithValue("@UserPassword", usuario.UserPassword);

        //    //    string result = (string)await command.ExecuteScalarAsync();
        //    //    return result;
        //    //}
        //}
    }
}
