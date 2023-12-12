using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_Database2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private AnimalService service;

        public MainWindow()
        {
            InitializeComponent();
            this.service = new AnimalService();
            Read();
        }

        private void Read()
        {
            dataGridAnimals.ItemsSource = this.service.GetAll();
        }

        private void buttonCreate_Click(object sender, RoutedEventArgs e)
        {

        }

        private void buttonUpdate_Click(object sender, RoutedEventArgs e)
        {

        }

        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            Animal selected = dataGridAnimals.SelectedItem as Animal;
            if (selected == null)
            {
                MessageBox.Show("Törléshez válasszon ki egy állatot!");
                return;
            }

            MessageBoxResult result = MessageBox.Show($"Biztosan törölni szeretné az alábbi állatot: {selected.Name}?",
                "Törlés", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                if (this.service.Delete(selected.Id))
                {
                    MessageBox.Show("Sikeres törlés!");
                }
                else
                {
                    MessageBox.Show("Sikertelen törlés!");
                }
                Read();
            }
        }
    }
}