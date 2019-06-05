using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
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

namespace SP_HW_3
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var thread = new Thread(PlayMusic);
            thread.IsBackground = true;
            thread.Start();
            PlayMusic();            
        }

        static void PlayMusic()
        {
            var currentThread = Thread.CurrentThread;
            //var player = new MediaPlayer();
            //player.MediaFailed += (s, e) => MessageBox.Show("Error");
            //player.Open(new Uri("MP3/Rammstein_-_01_Benzin(uzimusic.ru).mp3", UriKind.RelativeOrAbsolute));
            ////player.Play();

            ////player.Position = TimeSpan.Zero;
            //player.Play();
            SoundPlayer sp = new SoundPlayer();
            sp.SoundLocation = "ramshtayn-rammstein-rise-rise (online-audio-converter.com).wav";
            sp.Load();
            sp.PlayLooping();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {            
            Thread myThread = new Thread(new ParameterizedThreadStart(saveAsOwnTextFormat));
            myThread.Start(new MyClass {filename = "MyFile.txt", textToSave = new TextRange(MyTexBox.Document.ContentStart, MyTexBox.Document.ContentEnd).Text });            
        }

        private void saveAsOwnTextFormat(object myClass)
        {
            try
            {
                StreamWriter sw = File.CreateText((myClass as MyClass).filename);
                sw.WriteLine((myClass as MyClass).textToSave);
                sw.Close();
            }
            catch (Exception ex) //Хэндлим ошибки
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
