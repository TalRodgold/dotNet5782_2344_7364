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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for BaseStationListPage.xaml
    /// </summary>
    public partial class BaseStationListPage : Page
    {
        public BaseStationListPage()
        {
            
            InitializeComponent();
            w = new DroneListWindow();
        }

        private void TabItem_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}