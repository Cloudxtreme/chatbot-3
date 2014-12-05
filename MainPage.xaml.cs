using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.System;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace chatbot
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        string myname = "LUCIBOT";
        string urname;
        int flag;
        string reply;
        bool istyping = false;
        int askedname = 0;
        private DispatcherTimer timer;
        private int i, j;
      
        public MainPage()
        {
            this.InitializeComponent();
             flag = 0;
             timer = new DispatcherTimer();
             timer.Tick += timer_Tick;
            timer.Interval = TimeSpan.FromSeconds(.01);
            namein.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            nameout.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            nameok.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }
        void timer_Tick(object sender, object e)
        {

            input.IsReadOnly = true;
            
                if (i < reply.Length)
                {
                    output.Text += reply[i];
                    i++;
                    istyping = true;
                }
                else
                {
                    istyping = false;
                    timer.Stop();
                    input.IsReadOnly = false;
                    i = 0;
                }
            }
        
        

    
        private void onKeyDownHandler(object sender, KeyRoutedEventArgs e)
        {
            if (istyping == false)
            {
                if (e.Key.Equals(VirtualKey.Enter))
                {
                    output.Text = "";
                    if (input.Text == "")
                    {
                        if (flag == 0)
                        {
                            reply = "Oh God ! Why am i always an ICEBREAKER !!";
                            timer.Start();
                            Random rnd = new Random();
                            flag = rnd.Next(0, 3);
                        }
                        else if (flag == 1)
                        {
                            reply = "Hey ! How Can i reply to Blank Input.Even Robots need a good starter..!!!";
                            timer.Start();
                            flag = 2;
                        }
                        else if (flag == 2)
                        {
                            reply = "Is your Finger Stuck with 'Enter Key'??? Type Something dude";
                            timer.Start();
                            flag = 3;
                        }
                        else
                        {
                            flag = 0;
                        }
                    }
                    if (askedname == 1)
                    {
                        input.IsReadOnly = true;
                    }
                    else
                        specialengine();
                        
                   
                }
            }
        }

        private void specialengine()
        {
            string[] starterssent = { "hey there", "hello lucibot", "hi lucibot" };
            for (int i = 0; i < starterssent.Length; i++)
            {
                if (input.Text == starterssent[i] && askedname == 0)
                {
                        reply = "What is Your Name ?";
                        askedname = 1;
                        getname();
                        timer.Start();
                        break;
                }
                if (input.Text == starterssent[i] && askedname == 3)
                    {
                        reply = "I think we are already done with 'Hello and hii'...";
                        timer.Start();
                    }
                else
                {
                    matrixengine();
                    break;
                }
            }
        }

     
        private void getname()
        {
          
            input.IsReadOnly = true;
            namein.Visibility = Windows.UI.Xaml.Visibility.Visible;
            nameout.Visibility = Windows.UI.Xaml.Visibility.Visible;
            nameok.Visibility = Windows.UI.Xaml.Visibility.Visible;
        }
       
        private void matrixengine()
        {
            string[] starterswords = { "hey","hii","hi","hello" };
            for (int i = 0; i < starterswords.Length; i++)
            {
                if (input.Text == starterswords[i] && askedname == 0)
                {
                    reply = "What is Your Name ?";
                    askedname = 1;
                    getname();
                    timer.Start();
                    break;
                }
                if (input.Text == starterswords[i] && askedname == 3)
                {
                    reply = "heyyaaaa";
                    timer.Start();
                    break;
                }
            }
        }
        private void output_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }

        private void nameok_Click(object sender, RoutedEventArgs e)
        {
            urname=namein.Text;
            askedname = 3;
            namein.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            nameout.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            nameok.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            output.Text = "";
            reply = "Hello "+urname+" Nice to meet you...";
            input.Text = "";
            timer.Start();
        }

       
    }
}
