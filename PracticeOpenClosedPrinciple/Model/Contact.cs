namespace PracticeOpenClosedPrinciple.Model;

public class Contact : BaseEntity
{
    public string Name { get; set; }
    public string Phone { get; set; }
    public bool Favorite { get; set; }
}