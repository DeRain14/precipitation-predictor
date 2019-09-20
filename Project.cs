using System;
using System.IO;
using System.Runtime.Serialization.Json;

namespace Avalara
{
    /*
     * Program for predicting the amount of precipitation on any given date
     * for the 27612 area code. Uses historical data measuring the
     * precipitation amounts from the past to predict the average amount
     * of precipitation to expect. Takes a .txt file as a command line argument
     * for the set of data to make the prediction with as well as an optional 
     * numerical date to predict for specifically, otherwise using the current date.
     * Outputs response in JSON format.
     */
    public class Project
    {
        //reads data and parses elements into PrecipDataElement objects
        private static DataReader dataReader;
        //makes the prediction based on given date or current date
        private static Predictor predictor;

        //
        static void Main(string[] args)
        {
            Prediction precipPred;
            DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(Prediction));

            //only execute if given filepath exists and is a .txt file
            if (!File.Exists(args[0]))
                Console.WriteLine("File path " + args[0] + " is invalid. File must exist and have the .txt extension.");
            else
            {
                dataReader = new DataReader(args[0]);
                predictor = new Predictor(dataReader);
                if (args.Length < 2) //if date not provided, use current date
                {
                    precipPred = predictor.GetPrediction();
                    //writes Prediction object as JSON to standard output
                    json.WriteObject(Console.OpenStandardOutput(), precipPred);
                }
                else
                {
                    //parse given date provided it matches standard conventions
                    try
                    {
                        DateTime givenDate = DateTime.Parse(args[1]);
                        precipPred = predictor.GetPrediction(givenDate);
                        //writes Prediction object as JSON to standard output
                        json.WriteObject(Console.OpenStandardOutput(), precipPred);
                    }
                    catch (FormatException e)
                    {
                        Console.WriteLine("Invalid argument. " +
                        "Date must be formatted using standard conventions e.g. MM/DD/YYYY or MM/DD/YY.");
                    }
                }
            }
            Console.Read();//waits for input before exiting
        }
    }
}