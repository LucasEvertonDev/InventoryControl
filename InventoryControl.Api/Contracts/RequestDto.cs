using InventoryControl.Models;
using InventoryControl.Models.DTOs;

namespace InventoryControl.Api.Contracts
{
    public class RequestDto<TDto> where TDto : BaseDTO
    {
        public RequestDto()
        {
            Items = new List<TDto>();
        }

        public TDto Entity { get; set; }

        public List<TDto> Items { get; set; }
    }
}
