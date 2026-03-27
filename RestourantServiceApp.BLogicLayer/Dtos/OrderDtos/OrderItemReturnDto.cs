namespace RestourantServiceApp.BLogicLayer.Dtos.OrderDtos
{
	public class OrderItemReturnDto
	{
		public string MenuItemName { get; set; } = null!;
		public decimal MenuItemPrice { get; set; }
		public int Count { get; set; }

		public override string ToString()
		{
			return $"Name: {MenuItemName}, Price: {MenuItemPrice}, Count: {Count}";
		}
	}
}
