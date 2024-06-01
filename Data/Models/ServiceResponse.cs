namespace Maxula_project.Models;

public class ServiceResponse<T>
{
    //T stands for type, and it's nullable because it's our return data
    public T? Data { get; set; }
    public bool Success { get; set; } = true;
    public string Message { get; set; } = string.Empty;

}
