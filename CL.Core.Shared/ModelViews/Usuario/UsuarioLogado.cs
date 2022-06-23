using System.Collections.Generic;

namespace CL.Core.Shared.ModelViews.Usuario;

public class UsuarioLogado
{
    public string Login { get; set; }
    public string Token { get; set; }
    public ICollection<ReferenciaFuncao> Funcoes { get; set; }
}
