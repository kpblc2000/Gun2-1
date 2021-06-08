using Gun2Core.ViewModels;
using System.Windows;

namespace Gun2Core.Views.Windows
{
    /// <summary>
    /// Логика взаимодействия для CoreSettingsView.xaml
    /// </summary>
    public partial class CoreSettingsView : Window
    {
        public CoreSettingsView()
        {
            InitializeComponent();
            vm = this.DataContext as CoreSettingsViewModel;
        }

        private void OnAcceptButtonClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            vm.SaveSettigns();
            Close();
        }

        private void OnCancelButtonClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private CoreSettingsViewModel vm;
    }
}
