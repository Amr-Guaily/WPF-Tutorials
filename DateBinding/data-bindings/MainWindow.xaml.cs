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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace data_bindings
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            // If you are using MVVM, the data context would be set to a viewModel
           this.DataContext = this;
           InitializeComponent();
        }

        private string boundText;
        public string BoundText
        {
            get { return boundText; }
            set
            {
                /*
                    What we need to achieve?
                      # Real-time updates: when we write somthing in "textbox" => the "textblock" should be updated immediately..
                        So, we need a way to tell/notify the UI when the "BoundText" property chanaged..
                        
                      ## Approach_1: access TextBox control directly..
                        - This approach have some downsides:
                              * Directly accessing controls in code-behind ties your business logic to the UI. This makes your code harder to maintain, test, and reuse.
                              * for real-time updates we can achieve this behaviour within "TextChanged" event. but we still have the same problem (Directly accessing controls in code-behind).
                            
                      ## Approach_2: Data binding using INotifyPropertyChanged..
                        - In WPF, for the UI to refelect changes in a property => "The property must notify the UI when it changes".
                        - This is typically done using the "INotifyPropertyChaged" interface.
                        - By implementing this interface, we make sure that our UI stays in sync with the underlying data.
                 */
                boundText = value;
                //  ### Approach_1 ###
                // tbResult.Text = boundText;

                // ### AQpproach_2 ###
                // onPropertyChanged(nameof(BoundText));
                onPropertyChanged();
            }
        }

        //  ### Approach_1 ###
        // private void txtInput_TextChanged(object sender, TextChangedEventArgs e)
        // {
        //     tbResult.Text = txtInput.Text;
        // }

        // ### Approach_2 ###
        public event PropertyChangedEventHandler? PropertyChanged;
        // private void onPropertyChanged(string propertyName)
        // {
        //     PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        // }


        // By using [CallerMemberName] => It's going to automatically populate this argument based on the caller.
        // In our case the caller name is "BoundText".
        private void onPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void btnSet_Click(object sender, RoutedEventArgs e)
        {
            BoundText = "Set from code";
        }

    }
}