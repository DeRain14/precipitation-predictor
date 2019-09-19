using System;
using System.DateTime;

namespace Avalara {
    public struct Prediction {
        //prediction date
        public DateTime date;
        //amount of precipitation predicted
        public float prediction;
    }

    public struct PrecipDataElement {
        //station
        public string station;
        //name of area
        public string name;
        //latitude of area
        public float latitude;
        //longitude of area
        public float longitude;
        //elevation of area
        public float elevation;
        //date of precipitation measurement
        public DateTime date;
        //amount of precipitation
        public float precip;
    }
}
