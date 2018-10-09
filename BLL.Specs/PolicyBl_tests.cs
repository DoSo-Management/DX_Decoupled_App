using DAL.BusinessObjects;
using DAL.ValueObjects;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using Shouldly;
using Xunit;

// ReSharper disable InconsistentNaming
namespace BLL.Specs
{
    public class PolicyBl_tests
    {
        public readonly UnitOfWork UnitOfWork;
        public PolicyBl_tests()
        {
            XpoDefault.DataLayer = new SimpleDataLayer(new InMemoryDataStore());
            UnitOfWork = new UnitOfWork();
        }

        [Fact(DisplayName = "როდესაც ვითვლით პოლისის პრემიას, სადაზღვევო თანხა უნდა გამრავლდეს რეითზე")]
        public void PolicyBl_CalculatePremium_ქართული_ტექსტი_123()
        {
            // Arrange
            var policy = new Policy(UnitOfWork) { Rate = 2.3m, SumInsured = 100 };

            // Act
            new PolicyBl().CalculatePremium(policy);

            // Assert
            policy.Premium.ShouldBe(230);
        }

        [Theory(DisplayName = "როდესაც ვითვლით პოლისის პრემიას, სადაზღვევო თანხა უნდა გამრავლდეს რეითზე")]
        [InlineData(10, 5, 50)]
        [InlineData(10, 0, 0)]
        [InlineData(.1, 50, 5)]
        public void PolicyBl_CalculatePremium_2(decimal sumInsured, decimal rate, decimal premiumResult)
        {
            // Arrange
            var pol = new Policy(UnitOfWork) { Rate = rate, SumInsured = sumInsured };

            // Act
            new PolicyBl().CalculatePremium(pol);

            // Assert
            pol.Premium.ShouldBe(premiumResult);
        }

        [Fact(DisplayName = "როდესაც ვქმნით პოლის პრემიას ნულზე ნაკლები პრემია არ უნდა იყოს დასაშვები")]
        public void PolicyPremium_lt_zero_not_allowed()
        {
            // Arrange
            // Act
            // Assert
            PolicyPremium.Create(-5, new Currency(UnitOfWork)).IsValid.ShouldBeFalse();
        }

        [Fact(DisplayName = "ცარიელი Currency და CurrencyName არ არის დასაშვები")]
        public void PolicyPremium_empty_currency_not_allowed()
        {
            // Arrange
            // Act
            // Assert
            PolicyPremium.Create(0, null).IsValid.ShouldBeFalse();
            PolicyPremium.Create(0, new Currency(UnitOfWork) { CurrencyName = null }).IsValid.ShouldBeFalse();
        }

        [Fact(DisplayName = "ორი ერთნაირი PolicyPremium უნდა იყოს ტოლი")]
        public void PolicyPremiums_should_be_equal()
        {
            // Arrange
            // Act
            // Assert
            PolicyPremium.Create(10, new Currency(UnitOfWork) { CurrencyName = "TEST" })
                .ShouldBe(
            PolicyPremium.Create(10, new Currency(UnitOfWork) { CurrencyName = "TEST" })
                         );
        }

        [Fact(DisplayName = "ორი სხვანაირი PolicyPremium -არ- უნდა იყოს ტოლი")]
        public void different_PolicyPremiums_should_NOT_be_equal()
        {
            // Arrange
            // Act
            // Assert
            PolicyPremium.Create(1, new Currency(UnitOfWork) { CurrencyName = "TEST" })
                .ShouldNotBe(
            PolicyPremium.Create(10, new Currency(UnitOfWork) { CurrencyName = "TEST" })
                );
        }

        [Fact(DisplayName = "ორი განსხვავებული არავალიდური PolicyPremium -არ- უნდა იყოს ტოლი")]
        public void PolicyPremiums_should_be_equal_even_with_invalid()
        {
            // Arrange
            // Act
            // Assert
            PolicyPremium.Create(-10, new Currency(UnitOfWork) { CurrencyName = "TEST" })
                .ShouldNotBe(
            PolicyPremium.Create(-1, new Currency(UnitOfWork) { CurrencyName = "TEST" })
                );
        }
    }
}
