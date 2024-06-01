using System.Threading.Tasks;

public interface IConsoleLoggerService
{
    Task LogAsync(object message);
}
