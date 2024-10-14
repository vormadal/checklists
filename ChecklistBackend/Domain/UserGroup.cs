namespace Domain;

public class UserGroup
{
    public int Id { get; set; }

    public string Name { get; set; }

    public ICollection<GroupMember> Members { get; set; }
}
