using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace WhatsappNet.API.Services.WhatsappCloud.SendMessage
{
    public class WhatsappCloudSendMessage : IWhatsappCloudSendMessage
    {
        //ESTE METODO EJECUTA LA LLAMADA A LA API de WHATSAPP
        public async Task<bool> Execute(object model)
        {
            var client = new HttpClient();
            var byteData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(model));

            using (var constent = new ByteArrayContent(byteData))
            {
                string endPoint = " https://graph.facebook.com";
                string phoneNumberId = "195626706963879";
                string accesToken = "EAAD8FoZAvJKkBO4sR4fOzXncjr41Hd5Hqqwe5lDChEslrhjnIXUfEsdcjk2mmsDK9mKuwetT7Vghj5AUVTaVGACJIyR9Vr0QHUJnfF511N4GFyDf22yUWscgJOBu1bwCNfKCbsqhyailgbZAQ1xUnlN944CpE4mfzhxC6zT5JYJ9Imso62F9F4EqvrkbDG6u2PhoQEDfypZAnJQnLQZD";
                string uri = $"{endPoint}/v17.0/{phoneNumberId}/messages";

                constent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accesToken}");
                var response = await client.PostAsync(uri, constent);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }
    }
}
