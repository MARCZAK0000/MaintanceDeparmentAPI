using DUR_Application;
using DUR_Application.Entities;
using DUR_Application.Middleware;
using DUR_Application.Model.Response;
using DUR_Application.Seeder;
using DUR_Application.Services.Services_Lane.LaneDto.CreateLane;
using DUR_Application.Services.Services_Lane.LaneDto.UpdateLane;
using DUR_Application.Services.Services_Lane.LaneServicesController;
using DUR_Application.Services.Services_Machine.MachineDto.AddMachine;
using DUR_Application.Services.Services_Machine.MachineServicesController;
using DUR_Application.Services.Services_Magazine.AddSpareParts;
using DUR_Application.Services.Services_Magazine.MagazineServicesController;
using DUR_Application.Services.Services_Magazine.ShowSpareParts;
using DUR_Application.Services.Services_Malfunctions.ChangeMalfunctions;
using DUR_Application.Services.Services_Malfunctions.CloseMalfunction;
using DUR_Application.Services.Services_Malfunctions.CreateMalfunction;
using DUR_Application.Services.Services_Malfunctions.GetMalfunctions.GetAllMalfunctionsQuery;
using DUR_Application.Services.Services_Malfunctions.GetMalfunctions.GetClosedMalfunctionsQuery;
using DUR_Application.Services.Services_Malfunctions.MalfunctionsServicesController;
using DUR_Application.Services.Services_Malfunctions.UpdateMalfunctions;
using DUR_Application.Services.Services_User.UserContexServices;
using DUR_Application.Services.Services_User.UserDtos.CreateUser;
using DUR_Application.Services.Services_User.UserDtos.LoginUser;
using DUR_Application.Services.Services_User.UserDtos.UpdateRole;
using DUR_Application.Services.Services_User.UserServicesController;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NLog.Web;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers().AddJsonOptions(options=> 
{
    options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
});
//MyServicest
var authenticationSettings = new AuthenticationSettings();

builder.Configuration.GetSection("Authentication").Bind(authenticationSettings);
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = "Bearer";
    option.DefaultScheme = "Bearer";
    option.DefaultChallengeScheme = "Bearer";
}).AddJwtBearer(cfg =>
{
    cfg.RequireHttpsMetadata = false;
    cfg.SaveToken = true;
    cfg.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidIssuer = authenticationSettings.JwtIssuer,
        ValidAudience = authenticationSettings.JwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey)),
    };
});
builder.Services.AddSingleton(authenticationSettings);
builder.Services.AddCors(options =>
{
    options.AddPolicy("ApiCors", cors =>
    {
        cors.AllowAnyMethod()
            .AllowAnyHeader()
            .WithOrigins(origins: builder.Configuration["AllowedOrigins"]);
    });
});
builder.Services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MyConnection"))); //RegisterDbContext
builder.Services.AddScoped<ErrorHandlingMiddleware>(); //AddMiddleware
builder.Services.AddScoped<DbSeeder>(); //AddSeeder
builder.Services.AddFluentValidationAutoValidation(); //UseFluentValdatior
builder.Host.UseNLog(); //UseNlog
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly()); //AutoMapper 
builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<ILaneServices, LaneServices>();
builder.Services.AddScoped<IMachineServices, MachineServices>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddScoped<IMalfunctionsServices, MalfunctionService>();
builder.Services.AddScoped<IMagazineServices, MagazineServices>();
builder.Services.AddScoped<IUserContextServices, UserContextServices>();

//Validators
/////////////////////////////
///User
builder.Services.AddScoped<IValidator<CreateUserDto>, CreateUserDtoValidator>();
builder.Services.AddScoped<IValidator<LoginUserDto>, LoginUserValidator>();
builder.Services.AddScoped<IValidator<UpdateRoleDto>, UpdateRoleValidtor>();
////////////////////////////
///Malfuncitons
builder.Services.AddScoped<IValidator<CreateMalfunctionDto>,CreateMalfunctionValidator>();
builder.Services.AddScoped<IValidator<UpdateMalfunctionsDto>,UpdateMalfunctionsValidatror>();
builder.Services.AddScoped<IValidator<CloseMalfunctioDto>,CloseMalfunctionValidator>();
builder.Services.AddScoped<IValidator<ChangeMalfunctionsDto>,ChangeMalfunctionsValidator>();
builder.Services.AddScoped<IValidator<GetClosedMalfunctionsQueryDto>, GetCloseMalfunctionsQueryValidator>();
builder.Services.AddScoped<IValidator<GetAllMalfunctionsQuery>, GetAllMalfucntionsQueryValidation>();
////////////////////////////
///Machine
builder.Services.AddScoped<IValidator<AddMachineDto>, AddMachineDtoValidator>();
////////////////////////////
///Lanes
builder.Services.AddScoped<IValidator<CreateLaneDto>,CreateLaneValidator>();
builder.Services.AddScoped<IValidator<UpdateLaneDto>, UpdateLaneValidator>();
////////////////////////////
///Magazine
builder.Services.AddScoped<IValidator<AddSparePartsDto>, AddSparePartsValidator>();
builder.Services.AddScoped<IValidator<SearchPartQuery>, SearchPartQueryValidator>();
////////////////////////////
//Validators


//Default Services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<DbSeeder>();

seeder.Seed();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseCors("ApiCors");
app.UseStaticFiles();
app.UseAuthentication();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
