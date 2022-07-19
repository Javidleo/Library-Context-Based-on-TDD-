using DomainModel;

namespace BookTest.Unit.Data.InteractionTestData
{
    public class InteractionBuilder
    {
        private int bookId = 1;
        private int userId = 1;
        private int adminId = 1;

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

        public Interaction Build()
        => Interaction.Create(userId, bookId, adminId);
    }
}
