using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Old_Time_Rail
{
    class TrainMgr
    {
        List<Carriage> carriageList = new List<Carriage>(); //list of carrages objects within the train


        public void Menu() // menu interface text
        {
            Console.WriteLine("OLD TIME RAIL BOOKING SYSTEM");
            Console.WriteLine();
            Console.WriteLine("1 - List Seats");
            Console.WriteLine("2 - Create Booking");
            Console.WriteLine("3 - View Timetable");
            Console.WriteLine("4 - Save");
            Console.WriteLine("5 - Load Train (program does this on startup anyway)");
            Console.WriteLine("6 - Return train to default");
            Console.WriteLine("0 - Exit");
        }

        public void MenuSelector() //switchcase that validates user input and responds accordingly.
        {
            int caseSwitch; // Variable to hold number

            ConsoleKeyInfo UserInput = Console.ReadKey(); // Get user input
            Console.WriteLine();

            // We check input for a Digit
            if (char.IsDigit(UserInput.KeyChar))
            {
                caseSwitch = int.Parse(UserInput.KeyChar.ToString()); // use Parse if it's a Digit
            }
            else
            {
                caseSwitch = 9999;  // Else we assign a default value
            }
            switch (caseSwitch)
            {
                case 1:
                    ListSeats(); //Jumps to the menu for different things to list
                    break;

                case 2:
                    BookMenu(); // Jumps to the menu for beginning a booking
                    break;
                case 3:
                    viewTimeTable(); // Displays the timetable explaining when and where the train is running
                    break;
                case 4:
                    saveMenu(); // jumps to the menu for saving options
                    break;
                case 5:
                    serializeLoad(); // jumps to the method that loads the train from hard disk
                    break;
                case 6:
                    restore(); //restores the train to empty
                    break;
                case 0:
                    Environment.Exit(1); //quits the program
                    break;
            }

        }


        public void ListSeats() // switchcase menu for selecting what the user wants to list.
        {
            Console.WriteLine("Select an option");
            Console.WriteLine("1) - All seats");
            Console.WriteLine("2) - Booked Seats");
            Console.WriteLine("3) - Available Seats");
            Console.WriteLine("0) - Return");

            int caseSwitch;

            ConsoleKeyInfo UserInput = Console.ReadKey(); // Get user input

            // We check input for a Digit
            if (char.IsDigit(UserInput.KeyChar))
            {
                caseSwitch = int.Parse(UserInput.KeyChar.ToString()); // use Parse if it's a Digit
            }
            else
            {
                caseSwitch = 9999;  // Else we assign a default value
            }
            switch (caseSwitch)
            {
                case 1:
                    foreach (Carriage C in carriageList)
                    {
                        C.listSeats(); //lists everything
                    }
                    break;

                case 2:
                    foreach (Carriage C in carriageList)
                    {
                        C.listBookedSeats(); //lists only booked seat timetables
                    }
                    break;
                case 3:
                    foreach (Carriage C in carriageList)
                    {
                        C.listAvailableSeats(); // lists avaialable seat timetables.
                    }
                    break;
                case 0:
                    break;
            }
            Console.WriteLine();
        }


        public void BookMenu()
        {
            int caseSwitch; // Variable to hold number
            Console.WriteLine();
            Console.WriteLine("1 - Whole Day");
            Console.WriteLine("2 - Partial Day (currently unavailable)");
            Console.WriteLine("0 - Exit");
            ConsoleKeyInfo UserInput = Console.ReadKey(); // Get user input
            Console.WriteLine();

            // We check input for a Digit
            if (char.IsDigit(UserInput.KeyChar))
            {
                caseSwitch = int.Parse(UserInput.KeyChar.ToString()); // use Parse if it's a Digit
            }
            else
            {
                caseSwitch = 9999;  // Else we assign a default value
            }
            Console.WriteLine();
            switch (caseSwitch)
            {
                case 1:
                    wholeBooking(); // books for the whole day
                    break;

                case 2:
                    partialBooking(); // would book from whichever stations the user would select, not implemented yet
                    break;

                case 0:
                    break;
            }
        }

        public void wholeBooking() // switchcase menu for selecting the desired carriage
        {
            Console.WriteLine("Select Which carriage you wish to be seated in");
            Console.WriteLine("1 - Carriage A - First Class - Compartment Carriage");
            Console.WriteLine("2 - Carriage B - First Class - Standard Carriage");
            Console.WriteLine("3 - Carriage C - Third Class - Standard Carriage");
            Console.WriteLine("4 - Carriage D - Third Class - Standard Carriage");
            int caseSwitch; // Variable to hold number

            ConsoleKeyInfo UserInput = Console.ReadKey(); // Get user input
            Console.WriteLine();

            // We check input for a Digit
            if (char.IsDigit(UserInput.KeyChar))
            {
                caseSwitch = int.Parse(UserInput.KeyChar.ToString()); // use Parse if it's a Digit
            }
            else
            {
                caseSwitch = 9999;  // Else we assign a default value
            }
            Console.WriteLine();
            switch (caseSwitch)
            {
                case 1:
                    carriageList[0].bookWhole(); // indexes the carriage list to choose appropriate carriage object.
                    break;

                case 2:
                    carriageList[1].bookWhole();
                    break;
                case 3:
                    carriageList[2].bookWhole();
                    break;
                case 4:
                    carriageList[3].bookWhole();
                    break;
                case 0:
                    break;
            }
        }


        public void partialBooking()
        {
            Console.WriteLine("Not implemented yet."); // would request the letters corresponding to the timeslots (see viewTimetable())
        }


        public void viewTimeTable() // displays the times and stations the train does on its journey
        {
            System.IO.StreamReader stationReader = new System.IO.StreamReader("TrainStopList.txt"); // needed two streamreaders to read through the txt file, returning to the beginning of the file caused the streamreader to output a questionmark seeminly randomly
            System.IO.StreamReader stationReader2 = new System.IO.StreamReader("TrainStopList.txt");
            string possibleLetters = "ABCDEFGHIJKLMNOP"; //letters to be indexed
            int index = 0; //index
            Console.WriteLine("Saturday \r\n");
            while (stationReader.EndOfStream == false) //if it hasnt reached the end of the file ... DO whats in brakets
            {
                string a = stationReader.ReadLine();                   // read whichever line the reader is up to
                Console.WriteLine(possibleLetters[index] + " " + a);   // write it to console along with its allocated letter from index
                index++; //increment index
            }
            Console.WriteLine();
            Console.WriteLine("Sunday \r\n");                        // do the same thing for the next day
            while (stationReader2.EndOfStream == false)
            {
                string a = stationReader2.ReadLine();
                Console.WriteLine(possibleLetters[index] + " " + a);
                index++;
            }
           // stationReader.BaseStream.Position = 0; attempt to only use on streamreader resulted in a questionmark appearing on console.
        }

        public void generateTrain() //creates the train from scratch using the classes public constructors, no longer in use due to templates in storage
        {
            Carriage A = new Carriage("First", "Compartment", 'A'); // instantiates each carriage object providing it with details so it knows what to fill its attributes and seatlists with
            Carriage B = new Carriage("Third", "Compartment", 'B');
            Carriage C = new Carriage("Third", "Standard", 'C');
            Carriage D = new Carriage("Third", "Standard", 'D');

            carriageList.Add(A); // adds them to the carriage list
            carriageList.Add(B);
            carriageList.Add(C);
            carriageList.Add(D);

        }

        public void saveMenu() // allows user to choose wether to save to a text file or serialize as binary to disk.
        {
            Console.WriteLine();
            Console.WriteLine("1 - Save Booked Seats to .txt file");
            Console.WriteLine("2 - Save Train (Use this to ensure any bookings for the weekend ahead are saved)");
            Console.WriteLine("0 - Exit");

            int caseSwitch; // Variable to hold number

            ConsoleKeyInfo UserInput = Console.ReadKey(); // Get user input
            Console.WriteLine();

            // We check input for a Digit
            if (char.IsDigit(UserInput.KeyChar))
            {
                caseSwitch = int.Parse(UserInput.KeyChar.ToString()); // use Parse if it's a Digit
            }
            else
            {
                caseSwitch = 9999;  // Else we assign a default value
            }
            switch (caseSwitch)
            {
                case 1:
                    using (new OutputUtility(DateTime.Now.ToString("dd_MM_yyyy"))) // redirect the output to a text file using custom output re-router
                    {
                        foreach (Carriage C in carriageList)
                        {
                            C.listBookedSeats(); // search method can be called as normal , the result just gets redirected to specified file
                        }
                    } //output rerouting stops here
                    break;
                case 2:
                    serializeSave(); // jumps to serializer method
                    break;
                case 0:
                    break;

            }
        }

        public void serializeSave() // saves the train to storage
        {
            try
            {
                using (Stream stream = File.Open("carriageList.bin", FileMode.Create)) // will create file if doesnt exist
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    bin.Serialize(stream, carriageList);
                }
            }
            catch (IOException)
            {
                Console.WriteLine("Failed");
            }
            }
        

        public void serializeLoad() //deserializes the file containing all the carriages and populates the carriagelist with these objects.
        {
            try
            {
                using (Stream stream = File.Open("carriageList.bin", FileMode.Open))
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    carriageList = (List<Carriage>)bin.Deserialize(stream);
                }
            }
            catch (IOException)
            {
                Console.WriteLine("Failed");
            }
        }
        public void restore() //security checks then makes the train empty ready to take bookings for next weekend.
        {
            string passcode = "RESTORE";
            Console.Clear();
            Console.WriteLine("WARNING - THIS SHOULD ONLY BE DONE AFTER THE TRAIN HAS DEPARTED ON THE SUNDAY OF EACH WEEK, THIS RETURNS THE SYSTEM TO DEFAULT READY TO TAKE BOOKINGS FOR THE NEXT WEEKEND. IF YOU WISH TO CONTINUE, PLEASE TYPE THE FOLLOWING :");
            Console.WriteLine("'" + passcode + "'");
            if (Console.ReadLine() == passcode)
            {
                foreach (Carriage C in carriageList) { C.restore(); }
                serializeSave(); // saves and loads it again just to double check.
                serializeLoad();
            }
            else { Console.WriteLine("INCORRECT"); }
        }

    }
}