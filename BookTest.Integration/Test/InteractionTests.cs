using BookDataAccess;
using BookDataAccess.Repository;
using DomainModel;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.RepositoryContract;
using Xunit;

namespace BookTest.Integration.Test
{
    public class InteractionTests
    {
        private readonly BookContext _context;
        private readonly IInteractionRepository _interactionRepository;
        private readonly DbContextOptionsBuilder<BookContext> _optionBuilder;
        private Interaction _interaction;
        public InteractionTests()
        {
            _optionBuilder = new ContextOptionBuilderGenerator().GenerateOptionBuilder();
            _context = new BookContext(_optionBuilder.Options);
            _interactionRepository = new InteractionRepository(_context);
        }
        [Fact]
        public void AddInteraction_CheckForWorkingWell()
        {
            _interactionRepository.Add(_interaction);
            var interaction = _interactionRepository.FindByUserId(_interaction.UserId);

            interaction[0].Should().BeEquivalentTo(_interaction);
        }

        [Fact]
        public void AddInteractionon_CheckForNullData_ThrowExcpetion()
        {
            void result() => _interactionRepository.Add(null);
            Assert.Throws<NullReferenceException>(result);
        }

        [Fact]
        public void FindByUserId_CheckForWorkingWell()
        {
            _interactionRepository.Add(_interaction);
            var newInteraction = Interaction.Create(_interaction.UserId, 1, 1);
            _interactionRepository.Add(newInteraction);

            var interaction = _interactionRepository.FindByUserId(_interaction.UserId);
            interaction[0].Should().BeEquivalentTo(_interaction);
            interaction[1].Should().BeEquivalentTo(newInteraction);
        }

        [Fact]
        public void FindByUserId_CheckForZero_ReturnNull()
        {
            var interaction = _interactionRepository.FindByUserId(0);
            interaction.Count.Should().Be(0);
            interaction.Should().BeNull();
        }

        [Fact]
        public void FindByBookId_CheckForWorkingWell()
        {
            _interactionRepository.Add(_interaction);
            var interaction = _interactionRepository.FindByBookId(_interaction.BookId);
            interaction.Should().Be(_interaction);
        }

        [Fact]
        public void FindByBookId_CheckForZero_ReturnNull()
        {
            var interaction = _interactionRepository.FindByBookId(0);
            interaction.Should().BeNull();
        }

        [Fact]
        public void FindByInteractionId_CheckForWorkingWell()
        {
            _interactionRepository.Add(_interaction);
            var interaction = _interactionRepository.FindByBookId(_interaction.BookId);

            var excpected = _interactionRepository.Find(interaction.Id);
            excpected.Should().BeEquivalentTo(_interaction);
        }

        [Fact]
        public void FindByInteractionId_CheckForZeroData()
        {
            var interaction = _interactionRepository.Find(0);
            interaction.Should().BeNull();
        }

        [Fact]
        public void UpdateUser_CheckForWorkingWell()
        {
            _interactionRepository.Add(_interaction);

            var interaction = _interactionRepository.FindByBookId(_interaction.BookId);
            interaction.Modify(2,2);

            _interactionRepository.Update(interaction);
            var modified = _interactionRepository.Find(interaction.Id);

            modified.BookId.Should().Be(2);
            modified.UserId.Should().Be(2);
        }

        [Fact]
        public void UpdateUser_CheckForNullData_ThrowException()
        {
            void result() => _interactionRepository.Update(null);
            Assert.Throws<ArgumentNullException>(result);
        }

        [Fact]
        public void GetAll_CheckForWorkingWell()
        {
            var list = new List<Interaction>()
            {
                Interaction.Create(1,1,1),
                Interaction.Create(2,2,2),
                Interaction.Create(3,3,3),
            };

            _context.Interactions.AddRange(list);
            _context.SaveChanges();

            var excpected = _interactionRepository.GetAll();

            excpected[0].Should().BeEquivalentTo(list[0]);
            excpected[1].Should().BeEquivalentTo(list[1]);
            excpected[2].Should().BeEquivalentTo(list[3]);
            excpected.Count().Should().Be(list.Count);
        }

        [Fact]
        public void GetAll_CheckForEmptyList()
        {
            var excpected = _interactionRepository.GetAll();
            excpected.Count.Should().Be(0);
        }
    }
}
