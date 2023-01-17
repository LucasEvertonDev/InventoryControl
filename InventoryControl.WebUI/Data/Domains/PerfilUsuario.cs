namespace AWASP.WebUI.Data.Domains
{
    public class PerfilUsuario : Entity
    {
        public string? Nome { get; set; }
        public int? Situacao { get; set; }
        public ICollection<Usuario>? Usuarios { get; set; }
        public ICollection<MapPerfilUsuariosAcessos>? MapPerfilUsuariosAcessos { get; set; }
    }
}
