using AutoMapper;
using BrunoCampiol.Application.Interfaces;
using BrunoCampiol.Application.ViewModels;
using BrunoCampiol.Domain.Interfaces;
using BrunoCampiol.Infra.Data.Models;

namespace BrunoCampiol.Application.Services
{
    public class VisitorAppService : IVisitorAppService
    {
        private readonly IMapper _mapper;
        private readonly IVisitorService _visitorService;

        public VisitorAppService(IMapper mapper, IVisitorService visitorService)
        {
            if (visitorService == null) throw new ArgumentNullException(nameof(visitorService));
            if (mapper == null) throw new ArgumentNullException(nameof(mapper));

            _visitorService = visitorService;
            _mapper = mapper;
        }

        public ICollection<VisitorViewModel> GetPagedVisitors(int page, int pageSize)
        {
            if (page <= 0) throw new ArgumentOutOfRangeException(nameof(page));
            if (pageSize <= 0) throw new ArgumentOutOfRangeException(nameof(pageSize));

            var lastVisitors = _visitorService.GetPagedVisitors(page, pageSize);
            var visitors = _mapper.Map<ICollection<VisitorViewModel>>(lastVisitors);

            return visitors;
        }

        public void HandleVisitor(VisitorViewModel visitorViewModel)
        {
            // TODO: add guard clauses & validation
            if (visitorViewModel == null) throw new ArgumentNullException(nameof(visitorViewModel));

            var visitor = _mapper.Map<VISITORS>(visitorViewModel);

            _visitorService.HandleVisitor(visitor);
        }

        public async Task HandleVisitorAsync(VisitorViewModel visitorViewModel)
        {
            // TODO: add guard clauses & validation
            if (visitorViewModel == null) throw new ArgumentNullException(nameof(visitorViewModel));

            var visitor = _mapper.Map<VISITORS>(visitorViewModel);

            await _visitorService.HandleVisitorAsync(visitor);
        }
    }
}