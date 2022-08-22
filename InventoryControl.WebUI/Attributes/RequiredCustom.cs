using System.ComponentModel.DataAnnotations;

namespace InventoryControl.WebUI.Attributes
{
    public class RequiredCustom : RequiredAttribute
    {
        public RequiredCustom()
        {
            ErrorMessage = "Este campo é obrigatório";
        }
    }
}
