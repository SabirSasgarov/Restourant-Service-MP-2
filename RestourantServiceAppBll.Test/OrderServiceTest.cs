using Microsoft.EntityFrameworkCore.Query;
using MockQueryable.Moq;
using Moq;
using RestourantServiceApp.BLogicLayer.Dtos.MenuItemDtos;
using RestourantServiceApp.BLogicLayer.Dtos.OrderDtos;
using RestourantServiceApp.BLogicLayer.Exceptions;
using RestourantServiceApp.BLogicLayer.Services;
using RestourantServiceApp.Core.Models;
using RestourantServiceApp.DataAccsessLayer.Interfaces;
using AutoMapper;
using System.Linq.Expressions;

namespace RestourantServiceAppBll.Test
{
    public class OrderServiceTest
    {
        private readonly Mock<IRepository<Order>> _mockOrderRepository;
        private readonly Mock<IRepository<MenuItem>> _mockMenuItemRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly OrderService _orderService;

        public OrderServiceTest()
        {
            _mockOrderRepository = new Mock<IRepository<Order>>();
            _mockMenuItemRepository = new Mock<IRepository<MenuItem>>();
            _mockMapper = new Mock<IMapper>();
            
            _orderService = new OrderService(_mockOrderRepository.Object, _mockMenuItemRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task GetOrders_ReturnsAllOrders()
        {
            // Arrange
            var orders = new List<Order>
            {
                new Order { Id = Guid.NewGuid(), TotalAmount = 10, Date = DateTime.Now },
                new Order { Id = Guid.NewGuid(), TotalAmount = 20, Date = DateTime.Now.AddDays(-1) }
            }.AsQueryable().BuildMock();

            _mockOrderRepository.Setup(r => r.GetAll(false, null, It.IsAny<Func<IQueryable<Order>, IIncludableQueryable<Order, object>>>(), It.IsAny<Func<IQueryable<Order>, IOrderedQueryable<Order>>>()))
                                .Returns(orders);

            var dtos = new List<OrderReturnDto>
            {
                new OrderReturnDto { TotalAmount = 10 },
                new OrderReturnDto { TotalAmount = 20 }
            };

            _mockMapper.Setup(m => m.Map<List<OrderReturnDto>>(It.IsAny<List<Order>>())).Returns(dtos);

            // Act
            var result = await _orderService.GetOrders();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task AddOrder_ThrowsException_WhenOrderItemsEmpty()
        {
            // Act & Assert
            await Assert.ThrowsAsync<MenuItemWrongValue>(() => _orderService.AddOrder(new List<(MenuItemReturnDto, int)>()));
        }

        [Fact]
        public async Task GetOrdersByPriceInterval_ReturnsFilteredOrders()
        {
            // Arrange
            var orders = new List<Order>
            {
                new Order { Id = Guid.NewGuid(), TotalAmount = 15 }
            }.AsQueryable().BuildMock();

            _mockOrderRepository.Setup(r => r.GetAll(false, It.IsAny<Expression<Func<Order, bool>>>(), It.IsAny<Func<IQueryable<Order>, IIncludableQueryable<Order, object>>>(), null))
                                .Returns(orders);

            var dtos = new List<OrderReturnDto>
            {
                new OrderReturnDto { TotalAmount = 15 }
            };

            _mockMapper.Setup(m => m.Map<List<OrderReturnDto>>(It.IsAny<List<Order>>())).Returns(dtos);

            // Act
            var result = await _orderService.GetOrdersByPriceInterval(10, 20);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(15, result[0].TotalAmount);
        }

        [Fact]
        public async Task GetOrdersByPriceInterval_ThrowsOrderNotFound_WhenNoOrdersFound()
        {
            // Arrange
            var orders = new List<Order>().AsQueryable().BuildMock();

            _mockOrderRepository.Setup(r => r.GetAll(false, It.IsAny<Expression<Func<Order, bool>>>(), It.IsAny<Func<IQueryable<Order>, IIncludableQueryable<Order, object>>>(), null))
                                .Returns(orders);

            // Act & Assert
            await Assert.ThrowsAsync<OrderNotFound>(() => _orderService.GetOrdersByPriceInterval(10, 20));
        }

        [Fact]
        public async Task AddOrder_SuccessfullyAddsOrder()
        {
            // Arrange
            var menuItem = new MenuItem { Id = Guid.NewGuid(), Name = "Burger", Price = 5 };
            var menuItems = new List<MenuItem> { menuItem }.AsQueryable().BuildMock();
            
            _mockMenuItemRepository.Setup(r => r.GetAll(false, It.IsAny<Expression<Func<MenuItem, bool>>>(), null, null))
                                   .Returns(menuItems);

            var orderItems = new List<(MenuItemReturnDto, int)>
            {
                (new MenuItemReturnDto { Name = "Burger" }, 2)
            };

            // Act
            await _orderService.AddOrder(orderItems);

            // Assert
            _mockOrderRepository.Verify(r => r.AddAsync(It.Is<Order>(o => o.TotalAmount == 10 && o.OrderItems.Count == 1)), Times.Once);
            _mockOrderRepository.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task AddOrder_ThrowsMenuItemNotFound_WhenItemDoesNotExist()
        {
            // Arrange
            var menuItems = new List<MenuItem>().AsQueryable().BuildMock();
            
            _mockMenuItemRepository.Setup(r => r.GetAll(false, It.IsAny<Expression<Func<MenuItem, bool>>>(), null, null))
                                   .Returns(menuItems);

            var orderItems = new List<(MenuItemReturnDto, int)>
            {
                (new MenuItemReturnDto { Name = "Unknown" }, 1)
            };

            // Act & Assert
            await Assert.ThrowsAsync<MenuItemNotFound>(() => _orderService.AddOrder(orderItems));
        }

        [Fact]
        public async Task GetOrdersByDatesInterval_ReturnsFilteredOrders()
        {
            // Arrange
            var orders = new List<Order>
            {
                new Order { Id = Guid.NewGuid(), TotalAmount = 15, Date = DateTime.Now }
            }.AsQueryable().BuildMock();

            _mockOrderRepository.Setup(r => r.GetAll(false, It.IsAny<Expression<Func<Order, bool>>>(), It.IsAny<Func<IQueryable<Order>, IIncludableQueryable<Order, object>>>(), null))
                                .Returns(orders);

            var dtos = new List<OrderReturnDto>
            {
                new OrderReturnDto { TotalAmount = 15 }
            };

            _mockMapper.Setup(m => m.Map<List<OrderReturnDto>>(It.IsAny<List<Order>>())).Returns(dtos);

            // Act
            var result = await _orderService.GetOrdersByDatesInterval(DateTime.Now.AddDays(-1), DateTime.Now.AddDays(1));

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
        }

        [Fact]
        public async Task GetOrdersByDatesInterval_ThrowsOrderNotFound_WhenNoOrdersFound()
        {
            // Arrange
            var orders = new List<Order>().AsQueryable().BuildMock();

            _mockOrderRepository.Setup(r => r.GetAll(false, It.IsAny<Expression<Func<Order, bool>>>(), It.IsAny<Func<IQueryable<Order>, IIncludableQueryable<Order, object>>>(), null))
                                .Returns(orders);

            // Act & Assert
            await Assert.ThrowsAsync<OrderNotFound>(() => _orderService.GetOrdersByDatesInterval(DateTime.Now.AddDays(-1), DateTime.Now.AddDays(1)));
        }

        [Fact]
        public async Task GetOrdersByDate_ReturnsFilteredOrders()
        {
            // Arrange
            var orders = new List<Order>
            {
                new Order { Id = Guid.NewGuid(), TotalAmount = 15, Date = DateTime.Now }
            }.AsQueryable().BuildMock();

            _mockOrderRepository.Setup(r => r.GetAll(false, It.IsAny<Expression<Func<Order, bool>>>(), It.IsAny<Func<IQueryable<Order>, IIncludableQueryable<Order, object>>>(), null))
                                .Returns(orders);

            var dtos = new List<OrderReturnDto>
            {
                new OrderReturnDto { TotalAmount = 15 }
            };

            _mockMapper.Setup(m => m.Map<List<OrderReturnDto>>(It.IsAny<List<Order>>())).Returns(dtos);

            // Act
            var result = await _orderService.GetOrdersByDate(DateTime.Now);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
        }

        [Fact]
        public async Task GetOrdersByDate_ThrowsOrderNotFound_WhenNoOrdersFound()
        {
            // Arrange
            var orders = new List<Order>().AsQueryable().BuildMock();

            _mockOrderRepository.Setup(r => r.GetAll(false, It.IsAny<Expression<Func<Order, bool>>>(), It.IsAny<Func<IQueryable<Order>, IIncludableQueryable<Order, object>>>(), null))
                                .Returns(orders);

            // Act & Assert
            await Assert.ThrowsAsync<OrderNotFound>(() => _orderService.GetOrdersByDate(DateTime.Now));
        }

        [Fact]
        public async Task RemoveOrder_SuccessfullyRemovesOrder()
        {
            // Arrange
            var date = DateTime.Now;
            var menuItem = new MenuItem { Name = "Burger", Price = 5 };
            var orderItem = new OrderItem { MenuItem = menuItem, Count = 2 };
            var order = new Order { Id = Guid.NewGuid(), Date = date, TotalAmount = 10, OrderItems = new List<OrderItem> { orderItem } };
            var orders = new List<Order> { order }.AsQueryable().BuildMock();
            
            _mockOrderRepository.Setup(r => r.GetAll(true, It.IsAny<Expression<Func<Order, bool>>>(), It.IsAny<Func<IQueryable<Order>, IIncludableQueryable<Order, object>>>(), null))
                                .Returns(orders);

            var dtosOi = new List<OrderItemReturnDto> { new OrderItemReturnDto { MenuItemName = "Burger", MenuItemPrice = 5, Count = 2 } };
            var orderDto = new OrderReturnDto { Date = date, TotalAmount = 10, OrderItems = dtosOi };

            // Act
            await _orderService.RemoveOrder(orderDto);

            // Assert
            _mockOrderRepository.Verify(r => r.Delete(order), Times.Once);
            _mockOrderRepository.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task GetOrderByNo_ReturnsOrder()
        {
            // Arrange
            var date = DateTime.Now;
            var menuItem = new MenuItem { Name = "Burger", Price = 5 };
            var orderItem = new OrderItem { MenuItem = menuItem, Count = 2 };
            var order = new Order { Id = Guid.NewGuid(), Date = date, TotalAmount = 10, OrderItems = new List<OrderItem> { orderItem } };
            var orders = new List<Order> { order }.AsQueryable().BuildMock();
            
            _mockOrderRepository.Setup(r => r.GetAll(false, It.IsAny<Expression<Func<Order, bool>>>(), It.IsAny<Func<IQueryable<Order>, IIncludableQueryable<Order, object>>>(), null))
                                .Returns(orders);

            var dtosOi = new List<OrderItemReturnDto> { new OrderItemReturnDto { MenuItemName = "Burger", MenuItemPrice = 5, Count = 2 } };
            var orderDto = new OrderReturnDto { Date = date, TotalAmount = 10, OrderItems = dtosOi };

            _mockMapper.Setup(m => m.Map<OrderReturnDto>(It.IsAny<Order>())).Returns(orderDto);

            // Act
            var result = await _orderService.GetOrderByNo(orderDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(10, result.TotalAmount);
        }
    }
}
