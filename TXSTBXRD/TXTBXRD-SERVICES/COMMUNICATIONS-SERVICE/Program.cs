using COMMUNICATIONS_SERVICE.Services;
using TXSTBXD_LIBS.Email;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddTransient<COMMUNICATIONS_SERVICE.Services.EmailService>();
builder.Services.AddTransient<TXSTBXD_LIBS.Email.EmailService>();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:5001/identity/") });

builder.Services.AddHttpClient<COMMUNICATIONS_SERVICE.Services.EmailService>(client =>
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
