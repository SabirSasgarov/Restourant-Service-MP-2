using Microsoft.EntityFrameworkCore;
using RestourantServiceApp.BLogicLayer.Interfaces;
using RestourantServiceApp.BLogicLayer.Services;
using RestourantServiceApp.Core.Enums;
using RestourantServiceApp.DataAccsessLayer.Contexts;

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
			RestourantDbContext context = new RestourantDbContext();
			IMenuItemService mis = new MenuItemService(context);

			Console.WriteLine("1 - Add new item, 2 - Update item, 3 - Remove item, 4 - All items,\n" +
				"5 - Search by categories, 6 - Search by price, 7 - Search by name, 0 - Exit");

			while (true)
			{
				Console.Write("->");
				string? opp = Console.ReadLine();
				switch (opp)
				{
					case "1":
						Console.WriteLine("Enter name for new item: ");
						string? newItemName = Console.ReadLine();

						Console.WriteLine("Enter price for new item: ");
						decimal newItemPrice = decimal.Parse(Console.ReadLine());

						Console.WriteLine("Enter category for new item: ");
						string? newItemCat = Console.ReadLine();
						Category cat;
						Enum.TryParse<Category>(newItemCat, out cat);

						mis.AddMenuItem(newItemName, newItemPrice, cat);
						break;
					case "2":
						int count = 1;
						foreach (var item in mis.GetMenuItems().Result)
						{
							Console.WriteLine(count++ + " - " + item);
						}
						Console.WriteLine("Choose to proceed: ");
						int index = int.Parse(Console.ReadLine());
						var itemIdToEdit = mis.GetMenuItems().Result[index - 1].Id;

						Console.WriteLine("Enter new name for it: ");
						string editedItemName = Console.ReadLine();

						Console.WriteLine("Enter new price for it: ");
						decimal editedItemPrice = decimal.Parse(Console.ReadLine());

						mis.EditMenuItem(itemIdToEdit, editedItemName, editedItemPrice);
						break;
					case "3":
						int countToRemove = 1;
						foreach (var item in mis.GetMenuItems().Result)
						{
							Console.WriteLine(countToRemove++ + " - " + item);
						}
						Console.WriteLine("Choose to proceed: ");
						int indexToRemove = int.Parse(Console.ReadLine());
						var itemIdToRemove = mis.GetMenuItems().Result[indexToRemove - 1].Id;

						mis.RemoveMenuItem(itemIdToRemove);
						break;
					case "4":
						var allItems = mis.GetMenuItems().Result;
						foreach (var item in allItems)
						{
							Console.WriteLine(item);
						}
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
			RestourantDbContext context = new RestourantDbContext();
			IOrderService os = new OrderService(context);

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
