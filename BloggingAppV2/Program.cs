using BloggingApp.Web.Extensions;

var builder = WebApplication.CreateBuilder(args);


builder.Services.ConfigureServices(builder.Configuration);
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseStaticFiles();

app.UseRouting();

// app.UseResponseCaching();
// app.UseHttpCacheHeaders();

app.UseAuthorization();

app.MapControllers();

app.Run();