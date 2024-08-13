using DinkToPdf.Contracts;
using DinkToPdf;
using FluxoCaixa.Application.CommandStack.Consumers;
using FluxoCaixa.Application.CommandStack.Lancamento.CriarLancamento;
using FluxoCaixa.Application.Infrastructure;
using FluxoCaixa.Application.Infrastructure.Consolidado.Abstractions;
using FluxoCaixa.Application.Infrastructure.Consolidado.Repositories;
using FluxoCaixa.Application.Infrastructure.Lancamento.Abstractions;
using FluxoCaixa.Application.Infrastructure.Lancamento.Repositories;
using FluxoCaixa.Application.QueryStack.ConsolidadoDiario.ObterConsolidadoDiario;
using FluxoCaixa.Application.WebApi;
using FluxoCaixa.Application.WebApi.ExceptionHandler;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using MongoFramework;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<FinanceiroContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"))
        .EnableSensitiveDataLogging() // Para logar valores de parâmetros
        .LogTo(Console.WriteLine, LogLevel.Information) // Para logar as consultas
);

builder.Services.AddTransient<IMongoDbConnection>(s =>
    MongoDbConnection.FromUrl(new MongoUrl(builder.Configuration["ConnectionMongoString"])));
builder.Services.AddTransient<FinanceiroContextMongo>();

// Configuração das injeções de dependência
builder.Services.AddScoped<IConsolidadoRepository, ConsolidadoRepository>();
builder.Services.AddScoped<ILancamentoRepository, LancamentoRepository>();

builder.Services.AddScoped<GlobalExceptionHandler>();

builder.Services.AddScoped(typeof(IRequestHandler<CriarLancamentoCommand, CriarLancamentoResponse>), typeof(CriarLancamentoCommandHandler));
builder.Services.AddScoped(typeof(IRequestHandler<CriarConsolidadoDiarioCommand, CriarConsolidadoDiarioResponse>), typeof(CriarConsolidadoDiarioCommandHandler));

builder.Services.AddScoped(typeof(IRequestHandler<ObterConsolidadoDiarioQuery, List<ObterConsolidadoDiariorReadModel>>), typeof(ObterConsolidadoDiarioQueryHandler));
builder.Services.AddScoped(typeof(IRequestHandler<ObterConsolidadoDiarioPdfQuery, byte[]>), typeof(ObterConsolidadoDiarioPdfQueryHandler));

//htmlpdf
ConfigurarHtmlPdf();

builder.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));

builder.Configuration
       .SetBasePath(Directory.GetCurrentDirectory())
       .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
       .AddEnvironmentVariables();

//Mediatr
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblyContaining<Program>();
    cfg.Lifetime = ServiceLifetime.Scoped;
});

// Configuração do MassTransit
builder.Services.AddMassTransit(config =>
{
    config.AddConsumer<LancamentoConsumer>();

    config.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration["RabbitMq_Envinronment"], 5672, "/", h =>
        {
            h.Username(builder.Configuration["RabbitMq_User"]);
            h.Password(builder.Configuration["RabbitMq_Password"]);
        });
        cfg.ReceiveEndpoint("consolidado-diario", ep =>
        {
            ep.ConfigureConsumer<LancamentoConsumer>(context);
        });
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

static void ConfigurarHtmlPdf()
{
    try
    {
        var architectureFolder = (IntPtr.Size == 8) ? "64 bit" : "32 bit";
        var wkHtmlToPdfPath = Path.Combine(AppContext.BaseDirectory, $"wkhtmltox", architectureFolder, "libwkhtmltox.dll");
        var customAssemblyLoadContext = new CustomAssemblyLoadContext();
        customAssemblyLoadContext?.LoadUnmanagedLibrary(wkHtmlToPdfPath);
    }
    catch
    {
        Console.WriteLine("falha ao carregar lib pdf");
    }
}