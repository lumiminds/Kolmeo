using AutoMapper;
using Kalmeo.Models;
using Kalmeo.ViewModels;

namespace Kalmeo.Services.Mapping
{
    public class KelmeoMapping : Profile
    {
        public KelmeoMapping()
        {
            CreateMap<Product, ProductViewModel>()
                .ReverseMap();
        }
    }
}
