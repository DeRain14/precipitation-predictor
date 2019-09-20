using System;
using System.Collections.Generic;
using System.IO;

namespace Avalara {
    public class DataReader {
        //the name of the file containing historical data
        private string filename;
        //the list of data elements
        private List<PrecipDataElement> data;

        public DataReader(string file) {
            filename = file;
            data = new List<PrecipDataElement>();
            ReadFile(); //populates data list
        }

        //return the elements of data that match the month and day given
        public List<PrecipDataElement> GetPrecipData(DateTime date) {
            List<PrecipDataElement> precipData = data.FindAll(dataElement
                                    => (dataElement.date.Day == date.Day)
                                    && (dataElement.date.Month == date.Month));
            return precipData;
        }

        //read all of the data into memory
        private void ReadFile() {
            try {
                using (StreamReader reader = new StreamReader(filename))
                {
                    string line = "";
                    int lineCount = 0;

                    // read and store elements from the data until the end of
                    // the file is reached
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] element = line.Split('\t');
                        if (lineCount != 0)
                        { //skipping the title line
                            PrecipDataElement precipDataElement = new PrecipDataElement();
                            //parse every data element as an object and as a list
                            //do nothing if data is missing parts
                            try
                            {
                                precipDataElement.station = element[0];
                                precipDataElement.name = element[1];
                                precipDataElement.latitude = float.Parse(element[2]);
                                precipDataElement.longitude = float.Parse(element[3]);
                                precipDataElement.elevation = double.Parse(element[4]);
                                precipDataElement.date = DateTime.Parse(element[5]);
                                precipDataElement.precip = double.Parse(element[6]);
                                data.Add(precipDataElement);
                            } catch (FormatException e)
                            {
                            }
                        }
                        lineCount++;
                    }
                }
            }
            catch (IOException e) {
                Console.WriteLine("File path " + filename + " is invalid. File must exist and have the .txt extension.");
            }
        }
    }
}
