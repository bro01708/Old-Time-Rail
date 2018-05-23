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
    class TimeSlot
    {
        private string customerName = "AVAILABLE"; // customer name is default available to enable checking availability on each slot
        private string id; // refers to the information on what times this is between and which stations those are
        private string telNum = "AVAILABLE"; // customers telephone number for security purposes and data integrity/reliability

        public TimeSlot(string t_id) { id = t_id; }

        public string getID() { return id; } // getters to allow public read access
        public string getcustomerName() { return customerName; }

        public string getTelNum() { return telNum; }

        public void bookWhole(string m_name , string m_num) // changes customer name from default tot he one supplied with the actual booking.
        {
            customerName = m_name;
            telNum = m_num;
        }

        public void print() //writes the bookigns information to the console.
        {
            Console.WriteLine(id + " " + customerName + " " + telNum);
        }

    }
    }
