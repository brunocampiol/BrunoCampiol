using BrunoCampiol.Application.ViewModels;

namespace BrunoCampiol.Application.Interfaces
{
    public interface IVisitorAppService
    {
        void HandleVisitor(VisitorViewModel visitor);
        Task HandleVisitorAsync(VisitorViewModel visitor);
    }
}