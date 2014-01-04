namespace FilmsTranslator.Main.Code.Spellers.Enum
{
    public enum YandexSpellerError
    {
        /// <summary>
        /// Слова нет в словаре.
        /// </summary>
        ERROR_UNKNOWN_WORD = 1,
        /// <summary>
        /// Повтор слова.
        /// </summary>
        ERROR_REPEAT_WORD = 2,
        /// <summary>
        /// Неверное употребление прописных и строчных букв.
        /// </summary>
        ERROR_CAPITALIZATION = 3,
        /// <summary>
        /// Текст содержит слишком много ошибок.
        /// </summary>
        ERROR_TOO_MANY_ERRORS = 4
    }
}