using System.Collections.Generic;

namespace DomainModel
{
    public class Book
    {
        public int Id { get; private set; }

        public string Name { get; private set; }

        public string authorName { get; private set; }

        public string DateofAdding { get; private set; }

        public virtual List<Interaction> Interactions { get; private set; }

        Book(string Name, string authorName, string DateofAdding)
        {
            this.Name = Name;
            this.authorName = authorName;
            this.DateofAdding = DateofAdding;
        }

        public static Book Create(string Name, string authorName, string DateofAdding)
        => new(Name, authorName, DateofAdding);

    }
}
