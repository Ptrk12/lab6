using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using Infrastructure.Entities;
using Infrastructure.Mappers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class QuizUserServiceEF : IQuizUserService
    {
        private readonly QuizDbContext _context;

        public QuizUserServiceEF(QuizDbContext context)
        {
            _context = context;
        }

        public Quiz CreateAndGetQuizRandom(int count)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Quiz> FindAllQuizzes()
        {
            return _context
            .Quizzes
            .AsNoTracking()
            .Include(q => q.Items)
            .ThenInclude(i => i.IncorrectAnswer)
            .Select(QuizMappers.FromEntityToQuiz)
            .ToList();
        }

        public Quiz? FindQuizById(int id)
        {
            var entity = _context
                .Quizzes
                .AsNoTracking()
                .Include(q => q.Items)
                .ThenInclude(i => i.IncorrectAnswer)
                .FirstOrDefault(e => e.Id == id);
            return entity is null ? null : QuizMappers.FromEntityToQuiz(entity);
        }

        public List<QuizItemUserAnswer> GetUserAnswersForQuiz(int quizId, int userId)
        {
            var result = _context.UserAnswers
                .Where(x => x.QuizId == quizId)
                .Where(x => x.UserId == userId)
                .Select(x => QuizMappers.FromEntityToUserAnswer(x))
                .ToList();

            return result;
        }

        public QuizItemUserAnswer SaveUserAnswerForQuiz(int quizId, int userId, int quizItemId, string answer)
        {
            var quizEntity = _context.Quizzes.Where(x => x.Id == quizId).FirstOrDefault();
            if (quizEntity is null)
            {
                throw new QuizItemNotFoundException($"Quiz with id: {quizId} not found");
            }

            var item = _context.QuizItems.Where(x => x.Id == quizItemId).FirstOrDefault();
            if(item is null)
            {
                throw new QuizItemNotFoundException($"Quiz item with id: {quizItemId} not found");
            }

            QuizItemUserAnswerEntity entity = new QuizItemUserAnswerEntity()
            {
                QuizId = quizId,
                UserAnswer = answer,
                UserId = userId,
                QuizItemId = quizItemId
            };

            var savedEntity = _context.Add(entity).Entity;
            _context.SaveChanges();

            return new QuizItemUserAnswer()
            {
                QuizId = quizId,
                UserId = userId,
                Answer = answer,
                QuizItem = QuizMappers.FromEntityToQuizItem(item),

            };
        }
    }
}
