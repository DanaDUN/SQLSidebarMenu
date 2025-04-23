public interface IMenuService
{
    Task<List<SidebarTreeViewModel>> GetMenuTreeAsync(IPrincipal user);
}
