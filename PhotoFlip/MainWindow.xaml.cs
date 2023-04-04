using System;
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