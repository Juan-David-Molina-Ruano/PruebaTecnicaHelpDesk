using Microsoft.Data.SqlClient;
using PruebaTecnica.BusinessEntities;
using PruebaTecnica.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.BusinessLogic
{
    public class QuestionBL
    {
        private readonly QuestionDAL _questionDAL;

        public QuestionBL(QuestionDAL questionDAL)
        {
            _questionDAL = questionDAL;
        }
        public async Task<string> createQuestion(Question question)
        {
            return await _questionDAL.createQuestion(question);
        }

        public async Task<IEnumerable<Question>> listQuestions()
        {
           return await _questionDAL.listQuestions();
        }

        public async Task<IEnumerable<Question>> listMyQuestionsAsync(int id)
        {
            return await _questionDAL.listMyQuestionsAsync(id);
        }

        public async Task<int> UpdateQuestion(Question question)
        {
            return await _questionDAL.UpdateQuestion(question);
        }

        public async Task<Question> GetQuestionById(int id)
        {
            return await _questionDAL.GetQuestionById(id);
        }

        public async Task<int> DeleteQuestion(int id)
        {
           return await _questionDAL.DeleteQuestion(id);
        }
    }
}
