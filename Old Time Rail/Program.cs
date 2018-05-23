using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Old_Time_Rail
{
    class Program
    {
        static void Main(string[] args)
        {
            TrainMgr TM = new TrainMgr(); //instantiates the Trainmanager Class, the central 'hub' that controls most of the main functions
            try
            {
                TM.serializeLoad(); //Load automatically if possible else generate train
            }
            catch { TM.generateTrain(); }
            bool active = true;

            while (active == true) //loops the menu while the progam is on so it only closes when desired.
            {
                Console.Clear();
                TM.Menu();
                TM.MenuSelector();
                Console.WriteLine();
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
            }
        }
    }
}
