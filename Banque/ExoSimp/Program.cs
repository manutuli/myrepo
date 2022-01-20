using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ExoSimp
{
    internal class Program
    {
        public enum AppState
        {
            Connexion = 101,
            MainMenu = 102,
            // Main menu options :
            CheckPosNegZer = 1,
            CheckCase = 2,
            CheckOddEven = 3,
            LargestNum = 4,
            LargestNumCond = 5,
            LargestNumThree = 6,
            CheckLeapYear = 7,
            CheckVowels = 8,
        }
        /*
       C# Program to Add Two Integers.
       C# Program to Swap Values of Two Variables.
       C# Program to Multiply two Floating Point Numbers
       C# Program to perform all arithmetic operations
       C# Program to convert feet to meter
       C# Program to convert celcius to farenheit
       C# Program to convert farenheit to celcius
       C# Program to find the Size of data types
       C# Program to Print ASCII Value
       C# Program to Calculate Area of Circle
       C# Program to Calculate Area of Square
       C# Program to Calculate Area of Rectangle
       C# Program to convert days to years, weeks and days
       */
        //C# program to check number is positive, negative or zero.
        public static string NumCheck(int arg)
        {
            if(arg == 0)
            {
                return $"The number \"{arg}\" is equal to zero !";
            }
            else if (arg > 1)
            {
                return $"The number \"{arg}\" is positive !";
            }
            else
            {
                return $"The number \"{arg}\" is negative !";
            }

        }
        //C# program to check uppercase or lowercase alphabets.
        public static string CaseCheck(string arg)
        {
            Regex _rgxUpper = new Regex(@"([A-Z])");
            Regex _rgxLower= new Regex(@"([a-z])");
            if (_rgxUpper.IsMatch(arg))
            {
                return "Cet alphabet contient des lettres majuscules.";
            }
            else if (_rgxLower.IsMatch(arg))
            {
                return "Cet alphabet contient des lettres minuscules.";
            }
            else { return "Cet alphabet ne contient ni majuscules, ni minuscules."; }
        }
        /*
        C# program to check entered character vowel or consonant.
        C# program to check whether a character is alphabet, digit or special character.
        C# program to print day name of week.
        C# program to accept two integers and check whether they are equal or not.
        C# program to detrermine a candidate\’s age is eligible for casting the vote or not.
        C# program to find the eligibility of admission for an engineering course based on the criteria.
        C# program to calculate the total marks, percentage and division of student.
        C# program to enter month number and print number of days in month.
        C# program to count total number of notes in entered amount.
        C# program to check whether a triangle can be formed by the given value for the angles.
        */
        public static void Main(string[] args)
        {
            while (_isRunning)
            {
                switch (_appstate)
                {

                }
            }
        }
        //C# Program to check whether an integer entered by the user is odd or even
        public static string OddNum()
        {
            int i = int.Parse(Console.ReadLine());
            if ((i % 2) == 0)
            {
                return $"Yes, {i} is even !";
            }
            else
            {
                return $"No, {i} is not even !";
            }
        }

        //C# Program to find the largest number among three number.
        public static int LargestNum(int num1, int num2, int num3)
        {

            int[] ar = new int[3] {num1, num2, num3 };
            Array.Sort(ar);
            return ar[3];
            /*
            int[] ar = new int[3] {num1, num2, num3 };
            int[] tb = new int[3] ;
            foreach(int item in ar)
            {
                for (int i = 0; i < ar.Length; i++)
                {
                    int x = ar[i] <= ar[i+1] ? new int { ar[i+1] } : new int { ar[i] };
                    tb = new int { x };
                }
            }
             */
        }

        //C# Program to Find the Largest Number using Conditional Operator.
        public static int LargestCond(int num1, int num2, int num3)
        {
            int temp;
            int x ;
            int y ;
            temp = num1 > num2 ? num1 : num2;
            x = temp > num3 ? temp : num3;
            return y = x > num1 ? x : num1;        
        }

        //C# Program to find the Largest among Three Variables using Nested if.
        public static int LargestThree(int num1, int num2, int num3)
        {
            if (num1 != num2 && num1 != num3 && num2 != num3)
            {
                if (num1 < num3 && num2 < num3)
                {
                    return num3;
                }
                else if (num2 > num1 && num2 > num3){
                    return num2;
                }else{
                    return num1;
                }
            }
            else
            {
                return 0;
            }
        }

        //C# program to check leap year using conditional Operator.
        public static string LeapYear(int _year)
        {

            if ((_year % 4 == 0) && (_year % 100 != 0) || (_year % 400 == 0))
            {
                return $"The year {_year} is Leap year.";
            }
            else
            {
                return $"{_year} is Not Leap year.";
            }
        }

        //C# Program to Check whether an alphabet is a vowel or not.
        public static string CheckVowels()
        {
            Regex _rgxVowel = new Regex(@"([aoiueyAOIUEY])");
            if (_rgxVowel.IsMatch(Console.ReadLine()))
            {
                return "Ce texte contient des voyelles !";
            }
            else
            {
                return "Ce texte ne contient aucune voyelle, désolé !";
            }
        }

    }
}
