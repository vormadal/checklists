namespace Domain;

public class GroupInvite
{
    public int Id { get; set; }

    public int GroupId { get; set; }

    public UserGroup Group { get; set; }

    public string InviterId { get; set; }

    public IApplicationUser Inviter { get; set; }

    public string InviteeId { get; set; }

    public IApplicationUser Invitee { get; set; }

    public string InviteeEmail { get; set; }

    public bool IsAccepted { get; set; }
}
