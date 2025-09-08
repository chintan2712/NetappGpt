using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
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

// Enable Swagger for all environments
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "NetAppGPT API V1");
    c.RoutePrefix = string.Empty; // Swagger available at root
});

// No HTTPS redirection: Render handles TLS externally
// app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

app.Run();
