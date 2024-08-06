
using AutoMapper;
using Loyalty.System.API.Models;
using Loyalty.System.API.Models.Register;
using Loyalty.System.Data.Model;
using Microsoft.AspNetCore.Identity;

namespace Loyalty.System.API.Services;

public class UserService : BaseService
{ 
    protected IPasswordHasher<User> Hasher { get; }
    public UserService(IMapper mapper, IPasswordHasher<User> hasher, BaseContext context) : base(mapper, context)
    {
       
        Hasher = hasher;
    }
   
    public async Task<User> GetByEmailAsync(string email)
    {
        User entity = Context.Users.FirstOrDefault(x=>x.Email==email);
        return entity;


    }
    public async Task<User> CreateAsync(RegisterFormModel user)
    {
        var entity = Mapper.Map<User>(user);
        try
        {

            SetPassword(entity, user.Password);
            SetEntity(entity);
            UserPoints userPoints = new()
            { Id = entity.PublicId
            
            };
            
            Context.Add(entity);
            Context.Add(userPoints);
            Context.SaveChanges();
        }
        catch (Exception ex)
        {
            if (ex.InnerException != null)
            {
                if (ex.InnerException.Message.Contains("IX_Email"))
                    throw new Exception("Podany adres email jest zajêty");
                if (ex.InnerException.Message.Contains("IX_UserName"))
                    throw new Exception("Podany adres email jest zajêty");
            }
        }
        return entity;

    }
    #region SetPassword()
    public bool SetPassword(User user, string password)
    {
        if (user != null)
        {
            user.Password = Hasher.HashPassword(user, password);
            return true;
        }

        return false;
    }
    #endregion
    public async Task SetEntity(User user)
    {
        if (user != null)
        {
            user.PublicId = Guid.NewGuid().ToString();
            user.UserName = user.Email;
            user.DateCreatedUtc = DateTime.Now;
            user.DateModifiedUtc = DateTime.Now;

        }

    }
}
