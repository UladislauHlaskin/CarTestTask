namespace CarTestTask.Repository
{
    public interface IUnitOfWork
    {
        ICarRepository Cars { get; }
    }
}
