
using Microsoft.EntityFrameworkCore;
using XMorph.Currency.Core.Services;
using XMorph.Currency.DAL.DBContext;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var qyu = builder.Configuration.GetConnectionString("XmorphCurrencyDb");
builder.Services.AddDbContext<XMorphCurrencyContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("XmorphCurrencyDb")));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ICurrencyService,CurrencyService>();
builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<ICompanyFilterService, CompanyFilterService>();
builder.Services.AddScoped<ICompanyRateService, CompanyRateService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
