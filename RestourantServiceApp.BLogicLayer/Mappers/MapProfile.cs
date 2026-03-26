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

			CreateMap<Order, OrderReturnDto>();
		}
	}
}
