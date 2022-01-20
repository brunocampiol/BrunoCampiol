using AutoMapper;
using BrunoCampiol.Application.AutoMapper;

namespace BrunoCampiol.Unit.Test.Base
{
    public abstract class BaseUnitTest
    {
        protected readonly IMapper _mapper;

        public BaseUnitTest()
        {
            if (_mapper == null)
            {
                // Same config as in Web csproj
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new DomainToViewModelMappingProfile());
                    mc.AddProfile(new ViewModelToDomainMappingProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
        }
    }
}
