using System;

namespace DomainModel
{
    public class Interaction
    {
        public int Id { get; private set; }

        public int BookId { get; private set; }

        public int UserId { get; private set; }

        public int AdminId { get; private set; }

        private DateTime  Date { get; set; }
        private bool IsDeleted { get; set; }

        public virtual User User { get; private set; }

        public virtual Book Book { get; set; }
        
        public virtual Admin Admin { get; private set; }

        Interaction(int userId, int bookId, int adminId)
        {
            UserId = userId;
            BookId = bookId;
            AdminId = adminId;
            Date = DateTime.Now;
            IsDeleted = false;
        }

        public static Interaction Create(int userId, int bookId, int adminId)
        => new Interaction(userId, bookId, adminId);
    }
}