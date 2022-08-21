using GoogleOAuth2Practice.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddScoped<IOAuth2Service,GoogleAuthenticationService>();

var app = builder.Build();


app.MapControllers();

app.Run();
