namespace Domain.Exceptions;

/// <summary>
/// Базовый класс для ошибок типа BadRequestExceptions.
/// </summary>
public abstract class BadRequestException : Exception
{
    /// <summary>
    /// Конструктор с доступом к конструктору класса Exception
    /// </summary>
    /// <param name="message">Сообщение ошибки.</param>
    protected BadRequestException(string message)
        : base(message)
    {
    }
}
