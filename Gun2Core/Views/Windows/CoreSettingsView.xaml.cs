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
            vm.SaveSettigns();
            Close();
        }

        private void OnCancelButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private CoreSettingsViewModel vm;
    }
}
