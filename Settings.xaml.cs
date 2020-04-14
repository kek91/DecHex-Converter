using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Configuration;
using System.Diagnostics;

namespace DecHex_Converter
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {

        public Settings()
        {
            InitializeComponent();

            // Show correct checkmarks for checkboxes
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                if(appSettings["AlwaysOnTop"] == "true")
                {
                    CheckBox_AlwaysOnTop.IsChecked = true;
                }
                if (appSettings["EnableDarkTheme"] == "true")
                {
                    CheckBox_EnableDarkTheme.IsChecked = true;
                }
            }
            catch (ConfigurationErrorsException)
            {
                Debug.WriteLine("Error reading app settings");
            }
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            // Toggle always on top
            
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var appSettings = configFile.AppSettings.Settings;

                if (CheckBox_AlwaysOnTop.IsChecked == true)
                {
                    if (appSettings["AlwaysOnTop"] == null)
                    {
                        appSettings.Add("AlwaysOnTop", "true");
                    }
                    else
                    {
                        appSettings["AlwaysOnTop"].Value = "true";
                    }
                }
                else
                {
                    if (appSettings["AlwaysOnTop"] == null)
                    {
                        appSettings.Add("AlwaysOnTop", "false");
                    }
                    else
                    {
                        appSettings["AlwaysOnTop"].Value = "false";
                    }
                }
                
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);

                MessageBox.Show("Setting has been successfully saved.\nPlease note that the changes will not be updated until you close and re-open DecHex Converter.",
                    "Settings",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            catch (ConfigurationErrorsException)
            {
                Debug.WriteLine("Error writing app settings");
            }
        }

        private void CheckBox_Click_1(object sender, RoutedEventArgs e)
        {
            // Toggle dark theme
            var appSettings = ConfigurationManager.AppSettings;
            if (CheckBox_EnableDarkTheme.IsChecked == true)
            {
                appSettings["EnableDarkTheme"] = "true";
                MessageBox.Show("Dark theme not implemented yet.",
                    "Theme Settings",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            else
            {
                appSettings["EnableDarkTheme"] = "false";
            }
        }
    }
}
