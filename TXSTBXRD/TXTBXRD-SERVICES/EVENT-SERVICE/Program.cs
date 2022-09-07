using EVENT_SERVICE.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEntityFrameworkNpgsql().AddDbContext<EventDBContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default"));
}
);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton<EventService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

