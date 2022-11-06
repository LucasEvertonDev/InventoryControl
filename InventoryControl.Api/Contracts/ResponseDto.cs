using InventoryControl.Models;

namespace InventoryControl.Api.Contracts
{
    public class ResponseDto<TDto> where TDto : BaseModel
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
