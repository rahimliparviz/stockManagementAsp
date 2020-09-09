using Stock.Domain;

namespace Stock.Services.Repositories.Abstract
{
    public interface IRegulationRepository
    {
        Regulation Regulations();
        Response<Regulation> Create(Regulation regulation);
    }
}