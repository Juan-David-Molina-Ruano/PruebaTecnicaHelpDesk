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
        public int Id { get; set; }

        [Required(ErrorMessage = "Question text is required")]
        [StringLength(250, MinimumLength = 2, ErrorMessage = "Username must be between 2 and 250 characters")]
        public string QuestionText { get; set; }
        public DateTime CreateDate { get; set; }
        public int Estatus { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public IList<Answer> Answers { get; set; }
    }
}
