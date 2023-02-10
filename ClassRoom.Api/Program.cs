using ClassRoom.Api.Context;
using ClassRoom.Api.Entities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//authentication service
builder.Services.AddDbContext<AppLicationDbContext>(option =>
{
    option.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("ClassRoom"));
});



//database service
builder.Services.AddIdentity<User, Role>()
    .AddEntityFrameworkStores<AppLicationDbContext>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();