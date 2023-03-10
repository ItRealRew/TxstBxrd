using Microsoft.EntityFrameworkCore;
using TXSTBXRD_LIBS.Security;
using PERSONALITY_SERVICE.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddEntityFrameworkMySql().AddDbContext<UserstxstbxrdContext>(options =>
{
    options.UseMySql(builder.Configuration.GetConnectionString("Default"),
            ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("Default")));
}
);

builder.Services.AddTransient<Personality>();
builder.Services.AddTransient<CriptoSevice>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(options => options.WithOrigins("http://localhost:5117").AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseCors(options => options.WithOrigins("https://localhost:7114").AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
