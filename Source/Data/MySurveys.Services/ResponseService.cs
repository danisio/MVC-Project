namespace MySurveys.Services
{
    using System.Linq;
    using Data.Repository;
    using Models;
    using MySurveys.Services.Contracts;

    public class ResponseService : IResponseService
    {
        private IRepository<Response> responses;

        public ResponseService(IRepository<Response> responses)
        {
            this.responses = responses;
        }

        public Response Add(Response response)
        {
            this.responses.Add(response);
            this.responses.SaveChanges();

            return response;
        }

        public Response Update(Response response)
        {
            this.responses.Update(response);
            this.responses.SaveChanges();

            return response;
        }

        public void Delete(object id)
        {
            this.responses.Delete(id);
            this.responses.SaveChanges();
        }

        public IQueryable<Response> GetAll()
        {
            return this.responses.All();
        }

        public Response GetById(int id)
        {
            return this.responses.All()
                                  .FirstOrDefault(r => r.Id == id);
        }
    }
}
