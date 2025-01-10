namespace Domain.Entities.Tracking;

public class ReviewEvent : IBaseEntity
{
    public string ent_seq { get; set; }
    public Guid UserId { get; set; }
    public int Serial { get; set; }
    public DateTime Created { get; set; }
    public ReviewValue Value { get; set; }
    
    public TrackedEntry TrackedEntry { get; set; } // parent nav
}

public enum ReviewValue
{
    Zero,
    Again,
    Soon,
    Okay,
    Easy
}