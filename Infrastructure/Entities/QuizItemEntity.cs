using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public class QuizItemEntity
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string CorrectAnswser { get; set; }
        public ISet<QuizItemAnswerEntity> IncorrectAnswer { get; set; } = new HashSet<QuizItemAnswerEntity>();
        public ISet<QuizEntity> Quizes { get; set; } = new HashSet<QuizEntity>();
    }
}
