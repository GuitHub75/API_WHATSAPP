using OpenAI_API;
using OpenAI_API.Completions;

namespace WhatsappNet.API.Services.OpenAI.ChatGPT
{
    public class ChatGPTService : IChatGPTService
    {
        public async Task<string> Execute(string textUser)
        {
           try
            {
                string apikey = "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";
                var openAiServices = new OpenAIAPI(apikey);
                var completion = new CompletionRequest
                {
                    Prompt = textUser,
                    Model = "text-davinci-003",
                    NumChoicesPerPrompt = 1,
                    MaxTokens = 200
                };

                var result = await openAiServices.Completions.CreateCompletionAsync(completion);
                if (result != null && result.Completions.Count > 0)
                {
                    return result.Completions[0].Text;
                }
                else
                {
                    return "No se pudo obtener respuesta";
                }
            }
           catch (Exception ex)
            {
                return "Lo siento sucedio un problema intente mas tarde";
            }
        }
    }
}
