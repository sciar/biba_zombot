using System.Collections.Generic;

namespace BibaFramework.BibaNetwork
{
    public class BibaManifest
    {
        public List<ManifestLine> Lines = new List<ManifestLine>();
    }

    public class ManifestLine
    {
        public string FileName { get; set; }
        public int Version { get; set; }
    }
}