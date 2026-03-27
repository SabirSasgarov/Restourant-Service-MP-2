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
				Console.Write("1 - Menu operations\n2 - Order operations\n0 - Quit\n->");
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
				PrintHeader("MENU ITEM OPERATIONS");
				Console.WriteLine("1 - Add new item");
				Console.WriteLine("2 - Update item");
				Console.WriteLine("3 - Remove item");
				Console.WriteLine("4 - All items");
				Console.WriteLine("5 - Search by categories");
				Console.WriteLine("6 - Search by price");
				Console.WriteLine("7 - Search by name");
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("0 - Exit");
				Console.ResetColor();

				Console.Write("\nChoose operation\n-> ");
				string? opp = Console.ReadLine();
				switch (opp)
				{
					case "1":
						Console.Clear();
						try
						{
							Console.Write("Enter name for new item -> ");
							string? newItemName = Console.ReadLine();
							while (true)
							{
								bool isCorrect = true;
								if (string.IsNullOrEmpty(newItemName))
								{
									PrintWarning("Name cannot be empty!");
									Console.Write("Enter name for new item -> ");
									newItemName = Console.ReadLine();
									isCorrect = false;
								}
								for (int i = 0; i < newItemName.Length; i++)
								{
									if (char.IsDigit(newItemName[i]))
									{
										PrintWarning("Name cannot contain digits!");
										Console.Write("Enter name for new item -> ");
										newItemName = Console.ReadLine();
										isCorrect = false;
									}
								}
								if(isCorrect)
									break;
							}

							Console.Write("Enter price for new item -> ");
							decimal newItemPrice = decimal.Parse(Console.ReadLine().Replace('.', ','));
							while(newItemPrice <= 0)
							{
								PrintWarning("Price must be more than 0!");
								Console.Write("Enter price for new item -> ");
								newItemPrice = decimal.Parse(Console.ReadLine().Replace('.', ','));
							}

							PrintHeader("CATEGORIES");
							int counter = 1;
							foreach (var catName in Enum.GetNames(typeof(Category)))
								Console.WriteLine($"{counter++,2}. {catName}");

							Console.Write("Choose category -> ");
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
							PrintSuccess("Menu item added successfully.");
						}
						catch (Exception ex)
						{
							PrintException(ex);
							break;
						}
						break;
					case "2":
						Console.Clear();
						List<MenuItemReturnDto> itemsToEdit = mis.GetMenuItems().Result;
						if (itemsToEdit.Count == 0)
						{
							PrintWarning("No items found!");
							break;
						}

						PrintHeader("ALL MENU ITEMS");
						int count = 1;
						foreach (var item in itemsToEdit)
							Console.WriteLine($"{count++,2}. {item}");

						try
						{
							Console.Write("Choose item to update -> ");
							int index = int.Parse(Console.ReadLine());

							if (index < 1 || index > itemsToEdit.Count)
								throw new MenuItemWrongValue("Invalid selection!");

							var itemToEdit = itemsToEdit[index - 1];

							Console.Write("Enter new name -> ");
							string? editedItemName = Console.ReadLine();
							while (true)
							{
								int counter = 0;
								for (int i = 0; i < editedItemName.Length; i++)
								{
									if (char.IsDigit(editedItemName[i]))
									{
										counter++;
										PrintWarning("Name cannot contain digits!");
										break;
									}
								}
								if (counter == 0)
									break;
								Console.Write("Enter new name -> ");
								editedItemName = Console.ReadLine();
							}

							if (string.IsNullOrWhiteSpace(editedItemName))
								editedItemName = itemToEdit.Name;

							Console.Write("Enter new price -> ");
							string editedItemPrice = Console.ReadLine().Replace('.', ',');
							decimal editedItemPriceDecimal;
							if (!string.IsNullOrWhiteSpace(editedItemPrice))
								editedItemPriceDecimal = decimal.Parse(editedItemPrice);
							else
								editedItemPriceDecimal = itemToEdit.Price;

							while (editedItemPriceDecimal <= 0)
							{
								PrintWarning("Price must be more than 0!");
								Console.Write("Enter new price -> ");
								editedItemPrice = Console.ReadLine().Replace('.', ',');
								if (!string.IsNullOrWhiteSpace(editedItemPrice))
									editedItemPriceDecimal = decimal.Parse(editedItemPrice);
								else
									editedItemPriceDecimal = itemToEdit.Price;
							}

							mis.EditMenuItem(itemToEdit, editedItemName, editedItemPriceDecimal).Wait();
							PrintSuccess("Menu item updated successfully.");
						}
						catch (Exception ex)
						{
							PrintException(ex);
							break;
						}
						break;
					case "3":
						Console.Clear();
						var itemsToRemove = mis.GetMenuItems().Result;
						if (itemsToRemove.Count == 0)
						{
							PrintWarning("No items found!");
							break;
						}

						PrintHeader("ALL MENU ITEMS");
						int countToRemove = 1;
						foreach (var item in itemsToRemove)
							Console.WriteLine($"{countToRemove++,2}. {item}");
						try
						{
							Console.Write("Choose item to remove -> ");
							int indexToRemove = int.Parse(Console.ReadLine());
							if (indexToRemove < 1 || indexToRemove > itemsToRemove.Count)
								throw new MenuItemWrongValue("Invalid selection!");

							var itemIdToRemove = itemsToRemove[indexToRemove - 1];
							mis.RemoveMenuItem(itemIdToRemove).Wait();
							PrintSuccess("Menu item removed successfully.");
						}
						catch (Exception ex)
						{
							PrintException(ex);
							break;
						}
						break;
					case "4":
						Console.Clear();
						var allItems = mis.GetMenuItems().Result;
						if (allItems.Count == 0)
						{
							PrintWarning("No items found!");
							break;
						}
						PrintHeader("ALL MENU ITEMS");
						foreach (var item in allItems)
							Console.WriteLine(item);

						break;
					case "5":
						Console.Clear();
						PrintHeader("CATEGORIES");
						int catCount = 1;
						foreach (var catName in Enum.GetNames(typeof(Category)))
							Console.WriteLine($"{catCount++,2}. {catName}");
						try
						{
							Console.Write("Enter category to search -> ");
							int catToSearch = int.Parse(Console.ReadLine()) - 1;
							if (catToSearch < 0 || catToSearch >= Enum.GetNames(typeof(Category)).Length)
								throw new MenuItemWrongValue("Invalid category selection!");

							var itemsByCat = mis.GetMenuItemsByCategory((Category)catToSearch).Result;
							if (itemsByCat.Count == 0)
							{
								PrintWarning($"No items found in '{(Category)catToSearch}' category!");
								break;
							}

							PrintHeader("ITEMS FOUND");
							foreach (var item in itemsByCat)
								Console.WriteLine(item);
						}
						catch (Exception ex)
						{
							PrintException(ex);
							break;
						}
						break;
					case "6":
						Console.Clear();
						try
						{
							Console.Write("Enter first price to search -> ");
							string? startPriceInput = Console.ReadLine();
							if (string.IsNullOrWhiteSpace(startPriceInput))
								throw new MenuItemWrongValue("Price input cannot be empty!");
							decimal startPriceToSearch = decimal.Parse(startPriceInput.Replace('.', ','));
							while (startPriceToSearch <= 0)
							{
								PrintWarning("Price must be more than 0!");
								Console.Write("Enter first price to search -> ");
								startPriceToSearch = decimal.Parse(Console.ReadLine().Replace('.', ','));
							}

							Console.Write("Enter second price to search -> ");
							string? finalPriceInput = Console.ReadLine();
							if (string.IsNullOrWhiteSpace(finalPriceInput))
                                throw new MenuItemWrongValue("Price input cannot be empty!");
							decimal finalPriceToSearch = decimal.Parse(finalPriceInput.Replace('.', ','));
							while (finalPriceToSearch < 0)
							{
								PrintWarning("Price must be more than 0!");
								Console.Write("Enter second price to search -> ");
								finalPriceToSearch = decimal.Parse(Console.ReadLine().Replace('.', ','));
							}

							if (startPriceToSearch > finalPriceToSearch)
							{
								var temp = startPriceToSearch;
								startPriceToSearch = finalPriceToSearch;
								finalPriceToSearch = temp;
							}

							var itemsByPrice = mis.GetMenuItemsInRange(startPriceToSearch, finalPriceToSearch).Result;
							if (itemsByPrice.Count == 0)
							{
								PrintWarning("No items found!");
								break;
							}
							PrintHeader("ITEMS FOUND");
							foreach (var item in itemsByPrice)
								Console.WriteLine(item);
						}
						catch (Exception ex)
						{
							PrintException(ex);
							break;
						}
						break;
					case "7":
						Console.Clear();
						try
						{
							Console.Write("Enter name to search -> ");
							string? nameToSearch = Console.ReadLine();
							var itemsByName = mis.GetMenuItemsBySearch(nameToSearch).Result;
							if (itemsByName.Count == 0)
							{
								PrintWarning("No items found!");
								break;
							}
							PrintHeader("ITEMS FOUND");
							foreach (var item in itemsByName)
								Console.WriteLine(item);
						}
						catch (Exception ex)
						{
							PrintException(ex);
							break;
						}
						break;
					case "0":
						return;
					default:
						PrintWarning("Wrong operation!");
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
				PrintHeader("ORDER OPERATIONS");
				Console.WriteLine("1 - Add new order");
				Console.WriteLine("2 - Cancel order");
				Console.WriteLine("3 - All orders");
				Console.WriteLine("4 - Orders in a time interval");
				Console.WriteLine("5 - Search by price");
				Console.WriteLine("6 - Search by date");
				Console.WriteLine("7 - Search by id");
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("0 - Exit");
				Console.ResetColor();

				Console.Write("\nChoose operation\n-> ");
				string? opp = Console.ReadLine();
				switch (opp)
				{
					case "1":
						Console.Clear();
						var allMenuItems = mis.GetMenuItems().Result;
						if (allMenuItems.Count == 0)
						{
							PrintWarning("No menu items found. Please add menu items first.");
							break;
						}

						PrintHeader("ALL MENU ITEMS");
						int count = 1;
						foreach (var item in allMenuItems)
							Console.WriteLine($"{count++,2}. {item}");

						Console.ForegroundColor = ConsoleColor.Red;
						Console.WriteLine("\n0 - Finish order");
						Console.ResetColor();

						var orderItems = new List<(MenuItemReturnDto menuItemReturnDto, int Count)>();
						while (true)
						{
							try
							{
								Console.Write("Choose menu item -> ");
								int index = int.Parse(Console.ReadLine());

								if (index == 0)
								{
									os.AddOrder(orderItems).Wait();
									PrintSuccess("Order created successfully.");
									break;
								}

								if (index < 1 || index > allMenuItems.Count)
									throw new MenuItemWrongValue("Invalid selection!");

								int countItem;
								while (true)
								{
									Console.Write("Enter count -> ");
									countItem = int.Parse(Console.ReadLine());
									if (countItem <= 0)
										PrintWarning("Count must be greater than 0!");
									else
										break;
								}
								var menuItemDto = allMenuItems[index - 1];
								orderItems.Add((menuItemDto, countItem));
								PrintSuccess("Item added to order.");
							}
							catch (Exception ex)
							{
								PrintException(ex);
								break;
							}
						}
						break;
					case "2":
						Console.Clear();
						int countToRemove = 1;
						var allOrders = os.GetOrders().Result;
						if (allOrders.Count == 0)
						{
							PrintWarning("No orders found!");
							break;
						}

						PrintHeader("ALL ORDERS");
						foreach (var item in allOrders)
							Console.WriteLine($"{countToRemove++,2}. {item}");
						try
						{
							Console.Write("Choose order to cancel -> ");
							int indexToRemove = int.Parse(Console.ReadLine());
							if (indexToRemove < 1 || indexToRemove > allOrders.Count)
								throw new MenuItemWrongValue("Invalid selection!");

							var orderToRemove = allOrders[indexToRemove - 1];
							os.RemoveOrder(orderToRemove).Wait();
							PrintSuccess("Order canceled successfully.");
						}
						catch (Exception ex)
						{
							PrintException(ex);
							break;
						}
						break;
					case "3":
						Console.Clear();
						var allOrdersToShow = os.GetOrders().Result;
						if (allOrdersToShow.Count == 0)
						{
							PrintWarning("No orders found!");
							break;
						}
						PrintHeader("ORDERS FOUND");
						foreach (var item in allOrdersToShow)
						{
							Console.WriteLine(item);
							item.OrderItems.ForEach(oi => Console.WriteLine($"   - {oi}"));
						}
						break;
					case "4":
						Console.Clear();
						try
						{
							Console.Write("Start date -> ");
							DateTime date1 = DateTime.Parse(Console.ReadLine());
							Console.Write("Last date -> ");
							DateTime date2 = DateTime.Parse(Console.ReadLine());

							var orders = os.GetOrdersByDatesInterval(date1, date2).Result;
							if (orders.Count == 0)
							{
								PrintWarning("No orders found!");
								break;
							}
							PrintHeader("ORDERS FOUND");
							foreach (var item in orders)
							{
								Console.WriteLine(item);
								item.OrderItems.ForEach(oi => Console.WriteLine($"   - {oi}"));
							}
						}
						catch (Exception ex)
						{
							PrintException(ex);
							break;
						}
						break;
					case "5":
						Console.Clear();
						try
						{
							Console.Write("Start price -> ");
							decimal price1 = decimal.Parse(Console.ReadLine().Replace('.', ','));
							while (price1 < 0)
							{
								PrintWarning("Price must be more than 0!");
								Console.Write("Start price -> ");
								price1 = decimal.Parse(Console.ReadLine().Replace('.', ','));
							}

							Console.Write("Last price -> ");
							decimal price2 = decimal.Parse(Console.ReadLine().Replace('.', ','));
							while (price2 < 0)
							{
								PrintWarning("Price must be more than 0!");
								Console.Write("Last price -> ");
								price2 = decimal.Parse(Console.ReadLine().Replace('.', ','));
							}

							if (price1 > price2)
							{
								var temp = price1;
								price1 = price2;
								price2 = temp;
							}

							var ordersByPrice = os.GetOrdersByPriceInterval(price1, price2).Result;
							if (ordersByPrice.Count == 0)
							{
								PrintWarning("No orders found!");
								break;
							}
							PrintHeader("ORDERS FOUND");
							foreach (var item in ordersByPrice)
							{
								Console.WriteLine(item);
								item.OrderItems.ForEach(oi => Console.WriteLine($"   - {oi}"));
							}
						}
						catch (Exception ex)
						{
							PrintException(ex);
							break;
						}
						break;
					case "6":
						Console.Clear();
						try
						{
							Console.Write("Enter date -> ");
							DateTime dateToSearch = DateTime.Parse(Console.ReadLine());
							var ordersByDate = os.GetOrdersByDate(dateToSearch).Result;
							if (ordersByDate.Count == 0)
							{
								PrintWarning("No orders found!");
								break;
							}
							PrintHeader("ORDERS FOUND");
							foreach (var item in ordersByDate)
							{
								Console.WriteLine(item);
								item.OrderItems.ForEach(oi => Console.WriteLine($"   - {oi}"));
							}
						}
						catch (Exception ex)
						{
							PrintException(ex);
							break;
						}
						break;
					case "7":
						Console.Clear();
						var allOrdersToSearch = os.GetOrders().Result;
						if (allOrdersToSearch.Count == 0)
						{
							PrintWarning("No orders found!");
							break;
						}

						int countToSearch = 1;
						PrintHeader("ALL ORDERS");
						foreach (var item in allOrdersToSearch)
							Console.WriteLine($"{countToSearch++,2}. {item}");
						try
						{
							Console.Write("Choose order to view -> ");
							int indexToSearch = int.Parse(Console.ReadLine());
							if (indexToSearch < 1 || indexToSearch > allOrdersToSearch.Count)
								throw new MenuItemWrongValue("Invalid selection!");

							var orderToSearch = allOrdersToSearch[indexToSearch - 1];
							var order = os.GetOrderByNo(orderToSearch).Result;
							PrintHeader("ORDER DETAILS");
							Console.WriteLine(order);
							order.OrderItems.ForEach(oi => Console.WriteLine($"   - {oi}"));
						}
						catch (Exception ex)
						{
							PrintException(ex);
							break;
						}
						break;
					case "0":
						return;
					default:
						PrintWarning("Wrong operation!");
						break;
				}
			}
		}

		private static void PrintHeader(string title)
		{
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine($"\n==== {title} ====");
			Console.ResetColor();
		}

		private static void PrintSuccess(string message)
		{
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine(message);
			Console.ResetColor();
		}

		private static void PrintWarning(string message)
		{
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine(message);
			Console.ResetColor();
		}

		private static void PrintException(Exception ex)
		{
			if (ex is AggregateException aggregateException)
			{
				var flattened = aggregateException.Flatten();
				if (flattened.InnerExceptions.Count > 0)
				{
					PrintWarning(flattened.InnerExceptions[0].Message);
					return;
				}
			}

			PrintWarning(ex.GetBaseException().Message);
		}
	}
}
