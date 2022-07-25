using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telemetry
{
    public class WaveDictionary
    {
        public string[]? WaveAscii { get; set; }
        public string[]? WaveName { get; set; }
        public string[] CSVToBytesToArray()
        {
            string asciiFilePath = Path.Combine(Directory.GetCurrentDirectory(), "WaveAscii.csv");
            string asciiString = File.ReadAllText(asciiFilePath);
            WaveAscii = asciiString.Split(',');
            return WaveAscii;
        }
        public string[] CSVWaveNameToArray()
        {
            string waveNameFilePath = Path.Combine(Directory.GetCurrentDirectory(), "WaveNames.csv");
            string waveNameString = File.ReadAllText(waveNameFilePath);
            WaveName = waveNameString.Split(',');
            return WaveName;
        }
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
