public class MenuRepository : IMenuRepository
{
    private readonly string _conn;
    public MenuRepository(IConfiguration configuration)
    {
        _conn = configuration.GetConnectionString("MyConn");
    }
    public async Task<List<SidebarTreeViewModel>> GetMenuTreeAsync(string id) 
    {
        try
        {
            using (var conn = new SqlConnection(_conn))
            {
                var result = (await conn.QueryAsync<SidebarTreeViewModel>(
                        "SidebarTree",  // 呼叫預存程序
                        new { ID = id },
                        commandType: CommandType.StoredProcedure
                    )).ToList();

                // 建立樹狀結構
                var treeMenu = result.Where(x => x.Parent == "#").OrderBy(x => x.Seq).ToList();

                foreach (var menu in treeMenu)
                {
                    menu.Children = result.Where(x => x.Parent == menu.Form).OrderBy(x => x.Seq).ToList();
                }
                 
                return treeMenu;
            }
        }
        catch (Exception)
        {
            throw new Exception("無法載入Menu。");
        }
    }
}
