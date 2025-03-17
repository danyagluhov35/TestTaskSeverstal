namespace NotesSeverstal.Context
{
    /// <summary>
    ///     Класс, представляющий пользователя
    /// </summary>
    public class User
    {
        /// <summary>
        ///     Конструктор класса User. Генерирует уникальный идентификатор пользователя
        /// </summary>
        public User()
        {
            Id = Guid.NewGuid().ToString();
        }

        /// <summary>
        ///     Уникальный идентификатор пользователя
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        ///     Список заметок, принадлежащих пользователю. Может быть пустым
        /// </summary>
        public List<Note>? Notes { get; set; }
    }

}
