namespace MVC_03.Company.G05.PL.Services
{
    public interface IScopedServices
    {
        public Guid Guid { get; set; }
        string GetGuid();
    }
}
