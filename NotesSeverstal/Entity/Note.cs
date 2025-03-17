namespace NotesSeverstal.Context
{
    /// <summary>
    ///     Класс, представляющий заметку пользователя
    /// </summary>
    public class Note
    {
        /// <summary>
        ///     Конструктор класса Note. Генерирует уникальный идентификатор заметки
        /// </summary>
        public Note()
        {
            Id = Guid.NewGuid().ToString();
        }

        /// <summary>
        ///     Уникальный идентификатор заметки
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        ///     Описание заметки. Может быть пустым
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        ///     Пользователь, которому принадлежит заметка
        /// </summary>
        public User? User { get; set; }

        /// <summary>
        ///  Идентификатор пользователя, которому принадлежит заметка
        /// </summary>
        public string? UserId { get; set; }
    }

}
