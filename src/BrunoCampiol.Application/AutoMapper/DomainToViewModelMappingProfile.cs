using AutoMapper;
using BrunoCampiol.Application.ViewModels;
using BrunoCampiol.Infra.Data.Models;

namespace BrunoCampiol.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<VISITORS, VisitorViewModel>();
        }
    }
}