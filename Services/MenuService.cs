public class MenuService : IMenuService
{
    private readonly IMenuRepository _repo;
    public MenuService(IMenuRepository repository)
    {
        _repo = repository;
    }
    public async Task<List<SidebarTreeViewModel>> GetMenuTreeAsync(IPrincipal user)
    {
        var claimUser = user as ClaimsPrincipal;
        var empNo = claimUser?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
        if (string.IsNullOrEmpty(id))
        {
            throw new UnauthorizedAccessException("使用者未授權");
        }

        return await _repo.GetMenuTreeAsync(id);
    }
}
