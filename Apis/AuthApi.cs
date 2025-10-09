using Microsoft.EntityFrameworkCore;
using Qserp.Models;

namespace Qserp.Api
{
    public static class AuthApi
    {
        public static void Register(WebApplication app)
        {
            app.MapPost("/api/login", async (LoginRequest req, QserpDbContext db) =>
            {
                // 查詢資料庫是否有符合的帳號與密碼
                var user = await db.Users
                    .FirstOrDefaultAsync(u => u.EMPID == req.EMPID && u.PASS == req.PASS);

                if (user == null)
                {
                    // 查無此人，回傳 401
                    return Results.Unauthorized();
                }

                // 登入成功
                return Results.Ok(new
                {
                    success = true,
                    empid = user.EMPID,
                    isAdmin = user.IsAdmin == 1
                });
            });
        }
    }
}
// admin 1002327011
