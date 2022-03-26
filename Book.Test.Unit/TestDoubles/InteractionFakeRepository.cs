using BookTest.Unit.Data.InteractionTestData;
using DomainModel;
using System.Collections.Generic;
using UseCases.RepositoryContract;

namespace BookTest.Unit.TestDoubles
{
    public class InteractionFakeRepository : IInteractionRepository
    {
        private int validId;
        private int validUserId;
        private int validBookId;

        public void SetExistingBookId(int bookId) => validId = bookId;
        public void SetExistingUserId(int userId) => validUserId = userId;
        public void SetExistingInteractionId(int interactionId) => validId = interactionId;

        public void Add(Interaction interaction)
        {
        }

        public Interaction Find(int id)
        {
            if (id == validId)
                return new InteractionBuilder().Build();
            return null;
        }

        public List<Interaction> FindByUserId(int userId)
        {
            var list = new List<Interaction>
            {
                new InteractionBuilder().Build()
            };

            if (userId == validUserId)
                return list;
            return null;
        }

        public List<Interaction> GetAll()
        {
            return new List<Interaction>
            {
                new InteractionBuilder().Build(),
            };
        }

        public void Update(Interaction interaction)
        {
        }

        public List<Interaction> FindByBookId(int bookId)
        {
            var list = new List<Interaction>
            {
                new InteractionBuilder().Build()
            };

            if (bookId == validBookId)
                return list;
            return null;
        }
    }
}
