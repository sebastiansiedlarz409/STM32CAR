﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CarMobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Sensor : ContentPage
    {
        public Sensor()
        {
            InitializeComponent();
            BindingContext = new SensorViewModel(Navigation);
        }
    }
}