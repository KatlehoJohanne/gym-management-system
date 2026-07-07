using System;

namespace GymManagement.Classes
{
    public class GymClass
    {
        public int ClassID { get; set; }
        public string ClassName { get; set; }
        public string Description { get; set; }
        public string Instructor { get; set; }
        public string Schedule { get; set; }
        public int Capacity { get; set; }
        public string Duration { get; set; }

        public GymClass()
        {
        }

        public GymClass(string className, string description, string instructor,
            string schedule, int capacity, string duration)
        {
            ClassName = className;
            Description = description;
            Instructor = instructor;
            Schedule = schedule;
            Capacity = capacity;
            Duration = duration;
        }

        public bool IsValid()
        {
            if (string.IsNullOrEmpty(ClassName))
                return false;

            if (Capacity <= 0)
                return false;

            return true;
        }
    }
}
