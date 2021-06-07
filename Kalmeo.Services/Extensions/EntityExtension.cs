using AutoMapper;
using Kalmeo.Models;
using Kalmeo.ViewModels;

namespace Kalmeo.Services.Extensions
{
    public static class EntityExtension
    {
        public static Product ToModel(this ProductViewModel viewModel, IMapper mapper)
        {
            return mapper.Map<Product>(viewModel);
        }
    }
}
