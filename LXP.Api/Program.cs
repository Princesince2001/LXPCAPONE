using LXP.Core.Services;
using LXP.Core.IServices;
using LXP.Data.IRepository;
using LXP.Data.Repository;
using LXP.Data.DBContexts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ILearnerRepository,LearnerRepository>();
builder.Services.AddScoped<IProfileRepository,ProfileRepository>();
builder.Services.AddScoped<ILearnerService,LearnerService>();
builder.Services.AddScoped<IProfileService, ProfileService>();
builder.Services.AddSingleton<LXPDbContext>();
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
