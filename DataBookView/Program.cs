using DataBookView.Authentification;
using DataBookView.Controllers;
using DataBookView.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddTransient<IDataBookData, DataBookDataApi>();

        builder.Services.AddMvc(mvcOtions => mvcOtions.EnableEndpointRouting = false);

        var app = builder.Build();

        app.UseMvc();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=DataBook}/{action=Index}/{id?}");

        app.Run();
    }
}