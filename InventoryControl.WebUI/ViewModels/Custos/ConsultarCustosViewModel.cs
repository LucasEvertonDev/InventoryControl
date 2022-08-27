using InventoryControl.Models.Entities;

namespace InventoryControl.WebUI.ViewModels.Custos
{
    public class ConsultarCustosViewModel
    {
        public DateTime DataInicio { get; set; }

        public DateTime DataFim { get; set; }

        public List<CustosModel> Custos { get; set; }
    }
}
