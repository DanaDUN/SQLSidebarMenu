public class AccountController : Controller
{
    private readonly MyContext _db;
    public AccountController(MyContext context)
    {
        _db = context;
    }

    // 登入畫面
    [HttpGet]
    [AllowAnonymous]
    public IActionResult Login()
    {
        return View();
    }
    // 登入處理
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login(string id, string pwd)
    {
        // 驗證登入
        var user = _db.USER.FirstOrDefault(a => a.ID == id && a.PASSWORD == pwd); // 這邊就是查找使用者資料表
        if (user == null)
        {
            return Json(new { success = false, message = "登入失敗！" });
        }

        // 建立使用者的 Claims
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.ID), // Name 儲存 ID
            new Claim(ClaimTypes.Role, user.NAME) // Role 儲存 NAME
        };

        // 建立 ClaimsIdentity
        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        // 設定 Cookie 登入屬性
        var authProperties = new AuthenticationProperties
        {
            IsPersistent = false, // 關閉瀏覽器後清除Cookie
            ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(20) // Cookie 有效時間
        };

        // 登入
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

        //return RedirectToAction("Index", "Home");
        return Json(new { success = true, message = "登入成功！" });
    }

    // 登出
    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login");
    }

    // 登入狀態檢查
    [HttpPost]
    public IActionResult IsAuthenticated()
    {
        // 檢查是否已登入
        if (User.Identity?.IsAuthenticated == true)
        {
            return Ok(new { isAuthenticated = true });
        }
        return Unauthorized(new { isAuthenticated = false, message = "尚未登入。" });
    }

}
