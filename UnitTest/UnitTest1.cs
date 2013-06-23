using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Foogle_WPF;


namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
    

        [TestMethod]
        public void CleanStringTest()
        {
            StudentSearch ss = new StudentSearch();

            String t1 = "Uppercase letter";

            String tt1 = ss.cleanString(t1);

            Assert.AreEqual("uppercase letter", tt1);

            t1 = "  trim   ";

            tt1 = ss.cleanString(t1);

            Assert.AreEqual("trim", tt1);

        }




        //LinkedIn test methods

        [TestMethod]
        public void GetYearsOfExperienceFromLinkedInTest()
        {
            StudentRegistration sr = new StudentRegistration();

            StudentRegistration.access_token = "123";

            double status = sr.getYearsOfExperienceFromLinkedIn();

            //without access_code, always false
            Assert.AreEqual(0, status);

        }

        //LinkedIn test methods

        [TestMethod]
        public void GetUserEmailFromLinkedInTest()
        {
            StudentRegistration sr = new StudentRegistration();

            StudentRegistration.access_token = "123";

            string status = sr.getUserEmailFromLinkedIn();

            //without access_code, always false
            Assert.AreEqual("", status);

        }

        [TestMethod]
        public void ConvertDifferenceToYearsTest()
        {
            StudentRegistration sr = new StudentRegistration();

            //expected: 5 years
            DateTime d1 = new DateTime(2007, 1, 1);
            DateTime d2 = new DateTime(2012, 1, 1);

            double diff = Math.Round( sr.ConvertDifferenceToYears(d1, d2) );

            Assert.AreEqual(5, diff);


            //Expected: 2 years
            d1 = new DateTime(2010, 2, 1);
            d2 = new DateTime(2012, 7, 1);

            diff = Math.Round(sr.ConvertDifferenceToYears(d1, d2));

            Assert.AreEqual(2, diff);

        }



        [TestMethod]
        public void IsValidEmailTest()
        {
            StudentRegistration sr = new StudentRegistration();

            string e1 = "someemail@net.hr";

            bool s = sr.IsValidEmail(e1);

            Assert.AreEqual(true, s);

            string e2 = "some_long_email@hotmail.com";
            bool s2 = sr.IsValidEmail(e2);

            Assert.AreEqual(true, s2);


            string e3 = "invalid email";

            bool s3 = sr.IsValidEmail(e3);
            Assert.AreEqual(false, s3);

        }



    }
}
