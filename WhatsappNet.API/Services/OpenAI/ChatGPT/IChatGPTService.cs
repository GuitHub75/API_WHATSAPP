namespace WhatsappNet.API.Services.OpenAI.ChatGPT
{
    public interface IChatGPTService
    {
        Task<string> Execute(string textUser);
    }
}
