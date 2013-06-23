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

        public String[] suggs = new String[10];
        public string searchQuery = "";
        string prefix = "";

        public String[] SuggestSkillsSqlite(String searchText, String prefix)
        {
            suggs = new String[10];
            this.searchQuery = cleanString( searchText );


            this.prefix = prefix;

            Thread t2 = new Thread(new ThreadStart(PopulateSuggestSkills));

            t2.Start();
            t2.Join();


            return suggs;
        }

        public string cleanString(String input)
        {
            String new_str = "";

            new_str = input.Trim().ToLower();

            return new_str;
        }

        private void PopulateSuggestSkills()
        {

            using (var context = new FoogleContext())
            {
                //MessageBox.Show("user:id " + user_id + ", skill_id: " + skill_id);

                var suggestions = from s in context.Skills
                                  where s.name.ToLower().Contains(searchQuery.ToLower())
                                  select s;

                int i = 0;
                foreach (var s in suggestions)
                {
                    if (i < 10)
                    {
                        String res = prefix;
                        res += s.name;
                        suggs[i++] = res;
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

                    var user_skills = from b in context.UserSkills
                                      where b.user.id.Equals(user_id)
                                      where b.skill.id.Equals(skill_id)
                                      select b;

                    if (user_skills.Count() > 0)
                        return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message + ex.InnerException);
                return false;
            }
        }


        private string searchText = "";

        List<UserMatch> user_info_thread = new List<UserMatch>();
        public void SearchByQuery(String search)
        {
            this.searchText = search;

            Thread t1 = new Thread(new ThreadStart(SearchByQueryThread));

            t1.Start();
            t1.Join();

            SearchBySkillResults ssr = new SearchBySkillResults(user_info_thread);

            ssr.Show();
        }
        

        public void SearchByQueryThread()
        {
                
            String[] skills = searchText.Split(' ');

            //get all students, calculate # of matched skills
            var user_info = new List<UserMatch>();

            try
            {
                using (var context = new FoogleContext())
                {
                    var students = from b in context.Users
                                   where b.role.Equals("s")
                                   select b;


                    foreach (FoogleUser f in students)
                    {

                        int num_matched = 0;


                        foreach (String s in skills)
                        {
                            //get skill id
                            using (var context2 = new FoogleContext())
                            {
                                var matched_skills = from d in context2.Skills
                                                     where d.name.Equals(s)
                                                     select d;


                                //unsuccessfull map, continue
                                if (matched_skills.Count() == 0)
                                    continue;

                                Skill mapped_skill = matched_skills.First();

                                //MessageBox.Show("Mapped skill: " + mapped_skill.name);

                                if (mapped_skill != null &&
                                    UserHasSkill(Convert.ToInt32(f.id), Convert.ToInt32(mapped_skill.id)))
                                {
                                    ++num_matched;
                                }

                            }

                        }


                        //store in an array
                        user_info.Add(new UserMatch
                        {
                            num_matches = num_matched,
                            user = f,
                            email = f.email,
                            firstname = f.firstname,
                            lastname = f.lastname,
                            linkedin = f.linkedin,
                            exp = f.exp,
                            id = f.id
                        });

                    }

                    user_info_thread = user_info.OrderByDescending(si => si.num_matches).ToList();
                    
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message + ex.InnerException);
            }

        }

    }
}
