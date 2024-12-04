namespace MVC_03.Services
{
    public interface ITransientServices
    {
        public Guid Guid { get; set; }
        string GetGuid();
    }
}
