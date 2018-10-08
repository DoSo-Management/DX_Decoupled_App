using System;
using DAL.BusinessObjects;
using DAL.ValueObjects;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using NUnit.Framework;
using Shouldly;

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

        [Test(Description = "როდესაც ვითვლით პოლისის პრემიას, სადაზღვევო თანხა უნდა გამრავლდეს რეითზე")]
        public void როდესაც_ვითვლით_პოლისის_პრემიას_სადაზღვევო_თანხა_უნდა_გამრავლდეს_რეითზე()
        {
            // Arrange
            var policy = new Policy(UnitOfWork) { Rate = 2.3m, SumInsured = 100 };

            // Act
            new PolicyBl().CalculatePremium(policy); 

            // Assert
            policy.Premium.ShouldBe(230);  
        }

        [Test(Description = "როდესაც ვითვლით პოლისის პრემიას, სადაზღვევო თანხა უნდა გამრავლდეს რეითზე")]
        [TestCase(10, 5, 50)]
        [TestCase(10, 0, 0)]
        [TestCase(.1, 50, 5)]
        public void PolicyBl_CalculatePremium_2(decimal sumInsured, decimal rate, decimal premiumResult)
        {
            // Arrange
            var pol = new Policy(UnitOfWork) { Rate = rate, SumInsured = sumInsured };

            // Act
            new PolicyBl().CalculatePremium(pol);

            // Assert
            pol.Premium.ShouldBe(premiumResult);
        }

        [Test(Description = "როდესაც ვქმნით პოლის პრემიას ნულზე ნაკლები პრემია არ უნდა იყოს დასაშვები")]
        public void PolicyPremium_lt_zero_not_allowed()
        {
            // Arrange
            // Act
            // Assert
            PolicyPremium.Create(-5, new Currency(UnitOfWork)).IsFailure.ShouldBeTrue();
        }

        [Test(Description = "ცარიელი Currency და CurrencyName არ არის დასაშვები")]
        public void PolicyPremium_empty_currency_not_allowed()
        {
            // Arrange
            // Act
            // Assert
            PolicyPremium.Create(0, null).IsFailure.ShouldBeTrue();
            PolicyPremium.Create(0, new Currency(UnitOfWork) { CurrencyName = null }).IsFailure.ShouldBeTrue();
        }
    }
}
