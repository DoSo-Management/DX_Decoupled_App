﻿using System;
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
        public void PolicyBl_CalculatePremium()
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
            var policy = new Policy(UnitOfWork) { Rate = rate, SumInsured = sumInsured };

            // Act
            new PolicyBl().CalculatePremium(policy);

            // Assert
            policy.Premium.ShouldBe(premiumResult);
        }

        [Fact(DisplayName = "როდესაც ვქმნით პოლის პრემიას ნულზე ნაკლები პრემია არ უნდა იყოს დასაშვები")]
        public void PolicyPremium_lt_zero_not_allowed()
        {
            // Arrange
            var policy = new Policy(UnitOfWork);
            // Act
            // Assert
            Should.Throw<InvalidOperationException>(() => new PolicyPremium(-5, new Currency(UnitOfWork)));
        }

        [Fact(DisplayName = "ცარიელი Currency და CurrencyName არ არის დასაშვები")]
        public void PolicyPremium_empty_currency_not_allowed()
        {
            // Arrange
            // Act
            // Assert
            Should.Throw<InvalidOperationException>(() => new PolicyPremium(0, null));
            Should.Throw<InvalidOperationException>(() => new PolicyPremium(0, new Currency(UnitOfWork) { CurrencyName = null }));
        }
    }
}