using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WhatsappNet.Api.Util;
using WhatsappNet.API.Models.WhatsappCloud;
using WhatsappNet.API.Services.OpenAI.ChatGPT;
using WhatsappNet.API.Services.WhatsappCloud.SendMessage;
using WhatsappNet.API.Util;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WhatsappNet.API.Controllers
{
    [ApiController]
    [Route("api/whatsapp")]
    public class WhatsappController : Controller
    {
        private readonly IWhatsappCloudSendMessage _IWhatsappCloudSendMessage;
        private readonly IUtil _Util;
        private readonly IChatGPTService _chatGPTService;
           

        public WhatsappController(
            IWhatsappCloudSendMessage  whatsappCloudSendMessage, 
            IUtil Util , 
            IChatGPTService chatGPTService
            )
        {
            _IWhatsappCloudSendMessage = whatsappCloudSendMessage;
            _Util = Util;
            _chatGPTService = chatGPTService;
        }

        [HttpGet("test")]
        public async Task<IActionResult> Sample()
        {
            var data = new
            {
                messaging_product = "whatsapp",
                to = "78917518",
                type = "text",  
                text = new
                {
                    body = "Hola como estas Erick Escobar"
                }
            };
            var result = await _IWhatsappCloudSendMessage.Execute(data);
            return Ok("Ok example");
        }

        [HttpGet]
        public IActionResult verifyToken()
        {
            string AccesToken = "FVNN564INISKNDSDFS";
            var token = Request.Query["hub.verify_token"].ToString();
            var challenge = Request.Query["hub.challenge"].ToString();
            if (token != null && challenge != null && token == AccesToken)
            {
                return Ok(challenge);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<ActionResult> ReceivedMessage([FromBody] WhatsAppCloudModel body)
        {
            try
            {

               // string data = "";
                var Message = body.Entry[0]?.Changes[0]?.Value?.Messages[0];
                List<object> listObjectMessage = new List<object>();

                     //HttpContext.Session.SetString("Erick", GetUserText(Message));
                     //data = HttpContext.Session.GetString("Erick");
                
                if (Message != null)
                {
                    var userNumber = Message.From;
                    string TypeMessage = Message.Type;

                    if (TypeMessage != null )
                    {
                        if (TypeMessage.ToUpper() == "INTERACTIVE" )
                        {
                            string interactiveType = Message.Interactive.Type;

                            if (interactiveType.ToUpper() == "LIST_REPLY" && Message.Interactive.List_Reply.Id != null)
                            {
                                string Id = Message.Interactive.List_Reply.Id; // Id del boton seleccionado.
                                var objectMessage = _Util.TextMessage($"Perfecto has seleccionado la Opción {Id} {Message.Interactive.List_Reply.Title}", userNumber);
                                listObjectMessage.Add(objectMessage);
                            }
                        }
                        else
                        {
                            string userText = "";
                            userText = GetUserText(Message);
                            #region no chatgpt
                            if (userText.ToUpper().Contains("HOLA"))
                            {
                                //var objectMessage = _Util.ButtonsMessage(userNumber);
                                //listObjectMessage.Add(objectMessage);

                                var objectMessage2 = _Util.TextMessage("Responderé todas tus preguntas 😃", userNumber);
                                listObjectMessage.Add(objectMessage2);

                                var objectMessage = _Util.ListMessage(userNumber);
                                listObjectMessage.Add(objectMessage);

                                //var objectMessage3 = _Util.ImageMessage("https://biostoragecloud.blob.core.windows.net/resource-udemy-whatsapp-node/image_whatsapp.png", userNumber);
                                //listObjectMessage.Add(objectMessage3);

                            }
                            else if (userText.ToUpper().Contains("GRACIAS") || userText.ToUpper().Contains("AGRADECID"))
                            {
                                var objectMessage = _Util.TextMessage("Gracias a ti por escribirme. 😃", userNumber);
                                listObjectMessage.Add(objectMessage);
                            }
                            else if (userText.ToUpper().Contains("ADIOS") || userText.ToUpper().Contains("YA ME VOY"))
                            {
                                var objectMessage = _Util.TextMessage("Ve con cuidado. 😃", userNumber);
                                listObjectMessage.Add(objectMessage);
                            }
                            else
                            {
                                var objectMessage = _Util.TextMessage("Lo siento, no puedo entenderte. 😔", userNumber);
                                listObjectMessage.Add(objectMessage);
                            }
                            #endregion

                            #region chatgpt
                            // var responseChatGPT = await _chatGPTService.Execute(userText);
                            //var objectMesage = _Util.TextMessage(responseChatGPT, userNumber);
                            //listObjectMessage.Add(objectMesage);
                            #endregion
                        }
                        foreach (var item in listObjectMessage)
                        {
                            await _IWhatsappCloudSendMessage.Execute(item);
                        }
                    }
                }
                return Ok("EVENT_RECEIVED");
            }catch (Exception ex)
            {
                return Ok("EVENT_RECEIVED");
            }
        }

        private string GetUserText(Message meesage)
        {
            string TypeMessage = meesage.Type;
            if(TypeMessage.ToUpper() == "TEXT")
            {
                return meesage.Text.Body;
            }
            else
            {
                return string.Empty;
            } 
        }
    }
}
