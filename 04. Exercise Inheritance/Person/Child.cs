namespace Person;

public class Child : Person
{
    public Child(string name, int age) : base(name, age)
    {
    }

    // В това {override} пропътри извиквам базовия мембър {base.Age}, защото там имам проверката -> дали човек не е с отрицателно въведени години (ако не извикам базовия мембър ще се получи рекурсия "Stack overflow" exception)
    public override int Age
    {
        get
        {
            return base.Age;
        }
        set
        {
            if (value <= 15)
            {
                base.Age = value;
            }
        }
    }
}