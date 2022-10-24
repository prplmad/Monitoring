namespace Contracts;

public class StatisticsDto<T>
{
    public T? Id { get; set; }
    public string? UserName { get; set; }
    public string? ClientVersion { get; set; }
    public string? Os { get; set; }
    public DateTime UpdateDate { get; set; }
}