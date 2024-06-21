using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnica.BusinessEntities;
using PruebaTecnica.BusinessLogic;
using System.Security.Claims;

namespace PruebaTecnica.UI.Controllers
{
    [Authorize]
    public class QuestionController : Controller
    {
        private readonly QuestionBL _questionBL;

        public QuestionController(QuestionBL questionBL)
        {
            _questionBL = questionBL;
        }


        // GET: QuestionController
        public async Task<ActionResult> Index()
        {
            var questions = await _questionBL.listQuestions();
            return View(questions);
        }

        // GET: QuestionController/Create
        public ActionResult Create()
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

            var model = new Question { UserId = userId, CreateDate = DateTime.Now};
            return View(model);
        }

        public async Task<ActionResult> myQuestions(int id)
        {
            var questions = await _questionBL.listMyQuestionsAsync(id);
            return View(questions);
        }

        // POST: QuestionController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Question question)
        {
            try
            {
                await _questionBL.createQuestion(question);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(question);
            }
        }



        // GET: QuestionController/Edit/5
        public async Task<ActionResult> editMyQuestions(int id)
        {
            var question = await _questionBL.GetQuestionById(id);
            if (question == null)
            {
                return RedirectToAction("myQuestions","Question");
            }
            return View(question);
        }

        // POST: QuestionController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> editMyQuestions(int id, Question question)
        {
            try
            {
                await _questionBL.UpdateQuestion(question);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> deletetMyQuestion(int id)
        {
            try
            {
                await _questionBL.DeleteQuestion(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }




    }
}
