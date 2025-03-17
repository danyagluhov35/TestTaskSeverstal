using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NotesSeverstal.Context;
using NotesSeverstal.Core;
using NotesSeverstal.IService;
using NotesSeverstal.Middleware;
using NotesSeverstal.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddTransient<INoteService, NoteService>();
builder.Services.AddTransient<IUserService, UserService>();

builder.Services.AddControllersWithViews();


builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(op =>
{
    op.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = AuthOption.Issuer,
        ValidAudience = AuthOption.Audience,
        IssuerSigningKey = AuthOption.GetSymmetricSecurityKey()
    };
});





var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();
app.UseDefaultFiles();
app.UseRouting();

app.MapControllerRoute
    (
        name : "default",
        pattern : "{controller=Home}/{action=Index}/{id?}"
    );

app.UseMiddleware<JwtSecurity>();


app.Run();
