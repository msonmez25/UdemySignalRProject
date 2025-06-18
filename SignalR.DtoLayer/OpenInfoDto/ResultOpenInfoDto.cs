using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.DtoLayer.OpenInfoDto
{
    public class ResultOpenInfoDto
    {
        public int OpenInfoID { get; set; }
        public string Title { get; set; }
        public DateTime DateFirst { get; set; }
        public DateTime DateEnd { get; set; }
    }
}
