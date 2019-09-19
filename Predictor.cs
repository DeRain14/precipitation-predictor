using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.DateTime;
using DataReader;
using PredictorObjects;

namespace Avalara {
    public class Predictor {

        //reads data and parses elements into PrecipDataElement objects
        private DataReader dataReader;

        //gets matching data elements from reader
        //and averages their precipitation amounts
        private Prediction makePrediction(DateTime date) {
            List<PrecipDataElement> precipData = dataReader.getPrecipData(date);
            float avgPrecip = 0.0f;
            foreach(PrecipDataElement precipElement in precipData) {
                avgPrecip += precipElement.precip;
            }
            avgPrecip /= precipData.Count;
            return new Prediction { date = date, precipPred = avgPrecip };
        }

        //returns prediction for given date
        public string getPrediction(DateTime date) {
            Prediction newPred = makePrediction(date);
            var result = new JavaScriptSerializer().Serialize(newPred);
            return result;
        }

        //returns prediction for current date
        public string getPrediction() {
            DateTime currentDate = DateTime.Today;
            return getPrediction(currentDate);
        }

        static void main(String args[]) {
            string precipPred = "";
            dataReader = new DataReader("27612-precipitation-data.txt");
            if (args.length < 2)
                precipPred = getPrediction();
            else {
                try {
                    precipPred = getPrediction(DateTime.Parse(args[1]));
                    System.printf("%s", precipPred);
                }
                catch (IOException e) {
                    System.printf("%s", "Invalid argument. " +
                    "Argument be formatted as a date using numerical values.");
                }
            }
        }
    }
}
