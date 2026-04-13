using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech;
using System.Speech.Synthesis;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Media;

namespace Test2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //These properties will allow me to copy and paste symbols and will display them when the code runs
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;
          

            Console.WriteLine(@"
 __ .   ,.__ .___.__   .__..  ..__..__ .___.  ..___ __. __.  .__ .__..___.
/  ` \./ [__)[__ [__)  [__]|  |[__][__)[__ |\ |[__ (__ (__   [__)|  |  |  
\__.  |  [__)[___|  \  |  ||/\||  ||  \[___| \|[___.__).__)  [__)|__|  |  
                                                                          
");//THE READ LINE WILL DISPLAY THIS ASCII FONT AS THE HEADING OF THE BOT
            //calling the methods
            Greeting();
            header();
           usersInfo();
            //this code will allow the user to input their desired user name so we can personalise the responses
            Responses userNme = new Responses();
            ErrorHandling nameHandle = new ErrorHandling();
            userNme.TypeEffects("Please input your desired user name:");
            string userName = nameHandle.userNameErrorHandling();
            SpeechSynthesizer speak = new SpeechSynthesizer();//THE SPEECH SYNTHESIZER BUILD IN CLASS WILL HELP CREATE A VOICE OVER FOR CERTAIN TEXTS

            //the speak object will help personalise thhe message below
            speak.Speak(userName + "Please ensure your questions and responses are in small caps to make the awareness bot work effectively");
           //calling the methods
            cyberAwareness();
           list();
            QandA();
            
            //creating an exit ASCII art to enhance the user experience
            Console.WriteLine(@"
  ______  _____   _____  ______  ______  __   __ _______
 |  ____ |     | |     | |     \ |_____]   \_/   |______
 |_____| |_____| |_____| |_____/ |_____]    |    |______
                                                        
");
            //this part will give the user a personalised goodbye message
            speak.Speak(" Thank you " + userName +  " for using the awareness bot, hope this helped you, Stay safe and goodbye");
            Console.ReadKey();



        }
        public static void Greeting()
        {
            //adding the voice sound player to place the opening message
            SoundPlayer voice = new SoundPlayer("Introduction.wav");
            voice.Play();
        }
        public static void header()
        {
            //Section header
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("=========👨🏽‍💻CYBER AWARENESS BOT🔐===================");
            Console.ResetColor();
        }
        
        public static void usersInfo()
        {//creating objects for these classes so we can access their methods 
            SpeechSynthesizer talk = new SpeechSynthesizer();
              ErrorHandling errors = new ErrorHandling();

            //changing the colour of the questions
            Console.ForegroundColor= ConsoleColor.DarkBlue;
            //the program will have dark blue colour to add creativity 

            Console.WriteLine("What is your name ?");//PROMPTING THE USER FOR THEIR information
            Console.ResetColor();
            string name = errors.nameErrorHandling();
            Console.ForegroundColor= ConsoleColor.DarkBlue;

            Console.WriteLine("What is your surname ?");
            Console.ResetColor();
            string surname = errors.surnameErrorHandling();

            Console.ForegroundColor = ConsoleColor.DarkBlue;

            Console.WriteLine ("How old are you?");
            Console.ResetColor();
            int age=errors.ageErrorHandling();

            Console.ForegroundColor = ConsoleColor.DarkBlue;

            Console.WriteLine("Date of birth: please use the format dd/MM/yyyy");
            Console.ResetColor();
            DateTime dob=errors.dateErrorHandling();
            
            //this will personalise the welcoming message plus the ASCII art
           talk.Speak(name + surname + " WELCOME TO THE CYBER AWARENESS BOT ");//USING THE SPEAK OBJECT TO WELCOME THE USER AND USE THE SPEECH SYNTHESIZER TO SAY THE USERS NAME
            Console.WriteLine(@"
    __    __ _     __
| ||_ |  /  / \|V||_ 
|^||__|__\__\_/| ||__
");//ADDING THE WELCOME ASCII ART TO CREATIVELY WELCOME THE USER
        }
        public static void cyberAwareness()
        {
            Console.ForegroundColor= ConsoleColor.DarkBlue;
            Console.WriteLine("Do you know what cyber awareness is? yes/no");//this part asks the user if they know what cyber awareness is
            Console.ResetColor();
            string aware = Console.ReadLine();
            if (aware.StartsWith("n"))//the if statement will check if the user say no then it will display the message
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                //the border will place the defination in a clean state
                Console.WriteLine(@"
=================================================================
");
                Console.WriteLine(@"
              Cyber Awareness👨🏽‍💻
Is the knowledge and understanding 
that users have about online threats and best practices
to protect networks.
");
                Console.WriteLine(@"
=================================================================
");
                Console.ResetColor();

            }
        }

        public static void list()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            //this print statement will display the questions that are available
            Console.WriteLine(@"
============These Are Our Available Questions=============
");
            Console.WriteLine(@"
~Purpose of the awareness bot
~Cyber attack most face
~What Phising is
~Format of a secure password and example
~Tips for safe online browsing
~What hacking is
~How to avoid hackers
*PLEASE ENSURE YOUR QUESTIONS ARE ASEKED IN SMALL CAPS TO MAKE THE PROGRAM RUN EFFECIENTLY
");
            Console.WriteLine(@"
=========================================================
");
            Console.ResetColor();
        }

        public static void QandA()
        {//objects for these classes
            Question question = new Question();
            Responses respond = new Responses();
            ErrorHandling errors = new ErrorHandling();

            Console.ForegroundColor = ConsoleColor.DarkBlue;
            //these statements promt the user for questions
            Console.WriteLine("Please ask any cyber awareness related question");
            Console.ResetColor();

            question.Text = errors.errorHandling();
            string answer = respond.GetResponse(question.Text);
            Console.WriteLine(answer);



            Console.ForegroundColor=ConsoleColor.DarkBlue;
            //this statement will check if the user wants to enter another question 
            Console.WriteLine("Do you want to ask another question (yes/no)");
            Console.ResetColor();
            string input = Console.ReadLine();
           //this loop will loop as long as the user states they want to enter another question

            while (input.StartsWith("y"))
            {
                Console.ForegroundColor=ConsoleColor.DarkBlue;
                Console.WriteLine("Please ask any cyber awareness related question");
                Console.ResetColor();
                
                question.Text = errors.errorHandling();


                string query = respond.GetResponse(question.Text);
                

                Console.WriteLine(query);
                

                Console.ForegroundColor=ConsoleColor.DarkBlue;
                Console.WriteLine("Do you want to ask another question (yes/no)");
                Console.ResetColor();
                input = Console.ReadLine().ToLower();
                if (string.IsNullOrEmpty(input))//this if statement will check if the user left an empty space if yes the error sound will play
                {
                    
                }
            }
        }
    }
  
   
}
        
    


        
    

