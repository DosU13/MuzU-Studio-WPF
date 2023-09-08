using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace MuzUStandard.data
{
    public class Node
    {
        public Node() { }

        /// <summary>
        /// Time as micro seconds
        /// </summary>
        public long Time { get; set; } = 0;
        /// <summary>
        /// Time as micro seconds
        /// </summary>
        public long? Length { get; set; }
        public int? Note{ get; set; }
        public string Lyrics { get; set; }
    }
}
