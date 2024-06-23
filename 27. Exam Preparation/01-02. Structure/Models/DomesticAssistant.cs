namespace RobotService.Models;

public class DomesticAssistant : Robot
{
    public DomesticAssistant(string model)
        : base(model, 20_000, 2_000)
    {
    }
}