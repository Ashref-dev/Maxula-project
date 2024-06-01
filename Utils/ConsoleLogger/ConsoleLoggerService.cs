using Microsoft.JSInterop;
namespace Maxula_project.Utils.ConsoleLogger;
public class ConsoleLoggerService : IConsoleLoggerService
{
    private readonly IJSRuntime _js;

    public ConsoleLoggerService(IJSRuntime js)
    {
        _js = js;
    }

    public async Task LogAsync(object message)
    {
        await _js.InvokeVoidAsync("console.log", message);
    }
}
