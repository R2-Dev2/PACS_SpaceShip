using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class EncodingConfig
    {
        public string OriginalFilesPath { get; set; }
        public string EncodedFilesPath { get; set; }
        public int NumFiles { get; set; }
        public int NumChars { get; set; }
    }
}
