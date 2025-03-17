using Microsoft.AspNetCore.Mvc;
using NotesSeverstal.Context;
using NotesSeverstal.IService;

namespace NotesSeverstal.Controllers
{
    /// <summary>
    ///     Контроллер для управления заметками пользователя
    /// </summary>
    public class HomeController : Controller
    {
        private INoteService NoteService;
        private string? UserId;
        public HomeController(INoteService noteService)
        {
            NoteService = noteService;
        }
        /// <summary>
        ///     Возвращает список заметок текущего пользователя
        /// </summary>
        /// <returns>Страница со списком заметок</returns>
        public async Task<IActionResult> Index()
        {
            UserId = HttpContext.User.Claims.FirstOrDefault(u => u.Type == "Id")?.Value!;
            if (UserId == null)
                return RedirectToAction("Index");

            return View(await NoteService.GetNotes(UserId));
        }
        /// <summary>
        ///     Добавляет новую заметку
        /// </summary>
        /// <param name="note">Объект заметки</param>
        /// <returns>JSON-ответ с сообщением об успехе или ошибке</returns>
        public async Task<IActionResult> AddNote(Note note)
        {
            var result = await NoteService.Add(note);

            return new JsonResult(new {message = result});
        }
        /// <summary>
        ///     Удаляет заметку по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор заметки</param>
        /// <returns>JSON-ответ с сообщением об успешном удалении или ошибке</returns>
        public async Task<IActionResult> DeleteNote([FromBody]string id)
        {
            var result = await NoteService.Delete(id);

            return new JsonResult(new { message = result });
        }
        /// <summary>
        ///     Редактирует существующую заметку
        /// </summary>
        /// <param name="note">Обновленный объект заметки</param>
        /// <returns>JSON-ответ с сообщением об успешном редактировании или ошибке</returns>
        public async Task<IActionResult> EditNote([FromBody]Note note)
        {
            var result = await NoteService.Edit(note);

            return new JsonResult(new { message = result });
        }
    }
}
