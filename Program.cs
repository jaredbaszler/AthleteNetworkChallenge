var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpClient("Nationalize", httpClient =>
{
    httpClient.BaseAddress = new Uri("https://api.nationalize.io");
});

// Add services 
builder.Services.AddMvc();
builder.Services.AddScoped<
    AthleteNetworkChallenge.Web.Services.INameService,
    AthleteNetworkChallenge.Web.Services.NameService>();

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
