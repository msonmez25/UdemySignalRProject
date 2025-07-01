using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.DtoLayer.MoneyCaseDto
{
    public class GetMoneyCaseDto
    {
        public int MoneyCaseID { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
