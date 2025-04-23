// 註冊服務
builder.Services.AddScoped<IMenuRepository, MenuRepository>();
builder.Services.AddScoped<IMenuService, MenuService>();
builder.Services.AddHttpContextAccessor();

// 讀取配置
var loginPath = builder.Configuration["Authentication:LoginPath"];
var logoutPath = builder.Configuration["Authentication:LogoutPath"];

// 註冊身分驗證
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = loginPath; // 未登入時跳轉的畫面(動態路由)
        options.LogoutPath = logoutPath; // 登出的畫面(動態路由)
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20); // Cookie 有效時間
        options.Cookie.IsEssential = true; // 重要 Cookie，不會被清除
        options.SlidingExpiration = true; // 滑動過期，20 分鐘內操作會刷新有效期
        options.Cookie.HttpOnly = true; // 僅允許伺服器存取 Cookie，防止 XSS 攻擊
        options.Cookie.SameSite = SameSiteMode.Strict; // 1. Strict: 完全禁止第三方 Cookie 2. Lax: 部分禁止第三方 Cookie 3. None: 允許第三方 Cookie
        options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest; // 1. None: 不使用 HTTPS 2. SameAsRequest: 與請求相同 3. Always: 強制使用 HTTPS
    });
builder.Services.AddAuthorization(); // 註冊授權

app.UseAuthentication();    // 啟用認證中介軟體 
app.UseAuthorization();     // 進行授權
