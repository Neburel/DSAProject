using DSAProject2Web.Server.Entities;

namespace DSAProjectWeb.Server.Entities
{
    public class CharakterDataRequest<T> : CharakterIDRequest
    {
        public T Data { get; set; }
    }
}
