using Microsoft.Data.SqlClient;
using PruebaTecnica.BusinessEntities;
using PruebaTecnica.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.BusinessLogic
{
    
    public class AnswerBL
    {
        private readonly AnswerDAL _answerDAL;

        public AnswerBL(AnswerDAL answerDAL)
        {
            _answerDAL = answerDAL;
        }

        public async Task<IEnumerable<Answer>> obtenerAnswersQuestion(int questionId)
        {
            return await _answerDAL.obtenerAnswersQuestion(questionId);
        }

        public async Task<string> SaveAnswer(Answer answer)
        {
            return await _answerDAL.SaveAnswer(answer);
        }
    }
}
