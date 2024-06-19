using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.BusinessEntities
{
    public class Question
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public DateOnly CreateDate { get; set; }
        public bool Estatus { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public IList<Answer> Answers { get; set; }
    }
}
