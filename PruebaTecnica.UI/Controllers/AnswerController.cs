using Microsoft.AspNetCore.Mvc;
using PruebaTecnica.BusinessEntities;
using PruebaTecnica.BusinessLogic;

namespace PruebaTecnica.UI.Controllers
{
    public class AnswerController : Controller
    {
        private readonly AnswerBL _answerBL;
        public AnswerController(AnswerBL answerBL) 
        {
            _answerBL = answerBL;
        }
        public async Task<IActionResult> Index(int id)
        {
            var answers =  await _answerBL.obtenerAnswersQuestion(id);
            return View(answers);
        }

        public IActionResult Create(int questionId)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
            {
                // Manejar el caso donde el claim Id no existe, redirigir al usuario a la página de inicio de sesión
                return RedirectToAction("Login", "User");
            }

            if (!int.TryParse(userIdClaim, out int userId))
            {
                // Manejar el caso donde la conversión falla
                return RedirectToAction("Login", "User");
            }

            var model = new Answer
            {
                QuestionId = questionId,
                UserId = userId,
                CreateDate = DateTime.Now
    
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Answer answer)
        {
           
            await _answerBL.SaveAnswer(answer); 
            return RedirectToAction("Index", "Question"); 
            
            return View(answer);
        }


    }

  
}
