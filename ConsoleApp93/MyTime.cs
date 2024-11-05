namespace Structura
{
    internal class MyTime
    {
        private int hour;
        private int minute;
        private int second;

        // Конструктор, який буде використовуватися для створення екземплярів
        public MyTime(int hour, int minute, int second)
        {
            SetTime(hour, minute, second);
        }

        public MyTime(string time)
        {
            var parts = time.Split(':');
            if (parts.Length != 3)
                throw new ArgumentException("The time must be in the format HH:MM:SS.");
            SetTime(int.Parse(parts[0]), int.Parse(parts[1]), int.Parse(parts[2]));
        }

        // Властивості
        public int Hour => hour;
        public int Minute => minute;
        public int Second => second;

        private void SetTime(int hour, int minute, int second)
        {
            if (hour < 0 || hour > 23)
                throw new ArgumentOutOfRangeException(nameof(hour), "The hour must be between 0 and 23.");
            if (minute < 0 || minute > 59)
                throw new ArgumentOutOfRangeException(nameof(minute), "The minute must be between 0 and 59.");
            if (second < 0 || second > 59)
                throw new ArgumentOutOfRangeException(nameof(second), "The second must be between 0 and 59.");

            this.hour = hour;
            this.minute = minute;
            this.second = second;
        }

        public static int ToSecSinceMidnight(MyTime time) => time.hour * 3600 + time.minute * 60 + time.second;

        public static MyTime AddOneSecond(MyTime time)
        {
            return AddSeconds(time, 1);
        }

        public static MyTime AddOneMinute(MyTime time)
        {
            return AddMinutes(time, 1);
        }

        public static MyTime AddOneHour(MyTime time)
        {
            int newHour = (time.Hour + 1) % 24;
            return new MyTime(newHour, time.Minute, time.Second);
        }

        public static MyTime FromSecSinceMidnight(int totalSeconds)
        {
            const int secPerDay = 86400;
            totalSeconds %= secPerDay;
            if (totalSeconds < 0)
                totalSeconds += secPerDay;
            int h = totalSeconds / 3600;
            int m = (totalSeconds / 60) % 60;
            int s = totalSeconds % 60;
            return new MyTime(h, m, s);
        }

        public static MyTime AddSeconds(MyTime time, int seconds)
        {
            const int secondsInDay = 24 * 3600;
            int totalSeconds = ToSecSinceMidnight(time) + seconds;

            totalSeconds = totalSeconds % secondsInDay;
            if (totalSeconds < 0)
            {
                totalSeconds += secondsInDay;
            }

            return FromSecSinceMidnight(totalSeconds);
        }

        public static MyTime AddMinutes(MyTime time, int minutes)
        {
            return AddSeconds(time, minutes * 60);
        }

        public static int Difference(MyTime t1, MyTime t2)
        {
            int totalSeconds1 = t1.hour * 3600 + t1.minute * 60 + t1.second;
            int totalSeconds2 = t2.hour * 3600 + t2.minute * 60 + t2.second;

            int s = totalSeconds1 - totalSeconds2;

            return s;
        }

        public static bool IsInRange(MyTime start, MyTime finish, MyTime t)
        {
            int startSeconds = ToSecSinceMidnight(start);
            int finishSeconds = ToSecSinceMidnight(finish);
            int currentSeconds = ToSecSinceMidnight(t);

            if (finishSeconds < startSeconds)
            {
                startSeconds = startSeconds - 24 * 60 * 60; // Корекція для випадку, коли кінець менший за початок
            }

            return currentSeconds >= startSeconds && currentSeconds < finishSeconds;
        }

        public static string WhatLesson(MyTime mt)
        {
            MyTime[] lessons_start = {new MyTime(8, 0, 0), new MyTime(9, 40, 0), new MyTime(11, 20, 0),
                new MyTime(13, 0, 0), new MyTime(14, 40, 0), new MyTime(16, 10, 0)};

            MyTime[] lessons_end = {new MyTime(9, 20, 0), new MyTime(11, 0, 0),
                new MyTime(12, 40, 0),new MyTime(14, 20, 0),new MyTime(16, 0, 0),new MyTime(17, 30, 0)};

            if (Difference(mt, lessons_start[0]) < 0)
            {
                return "pairs have not started yet";
            }

            if (IsInRange(lessons_start[0], lessons_end[0], mt))
            {
                return "1st pair";
            }

            for (int i = 0; i < lessons_start.Length; i++)
            {
                if (IsInRange(lessons_start[i], lessons_end[i], mt))
                {
                    return $"{i + 1}st pair";
                }
                if (IsInRange(lessons_end[i - 1], lessons_start[i], mt))
                {
                    return $"break between {i} and {i + 1} pairs";
                }
            }
            return "pairs have already run out";
        }

        public override string ToString()
        {
            return $"{Hour:D2}:{Minute:D2}:{Second:D2}";
        }
    }

}