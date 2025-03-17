using NotesSeverstal.Context;

namespace NotesSeverstal.IService
{
    /// <summary>
    ///  Интерфейс для работы с пользователем
    /// </summary>
    public interface IUserService
    {
        // Создание пользователя
        Task Create(User user);
    }
}
