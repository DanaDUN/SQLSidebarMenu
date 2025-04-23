    public class SidebarTreeViewModel
    {
        public string? Form { get; set; }        // 表單名稱
        public string? Title { get; set; }       // 標題
        public string? Parent { get; set; }      // 父層
        public int? FormType { get; set; }       // 層級
        public int? Seq { get; set; }            // 排序
        public List<SidebarTreeViewModel>? Children { get; set; } = new();
    }
