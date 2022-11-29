using COMMUNICATIONS_SERVICE.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddTransient<EmailService>();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:5001/identity/") });

builder.Services.AddHttpClient<EmailService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:5001/identity/");
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
