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
        bool found = false;
        string reply;
        string[] starterssent={ "hey there", "hello lucibot", "hi lucibot","hey lucibot" };
        string[] queswords={ "who","you","produced","introduce","what","producer","?"};
        string[] quessent={"who made you","who produced you","who are you","introduce yourself","what is your name","your name"};
        string[] userwords = { "i", "me", "made", "my", "name","produced","?" };
        string[] usersent = { "who made me", "who am i", "who produced me", "what is my name", "my name" };
        string[] complementssent={ "same here", "thank you","you are awesome" };
        string[] complementswords = { "same", "thank","thanks","awesome","wow","cool" };
        string[] starterswords = { "hey", "hii", "hi", "hello" };
        bool istyping = false;
        int askedname = 0;
        Random rnd = new Random();
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
            input.Text = "";
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
            found = false;
            if (found == false)
            {
                for (int i = 0; i < starterssent.Length; i++)
                    if (input.Text == starterssent[i])
                    {
                        found = true;
                        startings();
                    }
            }
            if (found == false)
            {
                for (int i = 0; i < complementssent.Length; i++)
                    if (input.Text == complementssent[i])
                    {
                        complementing();
                        found = true;
                    }
            }
            if (found == false)
            {
                for (int i = 0; i < quessent.Length; i++)
                    if (input.Text == quessent[i])
                    {
                        found = true;
                        intro();
                    }
            }
            if (found == false)
            {
                for (int i = 0; i < usersent.Length; i++)
                    if (input.Text == usersent[i])
                    {
                        found = true;
                        outro();
                    }
            }
             if (found==false)
                 matrixengine();
        }

        private void matrixengine()
        {
            string[] words = input.Text.Split(' ');
            if (found == false)
            {
                for (int i = 0; i < complementswords.Length; i++)
                    if (words.Contains(complementswords[i]))
                    {
                        found = true;
                        complementing();
                    }
            }
            if (found == false)
            {
                for (int i = 0; i < starterswords.Length; i++)
                    if (words.Contains(starterswords[i]))
                    {
                        found = true;
                        startings();
                    }
            }
            if (found == false)
            {
                for (int i = 0; i < queswords.Length; i++)
                    if (words.Contains(queswords[i]))
                    {
                        found = true;
                        intro();
                    }
            }
            if (found == false)
            {
                for (int i = 0; i < userwords.Length; i++)
                    if (words.Contains(userwords[i]))
                    {
                        found = true;
                        outro();
                    }
            }
            if(found==false)
            outofbox();
        }

        private void outro()
        {
            int ch = rnd.Next(0, 4);
            if (askedname == 0)
                startings();
            else
            {
                switch (ch)
                {
                    case 0:
                        reply = "Let me guess..You are " + urname + " right?? humans made by god..hmm";
                        timer.Start();
                        break;
                    case 1:

                        reply = "God Made you..I think you are " + urname;
                        timer.Start();
                        break;
                    case 2:
                        reply = "Wait wait he told me,you got to be " + urname;
                        timer.Start();
                        break;
                    case 3:
                        reply = "We just discussed,you are " + urname+" A God's production";
                        timer.Start();
                        break;
                }
            }
        }
        private void outofbox()
        {
            if (askedname == 0)
                startings();
            int ch = rnd.Next(0, 4);
            switch (ch)
            {
                case 0:
                    reply = "I need a God's Dictionary for that...!!!";
                    timer.Start();
                    break;
                    break;
                case 1:
                    reply = "Oops,thats currently out of my dictionary.try something else";
                    timer.Start();
                    break;
                case 2:
                    reply = "That Sounds Russian to ME..Is it so??";
                    timer.Start();
                    break;
                case 3:
                    reply = "Oh My God..I lost my intelligence...";
                    timer.Start();
                    break;
            }
        }
        private void intro()
        {
            int ch = rnd.Next(0, 4);
            switch (ch)
            {
                case 0:
                    reply = "Oh!!My name is LuciBot-Made by Lucifer(Bandhan)";
                    timer.Start();
                    break;
                case 1:
                    reply = "Lucifer made me and named me LuciBot";
                    timer.Start();
                    break;
                case 2:
                    reply = "I am a cool bot named Lucibot";
                    timer.Start();
                    break;
                case 3:
                    reply = "I am an AI bot(LuciBot),Intelligent then humans";
                    timer.Start();
                    break;
            }
        }
        private void startings()
        {
            if (askedname == 0)
            {
                
                int ch = rnd.Next(0,4);
                switch (ch)
                {
                    case 0:
                        reply = "Oh!What is your name?";
                        timer.Start();
                        break;
                    case 1:
                        reply = "i didn't Got your name..what was that?";
                        timer.Start();
                        break;
                    case 2:
                        reply = "Lets start with your name first..";
                        timer.Start();
                        break;
                    case 3:
                        reply = "This memory deleted your name by mistake..What is it?";
                        timer.Start();
                        break;
                }
                askedname = 1;
                getname();
                timer.Start();
            }
            if (askedname == 3)
            {
                int ch = rnd.Next(0,4);
                switch(ch)
                { 
                    case 0:
                reply = "I think we are already done with 'Hello and hii'...";
                timer.Start();
                break;
                    case 1:
                reply = "Heyyyyaaaa "+urname;
                timer.Start();
                break;
                    case 2:
                reply = "hii "+urname;
                timer.Start();
                break;
                    case 3:
                reply = "lets talk about something else " + urname;
                timer.Start();
                break;
                 }
            }
           
    }
    
     
          private void complementing()
        {
            int ch = rnd.Next(0,5);
              switch(ch)
              { 
                  case 0:
                   reply = "Oh!!Mention not";
                   timer.Start();
                   break;
                  case 1:
                   reply = "Pleasure is all mine";
                   timer.Start();
                   break;
                  case 2:
                   reply = "Cool...";
                   timer.Start();
                   break;
                  case 3:
                   reply = "Me too";
                   timer.Start();
                   break;
                  case 4:
                   reply = "Oh come on..Thats fine";
                   timer.Start();
                   break;

          }
        }   
     
     
      
       
       

        private void getname()
        {

            input.IsReadOnly = true;
            namein.Visibility = Windows.UI.Xaml.Visibility.Visible;
            nameout.Visibility = Windows.UI.Xaml.Visibility.Visible;
            nameok.Visibility = Windows.UI.Xaml.Visibility.Visible;
            namein.IsTabStop = true;
            this.namein.Focus(FocusState.Keyboard);
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
