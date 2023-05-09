using ApplicationCore.Interfaces;
using Infrastructure.MongoDB.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/v2/quizzes")]
    [ApiController]
    public class QuizMongoDB : ControllerBase
    {
        private QuizUserServiceMongoDB _userServiceMongoDB;

        public QuizMongoDB(QuizUserServiceMongoDB userServiceMongoDB)
        {
            _userServiceMongoDB = userServiceMongoDB;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _userServiceMongoDB.FindAllQuizzes();
            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _userServiceMongoDB.FindQuizById(id);
            return result != null ? Ok(result) : NotFound();
        }
    }
}
