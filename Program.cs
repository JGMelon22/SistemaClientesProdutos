using System.Data;
using ClientesProdutos.Infrastructure.Repositories;
using ClientesProdutos.Interfaces;
using FluentValidation.AspNetCore;
using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// AutoMapper
builder.Services.AddAutoMapper(typeof(Program).Assembly);

// Fluent Validation
builder.Services.AddFluentValidationAutoValidation();

// SlqClient
builder.Services.AddScoped<IDbConnection>(x =>
    new SqlConnection(builder.Configuration.GetConnectionString("Default")));

// Repository and Interfaces
builder.Services.AddScoped<IClientRepository, ClientRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    "default",
    "{controller=Home}/{action=Index}/{id?}");

app.Run();