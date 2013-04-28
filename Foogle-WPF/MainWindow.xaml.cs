using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;

using Npgsql;
using System.Diagnostics;
using System.Net;
using System.IO;
using System.Xml;
using mshtml;


namespace Foogle_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            InitialiseDatabaseConnection();

        }

        //activates when professor logs in
        public static bool logged_in = false;

        //set it when professor logs in
        public static int professor_id = 0;

        public static void showButt()
        {
            
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            Login lw = new Login(LoggedInLabel);

            lw.Show();

        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Provjeri login status, dozvoli samo ako je prijavljen
            // Tražilica bi trebala raditi i na način da se mogu pretražiti korisnici po imenu ili bilo čemu drugome

            //simple sort: # of skills matched (no formula)
            string searchText = searchBox.Text;

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



                                Skill mapped_skill = matched_skills.First();

                                //MessageBox.Show("Mapped skill: " + mapped_skill.name);

                                if (mapped_skill != null &&
                                    UserHasSkill(f.id, mapped_skill.id))
                                {
                                    ++num_matched;
                                }

                            }

                        }

                        //MessageBox.Show("Num skills matched: " + num_matched);

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

                    var sortedList = user_info.OrderByDescending(si => si.num_matches).ToList();

                    foreach (UserMatch u in sortedList)
                    {
                        MessageBox.Show("Num skills matched: " + u.num_matches + ", user: " + u.user.firstname);
                    }

                    SearchBySkillResults ssr = new SearchBySkillResults(sortedList);

                    ssr.Show();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.InnerException);
            }

        }

        private Boolean UserHasSkill(int user_id, int skill_id)
        {
            try
            {
                using (var context = new FoogleContext())
                {
                    MessageBox.Show("user:id " + user_id + ", skill_id: " + skill_id);

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
                MessageBox.Show(ex.Message + ex.InnerException);
                return false;
            }
        }

        //called on every character deletion / add
        private void searchBoxPopulating(object sender, 
                System.Windows.Controls.PopulatingEventArgs e)
        {
            string text = searchBox.Text;

            if (text.Length > 0)
            {

                //get last word
                string lastWord = text.Substring(text.LastIndexOf(' ') + 1);

                //pass prefix to all sugestions
                int index1 = text.LastIndexOf(' ');
                string prefix = "";
                if(index1 != -1)
                    prefix = text.Substring(0, index1 + 1);
                //MessageBox.Show(prefix);

                //MessageBox.Show(lastWord);

                string[] arr = SuggestSkills(lastWord, prefix);

                foreach (String s in arr)
                {
                    //if(text.Length > 3)
                    //    MessageBox.Show(s);
                }

                AutoCompleteBox b = (AutoCompleteBox) sender;
                
                b.ItemsSource = arr;

                searchBox.PopulateComplete();
            }

        }


        /*
         * SuggestSkills()
         *
         * Based on current text in the search box, this
         * function returns a set of suggestions for the
         * user.
         * 
         * Example: user types "p", suggestions: "php, ..."
         * Example: user types "q", suggestion: "jquery, ..."
         * Examples are listed in a ListBox below the TextBox
         *
         * @param (String current_text) current text typed in the search box
         * @return (String[]) array of suggestions (strings)
         */
        private DataSet ds = new DataSet();
        private DataTable dt = new DataTable();

        private String[] SuggestSkills(String searchText, String prefix)
        {
            try
            {
                
                string sql = "select * from skill where lower(name) LIKE '%" + searchText.ToLower() + "%' limit 10;";
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, sqlConnection);

                ds.Reset();

                da.Fill(ds);
                // since C# DataSet can handle multiple tables, we will select first
                dt = ds.Tables[0];
                // connect grid to DataTable
                //dataGridView1.DataSource = dt;  


                // TODO: Mislim da bi trebalo tu izbacit van tablicu s imenima, popisom skillova i bodovima =)
                String[] suggestions = new String[10];
                int i = 0;
                foreach (DataRow row in dt.Rows)
                {
                    //suggest all until last word

                    String s = prefix;
                    s += row["name"] as String;
                    suggestions[i++] = s.ToLower();

                }

                return suggestions;

            }
            catch (Exception msg)
            {
                MessageBox.Show(msg.ToString());
                throw;
            }
        }



        public static NpgsqlConnection sqlConnection = null;
        private void InitialiseDatabaseConnection()
        {
            try
            {
                // PostgeSQL-style connection string
                string connectionString = String.Format("Server={0};Port={1};" +
                    "User Id={2};Password={3};Database={4};",
                    "localhost", "5432", "postgres", "alphaomega", "foogle"); // lokalno
                    //"161.53.120.202", "5432", "pi2013-20", "pi2013-20!#", "pi2013-20"); // miro.foi.hr

                sqlConnection = new NpgsqlConnection(connectionString);
                sqlConnection.Open();

            }
            catch (Exception msg)
            {
                MessageBox.Show(msg.ToString());
                MessageBox.Show("\tAko ne radi povezivanje te sje kod istog pokazuje greška no hg_pba.conf for bla bla bla il tako nešto, onda vam IP range-u nije dopušten pristup serveru te molim da mi u tom slučaju pošaljete svoj IP na zstrahij@foi.hr\n\tIsto tako, može biti da connection string nije dobar, provjerite isti, no ovaj slučaj je vrlo malo vjerojatan! " +
                                "Isto tako, problem može biti da aplikacija nema pristup bazi jer baza ili nije na svome mjestu ili ne radi server.");
                throw; // nema baze ili veze - nema koristi od aplikacije.
            }
        }

        private void searchBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        //NavigationWindow window = new NavigationWindow();
        private void UserRegisterButton(object sender, RoutedEventArgs e)
        {
           
            //r_fullprofile & r_emailaddress
            webBrowser1.Navigate("https://www.linkedin.com/uas/oauth2/authorization?response_type=code&client_id=y0ubjypbbvov&state=mylongstring&redirect_uri=http://www.google.com&scope=r_emailaddress r_fullprofile");

        }


        bool xml_page = false;
        string access_token = "";

        private void wbCompleted(object sender, NavigationEventArgs e)
        {
            if (xml_page)
            {

                string url = "https://api.linkedin.com/v1/people/~?oauth2_access_token=" + access_token;

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse resp = (HttpWebResponse)request.GetResponse();

                Stream resStream = resp.GetResponseStream();
                StreamReader reader = new StreamReader(resStream);
                string text = reader.ReadToEnd();

                MessageBox.Show(text);

                //the following two functions should be called periodically by the administrator
                //in case the user adds/removes skills / employment history


                //store years of experience (float)
                double exp = getYearsOfExperience();

                storeBasicUser(text, exp);


                //store skills
                storeUserSkills();


                xml_page = false; 
                return;
            }


            String uri = webBrowser1.Source.AbsoluteUri;

            String[] code = uri.Split('&');
            String temp = code[0];
            String[] new_code = temp.Split('=');

            String final_code = "";
            if (new_code.Length > 1)
            {
                final_code = new_code[1];
            }
            

            //first return
            if (uri.Contains("code="))
            {
                //MessageBox.Show("Im in");


                HttpWebRequest httpWReq =
                    (HttpWebRequest)WebRequest.Create("https://www.linkedin.com/uas/oauth2/accessToken?grant_type=authorization_code&code=" + final_code + @"&redirect_uri=http://www.google.com&client_id=y0ubjypbbvov&client_secret=iwxSaObfSq6X9szw");

                //MessageBox.Show(httpWReq.RequestUri.AbsoluteUri);
                ASCIIEncoding encoding = new ASCIIEncoding();
                string postData = "username=user";
                postData += "&password=pass";
                byte[] data = encoding.GetBytes(postData);

                httpWReq.Method = "POST";
                
                httpWReq.ContentType = "application/x-www-form-urlencoded";
                httpWReq.ContentLength = data.Length;

                using (Stream stream = httpWReq.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                HttpWebResponse response = null;
                try
                {
                    response = (HttpWebResponse) httpWReq.GetResponse();

                    string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                    //MessageBox.Show(responseString);

                    int startIndex = responseString.LastIndexOf(':') + 2;
                    int len = responseString.Count() - 2 - startIndex;

                    access_token = responseString.Substring(startIndex, len);

                    //File f = new File("access_tokens.txt");
                    
                    //MessageBox.Show(access_token);

                    webBrowser1.Navigate("https://api.linkedin.com/v1/people/~?oauth2_access_token=" + access_token);


                    //email
                    //webBrowser1.Navigate("https://api.linkedin.com/v1/people/~/email-address?oauth2_access_token=" + access_token);
                
                    

                    //skills
                    //webBrowser1.Navigate("https://api.linkedin.com/v1/people/~/skills?oauth2_access_token=" + access_token);

                    //companies
                    //webBrowser1.Navigate("https://api.linkedin.com/v1/people/~/positions?oauth2_access_token=" + access_token);

                    xml_page = true;

                }
                catch (Exception re)
                {
                    MessageBox.Show(re.Message);
                }
            }

        }

        private String getUserEmail()
        {

            string url = "https://api.linkedin.com/v1/people/~/email-address?oauth2_access_token=" + access_token;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse resp = (HttpWebResponse)request.GetResponse();

            Stream resStream = resp.GetResponseStream();
            StreamReader rdr = new StreamReader(resStream);
            string text = rdr.ReadToEnd();

            MessageBox.Show(text);


            try
            {
                using (XmlReader reader = XmlReader.Create(new StringReader(text)))
                {

                    reader.ReadToFollowing("email-address");
                    String email = reader.ReadElementContentAsString();

                    return email;

                }
            }
            catch (Exception err)
            {

                MessageBox.Show(err.Message);

                return "";
            }



        }


        private void storeUserSkills()
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

                MessageBox.Show(err.Message);
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

                    if (!UserHasSkill(u.id, s.id))
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
                MessageBox.Show(ex.Message + ex.InnerException);
            }


        }


        private void storeUniqueSkillToDatabase(String skill)
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
                        MessageBox.Show("Unosim novi skill: " + skill);
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
                MessageBox.Show(ex.Message + ex.InnerException);
            }


        }


        private double getYearsOfExperience()
        {

            string url = "https://api.linkedin.com/v1/people/~/positions?oauth2_access_token=" + access_token;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse resp = (HttpWebResponse)request.GetResponse();

            Stream resStream = resp.GetResponseStream();
            StreamReader rdr = new StreamReader(resStream);
            string text = rdr.ReadToEnd();

            MessageBox.Show(text);


            try
            {
                using (XmlReader reader = XmlReader.Create(new StringReader(text)))
                {

                    reader.ReadToFollowing("positions");
                    reader.MoveToFirstAttribute();
                    String amount = reader.Value;
                    //MessageBox.Show(amount);

                    double sum = 0;
                    
                    int j = Convert.ToInt32(amount);
                    for(int i = 0; i < j; ++i)
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

                        TimeSpan duration = b - a;

                        double days = duration.TotalDays;

                        double years = days / 365.242199;
                        int wholeYears = (int)Math.Floor(years);

                        double partYears = years - wholeYears;
                        double approxMonths = partYears * 12;

                        MessageBox.Show("Years of exp in company: " 
                            + wholeYears + ", months: " + approxMonths);


                        double total = wholeYears + (approxMonths / 12);


                        sum += total;
                    }
                    MessageBox.Show("Total exp: " + sum);

                    return sum;

                }
            }
            catch (Exception err)
            {

                MessageBox.Show(err.Message);

                return 0;
            }

        }


        private void storeBasicUser(String myXml, double exp)
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

                    MessageBox.Show(id);

                    //check if lid already exists
                    using (var context = new FoogleContext())
                    {
                        var usrs = from b in context.Users
                                    where b.linkedin_id.Equals(id)
                                    select b;

                        if (usrs.Count() > 0)
                        {
                            MessageBox.Show("Vec ste registrirani, jebo te");
                            return;
                        }
                    }


                    saveUser(fname, lname, lin, id, exp);
                }
            }
            catch (Exception err)
            {

                MessageBox.Show(err.Message);
            }

            //OutputTextBlock.Text = output.ToString();
        }



        private int regged_user_id = 0;
        private void saveUser(string first_name, string last_name, string lin,
            string lid, double experience)
        {
            try
            {
                using (var context = new FoogleContext())
                {

                    FoogleUser regged_user = new FoogleUser
                        {
                            email = getUserEmail(),
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

                    regged_user_id = regged_user.id;

                }

                MessageBox.Show("Uspjesno ste uneseni u bazu podataka.");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.InnerException);
            }
        }


        private void RegistracijaProfesor(object sender, RoutedEventArgs e)
        {
            //UserRegisterWindow ww = new UserRegisterWindow();
            Registration ww = new Registration();

            ww.Show();
        }

        private void PrijavaAdminButton_Click(object sender, RoutedEventArgs e)
        {
            AdminUserConfirmationWindow w = new AdminUserConfirmationWindow();

            w.Show();
        }

        private void EndorsementButton_Click(object sender, RoutedEventArgs e)
        {

            //if (logged_in == false)
            //{
            //    MessageBox.Show("Niste ulogirani.");
            //}
            //else
            //{

                Endorsement ew = new Endorsement();

                ew.Show();
            //}
        }

        private void TestDataButton(object sender, RoutedEventArgs e)
        {




        }

    }


}
