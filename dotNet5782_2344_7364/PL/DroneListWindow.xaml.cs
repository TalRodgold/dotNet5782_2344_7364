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

namespace PL
{
    /// <summary>
    /// Interaction logic for DroneListWindow.xaml
    /// </summary>
    public partial class DroneListWindow : Window
    {
        public IBL.BL bl;
        public DroneListWindow(IBL.BL bl) // constructor
        {
            this.bl = bl;
            InitializeComponent();
            DroneListView.ItemsSource = bl.GetListOfDronesToList().ToList();
            StatusSelector.ItemsSource = Enum.GetValues(typeof(IBL.BO.Enums.DroneStatuses));
            MaxWeightSelector.ItemsSource = Enum.GetValues(typeof(IBL.BO.Enums.WeightCategories));
        }

        private void MaxWeightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e) // combo box to select by weight categories
        {
            DroneListView.ItemsSource = bl.GetListOfDroneToListByPredicat(predicate => predicate.Weight == (IBL.BO.Enums.WeightCategories)MaxWeightSelector.SelectedItem).ToList();
        }

        private void StatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e) // combo box to select by status
        {
            DroneListView.ItemsSource = bl.GetListOfDroneToListByPredicat(predicate => predicate.DroneStatuses == (IBL.BO.Enums.DroneStatuses)StatusSelector.SelectedItem).ToList();
        }

        private void AddDrone_Click(object sender, RoutedEventArgs e) // add a drone
        {
            new DroneWindow(bl, this).Show();
        }

        private void UpdateDrone_doubleClick(object sender, MouseButtonEventArgs e) // ypdate drone
        {
            IBL.BO.DroneToList drone = (IBL.BO.DroneToList)DroneListView.SelectedItem;
            new DroneWindow(bl, this, drone).Show();
        }

        private void DroneListView_SelectionChanged(object sender, SelectionChangedEventArgs e) // open drone window
        {
            new DroneWindow(bl, this).Show();
        }

        private void Exit_Click(object sender, RoutedEventArgs e) // exit
        {
            Close();
        }
        public void Refresh() // refresh
        {
            if (StatusSelector.SelectedIndex == -1 || MaxWeightSelector.SelectedIndex == -1) // if no changes where selected
            {
                DroneListView.ItemsSource = bl.GetListOfDronesToList().ToList();
            }
            else
            {
                DroneListView.ItemsSource = bl.GetListOfDroneToListByPredicat(predicate => predicate.DroneStatuses == (IBL.BO.Enums.DroneStatuses)StatusSelector.SelectedItem).ToList();
                DroneListView.ItemsSource = bl.GetListOfDroneToListByPredicat(predicate => predicate.Weight == (IBL.BO.Enums.WeightCategories)MaxWeightSelector.SelectedItem).ToList();
            }
        }
    }
}
