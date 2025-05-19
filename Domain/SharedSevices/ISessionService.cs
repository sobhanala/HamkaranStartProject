using Domain.Users;

namespace Domain.SharedSevices
{
    public interface ISessionService
    {
        User CurrentUser { get; }
        bool IsAuthenticated { get; }
        void Initialize(User user);
        void Terminate();
    }
}