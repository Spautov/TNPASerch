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

namespace TNPASerch.View
{
    /// <summary>
    /// Interaction logic for WatchFilesView.xaml
    /// </summary>
    public partial class WatchFilesView : Window
    {
        public WatchFilesView()
        {
            InitializeComponent();
        }

        private void CommandCloseBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.DialogResult = false;
            Close();
        }
    }
}
