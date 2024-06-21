using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.BusinessEntities
{
    public class Answer
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Answer Date is required")]
        [StringLength(250, MinimumLength = 2, ErrorMessage = "Username must be between 2 and 250 characters")]
        public string AnswerText { get; set; }
        public DateTime CreateDate { get; set; }
        public int UserId { get; set; }
        public int QuestionId { get; set; }
        public User User { get; set; }
        public Question Question { get; set; }
    }
}
