using System.Collections.Generic;
using StorekeeperAssistant.Domain.AggregatesModel.ProductMovementAggregate;
using StorekeeperAssistant.Domain.Exceptions;
using Xunit;

namespace StorekeeperAssistant.UnitTests.Domain
{
    public class ProductMovementAggregateTest
    {
        [Fact]
        public void Create_product_movement_success()
        {
            //Arrange
            var companyId = 1;

            //Act
            var productMovement = new ProductMovement(companyId);

            //Assert
            Assert.NotNull(productMovement);
        }

        [Fact]
        public void Create_product_movement_fail()
        {
            //Arrange
            var companyId = 1;
            var companyId2 = 1;

            //Act, Assert
            Assert.Throws<StorekeeperAssistantDomainException>(() => new ProductMovement(companyId, companyId2));
        }

        [Fact]
        public void Add_nomenclature_movement_success()
        {
            //Arrange
            var companyId = 1;
            var nomenclatureMovementList = new List<NomenclatureMovement>
            {
                new(1, 1),
                new(2, 2)
            };

            //Act
            var productMovement = new ProductMovement(companyId);
            foreach (var nomenclatureMovement in nomenclatureMovementList)
            {
                productMovement.AddNomenclatureMovement(nomenclatureMovement.NomenclatureId, nomenclatureMovement.Count);
            }

            //Assert
            Assert.True(nomenclatureMovementList.Count == productMovement.NomenclatureMovements.Count);
        }

        [Fact]
        public void Add_nomenclature_movement_fail()
        {
            //Arrange
            var companyId = 1;
            var nomenclatureMovement = new NomenclatureMovement(1, 1);

            //Act
            var productMovement = new ProductMovement(companyId);
            productMovement.AddNomenclatureMovement(nomenclatureMovement.NomenclatureId, nomenclatureMovement.Count);

            //Assert
            Assert.Throws<StorekeeperAssistantDomainException>(() =>
                productMovement.AddNomenclatureMovement(nomenclatureMovement.NomenclatureId, nomenclatureMovement.Count));
        }

        [Fact]
        public void Add_nomenclature_movement_fail_necative_count()
        {
            //Arrange
            var companyId = 1;
            var nomenclatureMovement = new NomenclatureMovement(1, -1);

            //Act
            var productMovement = new ProductMovement(companyId);

            //Assert
            Assert.Throws<StorekeeperAssistantDomainException>(() =>
                productMovement.AddNomenclatureMovement(nomenclatureMovement.NomenclatureId, nomenclatureMovement.Count));
        }
    }
}
