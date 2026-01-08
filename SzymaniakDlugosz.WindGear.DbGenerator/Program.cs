using System;
using SzymaniakDlugosz.WindGear.DAOSQL;
using SzymaniakDlugosz.WindGear.Interfaces;

namespace SzymaniakDlugosz.WindGear.DbGenerator
{
    // Generowanie bazy danych i seedowanie przykładowych danych
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Generating Database...");
            try 
            {
                // Tworzenie instancji klasy DAOSQL przy użyciu interfejsu IDAO
                IDAO dao = new DAOSQL.DAOSQL();
                
                // Wywołanie DAO do pobrania producentów
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
