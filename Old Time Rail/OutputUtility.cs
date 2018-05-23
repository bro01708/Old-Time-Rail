using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Old_Time_Rail
{
    class OutputUtility : IDisposable // able to be used to redirect anything written to the console to a new text file, allowing you to perform search methods and have the result saved automatically, idisposable needs to be there to manage memory.
    {
        private StreamWriter fileOutput;
        private TextWriter oldOutput;
        public OutputUtility(string outFileName)
        {
            oldOutput = Console.Out; 
            fileOutput = new StreamWriter( new FileStream(outFileName, FileMode.Create));                    // changes default output type to a streamwriter that is pointed to whatever file it is told to create
            fileOutput.AutoFlush = true;
            Console.SetOut(fileOutput);
        }

        // Dispose() is called automatically when the object
        // goes out of scope
        public void Dispose()
        {
            Console.SetOut(oldOutput);  // Restore the console output so it doesnt keep outputting everything to text file.
            fileOutput.Close();        // Done with the file
        }

    }
}
