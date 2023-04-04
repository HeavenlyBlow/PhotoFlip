using System;
using System.Diagnostics;
using System.IO;
using System.Windows;



namespace PhotoFlip
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
            
            Action scroll = new Action((() => this.ScrollRight()));
            DataContext = new MainWindowViewModel(scroll);
            
            Process process = Process.Start(new ProcessStartInfo
            {
                FileName = "taskkill",
                Arguments = "/F /IM explorer.exe",
                CreateNoWindow = true,
                UseShellExecute = false,
                WindowStyle = ProcessWindowStyle.Hidden
            });
            process?.WaitForExit();
            Closing += (e, a) =>
            {
            
                Process.Start(Path.Combine(Environment.GetEnvironmentVariable("windir"), "explorer.exe"));
            };


        }
        

        private void ScrollRight()
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                var index = FlipView.SelectedIndex;
                var images = App.CurrentApp.Images - 1;
            
                if (index == images)
                {
                    FlipView.SelectedIndex = -1;
                }

                FlipView.SelectedIndex++;
            });
        }
    }
    
}