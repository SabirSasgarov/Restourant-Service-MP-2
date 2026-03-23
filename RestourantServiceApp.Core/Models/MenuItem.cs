using RestourantServiceApp.Core.Enums;
using RestourantServiceApp.Core.Models.Common;

namespace RestourantServiceApp.Core.Models
{
	public class MenuItem : BaseEntity
	{
		public string Name { get; set; } = null!;
		public decimal Price { get; set; }
		public Category Category { get; set; }
		public OrderItem OrderItem { get; set; } = null!;
	}
}
