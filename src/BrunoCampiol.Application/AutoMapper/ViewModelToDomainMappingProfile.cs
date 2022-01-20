using AutoMapper;
using BrunoCampiol.Application.ViewModels;
using BrunoCampiol.Infra.Data.Models;

namespace BrunoCampiol.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<VisitorViewModel, VISITORS>()
                .AfterMap((src, dst) =>
                    {
                        dst.CLIENT_BROWSER = src.ClientBrowser;
                        dst.CLIENT_HEADERS = src.ClientHeaders;
                        dst.CLIENT_OS = src.ClientOS;
                        dst.CLIENT_USER_AGENT = src.ClientUserAgent;
                        dst.CREATED_ON_UTC = src.CreatedUtc;
                    });

            //CreateMap<CustomerViewModel, RegisterNewCustomerCommand>()
            //    .ConstructUsing(c => new RegisterNewCustomerCommand(c.Name, c.Email, c.BirthDate));
            //CreateMap<CustomerViewModel, UpdateCustomerCommand>()
            //    .ConstructUsing(c => new UpdateCustomerCommand(c.Id, c.Name, c.Email, c.BirthDate));
        }
    }
}