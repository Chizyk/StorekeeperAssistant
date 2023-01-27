using StorekeeperAssistant.Domain.AggregatesModel.ProductAggregate;
using StorekeeperAssistant.Domain.Exceptions;
using System.Collections.Generic;
using Xunit;

namespace StorekeeperAssistant.UnitTests.Domain
{
    public class ProductAggregateTest
    {
        [Fact]
        public void Create_product_success()
        {
            //Arrange
            var count = 1;
            var nomenclatureId = 1;
            var companyId = 1;

            //Act
            var product = new Product(count, companyId, nomenclatureId);

            //Assert
            Assert.NotNull(product);
        }

        [Fact]
        public void Create_product_fail()
        {
            //Arrange
            var count = -1;
            var nomenclatureId = 1;
            var companyId = 1;

            //Act, Assert
            Assert.Throws<StorekeeperAssistantDomainException>(() => new Product(count, companyId, nomenclatureId));
        }

        [Theory]
        [MemberData(nameof(ReduceCountSuccessTestData))]
        public void Reduce_count_success(int productCount, int reduceCount)
        {
            //Arrange
            var count = productCount;
            var nomenclatureId = 1;
            var companyId = 1;

            //Act
            var product = new Product(productCount, companyId, nomenclatureId);

            product.ReduceCount(reduceCount);

            //Assert
            Assert.True(count - reduceCount == product.Count);
        }

        [Theory]
        [MemberData(nameof(ReduceCountFailTestData))]
        public void Reduce_count_fail(int productCount, int reduceCount)
        {
            //Arrange
            var nomenclatureId = 1;
            var companyId = 1;

            //Act
            var product = new Product(productCount, companyId, nomenclatureId);

            //Assert
            Assert.Throws<StorekeeperAssistantDomainException>(() => product.ReduceCount(reduceCount));
        }

        [Fact]
        public void Increase_count_success()
        {
            //Arrange
            var count = 10;
            var nomenclatureId = 1;
            var companyId = 1;

            //Act
            var product = new Product(count, companyId, nomenclatureId);

            product.IncreaseCount(count);

            //Assert
            Assert.True(count + count == product.Count);
        }

        [Theory]
        [MemberData(nameof(IncreaseCountFailTestData))]
        public void Increase_count_fail(int increaseCount)
        {
            //Arrange
            var count = 10;
            var nomenclatureId = 1;
            var companyId = 1;

            //Act
            var product = new Product(count, companyId, nomenclatureId);

            //Assert
            Assert.Throws<StorekeeperAssistantDomainException>(() => product.IncreaseCount(increaseCount));
        }

        public static IEnumerable<object[]> ReduceCountSuccessTestData()
        {
            yield return new object[] { 1, 1 };
            yield return new object[] { 2, 1 };
            yield return new object[] { 4, 3 };
        }

        public static IEnumerable<object[]> ReduceCountFailTestData()
        {
            yield return new object[] { 1, 100 };
            yield return new object[] { 1, 2 };
            yield return new object[] { 1000, 3555 };
            yield return new object[] { 1000, -3555 };
        }

        public static IEnumerable<object[]> IncreaseCountFailTestData()
        {
            yield return new object[] { 0 };
            yield return new object[] { -11 };
        }
    }
}
