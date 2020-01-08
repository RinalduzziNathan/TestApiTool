using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ApiTestingTool
{
    
    public sealed partial class MainPage : Page
    {
        GetJson Get;
        PostJson Post;
        public MainPage()
        {
            this.InitializeComponent();
            Post = new PostJson("null","oof");
            //Post.PostTheJson();
            Get = new GetJson("null");


                        

        }

        public object SampleTextBox { get; private set; }

        public async static void debug(string Message)//debug function, will print the parameter in a popup
        {
            var dialog = new MessageDialog(Message);
            await dialog.ShowAsync();
        }

        //click on Fetch_Button, async because it can take some time if you have bad connection so it doesn't block the thread
        private async void BTN_Fetch_Click(object sender, RoutedEventArgs e)
        {
            //security if the user input is empty
            if (URL_GET_Text.Text == string.Empty)
            {
                debug("Please enter an URL");
                return;
            }
            string ResulMessage = await Get.FetchTheUrl();
            GET_RESULT_TEXT.Text = ResulMessage;
        }

        //When user enter something on the textfield
        private void URL_GET_CHANGED(object sender, TextChangedEventArgs e)
        {
            Get._url = URL_GET_Text.Text;
        }

        private void String_POST_CHANGED(object sender, TextChangedEventArgs e)
        {
            Post._json = Message_POST_Text.Text;
        }

        private void URL_POST_CHANGED(object sender, TextChangedEventArgs e)
        {
            Post._url = URL_POST_Text.Text;
         
        }

        //click Post_Button
        private async void BTN_POST_Click(object sender, RoutedEventArgs e)
        {
            //security if the user input is empty
            if (URL_POST_Text.Text == String.Empty)
            {
                debug("Please enter an URL");
                return;
            }
            string s = await Post.PostTheJson();
            POST_RESPONSE_TEXT.Text =s ;

        }

        private void BTN_Clear_Click(object sender, RoutedEventArgs e)
        {
            GET_RESULT_TEXT.Text = String.Empty;
        }
    }
}
