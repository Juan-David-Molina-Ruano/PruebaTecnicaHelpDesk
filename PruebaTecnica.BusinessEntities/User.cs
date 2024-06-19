﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.BusinessEntities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public IList<Question> Questions { get; set; }
        public IList<Answer> Answers { get; set; }
    }
}
