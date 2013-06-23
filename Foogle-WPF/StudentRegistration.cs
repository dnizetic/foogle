using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;

namespace Foogle_WPF
{
    public class StudentRegistration
    {


        public bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static String access_token = "";


        public String getUserEmailFromLinkedIn()
        {

            try
            {

                string url = "https://api.linkedin.com/v1/people/~/email-address?oauth2_access_token=" + access_token;

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse resp = (HttpWebResponse)request.GetResponse();

                Stream resStream = resp.GetResponseStream();
                StreamReader rdr = new StreamReader(resStream);
                string text = rdr.ReadToEnd();

                //MessageBox.Show(text);



                using (XmlReader reader = XmlReader.Create(new StringReader(text)))
                {

                    reader.ReadToFollowing("email-address");
                    String email = reader.ReadElementContentAsString();

                    if(IsValidEmail(email))
                        return email;
                    
                    return "";

                }
            }
            catch (Exception err)
            {

                //MessageBox.Show(err.Message);

                return "";
            }

        }


        public double ConvertDifferenceToYears(DateTime a, DateTime b)
        {
            TimeSpan duration = b - a;

            double days = duration.TotalDays;

            double years = days / 365.242199;
            int wholeYears = (int)Math.Floor(years);

            double partYears = years - wholeYears;
            double approxMonths = partYears * 12;

            double total = wholeYears + (approxMonths / 12);

            return total;
        }


        public double getYearsOfExperienceFromLinkedIn()
        {

            try
            {
                string url = "https://api.linkedin.com/v1/people/~/positions?oauth2_access_token=" + access_token;

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse resp = (HttpWebResponse)request.GetResponse();

                Stream resStream = resp.GetResponseStream();
                StreamReader rdr = new StreamReader(resStream);
                string text = rdr.ReadToEnd();

                //Positions output
                //MessageBox.Show(text);


                using (XmlReader reader = XmlReader.Create(new StringReader(text)))
                {

                    reader.ReadToFollowing("positions");
                    reader.MoveToFirstAttribute();
                    String amount = reader.Value;
                    //MessageBox.Show(amount);

                    double sum = 0;

                    int j = Convert.ToInt32(amount);
                    for (int i = 0; i < j; ++i)
                    {
                        reader.ReadToFollowing("year");

                        int start_year = reader.ReadElementContentAsInt();

                        reader.ReadToFollowing("month");
                        int start_month = reader.ReadElementContentAsInt();

                        reader.ReadToFollowing("year");
                        int end_year = reader.ReadElementContentAsInt();

                        reader.ReadToFollowing("month");
                        int end_month = reader.ReadElementContentAsInt();

                        DateTime a = new DateTime(start_year, start_month, 01);
                        DateTime b = new DateTime(end_year, end_month, 01);

                        double total = ConvertDifferenceToYears(a, b);

                        sum += total;
                    }

                    //MessageBox.Show("Total exp: " + sum);

                    return sum;

                }
            }
            catch (Exception err)
            {

                //MessageBox.Show(err.Message);

                return 0;
            }

        }
    




        //save methods
        private int regged_user_id = 0;
        public void saveUser(string first_name, string last_name, string lin,
            string lid, double experience)
        {
            try
            {
                using (var context = new FoogleContext())
                {

                    FoogleUser regged_user = new FoogleUser
                    {
                        email = getUserEmailFromLinkedIn(),
                        confirmed = true,
                        firstname = first_name,
                        lastname = last_name,
                        role = "s",
                        activity = null,
                        password = "",
                        linkedin = lin,
                        linkedin_id = lid,
                        exp = experience
                    };

                    context.Users.Add(regged_user);

                    context.SaveChanges();

                    regged_user_id = Convert.ToInt32(regged_user.id);

                }

                MessageBox.Show("Uspjesno ste uneseni u bazu podataka.");

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message + ex.InnerException);
            }
        }





        public void storeUserSkills()
        {
            string url = "https://api.linkedin.com/v1/people/~/skills?oauth2_access_token=" + access_token;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse resp = (HttpWebResponse)request.GetResponse();

            Stream resStream = resp.GetResponseStream();
            StreamReader rdr = new StreamReader(resStream);
            string text = rdr.ReadToEnd();

            //MessageBox.Show(text);


            try
            {
                using (XmlReader reader = XmlReader.Create(new StringReader(text)))
                {

                    while (true)
                    {
                        reader.ReadToFollowing("name");
                        String sname = reader.ReadElementContentAsString();


                        String skill_to_lower = sname.ToLower();
                        storeUniqueSkillToDatabase(skill_to_lower);

                        storeUserSkill(skill_to_lower);

                        if (sname == null)
                            break;
                    }


                }
            }
            catch (Exception err)
            {

                //MessageBox.Show(err.Message);
            }
        }


        //OK
        private void storeUserSkill(String skill)
        {

            try
            {
                using (var context = new FoogleContext())
                {
                    var skills = from b in context.Skills
                                 where b.name.Equals(skill)
                                 select b;



                    Skill s = skills.First();
                    FoogleUser u = context.Users.SingleOrDefault(c => c.id == regged_user_id);

                    if (!StudentSearch.UserHasSkill(Convert.ToInt32(u.id), Convert.ToInt32(s.id)))
                    {
                        context.UserSkills.Add(
                            new UserSkills
                            {
                                skill = s,
                                user = u
                            });

                        context.SaveChanges();
                    }


                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message + ex.InnerException);
            }


        }

        //Test
        public void storeUniqueSkillToDatabase(String skill)
        {
            try
            {
                using (var context = new FoogleContext())
                {
                    var skills = from b in context.Skills
                                 where b.name.Equals(skill)
                                 select b;

                    if (skills.Count() == 0)
                    {
                        //MessageBox.Show("Unosim novi skill: " + skill);
                        context.Skills.Add(
                            new Skill
                            {
                                name = skill
                            });

                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message + ex.InnerException);
            }


        }

        


        public void saveIfUserDoesntExist(String myXml, double exp)
        {
            try
            {
                using (XmlReader reader = XmlReader.Create(new StringReader(myXml)))
                {

                    reader.ReadToFollowing("first-name");
                    String fname = reader.ReadElementContentAsString();


                    reader.ReadToFollowing("last-name");
                    String lname = reader.ReadElementContentAsString();

                    reader.ReadToFollowing("url");
                    String lin = reader.ReadElementContentAsString();

                    //= and &
                    int startIndex = lin.IndexOf('=');
                    int endIndex = lin.IndexOf('&');
                    string id = lin.Substring(startIndex + 1, endIndex - startIndex - 1);


                    //check if lid already exists
                    using (var context = new FoogleContext())
                    {
                        var usrs = from b in context.Users
                                   where b.linkedin_id.Equals(id)
                                   select b;

                        if (usrs.Count() > 0)
                        {
                            MessageBox.Show("Vec ste registrirani.");
                            return;
                        }
                    }

                    saveUser(fname, lname, lin, id, exp);
                }
            }
            catch (Exception err)
            {


            }
        }


    }
}
