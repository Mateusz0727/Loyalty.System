using AutoMapper;
using Loyalty.System.API.Models;
using Loyalty.System.API.Models.Qrcode;
using Loyalty.System.API.Models.User;
using Loyalty.System.Data.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Loyalty.System.API.Services.UserPointsService
{
    public class UserPointsService : BaseService
    {
        public UserPointsService(IMapper mapper, BaseContext context) : base(mapper, context)
        {

        }
        public void addUserPoint(QrCode qrCode)
        {
            UserPoints user = Context.UserPoints.FirstOrDefault(x => x.Id ==qrCode.Id);

            if (user != null&&user.QrCodeToken == qrCode.Token&&qrCode.ExpiredTime>DateTime.Now)
            {
                user.Points++;
                Context.Update(user);
                Context.SaveChanges();
                updateQrCodeToken(Guid.NewGuid().ToString(), user);
            }
        }
        public UserPointsModel getUserPoints(string id)
        {
            UserPoints user = Context.UserPoints.FirstOrDefault(x => x.Id == id);
            if(user != null)
            {

                return Mapper.Map<UserPointsModel>(user);
            }
            return null;
        }
        public void getPrize(QrCode qrCode)
        {
            UserPoints user = Context.UserPoints.FirstOrDefault(x => x.Id == qrCode.Id);

            if (user != null && user.QrCodeToken == qrCode.Token && qrCode.ExpiredTime > DateTime.Now)
            {
                user.CountOfPrize++;
                user.Points = 0;
                Context.Update(user);
                Context.SaveChanges();
                updateQrCodeToken(Guid.NewGuid().ToString(), user);
            }
        }
        public void updateQrCodeToken(QrCode qrCode)
        {

            UserPoints user = Context.UserPoints.FirstOrDefault(x => x.Id == qrCode.Id);
            user.QrCodeToken=qrCode.Token;
            Context.Update(user);
            Context.SaveChanges();
        }
        public void updateQrCodeToken(string token,UserPoints user)
        {
            user.QrCodeToken=token;
            Context.Update(user);
            Context.SaveChanges();
        }

       
    }
}
