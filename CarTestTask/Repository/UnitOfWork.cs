namespace CarTestTask.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(ICarRepository carRepository)
        {
            Cars = carRepository;
        }
        public ICarRepository Cars { get; }
    }
}
