namespace MySurveys.Services.Contracts
{
    using System.Linq;
    using Models;

    public interface IUserService : IService
    {
        IQueryable<User> GetAll();

        User GetById(int id);

        User Add(User user);
    }
}
