namespace RestourantServiceApp.PL
{
	public class Program
	{
		static void Main(string[] args)
		{
			while (true)
			{
				Console.Write("1 - Menu operations, 2 - Order operations, 3 - Quit\n->");
				string? opp = Console.ReadLine();
				switch (opp)
				{
					case "1":
						Console.Clear();
						MenuOperations();
						Console.Clear();
						break;

					case "2":
						Console.Clear();
						OrderOperations();
						Console.Clear();
						break;

					case "3":
						Console.WriteLine("---Exit---");
						return;

					default:
						Console.WriteLine("Wrong operation!");
						break;
				}
			}
		}

		static void MenuOperations()
		{
			Console.WriteLine("1 - Add new item, 2 - Update item, 3 - Remove item, 4 - All items,\n" +
				"5 - Search by categories, 6 - Search by price, 7 - Search by name, 0 - Exit");
			while (true)
			{
				Console.WriteLine("->");
				string? opp = Console.ReadLine();
				switch (opp)
				{
					case "1":
						break;
					case "2":
						break;
					case "3":
						break;
					case "4":
						break;
					case "5":
						break;
					case "6":
						break;
					case "7":
						break;
					case "0":
						return;
					default:
						Console.WriteLine("Wrong operation!");
						break;
				}
			}
		}

		static void OrderOperations()
		{
			Console.WriteLine("1 - Add new order, 2 - Cancel order, 3 - All orders, 4 - Orders in a time interval, " +
				"\n5 - Search by price, 6 - Search by date, 7 - Search by id, 0 - Exit");
			while (true)
			{
				Console.WriteLine("->");
				string? opp = Console.ReadLine();
				switch (opp)
				{
					case "1":
						break;
					case "2":
						break;
					case "3":
						break;
					case "4":
						break;
					case "5":
						break;
					case "6":
						break;
					case "7":
						break;
					case "0":
						return;
					default:
						Console.WriteLine("Wrong operation!");
						break;
				}
			}
		}

	}
}
