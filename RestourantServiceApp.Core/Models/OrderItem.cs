namespace RestourantServiceApp.Core.Models
{
	public class OrderItem
	{
		public int Count { get; set; }
		public Guid MenuItemId { get; set; }
		public MenuItem MenuItem { get; set; } = null!;
		public Guid OrderId { get; set; }
		public Order Order { get; set; } = null!;

		override public string ToString()
		{
			return $"Count: {Count}, MenuItem: {MenuItem}";
		}
	}
}
