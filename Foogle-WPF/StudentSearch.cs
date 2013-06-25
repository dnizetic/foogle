using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Npgsql;

namespace Foogle_WPF
{
    public class StudentSearch
    {
        public String[] finalSuggestions = new String[10];
        public string searchQuery = "";
        string prefix = "";

        public String[] SuggestSkillsSqlite(String searchText, String prefix)
        {
            finalSuggestions = new String[10];
            this.searchQuery = CleanString(searchText);
            this.prefix = prefix;

            Thread populateSkillsThread = new Thread(new ThreadStart(PopulateSuggestSkills));

            populateSkillsThread.Start();
            populateSkillsThread.Join();

            return finalSuggestions;
        }

        public string CleanString(String input)
        {
            return input.Trim().ToLower();
        }

        private void PopulateSuggestSkills()
        {
            using (var context = new FoogleContext())
            {
                var suggestions = from suggestion in context.Skills
                                  where suggestion.name.ToLower().Contains(searchQuery.ToLower())
                                  select suggestion;

                int i = 0;
                foreach (var suggestion in suggestions)
                {
                    if (i < 10)
                    {
                        String result = prefix;
                        result += suggestion.name;
                        finalSuggestions[i++] = result;
                    }
                }
            }
        }


        public static Boolean UserHasSkill(int user_id, int skill_id)
        {
            try
            {
                using (var context = new FoogleContext())
                {
                    //MessageBox.Show("user:id " + user_id + ", skill_id: " + skill_id);

                    var userSkillsMenuItem = from b in context.UserSkills
                                             where b.user.id.Equals(user_id)
                                             where b.skill.id.Equals(skill_id)
                                             select b;

                    if (userSkillsMenuItem.Count() > 0)
                        return true;
                }
            }
            catch { } 
            return false;
        }


        private string searchText = "";

        List<UserMatch> userInfoList = new List<UserMatch>();
        public void SearchByQuery(String search)
        {
            this.searchText = search;

            Thread querySearchThread = new Thread(new ThreadStart(SearchByQueryThread));

            querySearchThread.Start();
            querySearchThread.Join();

            SearchBySkillResults searchResults = new SearchBySkillResults(userInfoList);

            searchResults.Show();
        }


        public void SearchByQueryThread()
        {
            String[] skills = searchText.Split(' ');

            //get all students, calculate # of matched skills
            var userInfo = new List<UserMatch>();
            try
            {
                using (var context = new FoogleContext())
                {
                    var students = from user in context.Users
                                   where user.role.Equals("s")
                                   select user;

                    foreach (FoogleUser student in students)
                    {
                        int matchesCount = 0;

                        foreach (String skill in skills)
                        {
                            //get skill id
                            using (var context2 = new FoogleContext())
                            {
                                var matchedSkills = from match in context2.Skills
                                                     where match.name.Equals(skill)
                                                     select match;

                                //unsuccessfull map, continue
                                if (matchedSkills.Count() == 0)
                                    continue;

                                Skill mappedSkill = matchedSkills.First();
                                if (mappedSkill != null && UserHasSkill(Convert.ToInt32(student.id), Convert.ToInt32(mappedSkill.id)))
                                {
                                    ++matchesCount;
                                }
                            }
                        }

                        //store in an array
                        userInfo.Add(new UserMatch
                        {
                            num_matches = matchesCount,
                            user = student,
                            email = student.email,
                            firstname = student.firstname,
                            lastname = student.lastname,
                            linkedin = student.linkedin,
                            exp = student.exp,
                            id = student.id
                        });

                    }
                    userInfoList = userInfo.OrderByDescending(si => si.num_matches).ToList();
                }
            }
            catch { }
        }
    }
}
