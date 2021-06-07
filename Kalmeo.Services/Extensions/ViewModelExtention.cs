using AutoMapper;
using Kalmeo.Models;
using Kalmeo.ViewModels;

namespace Kalmeo.Services.Extensions
{
    public static class ViewModelExtention
    {
        public static ProductViewModel ToViewModel(this Product model, IMapper mapper)
        {
            return mapper.Map<ProductViewModel>(model);
        }
    }
}
