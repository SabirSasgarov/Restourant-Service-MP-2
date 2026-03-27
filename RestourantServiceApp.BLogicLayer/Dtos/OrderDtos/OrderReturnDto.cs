using RestourantServiceApp.Core.Models;

namespace RestourantServiceApp.BLogicLayer.Dtos.OrderDtos
{
	public class OrderReturnDto
	{
		public decimal TotalAmount { get; set; }
		public DateTime Date { get; set; } = DateTime.Now;
		public List<OrderItemReturnDto> OrderItems { get; set; } = null!;

		override public string ToString()
		{
			return $"TotalAmount: {TotalAmount}, Date: {Date}";
		}
	}
}
