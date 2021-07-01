﻿using SmartHelper.ViewModel;
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

namespace SmartHelper.Pages
{
    /// <summary>
    /// Interaction logic for SteelPage1.xaml
    /// </summary>
    public partial class SteelPage1 : Page
    {
        private SteelWindowViewModel _steelWindowViewModel;

        public SteelPage1()
        {
            InitializeComponent();
            _steelWindowViewModel = new SteelWindowViewModel();
            DataContext = _steelWindowViewModel;
        }
    }
}