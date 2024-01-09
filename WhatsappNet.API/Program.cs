using WhatsappNet.Api.Util;
using WhatsappNet.API.Services.OpenAI.ChatGPT;
using WhatsappNet.API.Services.WhatsappCloud.SendMessage;
using WhatsappNet.API.Util;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton<IWhatsappCloudSendMessage, WhatsappCloudSendMessage>();
builder.Services.AddSingleton<IUtil, Util>();
builder.Services.AddSingleton<IChatGPTService, ChatGPTService>();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();
app.UseSession();


// Configure the HTTP request pipeline.

app.UseHttpsRedirection();




app.UseAuthorization();

app.MapControllers();

app.Run();
