namespace RestourantServiceApp.BLogicLayer.Exceptions
{
	public class MenuItemNotFound : Exception
	{
		public MenuItemNotFound(string message) : base(message) { }
	}
}
