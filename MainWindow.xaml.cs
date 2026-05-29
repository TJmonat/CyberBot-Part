using System.Collections;
using System.Media;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using System;


namespace POE2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ArrayList keywords = new ArrayList();
        ArrayList responses = new ArrayList();
        //EXTERNAL AUDIOS TO INHANCE THE PROGRAM
       SoundPlayer intro=new SoundPlayer("C:\\Users\\tsheg\\source\\repos\\POE2\\POE2\\Introduction.wav");
        SoundPlayer errror= new SoundPlayer("C:\\Users\\tsheg\\source\\repos\\POE2\\POE2\\errorSound.wav");
        SoundPlayer ending= new SoundPlayer("C:\\Users\\tsheg\\source\\repos\\POE2\\POE2\\Red Daisy Street.wav");

        bool waitingForName = true;
        //USED TO STORE THE USERS NAME AND THEIR FAVOURITE TOPIC
        string usersName = " ";
        string favTopic = " ";

        public MainWindow()
        {
            InitializeComponent();
            storeResponses();
            
            intro.Play();//PLAYING THE INTRO 
            //FIRST STATEMAENTS OF THE PROGRAM WHEN IT RUNS
            AddBotMessage("Good Day!");
            AddBotMessage("Welcome To The Cyber Awareness Bot, Were i am here to help you stay safe online");
            AddBotMessage("What is your name?");

           
        }
       //USING AN ARRAY LIST TO STORE KEYWORDS AND RESPONSES THAT WILL BE USED TO FIND THE ANSWERS TO THE USERS QUESTIONS
        private void storeResponses()
        {
            keywords.Add("hello");
            responses.Add("Hey There!");

            keywords.Add("how are you?");
            responses.Add("I am doing great today, Thank You!");

            keywords.Add("cyber attack");
            responses.Add("Phising, Scammers and Password safety");


            keywords.Add("phishing");
            responses.Add("Phishing is a type of cyber attack where criminals impersonate organizations or individuals via email, text or phone to steal sensitive information like passwords.");

            keywords.Add("password");
            responses.Add("A secret string of characters like letters and numbers and symbols used to verify your identity");

            keywords.Add("scam");
            responses.Add("A scam is a deceptive scheme designed to trick you into giving away your money or personal information");

            keywords.Add("tell me more");
            responses.Add("To be safe online, Enable MFA, Make updates automatically, Use secure WI-FI, Always review privacy settings, Verify senders and use complex passwords");


            keywords.Add("tips");
            responses.Add(@"
               For Phishing: Never Click unsolicated links or download unexpected attachments
               For Scammers: Never answer calls from unknown numbers or click on unexpected links
               For Password Safety: Always ensure password is at least 16b characters long, contains, number, upper and lower case and a special character");

            keywords.Add("worried");
            responses.Add("It is completely normal to feel that way that is why i am here to help you stay safe.");

            keywords.Add("curious");
            responses.Add("Feel free to ask any cyber related questions to help clarify your curiousity.");

            keywords.Add("frustrated");
            responses.Add("Online safety can be frustrating but you can relax i am here to help reduce the frustration.");

            keywords.Add("bye");
            responses.Add("Thank you for using the cyber awareness Bot, hope this helped");

        }
        //TO CHECK IF THE NAME AND THE QUESTIONS ARE VALID
        private bool isValidName(string name)
        {
            return Regex.IsMatch(name, @"^[a-zA-Z\s]+$");
        }
        private bool isInvalidQuestion(string input)
        {
            return Regex.IsMatch(input, @"^\d+$");
        }
        //THIS METHOD RUNS WHEN THE USER CLICKS THE SEND BUTTON
        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            string userMessage = MessageTextBox.Text;
            if (!string.IsNullOrWhiteSpace(userMessage))
            {
                AddUserMessage(userMessage);

                if (waitingForName)
                {
                    while (waitingForName)
                    {
                        if (isValidName(userMessage))
                        {
                            usersName = userMessage;
                            waitingForName = false;
                            AddBotMessage("Welcome " + usersName + " to the cyber awareness bot");

                        }
                        else
                        {
                            errror.Play();
                            AddBotMessage("Invalid Name");
                        }
                        break;
                    }
                }
                else
                {
                    if (isInvalidQuestion(userMessage))
                    {
                        errror.Play();
                        AddBotMessage("Please enter a valid question using words");
                    }
                    else
                    {
                        if (userMessage.ToLower().Contains("interested in"))
                        {
                            favTopic = userMessage.Substring(userMessage.ToLower().IndexOf("interested in") + 13).Trim();
                            AddBotMessage("I'll remember that you are interested in " + favTopic);
                        }
                        
                        else if (userMessage.ToLower().Contains("what is my favourite topic"))
                        {
                            if (favTopic != "")
                            {
                                AddBotMessage("Your Favourite Topic is  " + favTopic);
                                if (favTopic.Contains("phishing"))
                                {
                                    AddBotMessage("As someone who is interested in phishing, always ensure to look out for suspicious links.");
                                }
                                else if (favTopic.Contains("scam"))
                                {
                                    AddBotMessage("As someone who is interested in learning more about scams, monitor inconsistancies");
                                }
                                else
                                {
                                    AddBotMessage("As someone is interested in password safety, ensure your password is 16 characters long");
                                }
                            }





                            else
                            {
                                AddBotMessage("You have not yet told me your name");
                            }
                        }
                        else if (userMessage.ToLower().Contains("what is my name"))
                        {
                            AddBotMessage("Your name is:" + usersName);
                        }
                        else
                        {

                            string botreply = FindResponse(userMessage);
                            AddBotMessage(botreply);
                            if (userMessage.ToLower() =="bye")
                            {
                                ending.Play();
                                AddBotMessage("GoodBye" + usersName);
                            }
                        }
                    }

                }

                MessageTextBox.Clear();
            }
        }
        //THIS METHOD LINKS THE USERS QUESTION AND INPUT TO THE KEY WORDS AND PRINTS OUT THE STATEMENT
        private string FindResponse(string message)
        {
            message = message.ToLower();
            for (int i = 0; i < keywords.Count; i++)
            {
                if (message.Contains(keywords[i].ToString()))
                {
                    return responses[i].ToString();
                }
            }
            errror.Play();
            return "Sorry i do not understand";
        }
        //THIS PLACES THE BOTS MESSAGES ON THE LEFT
        private void AddBotMessage(string message)
        {
            string time = DateTime.Now.ToString("HH:mm");

            Border b = new Border
            {
                Background = Brushes.LavenderBlush,
                CornerRadius = new CornerRadius(10),
                Padding = new Thickness(10),
                Margin = new Thickness(10),
                HorizontalAlignment = HorizontalAlignment.Left,
                MaxWidth = 300


            };
            TextBlock text = new TextBlock
            {
                Text = "BOT["+time+"]:" + message,
                Foreground = Brushes.Black,
                FontSize = 14,
                TextWrapping = TextWrapping.Wrap,
            };
            b.Child = text;
            ChatPanel.Children.Add(b);
        }
        //THIS METHOD PLACES THE USERS INPUT ON THE RIGHT
        private void AddUserMessage(string message)
        {
            string time = DateTime.Now.ToString("HH:mm");

            Border b = new Border
            {
                Background = Brushes.LavenderBlush,
                CornerRadius = new CornerRadius(10),
                Padding = new Thickness(10),
                Margin = new Thickness(10),
                HorizontalAlignment = HorizontalAlignment.Right,
                MaxWidth = 300,


            };

            TextBlock text = new TextBlock
            {
                Text = "YOU["+ time+"] :"+ message,
                Foreground = Brushes.Black,
                FontSize = 14,
                TextWrapping = TextWrapping.Wrap,
            };
            b.Child = text;
            ChatPanel.Children.Add(b);
        }

        private void MessageTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        
    }
}