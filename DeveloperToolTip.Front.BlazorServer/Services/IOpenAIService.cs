namespace DeveloperToolTip.Front.BlazorServer.Services
{
    public interface IOpenAIService
    {
        Task<string> SendRequestAsync(string prompt);
    }
}
