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



namespace Foogle_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private static string mode = "dev";

        public MainWindow()
        {
            InitializeComponent();

        }

        //activates when professor logs in
        public static bool logged_in = false;

        //set when professor logs in
        public static int professor_id = 0;


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
            string searchText = searchBox.Text.Trim();

            ss.SearchByQuery(searchText);

        }




        StudentSearch ss = new StudentSearch();
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
        

                string[] arr = ss.SuggestSkills(lastWord, prefix);

                AutoCompleteBox b = (AutoCompleteBox) sender;
                
                b.ItemsSource = arr;

                searchBox.PopulateComplete();
            }

        }


        private void UserRegisterButton(object sender, RoutedEventArgs e)
        {

            
            //hide webBrowser
            webBrowser1.Visibility = Visibility.Hidden;

            //show "Animation icon"
            gifImage.Visibility = Visibility.Visible;

            //r_fullprofile & r_emailaddress
            webBrowser1.Navigate("https://www.linkedin.com/uas/oauth2/authorization?response_type=code&client_id=y0ubjypbbvov&state=mylongstring&redirect_uri=http://www.google.com&scope=r_emailaddress r_fullprofile");


        }


        bool xml_page = false;
        string access_token = "";

        private void wbCompleted(object sender, NavigationEventArgs e)
        {
            

            if (xml_page)
            {

                webBrowser1.Visibility = Visibility.Hidden;

                string url = "https://api.linkedin.com/v1/people/~?oauth2_access_token=" + access_token;

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse resp = (HttpWebResponse)request.GetResponse();

                Stream resStream = resp.GetResponseStream();
                StreamReader reader = new StreamReader(resStream);
                string text = reader.ReadToEnd();

                //XML showing
                //MessageBox.Show(text);

    

                //store years of experience (float)
                double exp = sr.getYearsOfExperienceFromLinkedIn();

                storeBasicUser(text, exp);


                //store skills
                sr.storeUserSkills();


                xml_page = false;

                gifImage.Visibility = Visibility.Hidden;

                webBrowser1.Visibility = Visibility.Visible;

                webBrowser1.Navigate("http://www.google.com");

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
                webBrowser1.Visibility = Visibility.Hidden;

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
                    response = (HttpWebResponse)httpWReq.GetResponse();

                    string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                    //MessageBox.Show(responseString);

                    int startIndex = responseString.LastIndexOf(':') + 2;
                    int len = responseString.Count() - 2 - startIndex;

                    access_token = responseString.Substring(startIndex, len);
                    StudentRegistration.access_token = access_token;

                    webBrowser1.Navigate("https://api.linkedin.com/v1/people/~?oauth2_access_token=" + access_token);

            
                    xml_page = true;

                }
                catch (Exception re)
                {
                    //MessageBox.Show(re.Message);
                }
            }
            else
            {
                webBrowser1.Visibility = Visibility.Visible;
            }

            
        }


        StudentRegistration sr = new StudentRegistration();
        private String getUserEmail()
        {
            return sr.getUserEmailFromLinkedIn();
        }






        private void storeBasicUser(String myXml, double exp)
        {
            sr.saveIfUserDoesntExist(myXml, exp);
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

            if (logged_in == false)
            {
                MessageBox.Show("Niste ulogirani.");
            }
            else
            {

                Endorsement ew = new Endorsement();

                ew.Show();
            }
        }

        private void TestDataButton(object sender, RoutedEventArgs e)
        {

        }

        private void SearchByCategory(object sender, RoutedEventArgs e)
        {
            SearchSelectCategoryWindow sc = new SearchSelectCategoryWindow();

            sc.Show();

        }

    }


}
