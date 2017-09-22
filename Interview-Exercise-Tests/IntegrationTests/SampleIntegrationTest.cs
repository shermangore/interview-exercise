using System;
using NUnit.Framework;
using Rhino.Mocks;
using Interview_Exercise;

namespace Interview_Exercise_Tests.IntegrationTests
{
    [TestFixture]
    public class SampleIntegrationTest
    {
        [TestFixtureSetUp]
        protected void SetUp()
        {
            Repository repository = new Repository();
            Country country = new Country();
        }

        [TestFixtureTearDown]
        protected void FinalTearDown()
        {

        }

        [Test]
        public void CountryIsAllowedTest()
        {
            Repository repository = new Repository();
            string code = "USA";

            Assert.AreEqual(repository.IsCodeAllowed(code), true);
        }

        [Test]
        public void CountryIsNotAllowedTest()
        {
            Repository repository = new Repository();
            string code = "ZZZ";

            Assert.AreEqual(repository.IsCodeAllowed(code), false);
        }

        [Test]
        public void AddCountryTest()
        {
            // Checked to make sure no exception is thrown when country is added
        }

        [Test]
        public void DuplicateCountryTest()
        {
            // Check to see if a duplicate country insertion is allowed
        }

        [Test]
        public void UpdateCountryTest()
        {
            // Check to make sure an existing country can be updated
        }

        [Test]
        public void UpdateMissingCountryTest()
        {
            // Check to make sure a duplicate country insertion will throw an error
        }

        [Test]
        public void DeleteCountryTest()
        {
            // Check to make sure a country can be deleted
        }

        [Test]
        public void GetCountryTest()
        {
            // Check to make sure a countryCode and a countryName are returned when the GetCountry method is called
        }

        [Test]
        public void ClearFileTest()
        {
            // Check to make sure the Clear method does not throw an error
        }
    }
}