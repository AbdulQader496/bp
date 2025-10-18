using BPCalculator;
using BPCalculator.Pages;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TestProject1
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public void SystolicGreaterThanDiastolicErrorTest()
        {
            var page = new BloodPressureModel();
            page.BP = new BloodPressure
            {
                Systolic = 100,
                Diastolic = 100
            };

            page.OnPost();

            // Assert (directly assert the single, model-level error message)
            Assert.AreEqual(
                "Systolic must be greater than Diastolic",
                page.ModelState[string.Empty].Errors.Single().ErrorMessage
            );
        }

        [TestMethod]
        public void SystolicAndDiastolicValueOutOfRange()
        {
            var bp = new BloodPressure
            {
                Systolic = 210, // out of range
                Diastolic = 110 // out of range
            };
            var ctx = new ValidationContext(bp);
            var results = new List<ValidationResult>();
            var valid = Validator.TryValidateObject(bp, ctx, results, validateAllProperties: true);

            Assert.IsTrue(results.Any(r => r.ErrorMessage == "Invalid Systolic Value"));
            Assert.IsTrue(results.Any(r => r.ErrorMessage == "Invalid Diastolic Value"));

        }

        [TestMethod]
        public void LowBloodPressureCategoryTest()
        {
            var bp = new BloodPressure
            {
                Systolic = 85,
                Diastolic = 55
            };
            Assert.AreEqual(BPCategory.Low, bp.Category);
        }

        [TestMethod]
        public void IdealBloodPressureCategoryTest()
        {
            var bp = new BloodPressure
            {
                Systolic = 115,
                Diastolic = 75
            };
            Assert.AreEqual(BPCategory.Ideal, bp.Category);

        }

        [TestMethod]
        public void PreHighBloodPressureCategoryTest()
        {
            var bp = new BloodPressure
            {
                Systolic = 140,
                Diastolic = 89
            };
            Assert.AreEqual(BPCategory.PreHigh, bp.Category);
        }

        [TestMethod]
        public void HighBloodPressureCategoryTest()
        {
            var bp = new BloodPressure
            {
                Systolic = 190,
                Diastolic = 100
            };
            Assert.AreEqual(BPCategory.High, bp.Category);
        }

    }
}
