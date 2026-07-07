using System;

namespace GymManagement.Classes
{
    public class Member
    {
        public int MemberID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string TrainingProgram { get; set; }
        public DateTime MembershipStartDate { get; set; }
        public DateTime MembershipEndDate { get; set; }

        public Member()
        {
            // default constructor
        }

        public Member(string firstName, string lastName, DateTime dob, string gender,
            string phone, string address, string trainingProgram, DateTime startDate, DateTime endDate)
        {
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dob;
            Gender = gender;
            PhoneNumber = phone;
            Address = address;
            TrainingProgram = trainingProgram;
            MembershipStartDate = startDate;
            MembershipEndDate = endDate;
        }

        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }

        // validates required fields
        public bool IsValid()
        {
            if (string.IsNullOrEmpty(FirstName) || string.IsNullOrEmpty(LastName))
                return false;

            if (string.IsNullOrEmpty(PhoneNumber))
                return false;

            return true;
        }
    }
}
