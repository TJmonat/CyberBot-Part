using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;

namespace Test2
{
    
    internal class ErrorHandling
    {
       //This class handles any errors the user may input
       //The methods check the users name,surname,age,date of birth username and their questions
       //if the user leaves the space open the program will display a message and error sound re promting the user for entry
       //the while loops will loop as long as the user inputs wrong data or leave the space empty 
       //the if statements check for empty spaces or invalid data
       //THE ERROR SOUND HAVE BEEN REMOVED DUE TO THEIR LOCTION BUT WILL BE ADD IN PART 2
        public string nameErrorHandling()
        {
            string name;
            while (true)
            {
                name = Console.ReadLine();
                if (string.IsNullOrEmpty(name))
                {
                    
                    Console.ForegroundColor = ConsoleColor.DarkRed;

                    Console.WriteLine("Please Do Not Leave This Space Empty");
                    Console.ResetColor();

                    continue;
                }
                if (name.Any(Char.IsDigit))
                {

                   
                    Console.ForegroundColor = ConsoleColor.DarkRed;

                    Console.WriteLine("Name Should Not Contain Any Digits");
                    Console.ResetColor();

                    continue;
                }
                return name;

            }
        }
        public string surnameErrorHandling()
        {
            string surname;
            while (true)
            {
                surname = Console.ReadLine();
                if (string.IsNullOrEmpty(surname))
                {

                    Console.ForegroundColor = ConsoleColor.DarkRed;

                    Console.WriteLine("Please Do Not Leave This Space Empty");
                    Console.ResetColor();

                    continue;
                }
                if (surname.Any(Char.IsDigit))
                {

                    
                    Console.ForegroundColor = ConsoleColor.DarkRed;

                    Console.WriteLine("Name Should Not Contain Any Digits");
                    Console.ResetColor();

                    continue;
                }
                return surname;
            }

        }
        public int ageErrorHandling()
        {
            string input;
            int age;
            while (true)
            {
            input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {

                    
                    Console.ForegroundColor = ConsoleColor.DarkRed;

                    Console.WriteLine("Do not leave the space empty");
                    Console.ResetColor();

                    continue;
                }
                if (!int.TryParse(input, out age ))
                {

                    Console.ForegroundColor = ConsoleColor.DarkRed;

                    Console.WriteLine("Only numbers are required");
                    Console.ResetColor();

                    continue;
                }


                return age;

            }

        }
        public DateTime dateErrorHandling()
        {
            string input;
            DateTime dob;
            while (true)
            {
                input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))

                {
;
                    Console.ForegroundColor = ConsoleColor.DarkRed;

                    Console.WriteLine("Do not leave the space empty");
                    Console.ResetColor();

                    continue;
                }
                if (!DateTime.TryParseExact(input, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None,out dob))
                {

                    Console.ForegroundColor = ConsoleColor.DarkRed;

                    Console.WriteLine("Please follow the formatt dd/MM/yy");
                    Console.ResetColor();

                    continue;
                }


                return dob;

            }

        }
        public string userNameErrorHandling()
        {
            string userName;
            while (true)
            {
                userName = Console.ReadLine();
                if (string.IsNullOrEmpty(userName))
                {

                    Console.ForegroundColor = ConsoleColor.DarkRed;

                    Console.WriteLine("Please Do Not Leave This Space Empty");
                    Console.ResetColor();

                    continue;
                }
                
                return userName;
            }

        }
        
        public string errorHandling()
        {
            string question;

            while (true)
            {
                question = Console.ReadLine();

                if (string.IsNullOrEmpty(question))
                {

                    Console.ForegroundColor = ConsoleColor.DarkRed;

                    Console.WriteLine("Please Do not Leave any empty spaces");
                    Console.ResetColor();
                    continue;
                    
                }
                if (question.Any(char.IsDigit))
                {

                    
                    Console.ForegroundColor = ConsoleColor.DarkRed;

                    Console.WriteLine("Question Should Not contain any digits");
                    Console.ResetColor();

                    continue;
                }
                return question;

            }
        }

    }
}

