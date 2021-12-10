using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var AppAccess = "_AppAccess";

builder.Services.AddControllers();

var connStrKey = "PrsDbContextWinhost";

#if DEBUG
    connStrKey = "PrsDbContext";
#endif

builder.Services.AddDbContext<PrsDbContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString(connStrKey));
});

builder.Services.AddCors(x =>
{
    x.AddPolicy(name: AppAccess,
        builder =>
        {
            builder.WithOrigins("http://localhost", "http://localhost:4200")
                .AllowAnyMethod()
                .AllowAnyHeader();
        }
    );
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseRouting();

app.UseCors(AppAccess);

app.UseAuthorization();

app.MapControllers();

app.Run();
