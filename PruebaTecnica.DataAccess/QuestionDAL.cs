﻿using Microsoft.Data.SqlClient;
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

        public async Task<string> createQuestion(Question question)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                // Procede a crear el usuario
                SqlCommand command = new SqlCommand("CreateQuestion", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@QuestionText", question.QuestionText);
                command.Parameters.AddWithValue("@CreateDate", question.CreateDate);
                command.Parameters.AddWithValue("@Estatus", question.Estatus);
                command.Parameters.AddWithValue("@UserId", question.UserId);

                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    return reader.ToString();
                }
            }


        }

        public async Task<IEnumerable<Question>> listQuestions()
        {
            List<Question> questions = new List<Question>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand("SelectAllQuestionsWithUser", connection);
                command.CommandType = CommandType.StoredProcedure;

                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        Question loggedInUser = new Question
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            QuestionText = reader.GetString(reader.GetOrdinal("QuestionText")),
                            CreateDate = reader.GetDateTime(reader.GetOrdinal("CreateDate")),
                            Estatus = reader.GetInt32(reader.GetOrdinal("Estatus")),
                            UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                            User = new User 
                            {
                                UserName = reader.GetString(reader.GetOrdinal("UserName")) 
                            }
                        };

                        questions.Add(loggedInUser);
                    }
                }
            }

            return questions;
        }

    }
}
