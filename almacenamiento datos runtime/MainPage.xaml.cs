using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Almacenamiento_Datos_Runtime.Resources;
using System.IO;

namespace Almacenamiento_Datos_Runtime
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            this.Loaded += MainPage_Loaded;
        }

        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            // en este caos no se cuenta con un método que indique si el archivo ya existe, por lo que encerramos en un try-catch
            try 
            {

                var file = Windows.Storage.ApplicationData.Current.LocalFolder.GetFileAsync(fileName);

                var stream = file.OpenStreamForReadAsync();

                using (var reader = new StreamReader())
                {
                    var content = reader.ReadToEnd();
                    reader.Close();
                    txtName.Text = content;
                }

            }
            catch (Exception e)
            {
                // no existe el archivo
            }
            
        }
        const string fileName = "archivo.txt";
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // creamos el archivo y esperamos que se cree ya que es asíncrono
            var file =  Windows.Storage.ApplicationData.Current.LocalFolder.CreateFileAsync(fileName,Windows.Storage.CreationCollisionOption.ReplaceExisting);

            var stream = file.OpenSreamForWriteAsync();

            using (var writer = new StreamWriter(stream))
            {
                writer.WriteLine(txtName.Text);
                writer.Close();
            }
        }

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}