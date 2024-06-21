using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.BusinessEntities
{
    public class Question
    {
        [Key]
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public DateTime CreateDate { get; set; }
        public int Estatus { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public IList<Answer> Answers { get; set; }
    }
}
