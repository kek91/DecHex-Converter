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
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Configuration;

namespace DecHex_Converter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        

        public MainWindow()
        {
            InitializeComponent();

            var appSettings = ConfigurationManager.AppSettings;

            if (appSettings["AlwaysOnTop"] == "true")
            {
                this.Topmost = true;
            }
            else
            {
                this.Topmost = false;
            }
        }


        private void TextBox_Dec_KeyUp(object sender, KeyEventArgs e)
        {
            // Convert data to hex and update TextBox_Hex

            //Debug.WriteLine(TextBox_Dec.Text);
            string decValue = Regex.Replace(TextBox_Dec.Text, "[^0-9]", "");
            
            if(decValue.Length > 0)
            {
                string hexValue = int.Parse(decValue).ToString("X");
                TextBox_Hex.Text = hexValue;
            }
            else
            {
                TextBox_Hex.Text = "";
            }

        }

        private void TextBox_Hex_KeyUp(object sender, KeyEventArgs e)
        {
            // Convert data to dec and update TextBox_Dec

            string hexValue = Regex.Replace(TextBox_Hex.Text, "[^abcdefABCDEF0-9]", "");
            
            if(hexValue.Length > 0)
            {
                string decValue = Convert.ToInt64(hexValue, 16).ToString();
                TextBox_Dec.Text = decValue;
            }
            else
            {
                TextBox_Dec.Text = "";
            }
        }


        private void MenuItem_FileSettings_Click(object sender, RoutedEventArgs e)
        {
            Window SettingWindow = new Settings();
            SettingWindow.Show();
        }

        private void MenuItem_FileQuit_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult prompt = MessageBox.Show("Are you sure?",
                "Quit",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question,
                MessageBoxResult.No);
            if(prompt == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }

        private void MenuItem_AboutVersion_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("v1.0.0\n2020-03-27",
                "Version",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }

        private void MenuItem_AboutCredits_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Made by Teknix - Kim Eirik Kvassheim\nⒸ 2020\nteknix.no",
                "Credits",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }


    }
}
