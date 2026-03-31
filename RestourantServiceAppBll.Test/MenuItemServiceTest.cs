using AutoMapper;
using Moq;
using MockQueryable.Moq;
using RestourantServiceApp.BLogicLayer.Dtos.MenuItemDtos;
using RestourantServiceApp.BLogicLayer.Exceptions;
using RestourantServiceApp.BLogicLayer.Services;
using RestourantServiceApp.Core.Enums;
using RestourantServiceApp.Core.Models;
using RestourantServiceApp.DataAccsessLayer.Interfaces;
using System.Linq.Expressions;

namespace RestourantServiceAppBll.Test
{
    public class MenuItemServiceTest
    {
        private readonly Mock<IRepository<MenuItem>> _mockRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly MenuItemService _menuItemService;

        public MenuItemServiceTest()
        {
            _mockRepository = new Mock<IRepository<MenuItem>>();
            _mockMapper = new Mock<IMapper>();
            
            _menuItemService = new MenuItemService(_mockRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task GetMenuItems_ReturnsAllItems()
        {
            // Arrange
            var menuItems = new List<MenuItem>
            {
                new MenuItem { Id = Guid.NewGuid(), Name = "Burger", Price = 5, Category = Category.MainCourse },
                new MenuItem { Id = Guid.NewGuid(), Name = "Coke", Price = 2, Category = Category.Dessert }
            }.AsQueryable().BuildMock();

            _mockRepository.Setup(r => r.GetAll(false, null, null, null)).Returns(menuItems);

            var dtos = new List<MenuItemReturnDto>
            {
                new MenuItemReturnDto { Name = "Burger", Price = 5, Category = Category.MainCourse },
                new MenuItemReturnDto { Name = "Coke", Price = 2, Category = Category.Dessert }
            };

            _mockMapper.Setup(m => m.Map<List<MenuItemReturnDto>>(It.IsAny<List<MenuItem>>()))
                       .Returns(dtos);

            // Act
            var result = await _menuItemService.GetMenuItems();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task AddMenuItem_SuccessfullyAddsItem()
        {
            // Arrange
            var createDto = new MenuItemCreateDto { Name = "Salad", Price = 4, Category = Category.Dessert };
            var menuItem = new MenuItem { Name = "Salad", Price = 4, Category = Category.Dessert };

            _mockMapper.Setup(m => m.Map<MenuItem>(createDto)).Returns(menuItem);

            // Act
            await _menuItemService.AddMenuItem(createDto);

            // Assert
            _mockRepository.Verify(r => r.AddAsync(menuItem), Times.Once);
            _mockRepository.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task RemoveMenuItem_ThrowsMenuItemNotFound_WhenItemDoesNotExist()
        {
            // Arrange
            var dto = new MenuItemReturnDto { Name = "Unknown" };
            var menuItems = new List<MenuItem>().AsQueryable().BuildMock();
            
            _mockRepository.Setup(r => r.GetAll(true, It.IsAny<Expression<Func<MenuItem, bool>>>(), null, null))
                           .Returns(menuItems);

            // Act & Assert
            await Assert.ThrowsAsync<MenuItemNotFound>(() => _menuItemService.RemoveMenuItem(dto));
        }

        [Fact]
        public async Task RemoveMenuItem_SuccessfullyRemovesItem()
        {
            // Arrange
            var dto = new MenuItemReturnDto { Name = "Burger" };
            var menuItem = new MenuItem { Id = Guid.NewGuid(), Name = "Burger", Price = 5 };
            var menuItems = new List<MenuItem> { menuItem }.AsQueryable().BuildMock();
            
            _mockRepository.Setup(r => r.GetAll(true, It.IsAny<Expression<Func<MenuItem, bool>>>(), null, null))
                           .Returns(menuItems);

            // Act
            await _menuItemService.RemoveMenuItem(dto);

            // Assert
            _mockRepository.Verify(r => r.Delete(menuItem), Times.Once);
            _mockRepository.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task EditMenuItem_UpdatesNameAndPrice()
        {
            // Arrange
            var menuItem = new MenuItem { Id = Guid.NewGuid(), Name = "OldName", Price = 10 };
            var menuItems = new List<MenuItem> { menuItem }.AsQueryable().BuildMock();

            _mockRepository.Setup(r => r.GetAll(true, It.IsAny<Expression<Func<MenuItem, bool>>>(), null, null))
                           .Returns(menuItems);

            var dto = new MenuItemReturnDto { Name = "OldName" };

            // Act
            await _menuItemService.EditMenuItem(dto, "NewName", 15);

            // Assert
            Assert.Equal("NewName", menuItem.Name);
            Assert.Equal(15, menuItem.Price);
            _mockRepository.Verify(r => r.Update(menuItem), Times.Once);
            _mockRepository.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task EditMenuItem_ThrowsMenuItemWrongValue_WhenNoChangesDetected()
        {
            // Arrange
            var menuItem = new MenuItem { Id = Guid.NewGuid(), Name = "Burger", Price = 5 };
            var menuItems = new List<MenuItem> { menuItem }.AsQueryable().BuildMock();

            _mockRepository.Setup(r => r.GetAll(true, It.IsAny<Expression<Func<MenuItem, bool>>>(), null, null))
                           .Returns(menuItems);

            var dto = new MenuItemReturnDto { Name = "Burger" };

            // Act & Assert
            await Assert.ThrowsAsync<MenuItemWrongValue>(() => _menuItemService.EditMenuItem(dto, "Burger", 5));
        }

        [Fact]
        public async Task GetMenuItemsByCategory_ReturnsFilteredItems()
        {
            // Arrange
            var menuItems = new List<MenuItem>
            {
                new MenuItem { Id = Guid.NewGuid(), Name = "Coke", Price = 2, Category = Category.Dessert }
            }.AsQueryable().BuildMock();

            _mockRepository.Setup(r => r.GetAll(false, It.IsAny<Expression<Func<MenuItem, bool>>>(), null, null))
                           .Returns(menuItems);

            var dtos = new List<MenuItemReturnDto>
            {
                new MenuItemReturnDto { Name = "Coke", Price = 2, Category = Category.Dessert }
            };

            _mockMapper.Setup(m => m.Map<List<MenuItemReturnDto>>(It.IsAny<List<MenuItem>>()))
                       .Returns(dtos);

            // Act
            var result = await _menuItemService.GetMenuItemsByCategory(Category.Dessert);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal("Coke", result[0].Name);
        }

        [Fact]
        public async Task GetMenuItemsInRange_ReturnsItemsInRange()
        {
            // Arrange
            var menuItems = new List<MenuItem>
            {
                new MenuItem { Id = Guid.NewGuid(), Name = "Burger", Price = 5, Category = Category.MainCourse }
            }.AsQueryable().BuildMock();

            _mockRepository.Setup(r => r.GetAll(false, It.IsAny<Expression<Func<MenuItem, bool>>>(), null, null))
                           .Returns(menuItems);

            var dtos = new List<MenuItemReturnDto>
            {
                new MenuItemReturnDto { Name = "Burger", Price = 5, Category = Category.MainCourse }
            };

            _mockMapper.Setup(m => m.Map<List<MenuItemReturnDto>>(It.IsAny<List<MenuItem>>()))
                       .Returns(dtos);

            // Act
            var result = await _menuItemService.GetMenuItemsInRange(4, 10);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal("Burger", result[0].Name);
        }

        [Fact]
        public async Task GetMenuItemsBySearch_ReturnsMatchingItems()
        {
            // Arrange
            var menuItems = new List<MenuItem>
            {
                new MenuItem { Id = Guid.NewGuid(), Name = "Burger", Price = 5, Category = Category.MainCourse }
            }.AsQueryable().BuildMock();

            _mockRepository.Setup(r => r.GetAll(false, It.IsAny<Expression<Func<MenuItem, bool>>>(), null, null))
                           .Returns(menuItems);

            var dtos = new List<MenuItemReturnDto>
            {
                new MenuItemReturnDto { Name = "Burger", Price = 5, Category = Category.MainCourse }
            };

            _mockMapper.Setup(m => m.Map<List<MenuItemReturnDto>>(It.IsAny<List<MenuItem>>()))
                       .Returns(dtos);

            // Act
            var result = await _menuItemService.GetMenuItemsBySearch("Burger");

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal("Burger", result[0].Name);
        }
    }
}
