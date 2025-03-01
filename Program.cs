using Hazelcast;
using Hazelcast.Configuration;
using SpaceBasedPatternApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Access Configuration from the builder
var configuration = builder.Configuration;
// Hazelcast client setup
var hazelcastOptions = new HazelcastOptionsBuilder()
    .With(config =>
    {
        config.ClusterName = "dev";
        config.Networking.Addresses.Add("127.0.0.1:5701");
    })
    .Build();


builder.Services.AddSingleton(hazelcastOptions);
builder.Services.AddSingleton<IHazelcastClient>(sp => HazelcastClientFactory.StartNewClientAsync(hazelcastOptions).Result);


// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddScoped<ProductService>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll"); // Apply CORS policy

app.UseHttpsRedirection();

app.UseRouting();

app.MapControllers();

app.Run();