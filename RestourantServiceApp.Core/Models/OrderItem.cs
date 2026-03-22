namespace RestourantServiceApp.Core.Models
{
	public class OrderItem
	{
		public int Count { get; set; }
		public Guid MenuItemId { get; set; }
		public MenuItem MenuItem { get; set; }
		public Guid OrderId { get; set; }
		public Order Order { get; set; }
	}
}
