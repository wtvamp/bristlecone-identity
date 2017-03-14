using Bristlecone.Auth.Identity;
using NUnit.Framework;
using FluentAssertions;

namespace Bristlecone.Auth.IdentityTests
{
    [TestFixture]
    public class BristleconeAuthDbContextTests
    {

        [Test]
        public void Given_an_BristleconeAuthDbContext_when_create_is_called_then_a_new_instance_of_BristleconeAuthDbContext_should_be_returned()
        {
            // GIVEN an BristleconeAuthDbContext static constructor
            // WHEN created is call
            var BristleconeAuthContext = BristleconeAuthDbContext.Create();

            // Then a new instance of BristleconeAuthDbContext should be returned
            BristleconeAuthContext.Should().BeOfType<BristleconeAuthDbContext>("because a new instance of an BristleconeAuthDbContext was created.");
        }

        [Test]
        public void Given_an_BristleconeEmployeeAuthDbContext_when_create_is_called_then_a_new_instance_of_BristleconeEmployeeAuthDbContext_should_be_returned()
        {
            // GIVEN an BristleconeEmployeeAuthDbContext static constructor
            // WHEN created is call
            var BristleconeAuthContext = BristleconeEmployeeAuthDbContext.Create();

            // Then a new instance of BristleconeEmployeeAuthDbContext should be returned
            BristleconeAuthContext.Should().BeOfType<BristleconeEmployeeAuthDbContext>("because a new instance of an BristleconeEmployeeAuthDbContext was created.");
        }

        [Test]
        public void Given_an_BristleconeParticipantAuthDbContext_when_create_is_called_then_a_new_instance_of_BristleconeParticipantAuthDbContext_should_be_returned()
        {
            // GIVEN an BristleconeParticipantAuthDbContext static constructor
            // WHEN created is call
            var BristleconeAuthContext = BristleconeParticipantAuthDbContext.Create();

            // Then a new instance of BristleconeParticipantAuthDbContext should be returned
            BristleconeAuthContext.Should().BeOfType<BristleconeParticipantAuthDbContext>("because a new instance of an BristleconeParticipantAuthDbContext was created.");
        }
    }
}