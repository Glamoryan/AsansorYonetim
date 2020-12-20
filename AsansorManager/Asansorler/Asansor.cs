using System.Collections.Generic;

namespace AsansorManager.Asansorler
{
    public abstract class Asansor //Asansörlerimizin ortak özelliklerini tutan abstract class
    {
        // Asansor propertylerimizi tanımlıyoruz
        public readonly int capacity = 10;
        public string asansorAd { get; set; }        
        public bool mode { get; set; }
        public int floor { get; set; }
        public int destination { get; set; }
        public int count_inside { get; set; }
        public List<int[]> inside { get; set; }
        public string direction { get; set; }
    }
}
