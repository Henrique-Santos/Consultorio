namespace CL.Core.Domain
{
    public class Telefone
    {
        public int ClientId { get; set; }
        public string Numero { get; set; }
        public Cliente Cliente { get; set; }
    }
}