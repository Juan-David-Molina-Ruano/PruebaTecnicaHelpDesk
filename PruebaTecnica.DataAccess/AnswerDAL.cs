﻿using Microsoft.Data.SqlClient;
using PruebaTecnica.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.DataAccess
{
    public class AnswerDAL
    {
        private readonly string _connectionString;

        public AnswerDAL(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<Answer>> AnswersQuestion(int questionId)
        {
            List<Answer> answers = new List<Answer>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand("GetAnswersForQuestionWithUser", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@QuestionId", questionId);

                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        Answer answer = new Answer
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            AnswerText = reader.GetString(reader.GetOrdinal("AnswerText")),
                            CreateDate = reader.GetDateTime(reader.GetOrdinal("CreateDate")),
                            UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                            QuestionId = reader.GetInt32(reader.GetOrdinal("QuestionId")),
                            User = new User
                            {
                                UserName = reader.GetString(reader.GetOrdinal("UserDisplayName"))
                            },
                            Question = new Question
                            {
                                QuestionText = reader.GetString(reader.GetOrdinal("QuestionText"))
                            }
                        };

                        answers.Add(answer);
                    }
                }
            }

            return answers;
        }

        public async Task<string> SaveAnswer(Answer answer)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand("InsertAnswer", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                // Parameters for the stored procedure
                command.Parameters.AddWithValue("@AnswerText", answer.AnswerText);
                command.Parameters.AddWithValue("@CreateDate", answer.CreateDate);
                command.Parameters.AddWithValue("@UserId", answer.UserId);
                command.Parameters.AddWithValue("@QuestionId", answer.QuestionId);

                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    return reader.ToString();
                }

            }
        }

    }
}
