
namespace MVC_03.Services
{
    public class SingeltonServices : ISingeltonServices
    {
        public Guid Guid { get; set; }

        public SingeltonServices() 
        {
            Guid = Guid.NewGuid();
        }
        public string GetGuid()
        {
            return Guid.NewGuid().ToString();   
        }

        public override string ToString()
        {
            return Guid.ToString();
        }
    }
}
