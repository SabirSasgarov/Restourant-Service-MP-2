using RestourantServiceApp.Core.Enums;

namespace RestourantServiceApp.BLogicLayer.Dtos.MenuItemDtos
{
	public class MenuItemReturnDto
	{
		public string Name { get; set; } = null!;
		public decimal Price { get; set; }
		public Category Category { get; set; }

		public override string ToString()
		{
			return $"Name: {Name}, Price: {Price}, Category: {Category}";
		}
	}
}
