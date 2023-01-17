namespace AWASP.WebUI.Data.Domains
{
    public class MapPerfilUsuariosAcessos : Entity
    {
        public int? PerfilUsuarioId { get; set; }
        public int? AcessoId { get; set; }

        public PerfilUsuario? PerfilUsuario { get; set; }
        public Acesso? Acesso { get; set; }
    }
}
