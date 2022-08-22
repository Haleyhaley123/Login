using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.BaseModel
{
    public class WsResponse
    {
        public string Status { get; set; }
        public object Data { get; set; }
        public List<WsError> Errors { get; set; }
    }
}
