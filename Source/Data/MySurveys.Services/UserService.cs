namespace MySurveys.Services
{
    using System.Linq;
    using Contracts;
    using Data.Repository;
    using Models;

    public class UserService : IUserService
    {
        private IRepository<User> users;

        public UserService(IRepository<User> users)
        {
            this.users = users;
        }

        public IQueryable<User> GetAll()
        {
            return this.users.All();
        }

        public User GetById(int id)
        {
            return this.users.GetById(id);
        }

        public User Add(User user)
        {
            this.users.Add(user);
            this.users.SaveChanges();

            return user;
        }
    }
}
