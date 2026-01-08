using System;
using SzymaniakDlugosz.WindGear.DAOSQL;
using SzymaniakDlugosz.WindGear.Interfaces;

namespace SzymaniakDlugosz.WindGear.DbGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Generating Database...");
            try 
            {
                // Instantiate DAOSQL. In its constructor, it runs EnsureCreated() and seeds data.
                IDAO dao = new DAOSQL.DAOSQL();
                
                // Verify data
                var mans = dao.GetAllManufacturers();
                Console.WriteLine($"Database generated with {mans.Count} manufacturers.");
                Console.WriteLine("Done.");
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}
