global using StudentData.Dto;
global using Microsoft.EntityFrameworkCore;
global using StudentData.Models;
global using System;
global using Microsoft.EntityFrameworkCore.Migrations;
global using System.Diagnostics.Metrics;
global using Microsoft.AspNetCore.Mvc;
global using System.Collections.Generic;
global using StudentData.Data;
global using AutoMapper;
global using StudentData.Interfaces;
global using StudentData.Repository;
global using StudentData;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<ICampusRepository, CampusRepository>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<IContactInfoRepository, ContactInfoRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
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
