// 初始化一個新的 Web 應用程序的建構器
var builder = WebApplication.CreateBuilder(args);
using Microsoft.EntityFrameworkCore;

// 註冊服務 自動探索應用中的 API 端點、生成 Swagger 文檔
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MemberDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));



// 設定 CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173") // 允許來自 http://localhost:5173 的請求
                   .AllowAnyMethod() // 允許所有 HTTP 方法 (GET, POST, PUT, DELETE 等)
                   .AllowAnyHeader(); // 允許所有 HTTP 標頭
        });
});

// 建立應用程序實例
var app = builder.Build();

// 啟用之前定義的 CORS 策略
app.UseCors("AllowReactApp");

// 配置 HTTP 請求管道
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 強制將所有 HTTP 請求重定向到 HTTPS
app.UseHttpsRedirection();


// 新增登入 API
app.MapPost("/api/login", async (LoginRequest req, MemberDbContext db) =>
{
    var member = await db.Members.FirstOrDefaultAsync(m => m.Work_no == req.Work_no && m.Password == req.Password);
    if (member != null)
        return Results.Ok(new { success = true, work_no = HRUSER.EMPID });
    else
        return Results.Unauthorized();
});

app.Run();



public class User
{
    public int Id { get; set; }
    public string EMPID { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;  // 密碼要存 Hash
}
