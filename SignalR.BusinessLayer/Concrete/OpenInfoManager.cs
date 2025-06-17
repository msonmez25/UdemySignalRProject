using SignalR.BusinessLayer.Abstract;
using SignalR.DataAccessLayer.Abstract;
using SignalR.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.BusinessLayer.Concrete
{
    public class OpenInfoManager : IOpenInfoService
    {
        private readonly IOpenInfoDal _openInfoDal;

        public OpenInfoManager(IOpenInfoDal openInfoDal)
        {
            _openInfoDal = openInfoDal;
        }

        public void TAdd(OpenInfo entity)
        {
           _openInfoDal.Add(entity);
        }

        public void TDelete(OpenInfo entity)
        {
            _openInfoDal.Delete(entity);
        }

        public OpenInfo TGetByID(int id)
        {
            return _openInfoDal.GetByID(id);
        }

        public List<OpenInfo> TGetListAll()
        {
            return _openInfoDal.GetListAll();
        }

        public void TUpdate(OpenInfo entity)
        {
           _openInfoDal.Update(entity);
        }
    }
}
