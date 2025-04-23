public interface IMenuRepository
{
    Task<List<SidebarTreeViewModel>> GetMenuTreeAsync(string id);
}
