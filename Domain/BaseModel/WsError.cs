using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.BaseModel
{
    public class WsError
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public string Field { get; set; }
    }
}
