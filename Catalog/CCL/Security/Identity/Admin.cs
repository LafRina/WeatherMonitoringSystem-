namespace Security.Identity
{
    public class Admin : UserBase
    {
        public Admin(int userId, string name) 
            : base(userId, name, nameof(Admin))
        {
        }
    }
}