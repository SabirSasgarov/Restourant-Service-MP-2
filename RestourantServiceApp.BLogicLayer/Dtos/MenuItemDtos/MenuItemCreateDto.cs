using RestourantServiceApp.Core.Enums;


namespace RestourantServiceApp.BLogicLayer.Dtos.MenuItemDtos
{
	public class MenuItemCreateDto
	{
		public string Name { get; set; } = null!;
		public decimal Price { get; set; }
		public Category Category { get; set; }
	}
}
