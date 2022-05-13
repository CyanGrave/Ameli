var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Configure Persistence
var ctxUserManagement = new DAL.SQLite.SQLiteContextFactory<AmeliAPI.UserManagement.DAL.UserManagementContext>();

//Configure DAL
var dalUserManagement = new DAL.DataAccessProvider<AmeliAPI.UserManagement.DAL.UserManagementContext>(ctxUserManagement);

//Configure Services
AmeliAPI.UserManagement.Configuration.ConfigureServices(builder.Services, dalUserManagement);

var app = builder.Build();


//CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("bananaice",
      builder =>
      {
          builder
          .AllowAnyOrigin()
          .AllowAnyHeader()
          .AllowAnyMethod();
      });
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors("bananaice");

app.Run();
