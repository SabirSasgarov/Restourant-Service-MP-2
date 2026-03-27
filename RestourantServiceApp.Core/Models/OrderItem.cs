using RestourantServiceApp.Core.Models.Common;

namespace RestourantServiceApp.Core.Models
{
	public class OrderItem : BaseEntity
	{ 
		public int Count { get; set; }
		public Guid MenuItemId { get; set; }
		public MenuItem MenuItem { get; set; } = null!;
		public Guid OrderId { get; set; }
		public Order Order { get; set; } = null!;
	}
}
