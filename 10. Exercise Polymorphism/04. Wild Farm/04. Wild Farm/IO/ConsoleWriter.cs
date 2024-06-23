using WildFarm.IO.Interfaces;

namespace WildFarm.IO;

public class ConsoleWriter : IWriter
{
    public void WriteLine(object obj)
        => Console.WriteLine(obj);
}