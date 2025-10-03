var builder = WebApplication.CreateBuilder(args);

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

// 寫死的帳號密碼
var validEmpId = "1002327011";
var validPass = "1002327011";
var adminEmpId = "admin";
var adminPass = "admin";

// 登入 API
app.MapPost("/api/login", (LoginRequest req) =>
{
    if ((req.EMPID == validEmpId && req.PASS == validPass) || (req.EMPID == adminEmpId && req.PASS == adminPass))
    {
        var isAdmin = req.EMPID == adminEmpId;
        return Results.Ok(new { success = true, empid = req.EMPID, isAdmin = isAdmin });
    }
    else
    {
        return Results.Unauthorized();
    }
});

app.Run();

// 登入請求的資料模型
public class LoginRequest
{
    public required string EMPID { get; set; }
    public required string PASS { get; set; }
}