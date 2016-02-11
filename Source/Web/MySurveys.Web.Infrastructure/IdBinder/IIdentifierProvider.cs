namespace MySurveys.Web.Infrastructure.IdBinder
{
    public interface IIdentifierProvider
    {
        int DecodeId(string urlId);

        string EncodeId(int id);
    }
}
