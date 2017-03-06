using System.Collections.Generic;

namespace MobileDen.CodeChallenge.FileParsing.Model
{
    public interface IBook
    {
        string Name { get; set; }
        string Isbn { get; set; }
        string Author { get; set; }
    }

    public class Book : IBook
    {
        public Book(string name, string isbn, string author)
        {
            Name = name;
            Isbn = isbn;
            Author = author;
        }

        public string Name { get; set; }
        public string Isbn { get; set; }
        public string Author { get; set; }
    }
}
