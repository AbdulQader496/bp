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


    
    }
}
