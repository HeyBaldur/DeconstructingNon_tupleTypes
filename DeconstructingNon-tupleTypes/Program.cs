using System;
using System.Collections.Generic;

namespace DeconstructingNon_tupleTypes
{
    /// <summary>
    /// A tuple provides a lightweight way to retrieve multiple values from a method call. But once you retrieve the tuple, 
    /// you have to handle its individual elements. Working on an element-by-element basis is cumbersome, as the following example shows. 
    /// The QueryCityData method returns a three-tuple, and each of its elements is assigned to a variable in a separate operation.
    /// </summary>
    class Program
    {
        /// <summary>
        /// The tuples feature provides concise syntax to group multiple 
        /// data elements in a lightweight data structure. 
        /// </summary>
        static void KeyValuePairs()
        {
            Dictionary<string, int> favoriteLanguages = new(StringComparer.OrdinalIgnoreCase)
            {
                ["C#"] = 1,
                ["TypeScript"] = 2,
                ["F#"] = 3,
                ["JavaScript"] = 4,
                ["GoLang"] = 5
            };

            foreach ((string lang, int rank) in favoriteLanguages) // Here we deconstruct the key value pairs (string lang, int rank)
            {
                Console.WriteLine($"My {rank} favorite programming language is {lang}");
            }
        }

        static void CustomObjects()
        {
            Album album = new(
                id: 7,
                name: "Sabaton",
                askingPrice: 9.99m,
                releaseDate: new DateTime(1995, 10, 3)
           );

            var (_, name, askingPrice, (hasDate, date)) = album;
            Console.WriteLine($"The first CD i bought was {name}");
            Console.WriteLine($"The released was {date:MMMM} {date.Day} {date.Year}");
            Console.WriteLine($"The price was {askingPrice:c}");
        }

        static void ExtensionMethods()
        {
            (bool hasValue, int value) = new int?();
            Console.WriteLine($"Has value = {hasValue}, value = {value}");

            (hasValue, value) = new int?(77);
            Console.WriteLine($"Has value = {hasValue}, value = {value}");
        }

        static void PositionalRecords()
        {
            var (name, date) = new CompactDisc("Deftones", new DateTime(2003, 5, 20));
            Console.WriteLine(
                $"The self-titled album is {name} " +
                $"was released on {date:MMMM}, {date.Day} {date.Year}"
                );
        }

        static void Main(string[] args)
        {
            KeyValuePairs();
            CustomObjects();
            ExtensionMethods();
            PositionalRecords();
        }
    }

    public class Album
    {
        public Album(
            int id, 
            string name, 
            decimal askingPrice, 
            DateTime? releaseDate)
        {
            Id = id;
            Name = name;
            AskingPrice = askingPrice;
            ReleaseDate = releaseDate;
        }

        public void Deconstruct(
            out int id,
            out string name,
            out decimal askingPrice,
            out DateTime? releaseDate)
        {
            id = Id;
            name = Name;
            askingPrice = AskingPrice;
            releaseDate = ReleaseDate;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal AskingPrice { get; set; }
        public DateTime? ReleaseDate { get; set; }
    }

    public static class NullableExtensions
    {
        public static void Deconstruct<T>(
            this T? nullable, out bool hasValue, out T value) where T : struct =>
            (hasValue, value) =
                (nullable.HasValue, nullable.GetValueOrDefault());
    }

    record CompactDisc(string Name, DateTime ReleaseDate);
}
