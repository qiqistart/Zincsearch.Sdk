using Zincsearch.Sdk.Client;
using Zincsearch.Sdk.Config;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<ZincsearchConfig>(opt =>
{
    opt.ZincsearchUrl = "http://192.168.1.8:4080";
    opt.UserName = "admin";
    opt.PassWord = "q123qq..";
    opt.DefaultIndex = "";
});
builder.Services.AddSingleton<IZincsearchClient, ZincsearchClient>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
