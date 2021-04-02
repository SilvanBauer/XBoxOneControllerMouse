using System.Windows;

namespace XBoxOneControllerMouse {
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window {
        public int Sensitivity { get; set; }
        public int TimeBetweenUpdates { get; set; }

        public Settings() {
            InitializeComponent();

            DataContext = this;

            Sensitivity = Properties.Settings.Default.Sensitivity;
            TimeBetweenUpdates = Properties.Settings.Default.TimeBetweenUpdates;
        }

        private void Save(object sender, RoutedEventArgs e) {
            Properties.Settings.Default.Sensitivity = Sensitivity;
            Properties.Settings.Default.TimeBetweenUpdates = TimeBetweenUpdates;
            Properties.Settings.Default.Save();

            Close();
        }
    }
}
