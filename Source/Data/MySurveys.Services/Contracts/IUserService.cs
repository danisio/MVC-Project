namespace MySurveys.Services.Contracts
{
    using System.Linq;
    using Models;

    public interface IUserService : IService
    {
        IQueryable<User> GetAll();

        User GetById(object id);

        User Update(User user);

        void Delete(object id);
    }
}
