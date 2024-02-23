using Loyalty.System.Data.Model;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Loyalty.System.API.Services.UserPointsService
{
    public class UserPointsService : BaseService
    {
        public UserPointsService(BaseContext baseContext) : base(baseContext)
        {

        }
        public void addUserPoint(string id)
        {
            UserPoints user = Context.UserPoints.FirstOrDefault(x => x.Id ==id);
            if (user != null)
            {
                user.Points++;
                Context.Update(user);
                Context.SaveChanges();
            }
        }
        public ushort getUserPoints(string id)
        {
            UserPoints user = Context.UserPoints.FirstOrDefault(x => x.Id == id);
            if(user != null)
            {
                
                return Convert.ToUInt16(user.Points % 10);
            }
            return 0;
        }
    }
}
