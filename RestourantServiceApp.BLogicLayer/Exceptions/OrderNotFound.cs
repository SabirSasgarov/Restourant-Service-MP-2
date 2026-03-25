namespace RestourantServiceApp.BLogicLayer.Exceptions
{
	public class OrderNotFound : Exception
	{
		public OrderNotFound(string message) : base(message) { }
	}
}
