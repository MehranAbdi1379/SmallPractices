namespace PracticeOpenClosedPrinciple;

public interface IContactFunction
{
    public string OptionCode { get; }
    public string Description { get; }

    public Task Action();
}