namespace MySurveys.Services.Contracts
{
    using System.Linq;
    using Models;

    public interface IResponseService : IService
    {
        IQueryable<Response> GetAll();

        Response GetById(int id);

        Response Add(Response response);

        Response Update(Response response);

        void Delete(object id);
    }
}
