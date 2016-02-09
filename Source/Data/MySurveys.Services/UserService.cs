namespace MySurveys.Services
{
    using System;
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

        public User GetById(string id)
        {
            return this.users.GetById(id);
        }

        public User Update(User user)
        {
            this.users.Update(user);
            this.users.SaveChanges();

            return user;
        }

        public void Delete(string id)
        {
            this.users.Delete(id);
            this.users.SaveChanges();
        }
    }
}
