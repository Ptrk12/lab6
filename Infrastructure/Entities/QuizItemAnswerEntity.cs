﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public class QuizItemAnswerEntity
    {
        public int Id { get; set; }
        public string Answer { get; set; }
        public ISet<QuizItemEntity> QuizItems = new HashSet<QuizItemEntity>();
    }
}
