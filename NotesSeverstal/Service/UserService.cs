using NotesSeverstal.Context;
using NotesSeverstal.IService;

namespace NotesSeverstal.Service
{
    /// <summary>
    ///     Сервис для управления пользователями
    /// </summary>
    public class UserService : IUserService
    {
        private readonly ApplicationContext db;
        public UserService(ApplicationContext db) => this.db = db;

        /// <summary>
        ///     Создает нового пользователя и добавляет его в БД
        /// </summary>
        public async Task Create(User user)
        {
            try
            {
                db.Users.Add(user);
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
