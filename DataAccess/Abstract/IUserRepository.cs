using BlogApp.Entities;

namespace BlogApp.DataAccess.Abstract
{
    public interface IUserRepository
    {
        IQueryable<User> Users { get; }
        void CreateUser(User user); 
    }
}
