using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telemetry
{
    /// <summary>
    /// Class that creates the dictionary used to provide wave form questions 
    /// to the user with questions being the key and answers being the value.
    /// </summary>
    public class WaveDictionary
    {
        public string[]? WaveAscii { get; set; }
        public string[]? WaveName { get; set; }
        /// <summary>
        /// Reads the .csv file containing the ascii wave forms and adds them to an array
        /// </summary>
        /// <returns>A string array for the WaveAscii field</returns>
        public string[] CSVWaveFormToArray()
        {
            //TODO: Error handling
            string asciiFilePath = Path.Combine(Directory.GetCurrentDirectory(), "WaveAscii.csv");
            string asciiString = File.ReadAllText(asciiFilePath);
            WaveAscii = asciiString.Split(',');
            return WaveAscii;
        }
        /// <summary>
        /// Reads the .csv file containing the wave form names and adds them to an array
        /// </summary>
        /// <returns>A string array for the WaveName field</returns>
        public string[] CSVWaveNameToArray()
        {
            //TODO: Error handling
            string waveNameFilePath = Path.Combine(Directory.GetCurrentDirectory(), "WaveNames.csv");
            string waveNameString = File.ReadAllText(waveNameFilePath);
            WaveName = waveNameString.Split(',');
            return WaveName;
        }
        /// <summary>
        /// Creates a dictionary using the WaveAscii and WaveName fields representing kvp respectively
        /// </summary>
        /// <returns>A string, string kvp dictionary</returns>
        public Dictionary<string, string> CSVToDictionary()
        {
            if (WaveAscii != null && WaveName != null)
            {
                Dictionary<string, string> waves = WaveAscii.Zip(WaveName, (k, v) => new { k, v }).ToDictionary(x => x.k, x => x.v);
                return waves;
            }
            Dictionary<string, string> wavesNull = new();
            return wavesNull;
        }

    }
}
