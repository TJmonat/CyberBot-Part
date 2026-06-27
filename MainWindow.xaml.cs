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

        bool waitinfForTaskTitle = false;
        bool waitingForTaskDetails=false;
        bool waitingForRemindDate = false;
        bool waitingForReminderChoice= false;

        string taskTitle = " ";
        string taskDetails = " ";
        DatabaseConnection db = new DatabaseConnection();

        private int currentQuestion = 0;
        private int score = 0;
        private bool quizMode=false;
        //Array lists to store the quiz questions, answers and explanations
        private List<string> questions= new List<string>();
        private List<string> answers = new List<string>();
        private List<string> explanation = new List<string>();
        //array list to store the activity log 
        private List<string> activityLog = new List<string>();
        private const int MAX_LOGS = 10;

        public MainWindow()
        {
            InitializeComponent();
            TestConnection.Test();

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
            //if the user types quiz this if statement will trigger the start quiz method
            if (userMessage.ToLower() == "quiz")
            {
                StartQuiz();
                MessageTextBox.Clear();
                return;
            }//this if statement will the answer to the quiz question
            if (quizMode)
            {
                CheckQuizeAnswer(userMessage);
                MessageTextBox.Clear();
                return;
            }//if statement will show the activity log if the user types in any of these key words
            if (userMessage.ToLower().Contains("show activity log") ||
               userMessage.ToLower().Contains("activity log") ||
                userMessage.ToLower().Contains("what have you done for me")
 
                )

            {
                if (activityLog.Count == 0)
                {
                    AddBotMessage("No activity recorded.");
                }
                else
                {
                    for (int i = 0; i < activityLog.Count; i++)
                    {
                        AddBotMessage((i + 1) + ". " + activityLog[i]);
                    }
                }

                MessageTextBox.Clear();
                return;
            }
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
                            AddActivities("User Logged in:"+ usersName);
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
                    {//this if statement will check if the users input contains any of these keywords and finds a suitable response
                        if (userMessage.ToLower().StartsWith("add task")||
                            userMessage.ToLower().Contains("create new task")||
                            userMessage.ToLower().Contains("add task")||
                           userMessage.ToLower().Contains("make new task")||
                           userMessage.ToLower().Contains("establish task"))

                        {

                            taskTitle = userMessage.Replace("add task", "").Trim();

                            AddActivities("User created a task");

                            AddBotMessage("Please enter the task description.");

                            waitingForTaskDetails = true;

                            MessageTextBox.Clear();

                            return;

                        }
                        if (waitingForTaskDetails)

                        {

                            taskDetails = userMessage;

                            waitingForTaskDetails = false;

                            waitingForReminderChoice = true;

                            AddBotMessage("Would you like to set a reminder? (Yes/No)");
                            MessageTextBox.Clear();

                            return;

                        }
                        if (waitingForReminderChoice)

                        {

                            waitingForReminderChoice = false;

                            if (userMessage.ToLower() == "yes")

                            {
                                AddActivities("User Added reminder date");

                                waitingForRemindDate = true;

                                AddBotMessage("Please enter the reminder date or timeframe.");

                                MessageTextBox.Clear();

                                return;

                            }

                            if (userMessage.ToLower() == "no")

                            {

                                bool success = db.AddTask(

                                    taskTitle,

                                    taskDetails,

                                    "",

                                    "Pending");

                                if (success)

                                {

                                    AddBotMessage("Task saved successfully!");

                                    AddActivities("User added task");

                                }

                                else

                                {

                                    AddBotMessage("Task could not be saved.");

                                }

                                MessageTextBox.Clear();

                                return;

                            }

                            AddBotMessage("Please answer Yes or No.");

                            MessageTextBox.Clear();

                            return;

                        }
                        if (waitingForRemindDate)

                        {

                            waitingForRemindDate = false;

                            bool success = db.AddTask(

                                taskTitle,

                                taskDetails,

                                userMessage,

                                "Pending");

                            if (success)

                            {

                                AddBotMessage("Task saved successfully!");

                                AddActivities("User added task");

                            }

                            else

                            {

                                AddBotMessage("Task could not be saved.");

                            }

                            MessageTextBox.Clear();

                            return;

                        }
                        if (userMessage.ToLower().Contains("show tasks")

                            ||

                          userMessage.ToLower().Contains("view tasks")

                             ||

                              userMessage.ToLower().Contains("my tasks")||
                              userMessage.ToLower().Contains("display tasks"))

                        {
                           
                            List<Task> tasks = db.GetTasks();

                            if (tasks.Count == 0)

                            {

                                AddBotMessage("You have no saved tasks.");

                            }

                            foreach (Task task in tasks)

                            {

                                AddBotMessage(

                                "Title : " + task.Title +

                                "\nDescription : " +

                                task.Description +

                                "\nReminder : " +

                                task.Reminder +

                                "\nStatus : " +

                                task.Status

                                );

                            }

                            MessageTextBox.Clear();

                            return;

                        }



                        if (userMessage.ToLower().StartsWith("delete task") ||
                            userMessage.ToLower().Contains("erase task") ||
                            userMessage.ToLower().Contains("remove task")
                            )
                        {
                            AddActivities("User Deleted task");

                            string title =

                            userMessage.Replace("delete task", "").Trim();

                            if (db.DeleteTask(title))

                            {

                                AddBotMessage("Task deleted successfully.");

                            }

                            else

                            {

                                AddBotMessage("Task not found.");

                            }

                            MessageTextBox.Clear();

                            return;

                        }


                        if (userMessage.ToLower().StartsWith("complete task"))

                        {

                            string title =

                            userMessage.Replace("complete task", "").Trim();

                            if (db.CompleteTask(title))

                            {

                                AddBotMessage("Task marked as completed.");

                            }

                            else

                            {

                                AddBotMessage("Task not found.");

                            }

                            MessageTextBox.Clear();

                            return;

                        }
                        if (userMessage.ToLower().Contains("interested in"))
                        {
                            favTopic = userMessage.Substring(userMessage.ToLower().IndexOf("interested in") + 13).Trim();
                            AddBotMessage("I'll remember that you are interested in " + favTopic);
                            AddActivities("NLP used to review users favourite topic");
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
                                AddBotMessage("GoodBye " + usersName);
                            }
                        }
                    }

                }

                MessageTextBox.Clear();
            }
         
        }
        private void LoadQuizeQuestions()//this method is used to store the questions and their responses
        {
            questions.Add("1. What should you do if you receive an email asking for your password?\nA) Reply with your password\nB) Delete the email");

            answers.Add("B");

            explanation.Add("Legitimate companies never ask for passwords via email.");

            questions.Add("2. True or False: You should use the same password for all accounts.");

            answers.Add("FALSE");

            explanation.Add("Different passwords reduce security risks.");

            questions.Add("3. What is phishing?\nA) A cyberattack that tricks users into revealing information\nB) A type of antivirus");

            answers.Add("A");

            explanation.Add("Phishing tricks people into giving away sensitive information.");

            questions.Add("4. True or False: Two-factor authentication improves security.");

            answers.Add("TRUE");

            explanation.Add("2FA adds an extra layer of protection.");

            questions.Add("5. Which password is strongest?\nA) Password123\nB) T7#pL9@xQ2!");

            answers.Add("B");

            explanation.Add("Strong passwords use symbols, numbers and mixed characters.");

            questions.Add("6. True or False: Public Wi-Fi is always safe.");

            answers.Add("FALSE");

            explanation.Add("Public Wi-Fi can expose your data.");

            questions.Add("7. What should you do before clicking a link?\nA) Verify the sender\nB) Click immediately");

            answers.Add("A");

            explanation.Add("Always verify links before clicking.");

            questions.Add("8. True or False: Antivirus software helps protect your device.");

            answers.Add("TRUE");

            explanation.Add("Antivirus software detects and blocks threats.");

            questions.Add("9. What is social engineering?\nA) Manipulating people into revealing information\nB) Building websites");

            answers.Add("A");

            explanation.Add("Social engineering targets people instead of systems.");

            questions.Add("10. True or False: Software updates improve security.");

            answers.Add("TRUE");

            explanation.Add("Updates often patch security vulnerabilities.");

            questions.Add("11. What should you do if you suspect a scam?\nA) Report it\nB) Ignore it and continue");

            answers.Add("A");

            explanation.Add("Reporting scams helps protect others.");
        }
        public void StartQuiz()
        {
            questions.Clear();
            answers.Clear();
            explanation.Clear();

            LoadQuizeQuestions();

            currentQuestion = 0;
            score = 0;
            quizMode = true;

            AddBotMessage("CyberSecurity Quize Started!");
            AddBotMessage(questions[currentQuestion]);
            AddActivities("Quiz Started");
        }
        public void CheckQuizeAnswer(string userAnswer)
        {
            if (userAnswer.ToUpper() == answers[currentQuestion])

            {

                score++;

                AddBotMessage("Correct!");

            }

            else

            {

                AddBotMessage("Incorrect!");

            }

            AddBotMessage(explanation[currentQuestion]);

            currentQuestion++;

            if (currentQuestion < questions.Count)

            {

                AddBotMessage(questions[currentQuestion]);

            }

            else

            {

                quizMode = false;

                AddBotMessage("Quiz Complete!");

                AddBotMessage("Final Score: " + score + "/" + questions.Count);
                AddActivities("Quiz complete with the score of: " + score);
                if (score >= 8)

                {

                    AddBotMessage("Great job! You're a cybersecurity pro!");

                }

                else

                {

                    AddBotMessage("Keep learning to stay safe online!");

                }

            }

        }
        private void AddActivities(string action)//this is the activity log method 
        {

            activityLog.Add(action);

            if (activityLog.Count > MAX_LOGS)
            {
                activityLog.RemoveAt(0);
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