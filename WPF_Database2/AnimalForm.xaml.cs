using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPF_Database2
{
    /// <summary>
    /// Interaction logic for AnimalForm.xaml
    /// </summary>
    public partial class AnimalForm : Window
    {
        private AnimalService service;
        private Animal animalToUpdate;
        public AnimalForm(AnimalService animalService)
        {
            InitializeComponent();
            this.service = animalService;
            this.labelTitle.Content = "Új állat felvétele";
        }

        public AnimalForm(AnimalService animalService, Animal animal)
        {
            InitializeComponent();
            this.service = animalService;
            this.animalToUpdate = animal;
            this.buttonAdd.Visibility = Visibility.Collapsed;
            this.buttonModify.Visibility = Visibility.Visible;
            this.labelTitle.Content = "Beköltöző állat módosítása";

            textBoxName.Text = animal.Name;
            textBoxAge.Text = animal.Age.ToString();
            radioButtonHerbivorous.IsChecked = animal.Species == "növényevő";
            radioButtonPredatory.IsChecked = animal.Species == "ragadozó";
            radioButtonOmnivorous.IsChecked = animal.Species == "mindenevő";
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Animal animal = CreateAnimalFromInput();
                if (this.service.Create(animal))
                {
                    MessageBox.Show("Sikeres felvétel!");
                    textBoxName.Text = "";
                    textBoxAge.Text = "";
                    radioButtonHerbivorous.IsChecked = false;
                    radioButtonPredatory.IsChecked = false;
                    radioButtonOmnivorous.IsChecked = false;
                }
                else
                {
                    MessageBox.Show("Hiba történt a felvétel során!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonModify_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Animal animal = CreateAnimalFromInput();
                if (this.service.Update(this.animalToUpdate.Id, animal))
                {
                    MessageBox.Show("Sikeres módosítás!");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Hiba történt a módosítás során! Javasolt az ablak bezárása!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private Animal CreateAnimalFromInput()
        {
            string name = textBoxName.Text.Trim();
            string ageString = textBoxAge.Text.Trim();
            string species = "";
            if ((bool)radioButtonHerbivorous.IsChecked)
            {
                species = "növényevő";
            } else if ((bool)radioButtonPredatory.IsChecked)
            {
                   species = "ragadozó";
            } else if ((bool)radioButtonOmnivorous.IsChecked)
            {
                species = "mindenevő";
            }

            if (string.IsNullOrEmpty(name))
            {
                throw new Exception("Az állat nevének megadása kötelező!");
            }
            if (string.IsNullOrEmpty(ageString))
            {
                throw new Exception("Az állat életkorának megadása kötelező!");
            }
            if (!int.TryParse(ageString, out int age))
            {
                throw new Exception("Az állat életkora csak szám lehet!");
            }
            if (age < 1 || age > 50)
            {
                throw new Exception("Az állat életkora 1 és 50 közötti szám lehet!");
            }
            if (!(bool)radioButtonHerbivorous.IsChecked && !(bool)radioButtonPredatory.IsChecked && !(bool)radioButtonOmnivorous.IsChecked)
            {
                throw new Exception("Az állat fajtájának megadása kötelező!");
            }

            Animal animal = new Animal();
            animal.Name = name;
            animal.Age = age;
            animal.Species = species;
            return animal;
        }
    }
}
