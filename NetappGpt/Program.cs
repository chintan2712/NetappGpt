using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Configure Kestrel to listen on all IPs for HTTP
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(8080); // HTTP only
    // Don't enable HTTPS here; Render handles HTTPS externally
});

// Add services to the container
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// HttpClient
builder.Services.AddHttpClient("NetAppClient", client =>
{
    client.BaseAddress = new Uri("https://10.239.12.3");
    client.DefaultRequestHeaders.Authorization =
        new AuthenticationHeaderValue("Basic", Convert.ToBase64String(
            System.Text.Encoding.ASCII.GetBytes("admin:netapp1!")));
    client.DefaultRequestHeaders.Accept.Add(
        new MediaTypeWithQualityHeaderValue("application/json"));
});

var app = builder.Build();

// Bind to 0.0.0.0:8080
app.Urls.Add("http://0.0.0.0:8080");

// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "NetAppGPT API V1");
        c.RoutePrefix = string.Empty; // Swagger at root for dev
    });
}

// **Disable HTTPS redirection for production on Render**
// app.UseHttpsRedirection(); // <-- comment this out

app.UseAuthorization();

app.MapControllers();

app.Run();
