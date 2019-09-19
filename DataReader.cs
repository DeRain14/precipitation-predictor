using System;
using System.Collections.Generic;
using System.IO;

namespace Avalara {
    public class DataReader {
        private string filename;
        private List<PrecipDataElement> data;

        public DataReader(string file) {
            filename = file;
            data = new List<PrecipDataElement>();
            readFile(); //populates data list
        }

        //return the elements of data that match the month and day given
        public static List<PrecipDataElement> getPrecipData(DateTime date) {
            List<PrecipDataElement> precipData = data.FindAll(dataElement
                                    => (dataElement.date.Day == date.Day)
                                    && (dataElement.date.Month == date.Month) )
            return precipData;
        }

        //read all of the data into memory
        private void readFile() {
            try {
                using (StreamReader reader = new StreamReader(filename))
                {
                    string line = "";
                    int lineCount = 0;

                    // read and store elements from the data until the end of
                    // the file is reached
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] element = line.split(" ");
                        if (lineCount != 0) { //skipping the title line
                            PrecipDataElement precipDataElement = {
                                station = element[0],
                                name = element[1],
                                latitude = float.Parse(element[2]),
                                longitude = float.Parse(element[3]),
                                elevation = float.Parse(element[4]),
                                date = DateTime.Parse(element[5]),
                                precip = float.Parse(element[6])
                            }
                            data.add(precipDataElement);
                        }
                        lineCount++;
                    }
                }
            }
            catch (IOException e) {
                return e.getMessage();
            }
        }
    }
}
