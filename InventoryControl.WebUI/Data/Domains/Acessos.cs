namespace AWASP.WebUI.Data.Domains
{
    public class Acesso : Entity
    {
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public int? Situacao { get; set; }
        public ICollection<MapPerfilUsuariosAcessos>? MapPerfilUsuariosAcessos { get; set; }
    }
}
