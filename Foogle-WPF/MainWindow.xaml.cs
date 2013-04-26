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

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow lw = new LoginWindow();

            lw.Show();

            /* TODO: 
             *      Korisnik nije prijavljen:
             *          1. otvori formu za prijavu i ponudi:
             *                                      + registraciju
             *                                      + povezivanje s LinkedIn računom
             *                                      + prijavu sa postoječim podacima
             *          2. poveži se s bazom
             *          3. promijeni naziv u "Odjava" i dodaj korisničko ime u UserLabel
             *          
             *      Korisnik je prijavljen:
             *          1. prekini vezu sa bazom
             *          2. Promjeni naziv u "Prijava" i makni korisničko ime iz UserLabel
             */
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Provjeri login status, dozvoli samo ako je prijavljen
            // Tražilica bi trebala raditi i na način da se mogu pretražiti korisnici po imenu ili bilo čemu drugome

            /*string searchText = searchBox.Text;

            String[] skills = searchText.Split(' ');

            //query: students, recommendations, employment history, portfolio projects, skills
            string sql = @"
                select first_name, last_name from student
                join student_skills on student.id = student_skills.student_id
                join recommendation on student.id = recommendation.student_id
                join employment_history on student.id = employment_history.student_id
                join portfolio_projects on student.id = portfolio_projects.id
            ";

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, sqlConnection);
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            ds.Reset(); //data set
            da.Fill(ds); // //data adapter - fill data set
            dt = ds.Tables[0]; //data table

            foreach (DataRow row in dt.Rows)
            {
                //s += row["skill_tag"] as String;

            }*/

          

            /*
            //http://msdn.microsoft.com/en-us/data/jj574232.aspx
            using (var context = new FoogleContext())
            {
                var students = from a in context.Students
                               //.Include(a )
                               select a;

                var pfs = from b in context.PortfolioProjects
                               //.Include(a )
                               select b;

                foreach (var p in pfs)
                {
                    //MessageBox.Show(p.project_name);
                }

                
                var studs = from a in context.Students
                            .Include("PortfolioProjects")
                            select a;

                foreach (var s in students)
                {
                    MessageBox.Show(s.id.ToString());
                    MessageBox.Show(s.first_name);

                    if (s.PortfolioProjects != null)
                    {
                        foreach (var pp in s.PortfolioProjects)
                        {
                            MessageBox.Show(pp.id.ToString());
                        }
                    }

                    //MessageBox.Show(s.skill_tag);
                    //Console.WriteLine(s.SkillTag);
                }
            } */

        }

        private Boolean StudentHasSkill(String[] skills, String skill)
        {
            foreach (String s in skills)
            {
                if (skill.CompareTo(s) == 0)
                    return true;

            }
            return false;
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
                
                string sql = "select * from skill where skill_tag LIKE '%" + searchText + "%' limit 10;";
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
                    s += row["skill_tag"] as String;
                    suggestions[i++] = s;

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
                    "localhost", "5432", "postgres", // TODO: postaviti bazu online i promijeniti adresu.
                    "alphaomega", "foogle");

                sqlConnection = new NpgsqlConnection(connectionString);
                sqlConnection.Open();

            }
            catch (Exception msg)
            {
                MessageBox.Show(msg.ToString());
                MessageBox.Show("Ako ne radi povezivanje, onda connection string nije dobar, provjerite isti! " +
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
           
            webBrowser1.Navigate("https://www.linkedin.com/uas/oauth2/authorization?response_type=code&client_id=y0ubjypbbvov&state=mylongstring&redirect_uri=http://www.google.com&scope=r_fullprofile");

        }


        bool xml_page = false;
        private void wbCompleted(object sender, NavigationEventArgs e)
        {
            if (xml_page)
            {
                HTMLDocument h = (HTMLDocument) webBrowser1.Document;

                String innerText = h.documentElement.innerText;
                MessageBox.Show(innerText);

                parseXml(innerText);

                xml_page = false;
                return;
            }


            String uri = webBrowser1.Source.AbsoluteUri;

            String[] code = uri.Split('&');
            String temp = code[0];
            String[] new_code = temp.Split('=');

            String final_code = new_code[1];

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

                    String access_token = responseString.Substring(startIndex, len);

                    //File f = new File("access_tokens.txt");
                    
                    //MessageBox.Show(access_token);

                    webBrowser1.Navigate("https://api.linkedin.com/v1/people/~?oauth2_access_token=" + access_token);

                    
                    //webBrowser1.Navigate("http://api.linkedin.com/v1/people/~/skills");
                    //now we have a link to his profile; we can scrape skills / categories / etc
                    
                    //first-name & last-name: get from xml
                    xml_page = true;

                    //parseXml(webBrowser1.Document.);

                    /*
                    using (var context = new FoogleContext())
                    {
                        context.Users.Add(
                            new FoogleUser
                            {
                                email = email,
                                confirmed = false,
                                firstname = firstname,
                                lastname = lastname,
                                role = "s",
                                activity = null,
                                password = "",
                                title = null
                            });

                        context.SaveChanges();
                    } */

                }
                catch (Exception re)
                {
                    MessageBox.Show(re.Message);
                }
            }

        }



        private void parseXml(String myXml)
        {
            StringBuilder output = new StringBuilder();


            XmlDocument xml = new XmlDocument();
            string newXml = "";

            newXml = myXml.Replace("-", "");
    

            xml.LoadXml(newXml); // suppose that myXmlString contains "<Names>...</Names>"

            MessageBox.Show(newXml);

            XmlNodeList xnList = xml.SelectNodes("/person");
            foreach (XmlNode xn in xnList)
            {
                string firstName = xn["first-name"].InnerText;
                string lastName = xn["last-name"].InnerText;
                //Console.WriteLine("Name: {0} {1}", firstName, lastName);
                MessageBox.Show(firstName);
            }
            //OutputTextBlock.Text = output.ToString();
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

    }
}
