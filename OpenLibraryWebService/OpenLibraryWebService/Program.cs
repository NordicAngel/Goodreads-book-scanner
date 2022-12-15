var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Using adding Policy for Cors
builder.Services.AddCors(options => {
options.AddPolicy(
    "Allow All", builder =>
    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
}

);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// Making builder use Cors
app.UseCors("Allow All");

app.UseAuthorization();

app.MapControllers();

app.Run();
