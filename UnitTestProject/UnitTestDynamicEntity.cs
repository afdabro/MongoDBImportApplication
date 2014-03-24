using Common;
using System;
using Xunit;
using FluentAssertions;
using Xunit.Extensions;

namespace UnitTestProject
{
    public class UnitTestDynamicEntity
    {
        public DynamicEntityContainer dynamicEntity;

        [Trait("DynamicEntity", "Constructor")]
        [Fact]
        public void TestDynamicEntityConstructor()
        {
            dynamicEntity = new DynamicEntityContainer();

            dynamicEntity.Should().NotBeNull("Because dynamic entity initialized");
        }

        [Trait("DynamicEntity", "AddProperty")]
        [Theory]
        [InlineData("myTestProp", 123)]
        public void TestDynamicEntityAddProperty(string propertyName, object propertyValue)
        {
            this.TestDynamicEntityConstructor();
            dynamicEntity.AddProperty(propertyName, propertyValue);

            dynamicEntity.dataDictionary.data.Should().Contain(propertyName, propertyValue, "Because we added this property");
        }

        [Trait("DynamicEntity", "RemoveProperty")]
        [Theory]
        [InlineData("Id", null)]
        public void TestDynamicEntityRemoveProperty(string propertyName, object propertyValue)
        {
            this.TestDynamicEntityConstructor();
            dynamicEntity.RemoveProperty(propertyName);
            dynamicEntity.dataDictionary.data.Should().NotContain(propertyName, propertyValue, "Because we removed this property");
        }

        [Trait("DynamicEntity", "GetProperty")]
        [Theory]
        [InlineData("myTestProp", true)]
        public void TestDynamicEntityGetProperty(string propertyName, object propertyValue)
        {
            this.TestDynamicEntityConstructor();
            this.TestDynamicEntityAddProperty(propertyName, propertyValue);
            var testProp = dynamicEntity.GetProperty(propertyName);
            testProp.Should().Be(propertyValue, "Becuase we are extracting a specific value");
        }
    }
}
