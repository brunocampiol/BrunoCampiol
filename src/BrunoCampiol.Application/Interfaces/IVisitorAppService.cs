using BrunoCampiol.Application.ViewModels;

namespace BrunoCampiol.Application.Interfaces
{
    public interface IVisitorAppService
    {
        ICollection<VisitorViewModel> GetPagedVisitors(int page, int pageSize);
        void HandleVisitor(VisitorViewModel visitor);
        Task HandleVisitorAsync(VisitorViewModel visitor);
    }
}