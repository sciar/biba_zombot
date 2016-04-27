using System.Collections.Generic;
using UnityEngine;
using System;

namespace BibaFramework.BibaNetwork
{
    public class BibaManifest
    {
		public DateTime TimeStamp { get; set; }
        public List<ManifestLine> Lines = new List<ManifestLine>();
    }

    public class ManifestLine
    {
        public string FileName { get; set; }
		public DateTime TimeStamp { get; set; }
        public string HashCode { get; set; }
        public bool OptionalDownload { get; set; }
    }
}