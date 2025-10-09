using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WOMS.Application.Features.Auth.Dtos
{
    public class ApiResponse<T>
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public List<string> Errors { get; set; }
        public bool IsSuccess { get; set; }
      //  public PaginationMetadata Pagination { get; set; } = new();
        public T? Data { get; set; }

        public ApiResponse()
        {
            Errors = new List<string>();
        }
    }
}
