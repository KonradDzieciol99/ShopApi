using AutoMapper;
using Core.Entities;
using Core.Dtos;
using Core.Models;

namespace ShopApi.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Product, CreateProductDto>();
            CreateMap<CreateProductDto, Product>();
            CreateMap<AppUserAddress, AddressDto>();
            CreateMap<AddressDto, AppUserAddress>();

            CreateMap<AppUser, UserDataDto>();
            CreateMap<UserDataDto, AppUser>();
            CreateMap<RegisterDto, AppUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(source => source.Email))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(source => source.Email));

            CreateMap<Photo, PhotoDto>();
            CreateMap<PhotoDto,Photo>();

            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto,Product>();

            CreateMap<BrandOfProductDto,BrandOfProduct>();
            CreateMap<BrandOfProduct, BrandOfProductDto>();

            CreateMap<CategoryOfProduct, CategoryOfProductDto>();
            CreateMap<CategoryOfProductDto, CategoryOfProduct>();
            CreateMap<OrderAddress, AddressDto>().ReverseMap();
            CreateMap<CustomerBasketDto, CustomerBasket>();
            CreateMap<CustomerBasket, CustomerBasketDto>();
            CreateMap<BasketItemDto, BasketItem>();
            CreateMap<BasketItem, BasketItemDto>();
            CreateMap<AddressDto, OrderAddress>();
            CreateMap<Order, OrderToReturnDto>()
                .ForMember(d => d.DeliveryMethod, o => o.MapFrom(s => s.DeliveryMethod.ShortName))
                .ForMember(d => d.ShippingPrice, o => o.MapFrom(s => s.DeliveryMethod.Price));
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(d => d.ProductId, o => o.MapFrom(s => s.ProductItemId))
                .ForMember(d => d.ProductName, o => o.MapFrom(s => s.ProductName))
                .ForMember(d => d.PictureUrl, o => o.MapFrom(s => s.PictureUrl));
        }
    }
}
