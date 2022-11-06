using InventoryControl.Models;

namespace InventoryControl.Api.Contracts
{
    public class RequestDto<TDto> where TDto : BaseModel
    {
        public RequestDto()
        {
            Items = new List<TDto>();
        }

        public TDto Entity { get; set; }

        public List<TDto> Items { get; set; }
    }
}
