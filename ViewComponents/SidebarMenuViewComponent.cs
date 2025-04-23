public class SidebarMenuViewComponent : ViewComponent
{
    private readonly IMenuService _menu;
    public SidebarMenuViewComponent(IMenuService menu)
    {
        _menu = menu;
    }
    public async Task<IViewComponentResult> InvokeAsync()
    {
        if (!User.Identity.IsAuthenticated)
            return View(new List<SidebarTreeViewModel>());

        var menu = await _menu.GetMenuTreeAsync(User);
        return View(menu);
    }
}
