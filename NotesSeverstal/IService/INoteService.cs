using NotesSeverstal.Context;

namespace NotesSeverstal.IService
{
    /// <summary>
    ///     Интерфейс для работы с заметками
    /// </summary>
    public interface INoteService
    {
        // Добавление
        Task<string> Add(Note note);
        // Удаление
        Task<string> Delete(string id);
        // Редактирование
        Task<string> Edit(Note note);
        // Получение заметок
        Task<List<Note>> GetNotes(string userId);
    }
}
