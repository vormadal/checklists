namespace Domain;

public class GroupMember
{
    public int Id { get; set; }

    public int GroupId { get; set; }

    public UserGroup Group { get; set; }

    public string UserId { get; set; }

    public IApplicationUser User { get; set; }

    public bool IsAdmin { get; set; }
}
