using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MongoDBImportDataApplication
{
    /// <summary>
    /// Interaction logic for ImportSingleEntity.xaml
    /// </summary>
    public partial class ImportSingleEntity : Page
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ImportController singleImport;
        public string CollectionName { get; set; }
        public string PropertyName { get; set; }

        public ImportSingleEntity()
        {
            InitializeComponent();
            singleImport = new ImportController();
            tbCollectionName.Text = string.Empty;
            tbPropertyName.Text = string.Empty;
            Resources["Entity"] = singleImport.entity.dataDictionary.data;
        }

        /// <summary>
        /// Remove all entity data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearEntity_Click(object sender, RoutedEventArgs e)
        {
            singleImport.ClearEntity();
            tbPropertyName.Text = string.Empty;
            tbPropertyValue.Text = string.Empty;
        }

        /// <summary>
        /// Commit a single entity to MongoDB
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CommitEntity_Click(object sender, RoutedEventArgs e)
        {
            // Must set the Collection name
            if (!String.IsNullOrWhiteSpace(tbCollectionName.Text))
            {
                CollectionName = tbCollectionName.Text;
                singleImport.CommitEntity(CollectionName);
                tbPropertyName.Text = string.Empty;
                tbPropertyValue.Text = string.Empty;
                
            }
            else
            {
                MessageBox.Show("Error: Please Enter a Collection Name.");
            }
        }

        /// <summary>
        /// Add a property to the entity
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddProperty_Click(object sender, RoutedEventArgs e)
        {
            // Must set the Property name
            if (!String.IsNullOrWhiteSpace(tbPropertyName.Text))
            {
                PropertyName = tbPropertyName.Text;

                if (!singleImport.entity.dataDictionary.data.ContainsKey(PropertyName))
                {
                    var propType = cbPropertyType.SelectedItem as string;
                    if (!String.IsNullOrWhiteSpace(tbPropertyValue.Text))
                    {
                        switch (propType)
                        {
                            case "String":
                                // TODO: Add Random Generated String if cGenerate is checked
                                singleImport.entity.AddProperty(PropertyName, tbPropertyValue.Text);
                                OnPropertyChanged("Entity");
                                break;
                            case "Int":
                                if ((bool)cGenerate.IsChecked)
                                {
                                    var randInt = new Random(100);
                                    singleImport.entity.AddProperty(PropertyName, randInt.Next(100));
                                    OnPropertyChanged("Entity");
                                }
                                else
                                {
                                    int intVal;
                                    if (Int32.TryParse(tbPropertyValue.Text, out intVal))
                                    {
                                        singleImport.entity.AddProperty(PropertyName, intVal);
                                        OnPropertyChanged("Entity");
                                    }
                                    else
                                    {
                                        MessageBox.Show("Error: Please Enter a valid Integer Property Value or check the generate box.");
                                    }

                                }
                                break;
                            case "Date Time":
                                if ((bool)cGenerate.IsChecked)
                                {
                                    singleImport.entity.AddProperty(PropertyName, DateTime.UtcNow);
                                    OnPropertyChanged("Entity");
                                }
                                else
                                {
                                    DateTime dateVal;
                                    if (DateTime.TryParse(tbPropertyValue.Text, out dateVal))
                                    {
                                        singleImport.entity.AddProperty(PropertyName, dateVal);
                                        OnPropertyChanged("Entity");
                                    }
                                    else
                                    {
                                        MessageBox.Show("Error: Please Enter a valid Date Time property value or check the generate box.");
                                    }
                                }
                                break;
                            case "Time Span":
                                if ((bool)cGenerate.IsChecked)
                                {
                                    var randTime = new Random();
                                    singleImport.entity.AddProperty(PropertyName, new TimeSpan(randTime.Next(3, 6), 0, 0));
                                    OnPropertyChanged("Entity");
                                }
                                else
                                {
                                    TimeSpan spanVal;
                                    if (TimeSpan.TryParse(tbPropertyValue.Text, out spanVal))
                                    {
                                        singleImport.entity.AddProperty(PropertyName, spanVal);
                                        OnPropertyChanged("Entity");
                                    }
                                    else
                                    {
                                        MessageBox.Show("Error: Please Enter a valid Time Span property value or check the generate box.");
                                    }
                                }
                                break;
                            default:
                                MessageBox.Show("Error: Unknown Property Type selected.");
                                break;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Error: Please Enter a Property Value or check the generate box.");
                    }
                }
                else
                {
                    MessageBox.Show("Error: Please Enter a Property Name.");
                }
            }
            else
            {
                MessageBox.Show("Error: Property Name already Exists");
            }
        }

        /// <summary>
        /// Remove a single property from the entity
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoveProperty_Click(object sender, RoutedEventArgs e)
        {
             // Must set the Property name
            if (!String.IsNullOrWhiteSpace(tbPropertyName.Text))
            {
                PropertyName = tbPropertyName.Text;
                if (singleImport.entity.dataDictionary.data.ContainsKey(PropertyName))
                {
                    singleImport.entity.RemoveProperty(PropertyName);
                    OnPropertyChanged("Entity");
                }
                else
                {
                    MessageBox.Show("Error: Property Name does not exist.");
                }
            }
            else
            {
                MessageBox.Show("Error: Please Enter a Property Name.");
            }
        }

        private void cbPropertyType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        /// <summary>
        /// Initialize the Property Type Combo Box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbPropertyType_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> data = new List<string>()
            {
                "String", 
                "Int",
                "Date Time",
                "Time Span"
            };

            var comboBox = sender as ComboBox;
            comboBox.ItemsSource = data;
            comboBox.SelectedIndex = 0;
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

    }
}
