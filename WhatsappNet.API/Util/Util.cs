using WhatsappNet.API.Util;

namespace WhatsappNet.Api.Util
{
    public class Util : IUtil
    {
        public object TextMessage(string message, string number)
        {
            return new
            {
                messaging_product = "whatsapp",
                to = number,
                type = "text",
                text = new
                {
                    body = message
                }
            };
        }

        public object ImageMessage(string url, string number)
        {
            return new
            {
                messaging_product = "whatsapp",
                to = number,
                type = "image",
                image = new
                {
                    link = url
                }
            };
        }

        public object AudioMessage(string url, string number)
        {
            return new
            {
                messaging_product = "whatsapp",
                to = number,
                type = "audio",
                audio = new
                {
                    link = url
                }
            };
        }

        public object VideoMessage(string url, string number)
        {
            return new
            {
                messaging_product = "whatsapp",
                to = number,
                type = "video",
                video = new
                {
                    link = url
                }
            };
        }

        public object DocumentMessage(string url, string number)
        {
            return new
            {
                messaging_product = "whatsapp",
                to = number,
                type = "document",
                document = new
                {
                    link = url
                }
            };
        }

        public object LocationMessage(string number)
        {
            return new
            {
                messaging_product = "whatsapp",
                to = number,
                type = "location",
                location = new
                {
                    latitude = "-12.067079752918158",
                    longitude = "-77.03371847563524",
                    name = "Estadio Nacional del Perú",
                    address = "C. José Díaz s/n, Lima 15046"
                }
            };
        }

        public object ButtonsMessage(string number)
        {
            return new
            {
                messaging_product = "whatsapp",
                to = number,
                type = "interactive",
                interactive = new
                {
                    type = "button",
                    body = new
                    {
                        text = "Selecciona una opción"
                    },
                    action = new
                    {
                        buttons = new List<object>
                        {
                            new
                            {
                                type = "reply",
                                reply = new
                                {
                                    id = "01",
                                    title = "Comprar"
                                }
                            },
                            new
                            {
                                type = "reply",
                                reply = new
                                {
                                    id = "02",
                                    title = "Vender"
                                }
                            }
                            ,
                            new
                            {
                                type = "reply",
                                reply = new
                                {
                                    id = "03",
                                    title = "Vender"
                                }
                            }
                        }
                    }
                }
            };
        }

        public object ListMessage(string number)
        {
            return new
            {
                messaging_product = "whatsapp",
                recipient_type = "individual",
                to = number,
                type = "interactive",
                interactive = new
                {
                    type = "list",
                    //header = new
                    //{
                    //    type = "text",
                    //    text = "Selecciona una opción"
                    //},
                    body = new
                    {
                        text = "Selecciona una opción"
                    },
                    //footer = new
                    //{
                    //    text = "Selecciona una opción"
                    //},
                    action = new
                    {
                        button = "Menú",
                        sections = new List<object>
                        {
                            new
                            {
                                title = "Tecnología",
                                rows = new List<object>
                                {
                                    new
                                    {
                                        id = "03",
                                        title = "Electrónica",
                                        description = "Productos electronicos"
                                    },
                                   new
                                    {
                                        id = "04",
                                        title = "Electrecidad",
                                        description = "Productos electricos"
                                    }
                                }
                            },
                            new
                            {
                                title = "Alimentos",
                                rows = new List<object>
                                {
                                  new
                                    {
                                        id = "01",
                                        title = "chatarra",
                                        description = "Productos de cómida rápida"
                                    },
                                   new
                                    {
                                        id = "02",
                                        title = "Saludables",
                                        description = "Productos Saludables"
                                    }
                                }
                            }
                        }
                    }
                }
            };
        }
    }
}
