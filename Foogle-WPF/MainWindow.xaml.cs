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
using System.Runtime.InteropServices;
using mshtml;
using System.Data.SQLite;
using System.Threading;
using System.ComponentModel;



namespace Foogle_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        public static short authLevel = 0;
        public static int userID = 0;

        public MainWindow()
        {
            InitializeComponent();
            SetupMenuByAuthLevel(0);

            SearchButton.IsDefault = true;

            animateBackgroundThread = new Thread(new ThreadStart(AnimateBackground));
            animateBackgroundThread.Priority = ThreadPriority.BelowNormal;
            animateBackgroundThread.Start();
            //animateBackgroundThread.Join();

            //SQLite support
            SQLiteConnection m_dbConnection;

            //SQLiteConnection.CreateFile("MyDatabase.sqlite");

            m_dbConnection = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;");
            m_dbConnection.Open();

            string sql = @"
                CREATE TABLE IF NOT EXISTS category  (
                    id integer NOT NULL PRIMARY KEY AUTOINCREMENT,
                    name character varying(50) NOT NULL,
                    master_category integer
                );

                CREATE TABLE  IF NOT EXISTS  foogle_user (
                    id integer NOT NULL PRIMARY KEY AUTOINCREMENT,
                    email character varying(50) NOT NULL,
                    firstname character varying(50),
                    lastname character varying(50),
                    role character(1) DEFAULT 'S',
                    activity character(1) DEFAULT 'P',
                    password character varying(45),
                    confirmed boolean,
                    linkedin_id character varying(255),
                    linkedin character varying(255),
                    exp real
                );

                CREATE TABLE IF NOT EXISTS recommendation  (
                    category_id integer NOT NULL,
                    teacher_id integer NOT NULL,
                    student_id integer  NOT NULL,
                    id integer NOT NULL  PRIMARY KEY AUTOINCREMENT,
                    FOREIGN KEY (teacher_id) REFERENCES foogle_user(id),
                    FOREIGN KEY (student_id) REFERENCES foogle_user(id),
                    FOREIGN KEY (category_id) REFERENCES category(id)
                );

                CREATE TABLE  IF NOT EXISTS skill  (
                    id integer NOT NULL PRIMARY KEY AUTOINCREMENT,
                    name character varying(50) NOT NULL
                );

                CREATE TABLE IF NOT EXISTS userSkillsMenuItem  (
                    id integer NOT NULL PRIMARY KEY AUTOINCREMENT,
                    user_id integer,
                    skill_id integer,
                    FOREIGN KEY (user_id) REFERENCES foogle_user(id),
                    FOREIGN KEY (skill_id) REFERENCES skill(id)
                );
                
                drop trigger if exists provjera;

                CREATE TRIGGER provjera BEFORE INSERT 
                ON foogle_user when
                (
                   select email  from foogle_user where email = NEW.email        
                )

                BEGIN
                        delete from recommendation where student_id=(select id from foogle_user where email=NEW.email);
	                    delete from recommendation where teacher_id=(select id from foogle_user where email=NEW.email);
	                    delete from userSkillsMenuItem where user_id = (select id from foogle_user where email=NEW.email);
	                    delete from foogle_user where email = NEW.email;
                END;

                /*create trigger provjera before insert on foogle_user 
                when (
	                select count(*) as postoji from (
	                    select * from foogle_user where email=NEW.email
	                ) 
                    where postoji>0
                   
                )

                begin
	                delete from recommendation where student_id=(select id from foogle_user where email=NEW.email);
	                delete from recommendation where teacher_id=(select id from foogle_user where email=NEW.email);
	                delete from userSkillsMenuItem where user_id = (select id from foogle_user where email=NEW.email);
	                delete from foogle_user where email = NEW.email;

                end;*/

                INSERT OR REPLACE INTO category VALUES (1, 'Web development', 1);
                INSERT OR REPLACE INTO category VALUES (2, 'Web design', 1);
                INSERT OR REPLACE INTO category VALUES (3, 'Web programming', 1);
                INSERT OR REPLACE INTO category VALUES (4, 'Ecommerce', 1);
                INSERT OR REPLACE INTO category VALUES (5, 'UI design', 1);
                INSERT OR REPLACE INTO category VALUES (6, 'Website QA', 1);
                INSERT OR REPLACE INTO category VALUES (7, 'Website project managment', 1);
                INSERT OR REPLACE INTO category VALUES (8, 'Other - Web development', 1);
                INSERT OR REPLACE INTO category VALUES (9, 'Software development', 2);
                INSERT OR REPLACE INTO category VALUES (10, 'Desktop applications', 2);
                INSERT OR REPLACE INTO category VALUES (11, 'Game development', 2);
                INSERT OR REPLACE INTO category VALUES (12, 'Scripts and utilities', 2);
                INSERT OR REPLACE INTO category VALUES (13, 'Software Plug-ins', 2);
                INSERT OR REPLACE INTO category VALUES (14, 'Mobile apps', 2);
                INSERT OR REPLACE INTO category VALUES (15, 'Application interface design', 2);
                INSERT OR REPLACE INTO category VALUES (16, 'Software project managment', 2);
                INSERT OR REPLACE INTO category VALUES (17, 'Software QA', 2);
                INSERT OR REPLACE INTO category VALUES (18, 'VOIP', 2);
                INSERT OR REPLACE INTO category VALUES (19, 'Other - Software Development', 2);
                INSERT OR REPLACE INTO category VALUES (20, 'Networking and information systems', 3);
                INSERT OR REPLACE INTO category VALUES (21, 'Network administration', 3);
                INSERT OR REPLACE INTO category VALUES (22, 'DBA - Database administration', 3);
                INSERT OR REPLACE INTO category VALUES (23, 'Server administration', 3);
                INSERT OR REPLACE INTO category VALUES (24, 'Other - Networking and information systems', 3);
                INSERT OR REPLACE INTO category VALUES (25, 'ERP / CRM Implementation', 3);
                INSERT OR REPLACE INTO category VALUES (26, 'Design and multimedia', 4);
                INSERT OR REPLACE INTO category VALUES (27, 'Graphic design', 4);
                INSERT OR REPLACE INTO category VALUES (28, 'Logo design', 4);
                INSERT OR REPLACE INTO category VALUES (29, 'Illustration', 4);
                INSERT OR REPLACE INTO category VALUES (30, 'Print design', 4);
                INSERT OR REPLACE INTO category VALUES (31, '3D modelling & CAD', 4);
                INSERT OR REPLACE INTO category VALUES (32, 'Audio production', 4);
                INSERT OR REPLACE INTO category VALUES (33, 'Video production', 4);
                INSERT OR REPLACE INTO category VALUES (34, 'Voice talent', 4);
                INSERT OR REPLACE INTO category VALUES (35, 'Animation', 4);
                INSERT OR REPLACE INTO category VALUES (36, 'Other - design & multimedia', 4);
                INSERT OR REPLACE INTO category VALUES (37, 'Presentations', 4);
                INSERT OR REPLACE INTO category VALUES (38, 'Engineering and technical design', 4);
                
                Create view if not exists userSkillsMenuItem_view as
                select firstname, lastname, name from foogle_user join userSkillsMenuItem on 
                foogle_user.id=userSkillsMenuItem.user_id join skill on 
                userSkillsMenuItem.skill_id=skill.id;

                create view if not exists recommendation_view as
                select s.firstname as 'ime_studenta', s.lastname as 'prezime_studenta', 
                p.firstname as 'ime_profesora', p.lastname as 'prezime_profesora', name 
                from foogle_user s join recommendation on s.id=recommendation.student_id 
                join foogle_user p on recommendation.teacher_id=p.id join category on 
                recommendation.category_id=category.id;
            ";

            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
        }

        StudentSearch studentSearch = new StudentSearch();
        StudentRegistration studentRegistration = new StudentRegistration();
        Thread animateBackgroundThread;

        private void MainWindow_Activated(object sender, EventArgs e)
        {
            SetupMenuByAuthLevel(authLevel);
        }

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            windowExists = false;
        }

        private void AuthButton_Click(object sender, RoutedEventArgs e)
        {
            if (authLevel > 0)
            {
                SetupMenuByAuthLevel(0);
                return;
            }

            Login loginWindow = new Login();
            loginWindow.Show();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            if (authLevel < 333)
            {
                MessageBox.Show("Niste ulogirani ili nemate pravo pristupa tražilici!");
                AuthButton.Focus();
                return;
            }

            // Tražilica bi trebala raditi i na način da se mogu pretražiti korisnici po imenu ili bilo čemu drugome

            //simple sort: # of skills matched (no formula)
            string searchText = searchBox.Text.Trim();

            //put this into thread
            studentSearch.SearchByQuery(searchText);
        }

        //called on every character deletion / add
        private void SearchBoxPopulating(object sender, System.Windows.Controls.PopulatingEventArgs e)
        {
            if (authLevel < 333)
            {
                MessageBox.Show("Niste ulogirani ili nemate pravo pristupa tražilici!");
                AuthButton.Focus();
                searchBox.Text = "";
                return;
            }

            string text = searchBox.Text;

            if (text.Length > 0)
            {
                //get last word
                string lastWord = text.Substring(text.LastIndexOf(' ') + 1);

                //pass prefix to all sugestions
                int index = text.LastIndexOf(' ');
                string prefix = "";
                if (index != -1)
                    prefix = text.Substring(0, index + 1);

                string[] searchResults = studentSearch.SuggestSkillsSqlite(lastWord, prefix);

                AutoCompleteBox autoComplete = (AutoCompleteBox)sender;
                autoComplete.ItemsSource = searchResults;
                searchBox.PopulateComplete();
            }

        }

        private void LinkedInRegisterButton(object sender, RoutedEventArgs e)
        {
            if (authLevel > 0)
            {
                MessageBox.Show("Već ste ulogirani!");
                return;
            }

            webBrowser.Visibility = Visibility.Hidden;
            webBrowserBorder.Visibility = Visibility.Hidden;
            loadingImage.Visibility = Visibility.Visible;

            //r_fullprofile & r_emailaddress
            webBrowser.Navigate("https://www.linkedin.com/uas/oauth2/authorization?response_type=code&client_id=y0ubjypbbvov&state=mylongstring&redirect_uri=http://www.google.com&scope=r_emailaddress r_fullprofile");
        }

        bool xmlFound = false;
        string accessToken = "";

        private void BrowsingCompleted(object sender, NavigationEventArgs e)
        {
            if (xmlFound)
            {
                webBrowser.Visibility = Visibility.Hidden;
                webBrowserBorder.Visibility = Visibility.Hidden;
                loadingImage.Visibility = Visibility.Visible;

                string url = "https://api.linkedin.com/v1/people/~?oauth2_access_token=" + accessToken;

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                Stream responseStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream);
                string text = reader.ReadToEnd();

                //store years of experience (float)
                double experience = studentRegistration.getYearsOfExperienceFromLinkedIn();

                StoreBasicUser(text, experience);
                studentRegistration.StoreUserSkills();

                xmlFound = false;
                webBrowser.Navigate("http://www.google.com");

                webBrowser.Visibility = Visibility.Visible;
                webBrowserBorder.Visibility = Visibility.Visible;
                loadingImage.Visibility = Visibility.Hidden;

                return;
            }


            String uri = webBrowser.Source.AbsoluteUri;

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
                webBrowser.Visibility = Visibility.Hidden;
                webBrowserBorder.Visibility = Visibility.Hidden;

                HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create("https://www.linkedin.com/uas/oauth2/accessToken?grant_type=authorization_code&code="
                                                                                + final_code
                                                                                + @"&redirect_uri=http://www.google.com&client_id=y0ubjypbbvov&client_secret=iwxSaObfSq6X9szw");

                ASCIIEncoding encoding = new ASCIIEncoding();
                string postData = "username=user";
                postData += "&password=pass";
                byte[] data = encoding.GetBytes(postData);

                httpRequest.Method = "POST";
                httpRequest.ContentType = "application/x-www-form-urlencoded";
                httpRequest.ContentLength = data.Length;

                using (Stream stream = httpRequest.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                HttpWebResponse response = null;
                try
                {
                    response = (HttpWebResponse)httpRequest.GetResponse();

                    string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                    int startIndex = responseString.LastIndexOf(':') + 2;
                    int length = responseString.Count() - 2 - startIndex;

                    accessToken = responseString.Substring(startIndex, length);
                    StudentRegistration.accessToken = accessToken;

                    webBrowser.Navigate("https://api.linkedin.com/v1/people/~?oauth2_access_token=" + accessToken);

                    xmlFound = true;
                }
                catch { }
            }
            else
            {
                webBrowser.Visibility = Visibility.Visible;
                webBrowserBorder.Visibility = Visibility.Visible;
            }
        }

        private void StoreBasicUser(String xmlString, double experience)
        {
            studentRegistration.saveIfUserDoesntExist(xmlString, experience);
        }

        private void RegistracijaProfesor(object sender, RoutedEventArgs e)
        {
            if (authLevel > 0)
            {
                MessageBox.Show("Već ste ulogirani!");
                return;
            }

            Registration registrationWindow = new Registration();
            registrationWindow.Show();
        }

        private void PrijavaAdminButton_Click(object sender, RoutedEventArgs e)
        {
            if (authLevel < 1337)
            {
                MessageBox.Show("Niste ulogirani ili nemate pravo pristupa!");
                return;
            }

            AdminUserConfirmationWindow userConfirmationWindow = new AdminUserConfirmationWindow();
            userConfirmationWindow.Show();
        }

        private void EndorsementButton_Click(object sender, RoutedEventArgs e)
        {
            if (authLevel < 666)
            {
                MessageBox.Show("Niste ulogirani ili nemate pravo pristupa!");
                return;
            }

            Endorsement endorsmentWindow = new Endorsement();
            endorsmentWindow.Show();
        }

        private void SearchByCategory(object sender, RoutedEventArgs e)
        {
            if (authLevel < 333)
            {
                MessageBox.Show("Niste ulogirani ili nemate pravo pristupa!");
                return;
            }

            SearchSelectCategoryWindow categorySearchWindow = new SearchSelectCategoryWindow();
            categorySearchWindow.Show();
        }

        private void ReportsButton_Click(object sender, RoutedEventArgs e)
        {
            Reports reportsWindow = new Reports();
            reportsWindow.Show();
        }

        private void UsersButton_Click(object sender, RoutedEventArgs e)
        {
            Users usersWindow = new Users();
            usersWindow.Show();
        }

        public void SetupMenuByAuthLevel(short level)
        {
            authLevel = level;
            if (level <= 0)
            {
                LinkedInButton.Visibility = Visibility.Visible;
                ProfesorButton.Visibility = Visibility.Visible;
                AuthButton.Content = "Prijava";
                BlankField.Content = "";
                BlankField.Margin = new Thickness(324, 0, 86, 0);
                UsersButton.Visibility = Visibility.Hidden;
                EndorsementButton.Visibility = Visibility.Hidden;
                ReportsButton.Visibility = Visibility.Hidden;
                AdministrationButton.Visibility = Visibility.Hidden;
                return;
            }

            LinkedInButton.Visibility = Visibility.Hidden;
            ProfesorButton.Visibility = Visibility.Hidden;
            AuthButton.Content = "Odjava";
            BlankField.Content = "Dobrodošli u Foogle!";

            switch (level)
            {
                case 1:
                    UsersButton.Visibility = Visibility.Hidden;
                    EndorsementButton.Visibility = Visibility.Hidden;
                    ReportsButton.Visibility = Visibility.Hidden;
                    AdministrationButton.Visibility = Visibility.Hidden;
                    BlankField.Margin = new Thickness(0, 0, 86, 0);
                    break;
                case 333:
                case 666:
                    UsersButton.Visibility = Visibility.Hidden;
                    EndorsementButton.Visibility = Visibility.Visible;
                    ReportsButton.Visibility = Visibility.Hidden;
                    AdministrationButton.Visibility = Visibility.Hidden;
                    BlankField.Margin = new Thickness(0, 0, 194, 0);
                    EndorsementButton.Margin = new Thickness(0, 0, 86, 0);
                    break;
                case 1337:
                    UsersButton.Visibility = Visibility.Visible;
                    EndorsementButton.Visibility = Visibility.Visible;
                    ReportsButton.Visibility = Visibility.Visible;
                    AdministrationButton.Visibility = Visibility.Visible;
                    BlankField.Margin = new Thickness(0, 0, 427, 0);
                    EndorsementButton.Margin = new Thickness(0, 0, 256, 0);
                    break;
                default:
                    SetupMenuByAuthLevel(0);
                    break;
            }
        }

        short backgroundAnimationPhase = 0;
        float backgroundAnimationOffset = 0.0000033f;
        bool windowExists = true;
        Random random = new Random(13891);
        private void AnimateBackground()
        {
            while (windowExists)
            {
                if (!Dispatcher.CheckAccess())
                {
                    Dispatcher.Invoke((Action)delegate()
                    {
                        switch (backgroundAnimationPhase)
                        {
                            case 1:
                                BackgroundImageUL.Opacity += backgroundAnimationOffset;
                                BackgroundImageDL.Opacity -= backgroundAnimationOffset;
                                BackgroundImageUR.Opacity -= backgroundAnimationOffset;

                                if (BackgroundImageUL.Opacity >= 0.75f)
                                {
                                    backgroundAnimationPhase++;
                                }
                                break;
                            case 2:
                                BackgroundImageUL.Opacity -= backgroundAnimationOffset;
                                BackgroundImageDL.Opacity += backgroundAnimationOffset;
                                BackgroundImageUR.Opacity += backgroundAnimationOffset;

                                if (BackgroundImageUL.Opacity <= 0.10f)
                                {
                                    backgroundAnimationPhase++;
                                }
                                break;
                            default:
                                backgroundAnimationPhase = 1;
                                break;
                        }
                    });
                }
            }
        }
    }
}
