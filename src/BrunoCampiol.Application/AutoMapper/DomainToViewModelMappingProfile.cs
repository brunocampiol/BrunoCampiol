using AutoMapper;
using BrunoCampiol.Application.ViewModels;
using BrunoCampiol.Infra.Data.Models;

namespace BrunoCampiol.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<VISITORS, VisitorViewModel>()
                .AfterMap((src, dst) =>
                {
                    dst.ClientBrowser = src.CLIENT_BROWSER;
                    dst.ClientHeaders = src.CLIENT_HEADERS;
                    dst.ClientOS = src.CLIENT_OS;
                    dst.ClientUserAgent = src.CLIENT_USER_AGENT;
                    dst.CreatedUtc = src.CREATED_ON_UTC;
                });
        }
    }
}