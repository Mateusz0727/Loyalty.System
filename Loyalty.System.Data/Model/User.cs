
namespace Loyalty.System.API.Models;

public class User 
{

    public string? PublicId { get; set; }

    public int Id { get; set; }

    public string? UserName { get; set; }


    public string? Email { get; set; }

    public bool EmailConfirmed { get; set; }

    public string? GivenName { get; set; }

    public string? Surname { get; set; }
    public string? Password { get; set; }

    public DateTime DateCreatedUtc { get; set; }
    public DateTime DateModifiedUtc { get; set; }

}