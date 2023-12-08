using ExchangeDemo.POCO.Factory;
using ExchangeDemo.Services;
using ExchangeDemo.Services.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IExchangeService, ExchangeService>();
builder.Services.AddSingleton<SaveFactory>();
builder.Services.AddSingleton<InMemorySaveService>()
    .AddSingleton<ISave, InMemorySaveService>(s => s.GetService<InMemorySaveService>());


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
