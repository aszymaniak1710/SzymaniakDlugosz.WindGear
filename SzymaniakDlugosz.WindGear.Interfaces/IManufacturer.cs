namespace SzymaniakDlugosz.WindGear.Interfaces
{
    public interface IManufacturer
    {
        int Id { get; set; }
        string Name { get; set; }
        string Country { get; set; }
        int FoundedYear { get; set; }
    }
}
