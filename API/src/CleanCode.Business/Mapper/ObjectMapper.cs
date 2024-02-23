using AutoMapper;
using CleanCode.Business.Models;
using CleanCode.Core.Entities;
using CleanCode.Core.Models;

namespace CleanCode.Business.Mapper
{
    // The best implementation of AutoMapper for class libraries -> https://www.abhith.net/blog/using-automapper-in-a-net-core-class-library/
    public static class ObjectMapper
    {
        private static readonly Lazy<IMapper> Lazy = new(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                // This line ensures that internal properties are also mapped over.
                cfg.ShouldMapProperty = p => p.GetMethod != null && (p.GetMethod.IsPublic || p.GetMethod.IsAssembly);
                cfg.AddProfile<BNMSDtoMapper>();
            });
            var mapper = config.CreateMapper();
            return mapper;
        });

        public static IMapper Mapper => Lazy.Value;
    }

    public class CleanCodeDtoMapper : Profile
    {
        public CleanCodeDtoMapper()
        {
            CreateMap(typeof(PagedList<>), typeof(PagedList<>)).ConvertUsing(typeof(PagedListConverter<,>));
            //CreateMap<Product, ProductModel>().ReverseMap();
            //CreateMap<Category, CategoryModel>().ReverseMap();
            //CreateMap<Role, RoleModel>().ReverseMap();
            //CreateMap<User, UserModel>().ReverseMap();
            

            //<Do not Delete this line>
        }
    }

    public class PagedListConverter<TSource, TDestination> : ITypeConverter<PagedList<TSource>, PagedList<TDestination>>
    {
        public PagedList<TDestination> Convert(PagedList<TSource> source, PagedList<TDestination> destination,
            ResolutionContext context)
        {
            destination ??= new PagedList<TDestination>();
            destination.AddRange(source.Select(item => context.Mapper.Map<TSource, TDestination>(item)));
            destination.CurrentPage = source.CurrentPage;
            destination.PageSize = source.PageSize;
            destination.TotalPages = source.TotalPages;
            destination.TotalCount = source.TotalCount;
            destination.HasPreviousPage = source.HasPreviousPage;
            destination.HasNextPage = source.HasNextPage;
            return destination;
        }
    }
}
