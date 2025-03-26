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
using System.Windows.Shapes;

namespace WpfApp8
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        ServerConnection connection;

        public Window1(ServerConnection connection)
        {
            InitializeComponent();
            this.connection = connection;
            start();
        }
        void start()
        {
            asd();
        }
        async void create(object s, EventArgs e)
            
        {
            try {
                bool temp = await connection.createPerson(NameInput.Text, Convert.ToInt32(AgeInput.Text));
                if (temp)
                {
                    MessageBox.Show("Sikeres létrehozás");
                    asd();
                }
            }
            catch(Exception error) {
                MessageBox.Show(error.Message);
            }
            
        }
        public string oldname;
       

        async void deleteall(object s, EventArgs e) {
            bool temp = await connection.destroyall();
            if (temp) {
                MessageBox.Show("sikeres torles");
                asd();
            }
        }
        public int selectedindex;

        async void save(object s, EventArgs e) {
            bool temp = await connection.editperson(NameInput.Text, oldname, Convert.ToInt32(AgeInput.Text));
            if (temp)
            {
                MessageBox.Show("Sikeres modositas");
                NameInput.Clear();
                AgeInput.Clear();
                savebutton.IsEnabled = false;
                asd();
            }
        }
        async void asd()
        {

            NameStackPanel.Children.Clear();
            AgeStackPanel.Children.Clear();
            buttons.Children.Clear();
            editstackpanel.Children.Clear();
            List<string> allnames = await connection.AllNames();
            foreach (string item in allnames)
            {
                TextBlock namelabel = new TextBlock();
                namelabel.Text = item;
                NameStackPanel.Children.Add(namelabel);
                Button editbutton = new Button();
                
                Button newbtn = new Button();
                newbtn.Content = "x";
                newbtn.Click += async(s, e) => {
                    bool tmp = await connection.deletePerson(namelabel.Text);
                    if (tmp) {
                        MessageBox.Show("torolt");
                        asd();
                    }
                };
                buttons.Children.Add(newbtn);

                editbutton.Content = "edit";
                editbutton.Height = namelabel.Height;
                editbutton.Click += (s, e) => {
                    selectedindex = editstackpanel.Children.IndexOf(editbutton);
                    savebutton.IsEnabled = true;
                    NameInput.Text = item;
                    string agetext = ((TextBlock)AgeStackPanel.Children[selectedindex]).Text;
                    oldname = item;
                    AgeInput.Text = agetext;
                    asd();
                };
                editstackpanel.Children.Add(editbutton);

            }
            List<string> allages = await connection.AllAges();
            foreach (string item in allages)
            {
                AgeStackPanel.Children.Add(new TextBlock() { Text = item });
                
            }
        }
    }
}
