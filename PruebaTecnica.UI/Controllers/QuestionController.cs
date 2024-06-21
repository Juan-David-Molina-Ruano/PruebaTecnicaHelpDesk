using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnica.BusinessEntities;
using PruebaTecnica.BusinessLogic;

namespace PruebaTecnica.UI.Controllers
{
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

        // GET: QuestionController/Details/5
        public ActionResult Details(int id)
        {
            return View();
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
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: QuestionController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: QuestionController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: QuestionController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
