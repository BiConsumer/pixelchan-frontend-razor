using Pixelchan.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddSassCompiler();
builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<PostService>();
builder.Services.AddSingleton<TopicService>();
builder.Services.AddSingleton<CategoryService>();
builder.Services.AddSingleton<TranslationService, JsonTranslationService>();
builder.Services.AddSingleton<DateService>();

var app = builder.Build();

if (app.Environment.IsDevelopment()) {
	app.UseDeveloperExceptionPage();
	app.UseStaticFiles(new StaticFileOptions {
		OnPrepareResponse = context => context.Context.Response.Headers.Add("Cache-Control", "no-cache")
	});
} else {
	app.UseStaticFiles();
}

app.UseRouting();
app.UseEndpoints(endpoints => {
	endpoints.MapControllerRoute(
		name: "default",
		pattern: "{controller}/{action}/{id?}",
		defaults: new { controller = "Home", action = "Index" });
});

app.MapRazorPages();
app.Run();