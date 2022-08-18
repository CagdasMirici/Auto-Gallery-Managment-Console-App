using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OtoGaleri
{
    class Araba
    {
        public string Plaka { get; set; }
        public string Marka { get; set; }
        public float KiralamaBedeli { get; set; }
        public ARAC_TIPI AracTipi { get; set; }


        public DURUM Durum { get; set; }
        public List<DateTime> KiralanmaTarihleri { get; set; }
        public List<int> KiralamaSureleri { get; set; }


        public int ToplamKiralanmaSayisi
        {
            get
            {
                if (this.KiralamaSureleri != null)
                {
                    return this.KiralamaSureleri.Count;
                }
                return 0;
            }
        }

        public int ToplamKiralanmaSuresi
        {
            get
            {
                if (this.KiralamaSureleri != null)
                {
                    return this.KiralamaSureleri.Sum();
                }
                return 0;
            }
        }




        public Araba()
        {
            KiralanmaTarihleri = new List<DateTime>();
            KiralamaSureleri = new List<int>();
            this.Durum = DURUM.Galeride;
        }

        public Araba(string plaka, string marka, float kiralamaBedeli, ARAC_TIPI aracTipi)
        {
            KiralanmaTarihleri = new List<DateTime>();
            KiralamaSureleri = new List<int>();

            this.Plaka = plaka.ToUpper();
            this.Marka = marka.ToUpper();
            this.KiralamaBedeli = kiralamaBedeli;
            this.AracTipi = aracTipi;
            this.Durum = DURUM.Galeride;
        }






    }



    public enum DURUM
    {
        Empty,
        Kirada,
        Galeride,
        Serviste
    }
    public enum ARAC_TIPI
    {
        Empty,
        Sedan,
        SUV,
        Hatchback
    }


}
