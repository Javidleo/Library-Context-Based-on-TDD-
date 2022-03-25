using DomainModel;
using System;

namespace BookTest.Unit.Data.InteractionTestData
{
    public class InteractionBuilder
    {
        private int id = 1;
        private int bookId = 1;
        private int userId = 1;
        private int adminId = 1;
        private DateTime date = DateTime.Now;
        private bool isDeleted = false;

        public InteractionBuilder WithId(int id)
        {
            this.id = id;
            return this;
        }

        public InteractionBuilder WithBookId(int bookId)
        {
            this.bookId = bookId;
            return this;
        }

        public InteractionBuilder WithUserId(int userId)
        {
            this.userId = userId;
            return this;
        }

        public InteractionBuilder WithAdminId(int adminId)
        {
            this.adminId = adminId;
            return this;
        }

        public InteractionBuilder WithDate(DateTime date)
        {
            this.date = date;
            return this;
        }

        public InteractionBuilder WithIsDeleted(bool isDeleted)
        {
            this.isDeleted = isDeleted;
            return this;
        }

        public Interaction Build()
        => Interaction.Create(userId, bookId, adminId);
    }
}
