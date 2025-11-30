using AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {

        // Category mappings - från Entity till DTO (för responses)
        CreateMap<Category, CategoryResponseDto>()
            .ForMember(dest => dest.Children,
                opt => opt.MapFrom(src => src.Children ?? new List<Category>()));

        // Category mappings - från Request DTO till Entity
        CreateMap<CategoryDto, Category>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.Children, opt => opt.MapFrom(src => new List<Category>()))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true))
            .ForMember(dest => dest.SortOrder, opt => opt.MapFrom(src => 0))
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Parent, opt => opt.Ignore())
            .ForMember(dest => dest.Products, opt => opt.Ignore());



        //mapping for product
        CreateMap<Product, AddProductDto>();
        CreateMap<AddProductDto, Product>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());


        //  CreateMap<Product, UpdateProductDto>();
        //  CreateMap<UpdateProductDto, Product>()
        //  .ForMember(dest => dest.Id, opt => opt.Ignore());   

        CreateMap<Product, ProductResponseDto>()
        .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
        CreateMap<Product, ProductResponseDto>();  
    }


}