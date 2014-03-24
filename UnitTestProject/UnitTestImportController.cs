using MongoDBImportDataApplication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Extensions;
using FluentAssertions;

namespace UnitTestProject
{
    class UnitTestImportController
    {
        public ImportController import;

        [Trait("ImportController", "Constructor")]
        [Fact]
        public void TestImportControllerConstructor()
        {
            import = new ImportController();
            import.Should().NotBeNull("Because we are intializing a new instance of ImportController");
        }

        [Trait("ImportController", "CommitEntity")]
        [Theory]
        [InlineData("TestCommitCollection", "myTestPropCommit", 1234)]
        public void TestCommitEntity(string collectionName, string propertyName, object propertyValue)
        {
            this.TestImportControllerConstructor();

            import.entity.AddProperty(propertyName, propertyValue);

            import.CommitEntity(collectionName);
        }
    }
}
