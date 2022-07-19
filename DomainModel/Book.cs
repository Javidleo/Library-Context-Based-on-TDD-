using System.Collections.Generic;

namespace DomainModel
{
    public class Book
    {
        public int Id { get; private set; }

        public string Name { get; private set; }

        public string AuthorName { get; private set; }

        public string DateofAdding { get; private set; }

        public bool InUse { get; private set; }

        public virtual List<Interaction> Interactions { get; private set; }

        private Book(string Name, string authorName, string DateofAdding)
        {
            this.Name = Name;
            AuthorName = authorName;
            this.DateofAdding = DateofAdding;
            InUse = false;
        }

        public static Book Create(string Name, string authorName, string DateofAdding)
        => new(Name, authorName, DateofAdding);

        public void Available()
        => InUse = false;

        public void UnAvailable()
        => InUse = true;

        public void Modify(string name, string authorName, string dateofAdding)
        {
            Name = name;
            AuthorName = authorName;
            DateofAdding = dateofAdding;
        }
    }
}
