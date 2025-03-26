﻿using System;
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
    /// Interaction logic for afterlogin.xaml
    /// </summary>
    public partial class afterlogin : Window
    {
        ServerConnection connection;
        public afterlogin(ServerConnection connection)
        {
            InitializeComponent();
            this.connection = connection;
            start();
        }
        async void start()
        {
            List<string> all = await connection.Profiles();
            foreach(string item in all)
            {
                Lista.Children.Add(new TextBlock() { Text = item});
            }
        }
    }
}
