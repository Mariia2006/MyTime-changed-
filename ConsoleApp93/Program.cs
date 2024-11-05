using System;
using Structura;

namespace Structura
{
    internal class Menu
    {
        static MyTime EnterTime()
        {
            Console.Write("Enter the time in the format HH:MM:SS: ");
            return new MyTime(Console.ReadLine());
        }

        static int EnterSeconds()
        {
            Console.Write("Enter the number of seconds: ");
            return int.Parse(Console.ReadLine());
        }

        static void Main()
        {
            MyTime mt;
            int seconds;
            try
            {
                int choice;
                do
                {
                    Console.WriteLine("Select an option:");
                    Console.WriteLine("1 - Convert the time to the number of seconds from the beginning of the day");
                    Console.WriteLine("2 - Convert seconds from the beginning of the day to time format");
                    Console.WriteLine("3 - Add one second to the time");
                    Console.WriteLine("4 - Add one minute to the time");
                    Console.WriteLine("5 - Add one hour to the time");
                    Console.WriteLine("6 - Add a certain number of seconds to the time");
                    Console.WriteLine("7 - The difference between two time segments in seconds");
                    Console.WriteLine("8 - Check if the time is within a certain range");
                    Console.WriteLine("9 - Determine which pair is now");
                    Console.WriteLine("0 - Exit");

                    choice = int.Parse(Console.ReadLine());
                    switch (choice)
                    {
                        case 0:
                            Console.WriteLine("Completing the program...");
                            break;
                        case 1:
                            mt = EnterTime();
                            Console.WriteLine($"From midnight {mt} passed {MyTime.ToSecSinceMidnight(mt)} seconds.");
                            break;
                        case 2:
                            seconds = EnterSeconds();
                            Console.WriteLine($"Time for {seconds} seconds since the start of the day: {MyTime.FromSecSinceMidnight(seconds)}");
                            break;
                        case 3:
                            mt = EnterTime();
                            Console.WriteLine($"Time after adding one second: {MyTime.AddOneSecond(mt)}");
                            break;
                        case 4:
                            mt = EnterTime();
                            Console.WriteLine($"Time after adding one minute: {MyTime.AddOneMinute(mt)}");
                            break;
                        case 5:
                            mt = EnterTime();
                            Console.WriteLine($"Time after adding one hour: {MyTime.AddOneHour(mt)}");
                            break;
                        case 6:
                            mt = EnterTime();
                            seconds = EnterSeconds();
                            Console.WriteLine($"Time after adding {seconds} seconds: {MyTime.AddSeconds(mt, seconds)}");
                            break;
                        case 7:
                            Console.WriteLine("Enter the first time:");
                            MyTime mt1 = EnterTime();
                            Console.WriteLine("Enter the second time:");
                            MyTime mt2 = EnterTime();
                            int difference = MyTime.Difference(mt1, mt2);
                            Console.WriteLine($"The difference between {mt1} and {mt2} is {Math.Abs(difference)} seconds.");
                            break;
                        case 8:
                            Console.WriteLine("Enter the start of the range:");
                            MyTime start = EnterTime();
                            Console.WriteLine("Enter the end of the range:");
                            MyTime end = EnterTime();
                            Console.WriteLine("Enter the time you want to check:");
                            MyTime timeToCheck = EnterTime();
                            bool inRange = MyTime.IsInRange(start, end, timeToCheck);
                            Console.WriteLine(inRange
                                ? $"{timeToCheck} is in the range between {start} and {end}."
                                : $"{timeToCheck} is not in the range between {start} and {end}.");
                            break;
                        case 9:
                            mt = EnterTime();
                            Console.WriteLine($"For time {mt} - {MyTime.WhatLesson(mt)}");
                            break;
                        default:
                            Console.WriteLine("Incorrect option! Try again.");
                            break;
                    }
                } while (choice != 0);
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: " + e.Message);
            }
        }
    }
}