using MVC_03.Company.G05.PL.Services;

namespace MVC_03.Services
{
    public class ScopedServices : IScopedServices
    {
        public Guid Guid { get ; set ; }

        public ScopedServices() 
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
