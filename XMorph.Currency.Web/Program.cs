using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using XMorph.Currency.Core.Services;
using XMorph.Currency.DAL.DBContext;
using XMorph.Currency.Repository.Generic;
using XMorph.Currency.Repository.Generic.Interface;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<XMorphCurrencyContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("XmorphCurrencyDb")));
builder.Services.AddControllers();

builder.Services
    .AddAuthentication(
        options => {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }
    )
    .AddJwtBearer(
        options => {
            options.SaveToken = true;
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = new TokenValidationParameters() {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidAudience = builder.Configuration.GetSection("Jwt:Issuer").Value,
                ValidIssuer = builder.Configuration.GetSection("Jwt:Issuer").Value,
                ClockSkew = TimeSpan.Zero,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Jwt:Key").Value)
                )
            };
        }
    );


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    option => {
        option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
        option.AddSecurityDefinition(
            "Bearer",
            new OpenApiSecurityScheme {
                In = ParameterLocation.Header,
                Description = "Please enter a valid token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            }
        );
        option.AddSecurityRequirement(
            new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {  }
                }
            }
        );
    }
);


builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<ICurrencyService, CurrencyService>();
builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<ICompanyFilterService, CompanyFilterService>();
builder.Services.AddScoped<ICompanyRateService, CompanyRateService>();
builder.Services.AddScoped<IUserService, UserService>();


var app = builder.Build();


if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();


app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();


app.Run();












































//public static class Utilities {

//    public static List<Type> finalResult;

//    public static List<Type> GetAllType(Assembly assembly) {

//        var result = new List<Type>();

//        var assemblyDependecies = assembly.GetReferencedAssemblies().Where(x => x.FullName.Contains($"PublicKeyToken=null")).Select(Assembly.Load);
//        var hasNested = assemblyDependecies.Any();

//        foreach (var item in assemblyDependecies) {

//            var aaa = GetAllType(item);

//            result.AddRange(aaa);
//            //nestedResult.Add(rr.Any());

//        }

//        var classesWithInterfaceList = assembly.ExportedTypes.Where(x => x.GetInterfaces().Length > 0).ToList();
//        var interfaceList = assembly.ExportedTypes.Where(x => x.IsInterface).ToList();
//        foreach (var i in interfaceList) {
//            result.AddRange(classesWithInterfaceList.Where(x => x.GetInterfaces().Select(y => y.Name).ToList().Contains(i.Name)).ToList());
//        }

//        return result;



//    }

//    public static void ResolveAllDependencies(IServiceCollection serviceColletion) {

//        var type = typeof(CompanyService);
//        var result = new List<Type>();
//        var types = Assembly.GetEntryAssembly().GetReferencedAssemblies().Where(x => x.FullName.Contains($"PublicKeyToken=null")).Select(Assembly.Load);
//        var typses = Assembly.GetEntryAssembly().GetReferencedAssemblies().ToList();

//        foreach (var c in types) {
//            //var classesWithInterfaceList = c.ExportedTypes.Where(x => x.GetInterfaces().Length > 0).ToList();
//            //var interfaceList = c.ExportedTypes.Where(x => x.IsInterface).ToList();
//            //foreach (var i in interfaceList) {
//            //    result.AddRange(classesWithInterfaceList.Where(x => x.GetInterfaces().Select(y => y.Name).ToList().Contains(i.Name)).ToList());
//            //}

//            result.AddRange(GetAllType(c));

//        }

//        result= result.Distinct().ToList();


//        Type myType1 = Type.GetType("XMorph.Currency.Core");

//        foreach (var c in result) {
//            foreach (var i in c.GetInterfaces()) {




//                //CSharpScriptEngine.Execute();
//                var inter = i.GetType();
//                var cla = c.GetType();
//                var frank = Type.GetType($"{i.Namespace}.{i.Name}, AssemblyName");
//                //var aaa=serviceColletion.ToList().FirstOrDefault(x=>x.ServiceType.Name.Equals(i.Name));
//                //var b = serviceColletion.FirstOrDefault(x => x.ServiceType.Name.Equals(c.Name));
//                //serviceColletion.AddScoped(inter, result.GetType("ddsds"));
//            }

//        }

//    }

//}
