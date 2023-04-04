using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media.Imaging;

namespace PhotoFlip
{

    public class DisposableImage
    {


        /// <summary>
        /// Уничтожаемая картинка
        /// </summary>
        /// <param name="path">Путь до картинки</param>
        public DisposableImage(string path)
        {
            Update(path);
        }

        private Stream mediaStream;

        private BitmapImage _source;


        /// <summary>
        /// Картинка
        /// </summary>
        public BitmapImage Source
        {
            get => _source;
            private set
            {
                _source = value;
                OnPropertyChanged();
            }
        }


        /// <summary>
        /// Добавляем картинку
        /// </summary>
        /// <param name="path">путь</param>
        private void Update(string path)
        {
            Dispose();

            if (path != null)
            {
                var bitmap = new BitmapImage();

                if (path.Contains("pack://application:,,,"))
                {
                    mediaStream = Application.GetResourceStream(new Uri(path))?.Stream;
                }
                else
                {
                    mediaStream = File.OpenRead(path);
                }

                bitmap.BeginInit();
                bitmap.CacheOption = BitmapCacheOption.None;
                bitmap.StreamSource = mediaStream;
                bitmap.EndInit();
                bitmap.Freeze();
                Source = bitmap;
            }

        }


        /// <summary>
        /// Уничтожить картинку
        /// </summary>
        public void Dispose()
        {
            if (mediaStream != null)
            {
                Source = null;
                mediaStream.Close();
                mediaStream.Dispose();
                mediaStream = null;
                GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced, true);
            }
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