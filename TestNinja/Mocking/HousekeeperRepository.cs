using System.Collections.Generic;

namespace TestNinja.Mocking
{
    public interface IHousekeeperRepository
    {
        IEnumerable<Housekeeper> GetAll();
    }

    public class HousekeeperRepository : IHousekeeperRepository
    {
        private readonly UnitOfWork unitOfWork;

        public HousekeeperRepository()
        {
            unitOfWork = new UnitOfWork();
        }

        public IEnumerable<Housekeeper> GetAll()
        {
            return unitOfWork.Query<Housekeeper>();
        }
    }
}