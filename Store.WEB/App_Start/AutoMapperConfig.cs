using System;
using AutoMapper;
using Store.BLL.DTO;
using Store.DAL.Entities;
using Store.WEB.Models;
using Store.WEB.Models.GoodViewModels;
using Store.WEB.Models.OrderViewModels;

namespace Store.WEB.App_Start
{
    public class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.CreateMap<Good, GoodViewModel>()
                .ForMember("Category",
                    opt => opt.MapFrom(v => v.Category.Name))
                .ForMember("Color",
                    opt => opt.MapFrom(v => v.Color.Name));

            Mapper.CreateMap<Good, GoodCreateModel>()
                .ForMember("CategoryId",
                    opt => opt.MapFrom(v => v.Category.Id))
                .ForMember("ColorId",
                    opt => opt.MapFrom(v => v.Color.Id));

            Mapper.CreateMap<Good, GoodAdminView>()
                .ForMember("Category",
                    opt => opt.MapFrom(v => v.Category.Name))
                .ForMember("Color",
                    opt => opt.MapFrom(v => v.Color.Name))
                .ForMember("Size",
                    opt => opt.MapFrom(v => string.Format("{0} X {1} X {2}", v.SizeHeight, v.SizeWidth, v.SizeDepth)));

            Mapper.CreateMap<GoodDTO, GoodCreateModel>()
                .ForMember("CategoryId",
                    opt => opt.MapFrom(v => v.Category.Id))
                .ForMember("ColorId",
                    opt => opt.MapFrom(v => v.Color.Id));

            Mapper.CreateMap<GoodDTO, GoodAdminView>()
                .ForMember("Category",
                    opt => opt.MapFrom(v => v.Category.Name))
                .ForMember("Color",
                    opt => opt.MapFrom(v => v.Color.Name))
                .ForMember("Size",
                    opt => opt.MapFrom(v => string.Format("{0} X {1} X {2}", v.SizeHeight, v.SizeWidth, v.SizeDepth)));

            Mapper.CreateMap<GoodDTO, GoodViewModel>()
                .ForMember("Category",
                    opt => opt.MapFrom(v => v.Category.Name))
                .ForMember("Color",
                    opt => opt.MapFrom(v => v.Color.Name));

            Mapper.CreateMap<GoodDTO, GoodCreateModel>()
                .ForMember("CategoryId",
                    opt => opt.MapFrom(v => v.Category.Id))
                .ForMember("ColorId",
                    opt => opt.MapFrom(v => v.Color.Id));

            Mapper.CreateMap<GoodDTO, GoodEditModel>()
                .ForMember("CategoryId",
                    opt => opt.MapFrom(v => v.Category.Id))
                .ForMember("ColorId",
                    opt => opt.MapFrom(v => v.Color.Id));

            Mapper.CreateMap<Order, OrderViewModel>()
                .ForMember("User",
                    opt => opt.MapFrom(v => v.User.Name))
                .ForMember("DateSale",
                    opt => opt.MapFrom(v => v.DateSale.Date.ToShortDateString() == DateTime.Now.ToShortDateString()
                        ? v.DateSale.ToString()
                        : v.DateSale.Date.ToShortDateString()))
                .ForMember("Status",
                    opt => opt.MapFrom(v => v.Status.Name));

            Mapper.CreateMap<OrderDTO, OrderViewModel>()
                .ForMember("User",
                    opt => opt.MapFrom(v => v.User.Name))
                .ForMember("DateSale",
                    opt => opt.MapFrom(v => v.DateSale.Date.ToShortDateString() == DateTime.Now.ToShortDateString()
                        ? v.DateSale.ToString()
                        : v.DateSale.Date.ToShortDateString()))
                .ForMember("Status",
                    opt => opt.MapFrom(v => v.Status.Name));

            Mapper.CreateMap<ClientProfile, UserDTO>()
                .ForMember("Email",
                    opt => opt.MapFrom(c => c.ApplicationUser.Email))
                .ForMember("UserName",
                    opt => opt.MapFrom(c => c.ApplicationUser.UserName));

            Mapper.CreateMap<StatusDTO, Status>();
            Mapper.CreateMap<Status, StatusDTO>();

            Mapper.CreateMap<CategoryDTO, Category>();
            Mapper.CreateMap<Category, CategoryDTO>();

            Mapper.CreateMap<Color, ColorDTO>();
            Mapper.CreateMap<ColorDTO, Color>();

            Mapper.CreateMap<OrderItem, OrderItemDTO>();
            Mapper.CreateMap<OrderItemDTO, OrderItem>();

            Mapper.CreateMap<Order, OrderDTO>();
            Mapper.CreateMap<OrderDTO, Order>();

            Mapper.CreateMap<Good, GoodDTO>();
            Mapper.CreateMap<GoodDTO, Good>();

            Mapper.CreateMap<FilterModel, FilterModelDTO>();
            Mapper.CreateMap<FilterModelDTO, FilterModel>();

            Mapper.CreateMap<UserDTO, ClientProfile>();
            Mapper.CreateMap<ClientProfile, UserDTO>();

            Mapper.CreateMap<Delivery, DeliveryDTO>();
            Mapper.CreateMap<DeliveryDTO, Delivery>();

            //Mapper.CreateMap<GoodCreateModel, Good>()
            //  .ForMember("PriceIncome",
            //      opt => opt.MapFrom(v => new Price { Value = v.PriceIncome }))
            //  .ForMember("PriceSale",
            //      opt => opt.MapFrom(v => new Price { Value = v.PriceSale }))
            //  .ForMember("Category",
            //      opt => opt.MapFrom(v => _categoryLogic.Get(v.CategoryId)))
            //  .ForMember("Color",
            //      opt => opt.MapFrom(v => _colorLogic.Get(v.ColorId)));
        }
    }
}