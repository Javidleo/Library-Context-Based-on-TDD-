using System;

namespace DomainModel
{
    public class Book
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string authorName { get; private set; }
        public string DateofAdding { get; private set; }
        Book(int Id, string Name, string authorName, string DateofAdding)
        {
            this.Id = Id;
            this.Name = Name;
            this.authorName = authorName;
            this.DateofAdding = DateofAdding;
        }

        public static Book Create(int Id, string Name, string authorName, string DateofAdding)
        => new(Id, Name, authorName, DateofAdding);

    }
}
