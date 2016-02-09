namespace MySurveys.Services.Contracts
{
    using System.Linq;
    using Models;

    public interface IUserService : IService
    {
        IQueryable<User> GetAll();

        User GetById(string id);

        User Update(User user);

        void Delete(string id);
    }
}
