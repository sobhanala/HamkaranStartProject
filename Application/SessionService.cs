using Domain.SharedSevices;
using Domain.Users;

namespace Application
{
    public class SessionService : ISessionService
    {
        private User _currentUser;

        public User CurrentUser => _currentUser;
        public bool IsAuthenticated => _currentUser != null;

        public void Initialize(User user)
        {
            _currentUser = user;
        }

        public void Terminate()
        {
            _currentUser = null;
        }
    }
}