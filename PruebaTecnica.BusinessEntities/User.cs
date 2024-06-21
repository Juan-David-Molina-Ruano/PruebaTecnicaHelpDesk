using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.BusinessEntities
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "Username must be between 2 and 200 characters")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(150, MinimumLength = 6, ErrorMessage = "The password must be between 6 and 150 characters long.")]
        public string UserPassword { get; set; }
        public IList<Question> Questions { get; set; }
        public IList<Answer> Answers { get; set; }
    }
}
