using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PeopleSearch.Controllers;
using PeopleSearch.Models;

namespace PeopleSearchTest
{
    [TestClass]
    public class PersonControllerTests
    {
        public PersonContext Context { get; private set; }

        [TestMethod]
        public void SearchPeople()
        {
            SetupDbContext("SearchTest");
            var personController = new PersonController(Context);
            var result = personController.RetrievePeople("test");
            var okResult = result as OkObjectResult;

            Assert.IsNotNull(okResult, "Nothing was returned in the result");
            Assert.AreEqual(okResult.StatusCode,200, "The Search Failed");
        }

        [TestMethod]
        public void DeletePersonTest()
        {
            var idToDelete = SetupDbContext("DeleteTest").Id;
            var countAfterDeletion = Context.People.CountAsync().Result - 1;
            var personController = new PersonController(Context);
            var result = personController.DeletePerson(idToDelete);
            var okResult = result.Result as OkObjectResult;

            Assert.IsNotNull(okResult, "Nothing was returned in the result");
            Assert.AreEqual(okResult.StatusCode, 200, "The Delete Failed");
            Assert.AreEqual(Context.People.CountAsync().Result, countAfterDeletion);
        }

        //technically this tests the get person functionaliy as well.
        [TestMethod]
        public void UpdatePersonTest()
        {
            var updatedString = "This person has been updated";

            var personToUpdate = SetupDbContext("UpdateTest");
            var personController = new PersonController(Context);
            personToUpdate.Interests = updatedString;

            var updatedResult = personController.RetrievePerson(personToUpdate.Id);
            var okResult = updatedResult.Result as OkObjectResult;

            Assert.AreEqual(okResult.StatusCode, 200, "The get failed Failed");

            var person = okResult.Value as PersonModel;

            Assert.AreEqual(person.Interests, updatedString, "the update failed");
        }

        [TestMethod]
        public void AddPersonTest()
        {
            var newPerson = new PersonModel
            {
                FirstName = "Newbie",
                LastName = "Newman",
                Age = 31,
                Address = "123 Fake Street New York, NY",
                Interests = "Being a new person",
            };

            SetupDbContext("AddTest");
            var countAfterAdd = Context.People.CountAsync().Result + 1;
            var personController = new PersonController(Context);
            var result = personController.CreatePerson(newPerson).Result;

            Assert.AreEqual(countAfterAdd, 2, "the add failed");
        }

        /// <summary>
        /// The dbName is to give each test their own DB so it doesn't cause conflicts.
        /// </summary>
        /// <param name="dbName"></param>
        /// <returns></returns>
        private PersonModel SetupDbContext(string dbName)
        {
            var testPerson = CreateTestPerson();
            var dbContextOptions = new DbContextOptionsBuilder<PersonContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            Context = new PersonContext(dbContextOptions);

            var entityAdded = Context.People.Add(testPerson);
            Context.SaveChanges();
            return entityAdded.Entity;
        }

        private PersonModel CreateTestPerson()
        {
            return new PersonModel
            {
                FirstName = "Tester",
                LastName = "McTesterton",
                Age = 45,
                Address = "123 Fake Street New York, NY",
                Interests = "Unit testing projects",
            };
        }
    }
}