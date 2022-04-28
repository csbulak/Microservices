namespace WebUI.Models;

public class UserViewModel
{
    public string Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public IEnumerable<string> GetUserProps()
    {
        yield return UserName;
        yield return Email;
        yield return FirstName;
        yield return LastName;
    }
}