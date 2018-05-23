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
    class Carriage
    {
        private char carriageLetter; //Letter identifying which carriage it is, is the first character in a seat ID
        private string carriageClass; //first or third
        private string carriageType; // Compartment or Standard
        List<Seat> carriageContents = new List<Seat>(); // list of seats that is in each carriage

        public Carriage(string m_cClass, string m_cType, char m_carriageLetter)
        {
            carriageClass = m_cClass;
            carriageType = m_cType;
            carriageLetter = m_carriageLetter;
            if (m_cType == "Compartment") { populateCompartment(); } //ensures theyre populated with the right about of seats with the right proportions
            if (m_cType == "Standard") { populateStd(); }

        }

        public void listSeats() //runs through each seat of the carriage and prints its information and its status for each timeslot
        {
            Console.WriteLine("Carriage " + carriageLetter + carriageType + carriageClass);
            foreach (Seat S in carriageContents) { S.printSaturday(); S.printSunday(); }
        }

        public void listBookedSeats() //same as above but will only print if the seat has bookings
        {
            Console.WriteLine();
            foreach (Seat S in carriageContents)
            {
                if (S.IsBookedSaturday() == true) { S.printSaturday(); }
                if (S.IsBookedSunday() == true) { S.printSunday(); }
            }
        }

        public void listAvailableSeats() //same as above but will only print if the seat has NO bookings
        {
            foreach (Seat S in carriageContents)
            {
                if (S.IsBookedSaturday() == false) { S.printSaturday(); }
                if (S.IsBookedSunday() == false) { S.printSunday(); }
            }
        }

        public void populateCompartment() // nested for loops to populated a COMPARTMENT CARRIAGE
        {
            for (int x = 0; x < 4; x++) //counts to four (the amount of comparments)
            {
                for (int y = 0; y < 8; y++) //counts to 8 (the amount of seats in a compartment)
                {
                    Seat S = new Seat(carriageLetter, x, y); //creates the seat wtih the letter of the carriage, letter to indicate its compartment and individual seat
                    carriageContents.Add(S); // adds it to the list of seats 
                }
            }
        }
        public void populateStd() //nested for loops to populate a STANDARD CARRIAGE
        {
            for (int x = 0; x < 5; x++) //counts to five (number of bays)
            {
                for (int y = 0; y < 10; y++) //counts to ten (number of seats in a bay)
                {
                    Seat S = new Seat(carriageLetter, x, y); // creates a new seats providing the carriage letter and seat position to create its ID
                    carriageContents.Add(S); // adds it to the list of seats.
                }
            }
        }

        public void bookWhole() //books the whole day, allows user to select which day and automatically assigns them to the next available seat if possible, can be repeated for larger parties
        {
            Console.WriteLine("Select Day");
            Console.WriteLine("1) - Saturday");
            Console.WriteLine("2) - Sunday");
            Console.WriteLine("0) - Return");

            int caseSwitch; // Variable to hold number

            ConsoleKeyInfo UserInput = Console.ReadKey(); // Get user input
            Console.WriteLine();
            bool success = false;
            // We check input for a Digit
            if (char.IsDigit(UserInput.KeyChar))
            {
                caseSwitch = int.Parse(UserInput.KeyChar.ToString()); // use Parse if it's a Digit
            }
            else
            {
                caseSwitch = 9999;  // Else we assign a default value
            }

            string bookedSeat = "";
            string daySelected = "";
            switch (caseSwitch)
            {
                case 1:
                    foreach (Seat S in carriageContents)
                    {
                        if (S.IsBookedSaturday() == false) { S.bookSat(); success = true; bookedSeat = S.getId(); daySelected = "Saturday"; break; } // checks if its available , if so , books it and notifies the local variable of its succes and returns what was created, breaks out .
                    }
                    break;
                case 2:
                    foreach (Seat S in carriageContents)
                    {
                        if (S.IsBookedSunday() == false) { S.bookSun(); success = true; bookedSeat = S.getId(); daySelected = "Saturday"; break; } // same as above but for sunday if selected
                    }
                    break;

                case 0:
                    break;
            }
            Console.WriteLine();
            if (success == false) { Console.WriteLine("Unable to book, try another carriage or day"); }
            else // shows the bookings you have just created
            {
                Console.WriteLine("Booking Successful");
                foreach (Seat S in carriageContents)
                {
                    if (S.getId() == bookedSeat)
                    {
                        if (daySelected == "Saturday") { S.printSaturday(); }
                        if (daySelected == "Sunday") { S.printSunday(); }
                    }
                }

            }
        }

        public void restore() { foreach (Seat S in carriageContents) { S.restoreToDefault(); } } //tells each seat to reset itself.
    }
}
