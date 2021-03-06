﻿// Marbles
// (C) 2020 Mukunda Johnson
/////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Windows;
using System.Windows.Controls;

/////////////////////////////////////////////////////////////////////////////////////////
namespace Marbles
{
    //-----------------------------------------------------------------------------------
    public partial class StartSprintPage : Page
    {
        readonly App app;

        //-------------------------------------------------------------------------------
        public StartSprintPage()
        {
            app = App.Cur;
            InitializeComponent();
            ApplyAppSettings(App.Cur.settings);
            app.settings.Loaded += ApplyAppSettings;
        }

        //-------------------------------------------------------------------------------
        public void ApplyAppSettings(Settings settings)
        {
            SetSprintSettings(
                settings.fields.SprintTime,
                settings.fields.RestTime);
            UpdateInfo();
        }

        private void UpdateInfo()
        {
            var settings = app.settings;
            if (settings.fields.MarblesDoneToday == 0)
            {
                infoLabel.Content = "Press enter to start.";
            }
            else
            {
                var m = settings.fields.MarblesDoneToday;
                infoLabel.Content = $"{m} marble{(m == 1 ? "" : "s")} done.";
            }
        }

        //-------------------------------------------------------------------------------
        public void SetSprintSettings(string sprint, string rest)
        {
            sprintText.Text = sprint;
            restText.Text = rest;
        }

        //-------------------------------------------------------------------------------
        public (bool, double, double) GetSprintSettings()
        {

            double sprint, rest;
            if (!double.TryParse(sprintText.Text, out sprint)
                || !double.TryParse(restText.Text, out rest))
            {
                MessageBox.Show("Invalid input.", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                return (false, 0.0, 0.0);
            }
            if (sprint <= 0)
            {
                MessageBox.Show("Invalid sprint range. 😦", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                return (false, 0.0, 0.0);
            }
            if (rest < 0)
            {
                MessageBox.Show("Invalid rest range. 🧙", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                return (false, 0.0, 0.0);
            }

            return (true, sprint, rest);
        }

        private void OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            UpdateInfo();
        }

        private void sprintText_TextChanged(object sender, TextChangedEventArgs e)
        {
            app.settings.fields.SprintTime = sprintText.Text;
        }

        private void restText_TextChanged(object sender, TextChangedEventArgs e)
        {
            app.settings.fields.RestTime = restText.Text;
        }
    }
}
/////////////////////////////////////////////////////////////////////////////////////////
