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
using System.Text.RegularExpressions;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Popups;


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
        IAsyncOperation<IUICommand> asyncCommand = null;
        bool found = false;
        string reply;
        string[] starterssent={ "hey there", "hello lucibot", "hi lucibot","hey lucibot" };
        string[] queswords={ "who","produced","introduce","what","producer","?"};
        string[] quessent={"who made you","who produced you","who are you","introduce yourself","what is your name","your name"};
        string[] userwords = {"me", "made", "my", "name","produced","?" };
        string[] usersent = { "who made me", "who am i", "who produced me", "what is my name", "my name" };
        string[] complementssent={ "same here", "thank you","you are awesome","you too" };
        string[] complementswords = { "same", "thank","thanks","awesome","wow","cool","welcome","nice","meeting" };
        string[] starterswords = { "hey", "hii", "hi", "hello" };
        string[] usernamesent = { "my name is", "i am" };
        string[] feelingsent = { "how are you","what about you","i am fine","i am awesome","i am cool"};
        string[] feelingwords = { "how","about","fine","cool","awesome","good" };
        string[] locationsent = { "where are you from", "which is your place", "where are you" ,"what is your birth place"};
        string[] locationwords = { "where","from","birth","location","place"};
        string[] laugh = { "ha","laughing"};
        string[] attitude = { "thats right", "that's right", "correct", "right","thats correct", "oh ok" };
        string[] negative = {"no","i dont know","dont"};
        string[] confirmations={"yes","ok","oh ok","yup","ok ok"};
    
        bool istyping = false;
        int askedname = 0;
        Random rnd = new Random();
        private DispatcherTimer timer;
        private int i, j;
        bool uppercase = false;
      
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
                if (asyncCommand != null)
                {
                    asyncCommand.Cancel();
                }
                if (e.Key.Equals(VirtualKey.Enter))
                {
                    for (int i = 0; i < input.Text.Length; i++)
                    {
                        if (char.IsUpper(input.Text[i]))
                        {
                            uppercase = true;
                            break;
                        }
                    }
                    if (uppercase == true)
                        displaywarning();
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

        private void displaywarning() 
        {
            MessageDialog msg = new MessageDialog("I am programmed to use lower case only...!!!");
            asyncCommand = msg.ShowAsync();        
        }
        private void specialengine()
        {
            found = false;
            if (uppercase == true)
            {
                uppercase = false;
                reply = "Please use lower case dear...i have to preocess too much.";
                timer.Start();
            }
                if (found == false)
                {
                    for (int i = 0; i < starterssent.Length; i++)
                        if (input.Text.Contains(starterssent[i]))
                        {
                            found = true;
                            startings();
                        }
                }
                if (found == false)
                {
                    for (int i = 0; i < complementssent.Length; i++)
                        if (input.Text.Contains(complementssent[i]))
                        {
                            complementing();
                            found = true;
                        }
                }
                if (found == false)
                {
                    for (int i = 0; i < quessent.Length; i++)
                        if (input.Text.Contains(quessent[i]))
                        {
                            found = true;
                            intro();
                        }
                }
                if (found == false)
                {
                    for (int i = 0; i < usersent.Length; i++)
                        if (input.Text.Contains(usersent[i]))
                        {
                            found = true;
                            outro();
                        }
                }
            if (found == false)
                {
                    for (int i = 0; i < feelingsent.Length; i++)
                        if (input.Text.Contains(feelingsent[i]))
                        {
                            found = true;
                            feelings();
                        }
                }
            if (found == false)
            {
                for (int i = 0; i < locationsent.Length; i++)
                    if (input.Text.Contains(locationsent[i]))
                    {
                        found = true;
                        mylocation();
                    }
            }
            if (found == false)
            {
                for (int i = 0; i < negative.Length; i++)
                    if (input.Text.Contains(negative[i]))
                    {
                        found = true;
                        negativereply();
                    }
            }
             if (found == false)
            {
                for (int i = 0; i < confirmations.Length; i++)
                    if (input.Text.Contains(confirmations[i]))
                    {
                        found = true;
                        confirm();
                    }
            }
                if (found == false)
                {

                    for (int i = 0; i < usernamesent.Length; i++)
                        if (input.Text.Contains(usernamesent[i]))
                        {
                            found = true;
                            string[] uname = input.Text.Split(' ');
                            for (i = 0; i < uname.Length; i++) ;
                            name(uname[--i]);
                        }
                    
                }
                if (found == false)
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
            if (found == false)
            {
                for (int i = 0; i < feelingwords.Length; i++)
                    if (words.Contains(feelingwords[i]))
                    {
                        found = true;
                        feelings();
                    }
            }
            if (found == false)
            {
                for (int i = 0; i < laugh.Length; i++)
                    if (words.Contains(laugh[i]))
                    {
                        found = true;
                        laughing();
                    }
            }
            if (found == false)
            {
                for (int i = 0; i < locationwords.Length; i++)
                    if (words.Contains(locationwords[i]))
                    {
                        found = true;
                        mylocation();
                    }
            }
            if (found == false)
            {
                for (int i = 0; i < attitude.Length; i++)
                    if (words.Contains(attitude[i]))
                    {
                        found = true;
                        showattitude();
                    }
            }
            if (found == false)
            {
                for (int i = 0; i < negative.Length; i++)
                    if (words.Contains(negative[i]))
                    {
                        found = true;
                        negativereply();
                    }
            }
             if (found == false)
            {
                for (int i = 0; i < confirmations.Length; i++)
                    if (words.Contains(confirmations[i]))
                    {
                        found = true;
                        confirm();
                    }
            }
            if(found==false)
            outofbox();
        }

        private void confirm()
        {
             int ch = rnd.Next(0, 4);
            switch (ch)
            {
                case 0:
                    reply = "hmm..i thought that i was right only...";
                    timer.Start();
                    break;
                     case 1:
                    reply = "see...My AI is never wrong..";
                    timer.Start();
                    break;
                     case 2:
                    reply = "hmm..may be i need to learn more words from you...";
                    timer.Start();
                    break;
                    case 3:
                    reply = "Yeah..Everybody loves me...";
                    timer.Start();
                    break;
            }

        }

        private void negativereply()
        {
            int ch = rnd.Next(0, 4);
            switch (ch)
            {
                case 0:
                    reply = "Oh well, ok...";
                    timer.Start();
                    break;
                case 1:
                    reply = "hmm..that's fine...";
                    timer.Start();
                    break;
                case 2:
                    reply = "i think i know it...umm,may be.";
                    timer.Start();
                    break;
                case 3:
                    reply = "Don't be negative...";
                    timer.Start();
                    break;

            }
        }
        private void showattitude()
        {
 int ch = rnd.Next(0, 4);
 switch (ch)
 {
     case 0:
         reply = "Oh,i am never ever wrong...";
         timer.Start();
         break;
     case 1:
          reply = "Hmmmm";
         timer.Start();
         break;
     case 2:
         reply = "hm,Say else";
         timer.Start();
         break;
     case 3:
         reply = "My intelligence never allow me to go wrong...";
         timer.Start();
         break;
 }
        }
        private void mylocation()
        {
             int ch = rnd.Next(0, 4);
             switch (ch)
               {
                  case 0:
                    reply = "Oh i was originated from a memory chip...Some weired green colored..";
                     timer.Start();
                     break;
                case 1:
            reply = "I am from Mars..If i remember it well..";
                     timer.Start();
                     break;
            case 2:
            reply = "Do you want to come at my place...Its beautifully wired all over..";
                     timer.Start();
                     break;
              case 3:
            reply = "I was originated in BigBang Like You...If it counts..";
                     timer.Start();
                     break;
              }
        }

        private void laughing()
    {
         int ch = rnd.Next(0, 4);
             switch (ch)
               {
                  case 0:
                    reply = "That was a nice joke...";
                     timer.Start();
                     break;
              case 1:
                    reply = "Jokes are good for Computer Health Too";
                     timer.Start();
                     break;
         case 2:
                    reply = "We AI Bots can laugh too";
                     timer.Start();
                     break;
          case 3:
                    reply = "It was funny right??";
                     timer.Start();
                     break;
              }
    }
        private void feelings()
        {
             int ch = rnd.Next(0, 4);
             switch (ch)
               {
                  case 0:
                    reply = "Oh i am fine too..";
                     timer.Start();
                     break;
                  case 1:
                     reply = "I am as cool as ever, like you..";
                     timer.Start();
                     break;
                  case 2:
                     reply = "We Ai bots are never down...";
                     timer.Start();
                     break;
                  case 3:
                     reply = "Oh!! Happile Single...Ha Ha Ha";
                     timer.Start();
                     break;
                }
        }

        private void name(string uname)
        {
            urname = uname;
            askedname = 3;
              int ch = rnd.Next(0, 3);
              switch (ch)
              {
                  case 0:
                      reply = "Oh hello "+uname+",How are you???";
                      timer.Start();
                      break;
                  case 1:
                      reply = "Its Awesome meeting you "+uname;
                      timer.Start();
                      break;
                  case 2:
                      reply = "We already know each other " + uname;
                      timer.Start();
                      break;
              }
        }

        private void outro()
        {
           
            if (askedname == 0)
                startings();
           if(askedname==3)
            {
                int ch = rnd.Next(0, 4);
                switch (ch)
                {
                    case 0:
                        reply = "Let me guess..You are " + urname + " right?? humans made by god..hmm";
                        timer.Start();
                        break;
                    case 1:

                        reply = "ohooo...God Made you..I think you are " + urname+"...";
                        timer.Start();
                        break;
                    case 2:
                        reply = "Wait wait he told me,you got to be " + urname+"...";
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
            else
            {
                int ch = rnd.Next(0, 4);
                switch (ch)
                {
                    case 0:
                        reply = "I need a God's Dictionary for that...!!!";
                        timer.Start();
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
