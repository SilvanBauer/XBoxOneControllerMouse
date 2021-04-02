using System.Windows;
using System.Windows.Controls;

namespace XBoxOneControllerMouse {
    /// <summary>
    /// Interaction logic for TrayPopUp.xaml
    /// </summary>
    public partial class TrayPopUp : UserControl {
        public TrayPopUp() {
            InitializeComponent();
        }

        private void Exit(object sender, RoutedEventArgs e) => Application.Current.Shutdown();

        private void Settings(object sender, RoutedEventArgs e) => new Settings().ShowDialog();
    }
}
