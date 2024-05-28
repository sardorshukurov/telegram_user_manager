using TUM.Application.Services.BotService;
using TUM.Application.Services.UserService;
using TUM.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAppDbContext(builder.Configuration);

builder.Services.AddRepositories();
builder.Services.AddTransient<IBotService, BotService>();
builder.Services.AddTransient<IUserService, UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
    app.UseCors(b =>
    {
        b.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
