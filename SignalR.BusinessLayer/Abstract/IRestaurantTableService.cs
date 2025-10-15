using SignalR.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.BusinessLayer.Abstract
{
    public interface IRestaurantTableService : IGenericService<RestaurantTable>
    {
        public int TCountTable();
        public void TChangeRestaurantTableStatusToTrue(int id);
        public void TChangeRestaurantTableStatusToFalse(int id);
    }
}
