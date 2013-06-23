using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Foogle_WPF;


namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void duplicateSkillDatabaseTest()
        {
            StudentRegistration sr = new StudentRegistration();

            sr.storeUniqueSkillToDatabase("testSkill");

            string num_skills = sr.getNumberOfSkills();

            sr.storeUniqueSkillToDatabase("testSkill");

            string new_num_skills = sr.getNumberOfSkills();

            Assert.AreEqual(num_skills, "ha");
            Assert.AreNotEqual(num_skills, "dad");
            
        }


        [TestMethod]
        public void suggestSkillsTest()
        {
            

            StudentRegistration sr = new StudentRegistration();
            sr.storeUniqueSkillToDatabase("testSkill1");
            sr.storeUniqueSkillToDatabase("testSkill2");
            sr.storeUniqueSkillToDatabase("testSkill3");

            //ako upisemo "test" moramo dobiti 2 vjestine natrag
            StudentSearch ss = new StudentSearch();

            String[] arr = ss.SuggestSkillsSqlite("testSkill", "");

            int num = 0;
            foreach (var a in arr)
            {
                //MessageBox.Show(num);
            }

            Assert.AreEqual(3, num);

        }


    }
}
