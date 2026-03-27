using AutoMapper;
using RestourantServiceApp.BLogicLayer.Dtos.MenuItemDtos;
using RestourantServiceApp.BLogicLayer.Dtos.OrderDtos;
using RestourantServiceApp.Core.Models;

namespace RestourantServiceApp.BLogicLayer.Mappers
{
	public class MapProfile : Profile
	{
		public MapProfile()
		{

			CreateMap<MenuItemReturnDto, MenuItem>();
			CreateMap<MenuItem, MenuItemReturnDto>();

			CreateMap<MenuItemCreateDto, MenuItem>();
			CreateMap<MenuItem, MenuItemCreateDto>();

			CreateMap<OrderItem, OrderItemReturnDto>()
				.ForMember(dest => dest.MenuItemName, opt => opt.MapFrom(src => src.MenuItem.Name))
				.ForMember(dest => dest.MenuItemPrice, opt => opt.MapFrom(src => src.MenuItem.Price));

			CreateMap<Order, OrderReturnDto>();
		}
	}
}
