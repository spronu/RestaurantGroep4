public class MenuItems
{
    public int id { get; set; }
    public string name { get; set; }
    public string allergies { get; set; }
    public string course { get; set; }
    public string category { get; set; }
    public double price { get; set; }
}

public class MenuItem
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Json { get; set; }
    public bool Active { get; set; }
}