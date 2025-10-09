using Microsoft.EntityFrameworkCore;
using Qserp.Models;
using Qserp.Api;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<QserpDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
    
// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 設定 CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173")
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

var app = builder.Build();

app.UseCors("AllowReactApp");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// 註冊 Auth API
AuthApi.Register(app);

app.Run();


// using Microsoft.Data.SqlClient;
// using Microsoft.Extensions.Configuration;

// // 讀取 appsettings.json
// var config = new ConfigurationBuilder()
//     .AddJsonFile("appsettings.json")
//     .Build();

// string connectionString = config.GetConnectionString("DefaultConnection");

// using (SqlConnection connection = new SqlConnection(connectionString))
// {
//     try
//     {
//         connection.Open();
//         Console.WriteLine("✅ 資料庫連線成功！");
        
//     }
//     catch (Exception ex)
//     {
//         Console.WriteLine("❌ 資料庫連線失敗！");
//         Console.WriteLine("錯誤訊息：" + ex.Message);
//     }
// }
