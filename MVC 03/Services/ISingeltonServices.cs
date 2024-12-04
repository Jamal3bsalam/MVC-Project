namespace MVC_03.Services
{
    public interface ISingeltonServices
    {
        public Guid Guid { get; set; }
        string GetGuid();
    }
}
