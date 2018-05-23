using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Old_Time_Rail
{
    [Serializable]
    class Seat
    {
        private int char2; // second character in 3 character id
        private int char3; // thrid char in 3 character id
        private char carriageLetter; // 1st char in 3 char id

        [NonSerializedAttribute] // streamreader cant be serialized so has to be skipped over when saving the trains state
        System.IO.StreamReader stationReader = new System.IO.StreamReader("TrainStopList.txt"); // able to read in the txt files of possible stations for creating timeslots
        List<string> possibleStations = new List<string>(); // LIST OF IDENTIFIERS FOR EACH STATION TO BE USED DURING CREATION OF THE TIMESLOT OBJECTS. IMPORTED FROM TXT FILE
        List<TimeSlot> SundayTimes = new List<TimeSlot>(); // lIST OF TIMESLOTS FOR SUNDAY
        List<TimeSlot> SaturdayTimes = new List<TimeSlot>(); // lIST OF TIMESLOTS FOR SATURDAY
        public Seat(char m_carriageLetter, int m_char2, int m_char3)
        {
            carriageLetter = m_carriageLetter;
            char2 = m_char2;
            char3 = m_char3;
            ReadFile();
            TimeSlotCreator();
        }

        public void printSaturday()
        {
            Console.WriteLine(carriageLetter.ToString() + char2.ToString() + char3.ToString());
            Console.WriteLine("Saturday");
            foreach (TimeSlot i in SaturdayTimes)
            {
                i.print();

            }
            Console.WriteLine();
        } //prints of a seats timeslots for that day
        public void printSunday()
        {
            Console.WriteLine(carriageLetter.ToString() + char2.ToString() + char3.ToString());
            Console.WriteLine("Sunday");
            foreach (TimeSlot i in SundayTimes)
            {
                i.print();
            }
            Console.WriteLine();
        }

        public bool IsBookedSaturday() //checks if the seat has a booking made on it for that day by checking the timeslots associated customername to see if it is the unchanged default value
        {
            int bookingCount = 0;
            foreach (TimeSlot T in SaturdayTimes)
            {
                if (T.getcustomerName() != "AVAILABLE") { bookingCount++; }
            }
            if (bookingCount >= 1) { return true; }
            else { return false; }
        }

        public bool IsBookedSunday()
        {
            int bookingCount = 0;
            foreach (TimeSlot T in SundayTimes)
            {
                if (T.getcustomerName() != "AVAILABLE") { bookingCount++; }
            }
            if (bookingCount >= 1) { return true; }
            else { return false; }

        }

        public void bookSun() // requests the customer info and assigns them to every timeslot on sunday
        {
            Console.WriteLine("Please Enter The Customers Full Name");   //NEEDS VALIDATING
            string customerName = Console.ReadLine();
            Console.WriteLine("Please Enter The Customers Phone Number");
            string telNum = Console.ReadLine();

            foreach (TimeSlot T in SundayTimes)
            {
                T.bookWhole(customerName, telNum);
            }
        }
        public void bookSat()
        {
            Console.WriteLine();
            Console.WriteLine("Please Enter The Customers Full Name");   //NEEDS VALIDATING
            string customerName = Console.ReadLine();
            Console.WriteLine("Please Enter The Customers Phone Number");
            string telNum = Console.ReadLine();

            foreach (TimeSlot T in SaturdayTimes)
            {
                T.bookWhole(customerName, telNum);
            }
        } // same as above but for saturday

        public void TimeSlotCreator() // Creates 8 timeslots for each day and adds them to the days list.
        {
            for (int i = 0; i < 8; i++)
            {
                TimeSlot A = new TimeSlot(possibleStations[i]); //uses index to select from the list containing the lines read in from txt file
                SaturdayTimes.Add(A);
            }

            for (int i = 0; i < 8; i++)
            {
                TimeSlot A = new TimeSlot(possibleStations[i]);
                SundayTimes.Add(A);
            }

        }
        public void ReadFile() // streamreader reads txt file and adds the txts lines to the list of possible stations
        {
            while (stationReader.EndOfStream == false)
            {
                string a = stationReader.ReadLine();
                possibleStations.Add(a);
            }
        }

        public string getId() { return carriageLetter + char2.ToString() + char3.ToString(); } // public accessor to get a seats full ID

        public void restoreToDefault() //clears the 2 lists and repopulates them vith default timeslots which are unbooked.
        {
            SundayTimes.Clear();
            SaturdayTimes.Clear();
            TimeSlotCreator();
        }
    }
}
