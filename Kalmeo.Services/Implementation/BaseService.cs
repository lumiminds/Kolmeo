using AutoMapper;
using Kalmeo.Repositories.Interfaces;

namespace Kalmeo.Services.Implementation
{
    public abstract class BaseService
    {
        protected readonly IMapper _mapper;
        protected readonly IUnitOfWork _unitOfWork;

        public BaseService(IMapper mapper, IUnitOfWork unitOfWork) 
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
    }
}