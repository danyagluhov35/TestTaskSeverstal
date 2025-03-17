using Microsoft.EntityFrameworkCore;
using NotesSeverstal.Context;
using NotesSeverstal.IService;

namespace NotesSeverstal.Service
{
    /// <summary>
    ///     Сервис для управления заметками пользователя.
    /// </summary>
    public class NoteService : INoteService
    {
        private readonly ApplicationContext db;
        public NoteService(ApplicationContext db) => this.db = db;

        /// <summary>
        ///     Добавляет новую заметку в базу данных
        /// </summary>
        /// <param name="note">Объект заметки.</param>
        /// <returns>Сообщение об успешном добавлении или ошибке</returns>
        public async Task<string> Add(Note note)
        {
            try
            {
                db.Notes.Add(note);
                await db.SaveChangesAsync();

                return "Заметка добавлена";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return "Ошибка при добавлении заметки";
            }
        }

        /// <summary>
        ///     Удаляет заметку по её идентификатору
        /// </summary>
        /// <param name="id">Идентификатор заметки</param>
        /// <returns>Сообщение об успешном удалении или ошибке</returns>
        public async Task<string> Delete(string id)
        {
            try
            {
                var note = db.Notes.FirstOrDefault(n => n.Id == id);
                if(note != null)
                {
                    db.Notes.Remove(note);
                    await db.SaveChangesAsync();
                    return "Заметка удалена";
                }
                return "Заметка не найдена";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "Ошибка при удалении заметки";
            }
        }

        /// <summary>
        ///     Редактирует существующую заметку
        /// </summary>
        /// <param name="note">Объект заметки с обновлёнными данными</param>
        /// <returns>Сообщение об успешном редактировании или ошибке</returns>
        public async Task<string> Edit(Note note)
        {
            try
            {
                var noteFind = db.Notes.FirstOrDefault(n => n.Id == note.Id);
                if (noteFind != null) 
                {
                    noteFind.Description = note.Description;
                    db.Notes.Update(noteFind);
                    await db.SaveChangesAsync();

                    return "Заметка изменена";
                }
                return "Заметка не найдена";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "Ошибка при обновлении заметки";
            }
        }

        /// <summary>
        ///     Получает список всех заметок пользователя
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <returns>Список заметок пользователя или пустой список в случае ошибки</returns>
        public async Task<List<Note>> GetNotes(string userId)
        {
            try
            {
                return await db.Notes.Where(u => u.UserId == userId).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<Note>();
            }
        }
    }
}
