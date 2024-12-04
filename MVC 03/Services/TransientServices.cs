
namespace MVC_03.Services
{
    public class TransientServices : ITransientServices
    {
        public Guid Guid { get; set; }

        public TransientServices() 
        {
            Guid = Guid.NewGuid();
        }
        public string GetGuid()
        {
            return Guid.ToString();
        }
        public override string ToString()
        {
            return Guid.ToString();
        }
    }
}
