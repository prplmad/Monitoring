namespace Domain.Exceptions;

/// <summary>
/// Базовый класс для ошибок типа NotFoundException.
/// </summary>
public abstract class NotFoundException : Exception
{
    /// <summary>
    /// Конструктор с доступом к конструктору класса Exception
    /// </summary>
    /// <param name="message">Сообщение ошибки.</param>
    protected NotFoundException(string message)
        : base(message)
    {
    }
}
