namespace FinanceTracker.Core.Models
{
    public class MenuItem
    {
        public MenuCode Id { get; set; }
        public ToolCode ToolCode { get; set; }
        public string Name { get; set; } = null!;
        public int? ParentId { get; set; }
    }
}