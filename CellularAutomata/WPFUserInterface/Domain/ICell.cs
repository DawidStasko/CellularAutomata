namespace WPFUserInterface.Domain;

public interface ICell
{
    bool State { get; }
    Coordinates Coordinates { get; }
    void ChangeState();
}