using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;

namespace PhotoFlip
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public int Images { get; set; }
        public static App CurrentApp => App.Current as App;
        
        
        public App()
        {
          
        }
        
    }
}