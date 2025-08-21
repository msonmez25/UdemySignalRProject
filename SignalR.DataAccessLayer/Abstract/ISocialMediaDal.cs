using SignalR.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.DataAccessLayer.Abstract
{
    public interface ISocialMediaDal : IGenericDal<SocialMedia>
    {
        void SocialMediaStatusChangeTrue(int id);
        void SocialMediaStatusChangeFalse(int id);
        List<SocialMedia> GetSocialMediaListByStatusTrue();
    }
}
