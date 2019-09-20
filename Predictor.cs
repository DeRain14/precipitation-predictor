using System;
using System.Collections.Generic;

namespace Avalara {
    public class Predictor {

        private DataReader dataReader;

        public Predictor (DataReader reader)
        {
            dataReader = reader;
        }

        //gets matching data elements from reader
        //and averages their precipitation amounts
        private Prediction MakePrediction(DateTime date) {
            List<PrecipDataElement> precipData = dataReader.GetPrecipData(date);
            double avgPrecip = 0;
            foreach(PrecipDataElement precipElement in precipData) {
                avgPrecip += precipElement.precip;
            }
            avgPrecip /= precipData.Count;
            return new Prediction { date = date.ToShortDateString(), prediction = Math.Round(avgPrecip, 3) };
        }

        //returns prediction for given date
        public Prediction GetPrediction(DateTime date) {
            Prediction newPred = MakePrediction(date);
            return newPred;
        }

        //returns prediction for current date
        public Prediction GetPrediction() {
            DateTime currentDate = DateTime.Today;
            return GetPrediction(currentDate);
        }
    }
}
