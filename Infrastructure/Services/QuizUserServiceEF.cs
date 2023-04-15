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
                .Include(x => x.QuizItem)
                .Where(x => x.QuizId == quizId)
                .Where(x => x.UserId == userId)
                .Select(x => QuizMappers.FromEntityToUserAnswer(x))
                .ToList();


            return result;
        }
        public QuizItemUserAnswer SaveUserAnswerForQuiz(int quizId, int quizItemId, int userId, string answer)
        {
            QuizItemUserAnswerEntity entity = new QuizItemUserAnswerEntity()
            {
                UserId = userId,
                QuizItemId = quizItemId,
                QuizId = quizId,
                UserAnswer = answer
            };
            try
            {
                var saved = _context.UserAnswers.Add(entity).Entity;
                _context.SaveChanges();
                var quizItemTest = _context.QuizItems.Include(x => x.IncorrectAnswer).Where(x => x.Id == quizItemId).FirstOrDefault();


                return new QuizItemUserAnswer()
                {
                    UserId = saved.UserId,
                    QuizItem = QuizMappers.FromEntityToQuizItem(quizItemTest),
                    QuizId = saved.QuizId,
                    Answer = saved.UserAnswer
                };
            }
            catch (DbUpdateException e)
            {
                if (e.InnerException.Message.StartsWith("The INSERT"))
                {
                    throw new QuizNotFoundException("Quiz, quiz item or user not found. Can't save!");
                }
                if (e.InnerException.Message.StartsWith("Violation of"))
                {
                    throw new QuizAnswerItemAlreadyExistsException($"{quizId} {quizItemId} {userId} already exists !");
                }
                throw new Exception(e.Message);
            }
        }
    }
}
