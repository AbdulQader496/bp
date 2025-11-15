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
        public void SystolicAndDiastolicValueOutOfUpperRange()
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
        public void SystolicAndDiastolicValueOutOfLowerRange()
        {
            var bp = new BloodPressure
            {
                Systolic = 69, // out of range
                Diastolic = 39 // out of range
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
                Systolic = 130,
                Diastolic = 85
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

        [TestMethod]
        public void BoundaryCategoryTests()
        {
            Assert.AreEqual(BPCategory.Low,
                new BloodPressure { Systolic = 89, Diastolic = 59 }.Category);

            Assert.AreEqual(BPCategory.Ideal,
                new BloodPressure { Systolic = 90, Diastolic = 60 }.Category);

            Assert.AreEqual(BPCategory.Ideal,
                new BloodPressure { Systolic = 120, Diastolic = 80 }.Category);

            Assert.AreEqual(BPCategory.PreHigh,
                new BloodPressure { Systolic = 121, Diastolic = 81 }.Category);

            Assert.AreEqual(BPCategory.PreHigh,
                new BloodPressure { Systolic = 139, Diastolic = 89 }.Category);

            Assert.AreEqual(BPCategory.High,
                new BloodPressure { Systolic = 140, Diastolic = 90 }.Category);
        }

        [TestMethod]
        public void OnGet_DefaultValuesTest()
        {
            var page = new BloodPressureModel();
            page.OnGet();

            Assert.AreEqual(100, page.BP.Systolic);
            Assert.AreEqual(60, page.BP.Diastolic);
        }



        //[TestMethod]
        //public void PageModel_InitializesBPProperty()
        //{
        //    var page = new BloodPressureModel();

        //    Assert.IsNotNull(page.BP);
        //}
        //[TestMethod]
        //public void OnPost_ModelStateErrorsRemainOnReturn()
        //{
        //    var page = new BloodPressureModel();
        //    page.ModelState.AddModelError("Test", "Error");

        //    var result = page.OnPost();

        //    Assert.IsFalse(page.ModelState.IsValid);
        //}

        [TestMethod]
        public void PageModel_InitializesBPProperty_OnGet()
        {
            var page = new BloodPressureModel();

            page.OnGet(); // THIS is what initializes BP

            Assert.IsNotNull(page.BP);
            Assert.AreEqual(100, page.BP.Systolic);
            Assert.AreEqual(60, page.BP.Diastolic);
        }


        [TestMethod]
        public void OnPost_ModelStateErrorsRemainOnReturn()
        {
            var page = new BloodPressureModel();

            // initialize BP to avoid null
            page.BP = new BloodPressure { Systolic = 120, Diastolic = 80 };

            page.ModelState.AddModelError("Test", "Error");

            var result = page.OnPost();

            Assert.IsFalse(page.ModelState.IsValid);
            Assert.IsInstanceOfType(result, typeof(PageResult));
        }


        [TestMethod]
        public void OnPost_ValidValues_ModelStateRemainsValid()
        {
            var page = new BloodPressureModel();
            page.BP = new BloodPressure { Systolic = 120, Diastolic = 80 };

            var result = page.OnPost();

            Assert.IsTrue(page.ModelState.IsValid);
            Assert.IsInstanceOfType(result, typeof(PageResult));
        }

        [TestMethod]
        public void OnPost_SystolicLessThanOrEqualToDiastolic_AddsModelError()
        {
            var page = new BloodPressureModel();
            page.BP = new BloodPressure { Systolic = 100, Diastolic = 100 };

            page.OnPost();

            Assert.IsFalse(page.ModelState.IsValid);
            Assert.AreEqual("Systolic must be greater than Diastolic",
                page.ModelState[string.Empty].Errors.Single().ErrorMessage);
        }

        [TestMethod]
        public void CategoryFallbackTest()
        {
            var bp = new BloodPressure
            {
                Systolic = 100,  // ideal range
                Diastolic = 70
            };

            Assert.AreEqual(BPCategory.Ideal, bp.Category);
        }


    }
}
