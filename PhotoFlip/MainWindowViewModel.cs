using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Input;
using Newtonsoft.Json;

namespace PhotoFlip
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<DisposableImage> images;
        private SettingsModel _model;
        private Action _scroll;
        private Command _onLoaded;

        public ObservableCollection<DisposableImage> Images { get; set; }

        public MainWindowViewModel(Action scroll)
        {
            _scroll = scroll;
            Images = new ObservableCollection<DisposableImage>();
            InitSettings();
            InitPhoto();
            
        }

        
        public ICommand OnLoadedCommand => _onLoaded ??= new Command(a =>
        {
            InitTimer();
        });

        private void InitTimer()
        {
            var num = 0; 
            // устанавливаем метод обратного вызова
            var tm = new TimerCallback(Flip);
            // создаем таймер
            var timer = new Timer(tm, num, 60000, 60000);
        }

        private void Flip(object state)
        {
            _scroll.Invoke();
        }


        private void InitPhoto()
        {
            var allPhoto = Directory.GetFiles(_model.Path);
            // var allPhoto = Directory.GetFiles("images");
            App.CurrentApp.Images = allPhoto.Length;
            
            foreach (var photoPath in allPhoto)
            {
                Images.Add(new DisposableImage(photoPath));
            }
        }


        private void InitSettings()
        {
            var file = File.ReadAllText("settings.json");
            _model = JsonConvert.DeserializeObject<SettingsModel>(file);
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}