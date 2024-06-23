namespace PersonInfo;

public interface IPerson
{
    // В интерфейсите има само { get; }, защото в тях само се инициализира
    public string Name { get; }
    public int Age { get; }
}