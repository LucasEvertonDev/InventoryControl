using InventoryControl.Models;
using InventoryControl.Models.DTOs;

namespace InventoryControl.Api.Contracts
{
    public class ResponseDto<TDto> where TDto : BaseDTO
    {
        public ResponseDto()
        {
            Items = new List<TDto>();
        }

        public bool Sucess { get; set; }
        
        public string Message { get; set; }

        public TDto Entity { get; set; }

        public List<TDto> Items { get; set; }
    }
}
