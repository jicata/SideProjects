namespace EntityFrameworkCore
{
    public interface IEntity<TIdentifier>
    {
        TIdentifier Id { get; set; }
    }
}
