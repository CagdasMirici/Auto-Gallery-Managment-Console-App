using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OtoGaleri
{
    class OtoGaleri
    {
        public List<Araba> Arabalar { get; set; }


        public int ToplamKiralamaSayisi
        {
            get
            {
                if (this.Arabalar != null)
                {
                    return this.Arabalar.Sum(a => a.ToplamKiralanmaSayisi);
                }
                return 0;
            }
        }
        public int ToplamKiralamaSuresi
        {
            get
            {
                if (this.Arabalar != null)
                {
                    return this.Arabalar.Sum(a => a.ToplamKiralanmaSuresi);
                }
                return 0;
            }
        }
        public int ToplamAracSayisi
        {
            get
            {
                if (this.Arabalar != null)
                {
                    return this.Arabalar.Count;
                }
                return 0;
            }
        }
        public int KiradakiAracSayisi
        {
            get
            {
                if (this.Arabalar != null)
                {
                    return this.Arabalar.Where(a => a.Durum == DURUM.Kirada).ToList().Count;
                }
                return 0;
            }
        }
        public int GaleridekiAracSayisi
        {
            get
            {
                if (this.Arabalar != null)
                {
                    return this.Arabalar.Where(a => a.Durum == DURUM.Galeride).ToList().Count;
                }
                return 0;
            }
        }

        public double Ciro { get; }

        public OtoGaleri()
        {

        }


        public void AracKirala(string plakaVeyaAracTipi, int sure)
        {
            if (this.PlakaMi(plakaVeyaAracTipi))
            {
                this.Kirala(plakaVeyaAracTipi, sure);
            }
            else
            {
                ARAC_TIPI at = AracTipiBul(plakaVeyaAracTipi);
                this.Kirala(at, sure);
            }
        }

        public bool PlakaMi(string veri)
        {
            int i;
            string ilkBolum = veri.Substring(0, 2);

            return int.TryParse(ilkBolum, out i) && i > 0;

        }

        public void SahteAracGir()
        {

            Araba a = new Araba("34us2342", "opel", 50, ARAC_TIPI.Sedan);

            this.Arabalar.Add(a);

            this.Arabalar.Add(new Araba("34arb3434", "FIAT", 70, ARAC_TIPI.SUV));
            this.Arabalar.Add(new Araba("35arb3535", "KIA", 60, ARAC_TIPI.SUV));

        }

        public void Kirala(ARAC_TIPI aracTipi, int sure)
        {
            Araba a = this.Arabalar.Where(t => t.AracTipi == aracTipi && t.Durum == DURUM.Galeride).FirstOrDefault();
            if (a != null)
            {
                a.Durum = DURUM.Kirada;
                a.KiralamaSureleri.Add(sure);
                a.KiralanmaTarihleri.Add(DateTime.Now);
            }
        }

        public void Kirala(string plaka, int sure)
        {
            Araba a = this.Arabalar.Where(x => x.Plaka == plaka.ToUpper().Trim() && x.Durum == DURUM.Galeride).FirstOrDefault();

            if (a != null)
            {
                a.Durum = DURUM.Kirada;
                a.KiralamaSureleri.Add(sure);
                a.KiralanmaTarihleri.Add(DateTime.Now);
            }
        }

        public void KiralamaSonlandir(string plaka)
        {
            Araba a = this.Arabalar.Where(a => a.Plaka == plaka.ToUpper().Trim() && a.Durum == DURUM.Kirada).FirstOrDefault();
            a.Durum = DURUM.Galeride;
        }


        public void ArabaEkle(string plaka, string marka, float kiralamaBedeli, ARAC_TIPI aracTipi)
        {
            this.Arabalar.Add(new Araba(plaka, marka, kiralamaBedeli, aracTipi));
        }

        public void ArabaSil(string plaka)
        {
            Araba a = this.Arabalar.Where(a => a.Plaka.ToUpper() == plaka.ToUpper().Trim()).FirstOrDefault();
            this.Arabalar.Remove(a);
        }

        public void KiralamaIptal(string plaka)
        {
            Araba a = this.Arabalar.Where(a => a.Plaka.ToUpper() == plaka.ToUpper().Trim() && a.Durum == DURUM.Kirada).FirstOrDefault();
            a.KiralamaSureleri.RemoveAt(a.KiralamaSureleri.Count - 1);
            a.KiralanmaTarihleri.RemoveAt(a.KiralanmaTarihleri.Count - 1);
            a.Durum = DURUM.Galeride;

        }

        public bool KiralamayaUygun(ARAC_TIPI aracTipi)
        {
            Araba a = this.Arabalar.Where(a => a.AracTipi == aracTipi && a.Durum == DURUM.Galeride).FirstOrDefault();
            return a != null;
        }
        public bool KiralamayaUygun(string plaka)
        {
            Araba a = this.Arabalar.Where(a => a.Plaka == plaka.ToUpper() && a.Durum == DURUM.Galeride).FirstOrDefault();
            return a != null;
        }

        public ARAC_TIPI AracTipiBul(string veri)
        {
            ARAC_TIPI at = ARAC_TIPI.Empty;
            if (veri.ToUpper() == ARAC_TIPI.Hatchback.ToString().ToUpper())
            {
                at = ARAC_TIPI.Hatchback;
            }
            else if (veri.ToUpper() == ARAC_TIPI.Sedan.ToString().ToUpper())
            {
                at = ARAC_TIPI.Sedan;
            }
            else if (veri.ToUpper() == ARAC_TIPI.SUV.ToString().ToUpper())
            {
                at = ARAC_TIPI.SUV;
            }

            return at;
        }


        public DURUM AracKontrol(string veri)
        {
            Araba a;
            if (this.PlakaMi(veri))
            {
                a = this.Arabalar.Where(a => a.Plaka == veri.ToUpper()).FirstOrDefault();
            }
            else
            {
                ARAC_TIPI at = AracTipiBul(veri);
                a = this.Arabalar.Where(a => a.AracTipi.ToString().ToUpper() == veri.ToUpper()).FirstOrDefault();
            }

            if (a != null)
            {
                return a.Durum;
            }
            else
            {
                return DURUM.Empty;
            }
        }

      
    }
}
