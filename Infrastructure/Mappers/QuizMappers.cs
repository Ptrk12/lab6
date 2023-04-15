using ApplicationCore.Models;
using Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Mappers
{
    public class QuizMappers
    {
        public static QuizItem FromEntityToQuizItem(QuizItemEntity entity)
        {
            return new QuizItem(
                entity.Id,
                entity.Question,
                entity.IncorrectAnswer.Select(e => e.Answer).ToList(),
                entity.CorrectAnswser);
        }

        public static Quiz FromEntityToQuiz(QuizEntity entity)
        {
            return new Quiz(
                entity.Id,
                entity.Items.Select(x => FromEntityToQuizItem(x)).ToList(),
                entity.Title);
        }

        public static QuizItemUserAnswer FromEntityToUserAnswer(QuizItemUserAnswerEntity entity)
        {
            return new QuizItemUserAnswer(
                FromEntityToQuizItem(entity.QuizItem),
                entity.UserId,
                entity.QuizId,
                entity.UserAnswer);
        }
    }
}
