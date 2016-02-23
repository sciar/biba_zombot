using System.Collections.Generic;
using UnityEngine;

namespace BibaFramework.BibaNetwork
{
    public class BibaManifest
    {
        public int Version { get; set; }
        public List<ManifestLine> Lines = new List<ManifestLine>();
    }

    public class ManifestLine
    {
        public string FileName { get; set; }
        public int Version { get; set; }
        public string HashCode { get; set; }
        public bool OptionalDownload { get; set; }
    }
}