using RestourantServiceApp.Core.Models.Common;

namespace RestourantServiceApp.Core.Models
{
	public class Order : BaseEntity
	{
		public decimal TotalAmount {  get; set; }
		public DateTime	Date {  get; set; }
		public List<OrderItem> OrderItems {  get; set; } = null!;
	}
}
