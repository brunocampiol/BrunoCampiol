using BrunoCampiol.Application.Interfaces;
using BrunoCampiol.Application.ViewModels;
using BrunoCampiol.Domain.Interfaces;
using BrunoCampiol.Infra.Data.Models;

namespace BrunoCampiol.Application.Services
{
    public class VisitorAppService : IVisitorAppService
    {
        private readonly IVisitorService _visitorService;

        public VisitorAppService(IVisitorService visitorService)
        {
            if (visitorService == null) throw new ArgumentNullException(nameof(visitorService));

            _visitorService = visitorService;
        }

        public void HandleVisitor(VisitorViewModel visitor)
        {
            // TODO: add guard clauses & validation
            if (visitor == null) throw new ArgumentNullException(nameof(visitor));


            // TODO: use automapper and real object
            _visitorService.HandleVisitor(new VISITORS());
        }

        public async Task HandleVisitorAsync(VisitorViewModel visitor)
        {
            // TODO: add guard clauses & validation
            if (visitor == null) throw new ArgumentNullException(nameof(visitor));

            // TODO: use automapper and real object
            await _visitorService.HandleVisitorAsync(new VISITORS());
        }
    }
}