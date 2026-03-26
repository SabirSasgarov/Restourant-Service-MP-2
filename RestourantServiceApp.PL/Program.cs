using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using RestourantServiceApp.BLogicLayer.Dtos.MenuItemDtos;
using RestourantServiceApp.BLogicLayer.Exceptions;
using RestourantServiceApp.BLogicLayer.Interfaces;
using RestourantServiceApp.BLogicLayer.Mappers;
using RestourantServiceApp.BLogicLayer.Services;
using RestourantServiceApp.Core.Enums;
using RestourantServiceApp.DataAccsessLayer.Concretes;
using RestourantServiceApp.DataAccsessLayer.Contexts;
using RestourantServiceApp.DataAccsessLayer.Interfaces;

namespace RestourantServiceApp.PL
{
	public class Program
	{
		static void Main(string[] args)
		{
			while (true)
			{
				Console.Write("1 - Menu operations, 2 - Order operations, 0 - Quit\n->");
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

					case "0":
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
			var serviceCollection = new ServiceCollection();
			serviceCollection.AddScoped(typeof(IRepository<>), typeof(Repository<>));
			serviceCollection.AddScoped<IMenuItemService, MenuItemService>();
			serviceCollection.AddDbContext<RestourantDbContext>();
			serviceCollection.AddAutoMapper(opt => { opt.AddProfile(new MapProfile()); });
			serviceCollection.AddLogging();

			var serviceProvider = serviceCollection.BuildServiceProvider();
			var mis = serviceProvider.GetRequiredService<IMenuItemService>();

			while (true)
			{
				Console.WriteLine("1 - Add new item, 2 - Update item, 3 - Remove item, 4 - All items,\n" +
					"5 - Search by categories, 6 - Search by price, 7 - Search by name, 0 - Exit");

				Console.Write("Choose operation\n->");
				string? opp = Console.ReadLine();
				switch (opp)
				{
					case "1":
						try
						{
							Console.WriteLine("Enter name for new item: ");
							string? newItemName = Console.ReadLine();
							ValidateItemName(newItemName);

							Console.WriteLine("Enter price for new item: ");
							decimal newItemPrice = decimal.Parse(Console.ReadLine().Replace('.', ','));
							ValidatePrice(newItemPrice);

							Console.WriteLine("Choose category for new item: ");
							int counter = 1;
							foreach (var catName in Enum.GetNames(typeof(Category)))
								Console.WriteLine(counter++ + " - " + catName);

							int newItemCat = int.Parse(Console.ReadLine()) - 1;

							if (newItemCat < 0 || newItemCat >= Enum.GetNames(typeof(Category)).Length)
								throw new MenuItemWrongValue("Invalid category selection!");

							MenuItemCreateDto menuItemCreateDto = new MenuItemCreateDto
							{
								Name = newItemName,
								Price = newItemPrice,
								Category = (Category)newItemCat
							};

							mis.AddMenuItem(menuItemCreateDto).Wait();
						}
						catch (Exception ex)
						{
							Console.WriteLine(ex.Message);
							break;
						}
						Console.Clear();
						break;
					case "2":
						int count = 1;
						List<MenuItemReturnDto> itemsToEdit = mis.GetMenuItems().Result;
						foreach (var item in itemsToEdit)
						{
							Console.WriteLine(count++ + " - " + item);
						}
						try
						{
							Console.WriteLine("Choose to proceed: ");
							int index = int.Parse(Console.ReadLine());
							
							if (index < 1 || index > itemsToEdit.Count)
								throw new MenuItemWrongValue("Invalid selection!");

							var itemToEdit = itemsToEdit[index - 1];

							Console.WriteLine("Enter new name for it: ");
							string editedItemName = Console.ReadLine();
							for (int i = 0; i < editedItemName.Length; i++)
							{
								if (char.IsDigit(editedItemName[i]))
									throw new MenuItemWrongValue("Name cannot contain digits!");
							}

							Console.WriteLine("Enter new price for it: ");
							decimal editedItemPrice = decimal.Parse(Console.ReadLine().Replace('.', ','));
							ValidatePrice(editedItemPrice);

							mis.EditMenuItem(itemToEdit, editedItemName, editedItemPrice).Wait();
						}
						catch (Exception ex)
						{
							Console.WriteLine(ex.Message);
							break;
						}
						Console.Clear();
						break;
					case "3":
						int countToRemove = 1;
						var itemsToRemove = mis.GetMenuItems().Result;
						foreach (var item in itemsToRemove)
							Console.WriteLine(countToRemove++ + " - " + item);
						try
						{
							Console.WriteLine("Choose to proceed: ");
							int indexToRemove = int.Parse(Console.ReadLine());
							if (indexToRemove < 1 || indexToRemove > itemsToRemove.Count)
								throw new MenuItemWrongValue("Invalid selection!");

							var itemIdToRemove = itemsToRemove[indexToRemove - 1];
							mis.RemoveMenuItem(itemIdToRemove).Wait();
						}
						catch (Exception ex)
						{
							Console.WriteLine(ex.Message);
							break;
						}
						Console.Clear();
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
						try
						{
							Console.WriteLine("Enter first price to search: ");
							decimal startPriceToSearch = decimal.Parse(Console.ReadLine().Replace('.', ','));

							Console.WriteLine("Enter second price to search: ");
							decimal finalPriceToSearch = decimal.Parse(Console.ReadLine().Replace('.', ','));

							var itemsByPrice = mis.GetMenuItemsInRange(startPriceToSearch, finalPriceToSearch).Result;
							if (itemsByPrice.Count == 0)
							{
								Console.WriteLine("No items found!");
								break;
							}
							foreach (var item in itemsByPrice)
								Console.WriteLine(item);
						}
						catch (Exception ex)
						{
							Console.WriteLine(ex.Message);
							break;
						}
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
			var serviceCollection = new ServiceCollection();
			serviceCollection.AddScoped(typeof(IRepository<>), typeof(Repository<>));
			serviceCollection.AddScoped<IOrderService, OrderService>();
			serviceCollection.AddScoped<IMenuItemService, MenuItemService>();
			serviceCollection.AddDbContext<RestourantDbContext>();
			serviceCollection.AddAutoMapper(opt => { opt.AddProfile(new MapProfile()); });
			serviceCollection.AddLogging();

			var serviceProvider = serviceCollection.BuildServiceProvider();
			var mis = serviceProvider.GetRequiredService<IMenuItemService>();
			var os = serviceProvider.GetRequiredService<IOrderService>();

			while (true)
			{
				Console.WriteLine("1 - Add new order, 2 - Cancel order, 3 - All orders, 4 - Orders in a time interval, " +
					"\n5 - Search by price, 6 - Search by date, 7 - Search by id, 0 - Exit");

				Console.Write("Choose Operation\n->");
				string? opp = Console.ReadLine();
				switch (opp)
				{
					case "1":
						var allMenuItems = mis.GetMenuItems().Result;
						int count = 1;
						Console.WriteLine("All Menu Items: ");
						foreach (var item in allMenuItems)
							Console.WriteLine(count++ + " - " + item);
						Console.WriteLine("\n0 - Finish Order!");
						var orderItems = new List<(MenuItemReturnDto menuItemReturnDto, int Count)>();
						while (true)
						{
							try
							{
								Console.WriteLine("Choose menuitem: ");
								int index = int.Parse(Console.ReadLine());

								if (index == 0)
								{
									os.AddOrder(orderItems).Wait();
									break;
								}
								Console.WriteLine("Enter count: ");
								int countItem = int.Parse(Console.ReadLine());

								var menuItemDto = allMenuItems[index - 1];
								orderItems.Add((menuItemDto, countItem));
							}
							catch (Exception ex)
							{
								Console.WriteLine(ex);
								break;
							}
						}
						Console.Clear();
						break;
					case "2":
						int countToRemove = 1;
						var allOrders = os.GetOrders().Result;
						foreach (var item in allOrders)
							Console.WriteLine(countToRemove++ + " - " + item);
						try
						{
							Console.WriteLine("Choose to proceed: ");
							int indexToRemove = int.Parse(Console.ReadLine());
							if (indexToRemove < 1 || indexToRemove > allOrders.Count)
								throw new MenuItemWrongValue("Invalid selection!");

							var orderToRemove = allOrders[indexToRemove - 1];
							os.RemoveOrder(orderToRemove).Wait();
						}
						catch (Exception ex)
						{
							Console.WriteLine(ex.Message);
							break;
						}
						Console.Clear();
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
						try
						{
							Console.WriteLine("Start Date: ");
							DateTime date1 = DateTime.Parse(Console.ReadLine());
							Console.WriteLine("Last Date: ");
							DateTime date2 = DateTime.Parse(Console.ReadLine());

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
						try
						{
							Console.WriteLine("Start Price: ");
							decimal price1 = decimal.Parse(Console.ReadLine().Replace('.', ','));
							Console.WriteLine("Last Price: ");
							decimal price2 = decimal.Parse(Console.ReadLine().Replace('.', ','));

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
						}
						catch (Exception ex)
						{
							Console.WriteLine(ex.Message);
							break;
						}
						break;
					case "6":
						try
						{
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
						}
						catch (Exception ex)
						{
							Console.WriteLine(ex.Message);
							break;
						}
						break;
					case "7":
						var allOrdersToSearch = os.GetOrders().Result;
						int countToSearch = 1;
						foreach (var item in allOrdersToSearch)
							Console.WriteLine(countToSearch++ + " - " + item);
						try
						{
							Console.WriteLine("Choose to proceed: ");
							int indexToSearch = int.Parse(Console.ReadLine());
							if (indexToSearch < 1 || indexToSearch > allOrdersToSearch.Count)
								throw new MenuItemWrongValue("Invalid selection!");

							var orderToSearch = allOrdersToSearch[indexToSearch - 1];
							var order = os.GetOrderByNo(orderToSearch).Result;
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



		private static void ValidatePrice(decimal newItemPrice)
		{
			if (newItemPrice <= 0)
				throw new MenuItemWrongValue("Price must be more than 0!");
		}

		private static void ValidateItemName(string? newItemName)
		{
			if (string.IsNullOrEmpty(newItemName))
				throw new MenuItemWrongValue("Name cannot be empty!");

			for (int i = 0; i < newItemName.Length; i++)
			{
				if (char.IsDigit(newItemName[i]))
					throw new MenuItemWrongValue("Name cannot contain digits!");
			}
		}
	}
}
