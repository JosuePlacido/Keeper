using Domain.Repository;
using Domain.Models;
using Infrastructure.Data;
namespace Infrastruture.Repositorys
{
    public class RepositoryChampionship : RepositoryBase<Championship>, IRepositoryChampionship
    {
        private readonly ApplicationContext _context;
        public RepositoryChampionship(ApplicationContext Context)
            : base(Context)
        {
            _context = Context;
        }
    }
}
