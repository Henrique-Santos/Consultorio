namespace CL.Core.Shared.ModelViews.Endereco
{
    public class NovoEndereco
    {
        public string CEP { get; set; }
        public EstadoView Estado { get; set; }
        public string Cidade { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
    }
}