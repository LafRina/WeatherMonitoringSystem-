using Security.Identity;
namespace Security
{
    public static class SecurityContext
    {
        static UserBase _user = null;
        public static UserBase GetUser()
        {
            return _user;
        }
        public static void SetUser(UserBase user)
        {
            _user = user;
        }
    }
}