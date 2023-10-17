using EntitiesMapper.Tests.Dto;
using EntitiesMapper.Tests.Entities;

namespace EntitiesMapper.Tests
{
    [TestClass]
    public class MapperTests
    {
        #region PersonEntity properties
        static readonly PersonEntity JohnDoePersonEntity = new()
        {
            Name = "John",
            Surname = "Doe",
            Age = 30,
            Rule = "Ingegnere",
            BirthdayYear = 1992,
            Address = "Via del Sole, 123"
        };

        static readonly PersonEntity MarioRossiPersonEntity = new()
        {
            Name = "Mario",
            Surname = "Rossi",
            Age = 18,
            Rule = "Developer",
            BirthdayYear = 2013,
            Address = "Via della Luna, 456"
        };

        static readonly List<PersonEntity> FriendsPersonsEntities = new()
        {
            JohnDoePersonEntity,
            MarioRossiPersonEntity
        };
        #endregion

        #region PersonEntity2 properties
        static readonly PersonEntity2 JohnDoePersonEntity2 = new()
        {
            NomeNascita = "John",
            SurnameNascita = "Doe",
            Et‡Attuale = 30,
            ProfessioneAttuale = "Ingegnere"
        };

        static readonly PersonEntity2 MarioRossiPersonEntity2 = new()
        {
            NomeNascita = "Mario",
            SurnameNascita = "Rossi",
            Et‡Attuale = 18,
            ProfessioneAttuale = "Developer"
        };

        static readonly List<PersonEntity2> FriendsPersonsEntities2 = new()
        {
            JohnDoePersonEntity2,
            MarioRossiPersonEntity2
        };
        #endregion

        [TestMethod("Copia di un oggetto in un altro dello stesso tipo")]
        public void Test1()
        {
            Mapper.CopyObject(JohnDoePersonEntity, out PersonEntity anotherJohnDoe);
            Assert.AreEqual(JohnDoePersonEntity.Name, anotherJohnDoe.Name);
            Assert.AreEqual(JohnDoePersonEntity.Surname, anotherJohnDoe.Surname);
            Assert.AreEqual(JohnDoePersonEntity.Age, anotherJohnDoe.Age);
            Assert.AreEqual(JohnDoePersonEntity.Rule, anotherJohnDoe.Rule);
            Assert.AreEqual(JohnDoePersonEntity.BirthdayYear, anotherJohnDoe.BirthdayYear);
            Assert.AreEqual(JohnDoePersonEntity.Address, anotherJohnDoe.Address);
        }

        [TestMethod("Copia di un oggetto in un altro di tipo diverso")]
        public void Test2()
        {
            Mapper.CopyObject(JohnDoePersonEntity, out PersonDto anotherJohnDoe);
            Assert.AreEqual(JohnDoePersonEntity.Name, anotherJohnDoe.Nome);
            Assert.AreEqual(JohnDoePersonEntity.Surname, anotherJohnDoe.Cognome);
            Assert.AreEqual(JohnDoePersonEntity.Age, anotherJohnDoe.Et‡);
            Assert.AreEqual(JohnDoePersonEntity.Rule, anotherJohnDoe.Professione);
            Assert.AreEqual(JohnDoePersonEntity.BirthdayYear, anotherJohnDoe.AnnoNascita);
            Assert.AreEqual(JohnDoePersonEntity.Address, anotherJohnDoe.ViaResidenza);
        }

        [TestMethod("Copia di una lista in un'altra dello stesso tipo")]
        public void Test3()
        {
            Mapper.CopyList<PersonEntity, PersonEntity>(FriendsPersonsEntities, out var anotherFriendsPersonsEntities);

            for (int friendIndex = 0; friendIndex < anotherFriendsPersonsEntities.Count; friendIndex++)
            {
                var currentSourceFriend = FriendsPersonsEntities[friendIndex];
                var currentAnotherFriend = anotherFriendsPersonsEntities.ElementAt(friendIndex);

                Assert.AreEqual(currentSourceFriend.Name, currentAnotherFriend.Name);
                Assert.AreEqual(currentSourceFriend.Surname, currentAnotherFriend.Surname);
                Assert.AreEqual(currentSourceFriend.Age, currentAnotherFriend.Age);
                Assert.AreEqual(currentSourceFriend.Rule, currentAnotherFriend.Rule);
                Assert.AreEqual(currentSourceFriend.BirthdayYear, currentAnotherFriend.BirthdayYear);
                Assert.AreEqual(currentSourceFriend.Address, currentAnotherFriend.Address);
            }
        }

        [TestMethod("Copia di una lista in un'altra di tipo diverso")]
        public void Test4()
        {
            Mapper.CopyList<PersonEntity2, PersonDto>(FriendsPersonsEntities2, out var anotherFriendsPersonsEntities2);

            for (int friendIndex = 0; friendIndex < anotherFriendsPersonsEntities2.Count; friendIndex++)
            {
                var currentSourceFriend = FriendsPersonsEntities2[friendIndex];
                var currentAnotherFriend = anotherFriendsPersonsEntities2.ElementAt(friendIndex);

                Assert.AreEqual(currentSourceFriend.NomeNascita, currentAnotherFriend.Nome);
                Assert.AreEqual(currentSourceFriend.SurnameNascita, currentAnotherFriend.Cognome);
                Assert.AreEqual(currentSourceFriend.Et‡Attuale, currentAnotherFriend.Et‡);
                Assert.AreEqual(currentSourceFriend.ProfessioneAttuale, currentAnotherFriend.Professione);

                Assert.IsTrue(string.IsNullOrEmpty(currentAnotherFriend.ViaResidenza));
                Assert.IsTrue(currentAnotherFriend.AnnoNascita is 0);
            }
        }
    }
}