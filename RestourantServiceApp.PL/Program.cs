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
				Console.Write("Choose operation\n->");
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
							Console.WriteLine(countToRemove++ + " - " + item);

						Console.WriteLine("Choose to proceed: ");
						int indexToRemove = int.Parse(Console.ReadLine());
						try
						{
							var itemIdToRemove = mis.GetMenuItems().Result[indexToRemove - 1].Id;
							mis.RemoveMenuItem(itemIdToRemove);
						}
						catch (Exception ex)
						{
							Console.WriteLine(ex.Message);
							break;
						}
						break;
					case "4":
						var allItems = mis.GetMenuItems().Result;
						if (allItems.Count == 0)
						{
							Console.WriteLine("No items found!");
							break;
						}
						foreach (var item in allItems)
							Console.WriteLine(item);

						break;
					case "5":
						Console.WriteLine("Categories: ");
						int catCount = 1;
						foreach (var catName in Enum.GetNames(typeof(Category)))
							Console.WriteLine(catCount++ + " - " + catName);
						try
						{
							Console.WriteLine("Enter category to search: ");
							int catToSearch = int.Parse(Console.ReadLine()) - 1;

							var itemsByCat = mis.GetMenuItemsByCategory((Category)catToSearch).Result;
							if (itemsByCat.Count == 0)
							{
								Console.WriteLine("No items found!");
								break;
							}
							foreach (var item in itemsByCat)
								Console.WriteLine(item);
						}
						catch (Exception ex)
						{
							Console.WriteLine(ex.Message);
							break;
						}
						break;
					case "6":
						Console.WriteLine("Enter first price to search: ");
						decimal startPriceToSearch = decimal.Parse(Console.ReadLine());

						Console.WriteLine("Enter second price to search: ");
						decimal finalPriceToSearch = decimal.Parse(Console.ReadLine());

						var itemsByPrice = mis.GetMenuItemsInRange(startPriceToSearch, finalPriceToSearch).Result;
						if (itemsByPrice.Count == 0)
						{
							Console.WriteLine("No items found!");
							break;
						}
						foreach (var item in itemsByPrice)
							Console.WriteLine(item);
						break;
					case "7":
						Console.WriteLine("Enter name to search: ");
						string nameToSearch = Console.ReadLine();
						var itemsByName = mis.GetMenuItemsBySearch(nameToSearch).Result;
						if (itemsByName.Count == 0)
						{
							Console.WriteLine("No items found!");
							break;
						}
						foreach (var item in itemsByName)
							Console.WriteLine(item);
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
				Console.Write("Choose Operation\n->");
				string? opp = Console.ReadLine();
				switch (opp)
				{
					case "1":
						//Console.WriteLine("Enter items for new order in format: itemId1 count1, itemId2 count2, ...");
						break;
					case "2":
						int countToRemove = 1;
						var allOrders = os.GetOrders().Result;
						foreach (var item in allOrders)
							Console.WriteLine(countToRemove++ + " - " + item);

						Console.WriteLine("Choose to proceed: ");
						int indexToRemove = int.Parse(Console.ReadLine());
						try
						{
							var itemIdToRemove = allOrders[indexToRemove - 1].Id;
							os.RemoveOrder(itemIdToRemove);
						}
						catch (Exception ex)
						{
							Console.WriteLine(ex.Message);
							break;
						}
						break;
					case "3":
						var allOrdersToShow = os.GetOrders().Result;
						if (allOrdersToShow.Count == 0)
						{
							Console.WriteLine("No orders found!");
							break;
						}
						Console.WriteLine("Orders Found:");
						foreach (var item in allOrdersToShow)
						{
							Console.WriteLine(item);
							item.OrderItems.ForEach(oi => Console.WriteLine("\t" + oi));
						}
						break;
					case "4":
						Console.WriteLine("Start Date: ");
						DateTime date1 = DateTime.Parse(Console.ReadLine());
						Console.WriteLine("Last Date: ");
						DateTime date2 = DateTime.Parse(Console.ReadLine());
						try
						{
							var orders = os.GetOrdersByDatesInterval(date1, date2).Result;
							if (orders.Count == 0)
							{
								Console.WriteLine("No orders found!");
								break;
							}
							Console.WriteLine("Orders Found:");
							foreach (var item in orders)
							{
								Console.WriteLine(item);
								item.OrderItems.ForEach(oi => Console.WriteLine("\t" + oi));
							}
						}
						catch (Exception ex)
						{
							Console.WriteLine(ex.Message);
							break;
						}
						break;
					case "5":
						Console.WriteLine("Start Price: ");
						decimal price1 = decimal.Parse(Console.ReadLine());
						Console.WriteLine("Last Price: ");
						decimal price2 = decimal.Parse(Console.ReadLine());

						var ordersByPrice = os.GetOrdersByPriceInterval(price1, price2).Result;
						if (ordersByPrice.Count == 0)
						{
							Console.WriteLine("No orders found!");
							break;
						}
						Console.WriteLine("Orders Found:");
						foreach (var item in ordersByPrice)
						{
							Console.WriteLine(item);
							item.OrderItems.ForEach(oi => Console.WriteLine("\t" + oi));
						}
						break;
					case "6":
						Console.WriteLine("Enter date: ");
						DateTime dateToSearch = DateTime.Parse(Console.ReadLine());
						var ordersByDate = os.GetOrdersByDate(dateToSearch).Result;
						if (ordersByDate.Count == 0)
						{
							Console.WriteLine("No orders found!");
							break;
						}
						Console.WriteLine("Orders Found:");
						foreach (var item in ordersByDate)
						{
							Console.WriteLine(item);
							item.OrderItems.ForEach(oi => Console.WriteLine("\t" + oi));
						}
						break;
					case "7":
						var allOrdersToSearch = os.GetOrders().Result;
						int countToSearch = 1;
						foreach (var item in allOrdersToSearch)
							Console.WriteLine(countToSearch++ + " - " + item);
						Console.WriteLine("Choose to proceed: ");
						int indexToSearch = int.Parse(Console.ReadLine());
						try
						{
							var itemIdToSearch = allOrdersToSearch[indexToSearch - 1].Id;
							var order = os.GetOrderByNo(itemIdToSearch).Result;
							Console.WriteLine(order);
							order.OrderItems.ForEach(oi => Console.WriteLine("\t" + oi));
						}
						catch (Exception ex)
						{
							Console.WriteLine(ex.Message);
							break;
						}
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
