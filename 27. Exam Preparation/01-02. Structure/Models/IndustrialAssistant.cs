namespace RobotService.Models;

public class IndustrialAssistant : Robot
{
    public IndustrialAssistant(string model)
        : base(model, 40_000, 5_000)
    {
    }
}