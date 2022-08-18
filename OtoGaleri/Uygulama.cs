using System;
using System.Collections.Generic;
using System.Linq;




#! single responsability principe Her method yada her sinif sadece kendi isini yapar
#! classlarla calisma oop projeler yaptik oto gleri yonetimi vs 2. adim php laravel 3. asama python pandas numpy veri bilimi pradiction. Backend dile getirme.
#! bilinen diller, C# , Python, su an icin Bu dillerde Backend dev olarak calismak istiyorum, veri tabaninda crud islemleri iliskisel mantiklar primery foreign keys

namespace OtoGaleri
{
    class Uygulama
    {
        OtoGaleri OG = new OtoGaleri();

        public void SahteVeri()
        {
            Araba a1 = new Araba("55EP983", "FIAT", 220, ARAC_TIPI.SUV);


            Araba a2 = new Araba();
            a2.Plaka = "55EE852";
            a2.Marka = "OPEL";
            a2.AracTipi = ARAC_TIPI.Hatchback;
            a2.Durum = DURUM.Galeride;
            a2.KiralamaBedeli = 200;

            Araba a3 = new Araba();
            a3.Plaka = "55CC777";
            a3.Marka = "KIA";
            a3.AracTipi = ARAC_TIPI.Sedan;
            a3.Durum = DURUM.Galeride;
            a3.KiralamaBedeli = 250;

            OG.Arabalar.Add(a1);
            OG.Arabalar.Add(a2);
            OG.Arabalar.Add(a3);
        }
        public void Calistir()
        {
            OG.Arabalar = new List<Araba>();
            AnaEkran();
        }
        public void Menu()
        {
            Console.WriteLine("Galeri Otomasyon            ");
            Console.WriteLine("1 - Araba Kirala(K)                    ");
            Console.WriteLine("2 - Araba Teslim Al(T)                 ");
            Console.WriteLine("3 - Kiradaki arabaları listele(R)      ");
            Console.WriteLine("4 - Müsait arabaları listele(M)        ");
            Console.WriteLine("5 - Tüm arabaları listele(A)           ");
            Console.WriteLine("6 - Kiralama İptali(I)                 ");
            Console.WriteLine("7 - Yeni araba Ekle(Y)                 ");
            Console.WriteLine("8 - Araba sil(S)                       ");
            Console.WriteLine("9 - Bilgileri göster(G)                ");
        }
        public void AnaEkran()
        {
            SahteVeri();
            Menu();
            do
            {
                string secim = Secim();

                switch (secim)
                {
                    case "1":
                    case "K":
                        AracKirala();
                        break;
                    case "2":
                    case "T":
                        ArabaTeslim();
                        break;
                    case "3":
                    case "E":
                        AracListele(DURUM.Kirada);
                        break;
                    case "4":
                    case "L":
                        AracListele(DURUM.Galeride);
                        break;
                    case "5":
                    case "H":
                        AracListele(DURUM.Empty);
                        break;
                    case "6":
                    case "I":
                        KiralamaIptal();
                        break;
                    case "7":
                    case "Y":
                        ArabaEkle();
                        break;
                    case "8":
                    case "S":
                        ArabaSilme();
                        break;
                    case "9":
                        BilgileriYazdir();
                        break;
                }

            } while (true);
        }
        public void ListeYazdir(List<Araba> liste)
        {

            if (liste.Count > 0)
            {
                Console.WriteLine("Plaka".PadRight(12) + "Marka".PadRight(14) + "K. Bedeli".PadRight(15) + "Araç Tipi".PadRight(24) + "K. Sayısı".PadRight(12) + "Durum  ");
                Console.WriteLine("-----------------------------------------------------------------------------------------");
                foreach (Araba item in liste)
                {
                    Console.WriteLine(item.Plaka.PadRight(12) + item.Marka.PadRight(14) + item.KiralamaBedeli.ToString().PadRight(15) + item.AracTipi.ToString().PadRight(24) + item.KiralamaSureleri.Count.ToString().PadRight(12) + item.Durum);
                }
            }

        }
        public string Secim()
        {
            string secimler = "123456789KTRMAIYSG";
            string s;
            do
            {
                Console.WriteLine();
                Console.Write("Seçiminiz: ");
                s = Console.ReadLine().ToUpper();
                Console.WriteLine();
            } while (s.Length != 1 || secimler.Contains(s) == false);
            return s;

        }
        public void AracKirala()
        {

            Console.WriteLine("-Araç Kirala -");
            string giris;
            while (true)
            {
                Console.Write("Kiralanacak aracın plakası veya araç tipi:");
                giris = Console.ReadLine().ToUpper();

                DURUM sonuc = OG.AracKontrol(giris);

                if (!OG.PlakaMi(giris) && OG.AracTipiBul(giris) == ARAC_TIPI.Empty)
                {// eğer giriş plaka değilse ve bu girişe göre bir araç tipi bulunamıyorsa, tanımsız bir giriş yapılmıştır.
                    Console.WriteLine("Giriş tanımlanamadı.Tekrar deneyin.");
                }
                else if (sonuc == DURUM.Kirada)
                {
                    Console.WriteLine("Araç müsait değil. Başka bir araç seçin.");
                }
                else if (sonuc == DURUM.Empty)
                {
                    Console.WriteLine("Galeriye ait bu plakada bir araç yok.");
                }
                else if (sonuc == DURUM.Galeride)
                {
                    break;
                }



            }

            int sure = SayiAl("Kiralama süresi: ");
            OG.AracKirala(giris, 2);

            Console.WriteLine("Araç " + sure + " saatliğine kiralandı.");

        }
        public void ArabaTeslim()
        {
            Console.WriteLine("-Araç Teslim -");
            string giris;
            while (true)
            {
                Console.Write("Teslim edilecek aracın plakası: ");
                giris = Console.ReadLine();
                DURUM sonuc = OG.AracKontrol(giris);

                if (!OG.PlakaMi(giris) && OG.AracTipiBul(giris) == ARAC_TIPI.Empty)
                {// eğer giriş plaka değilse ve bu girişe göre bir araç tipi bulunamıyorsa, tanımsız bir giriş yapılmıştır.
                    Console.WriteLine("Giriş tanımlanamadı.Tekrar deneyin.");
                }
                else if (sonuc == DURUM.Kirada)
                {
                    OG.KiralamaSonlandir(giris);
                    Console.WriteLine();
                    Console.WriteLine("Araç galeride beklemeye alındı.");
                    Console.WriteLine();
                    break;
                }
                else if (sonuc == DURUM.Empty)
                {
                    Console.WriteLine("Galeriye ait bu plakada bir araç yok.");
                }
                else if (sonuc == DURUM.Galeride)
                {
                    Console.WriteLine("Hatalı giriş yapıldı. Araç zaten galeride. ");

                }

            }
        }
        public void AracListele(DURUM durum)
        {
            List<Araba> liste;

            if (durum != DURUM.Empty)
            {
                liste = OG.Arabalar.Where(a => a.Durum == durum).ToList();
            }
            else
            {
                liste = OG.Arabalar;
            }

            if (liste != null && liste.Count == 0)
            {
                Console.WriteLine("Listede gösterilecek araç yok.");
            }
            else
            {
                ListeYazdir(liste);
            }



        }
        public void KiralamaIptal()
        {
            Console.WriteLine("--Kiralama İptali- -");
            string giris;
            while (true)
            {
                Console.Write("Kiralaması iptal edilecek aracın plakası: ");
                giris = Console.ReadLine();
                DURUM sonuc = OG.AracKontrol(giris);

                if (!OG.PlakaMi(giris) && OG.AracTipiBul(giris) == ARAC_TIPI.Empty)
                {
                    Console.WriteLine("Giriş tanımlanamadı.Tekrar deneyin.");
                }
                else if (OG.PlakaMi(giris) && OG.AracTipiBul(giris) == ARAC_TIPI.Empty)
                {
                    Console.WriteLine("Giriş tanımlanamadı.Tekrar deneyin.");
                }
                else if (sonuc == DURUM.Galeride)
                {
                    Console.WriteLine("Hatalı giriş yapıldı. Araç zaten galeride. ");
                }
                else if (sonuc == DURUM.Kirada)
                {
                    OG.KiralamaIptal(giris);
                    Console.WriteLine();
                    Console.WriteLine("İptal gerçekleştirildi.");
                    Console.WriteLine();
                    break;
                }

            }
        }
        public void ArabaEkle()
        {

            Console.WriteLine("-Yeni Araç Ekle-");


            string plaka;
            while (true)
            {
                Console.Write("Plaka: ");
                plaka = Console.ReadLine();


                if (OG.PlakaMi(plaka))
                {
                    DURUM sonuc = OG.AracKontrol(plaka);
                    if (sonuc == DURUM.Galeride || sonuc == DURUM.Kirada || sonuc == DURUM.Serviste)
                    {
                        Console.WriteLine("Aynı plakada araç mevcut. Girdiğiniz plakayı kontrol edin.");
                        continue;
                    }
                    else
                    {

                        break;
                    }
                }
                else
                {
                    Console.WriteLine("Bu şekilde plaka girişi yapamazsınız. Tekrar deneyin.");
                    continue;
                }
            }

            Console.Write("Marka:");
            string marka = Console.ReadLine();


            string ucret;
            float ucretMi;
            while (true)
            {
                Console.Write("Kiralama Bedeli: ");
                ucret = Console.ReadLine();
                if (float.TryParse(ucret, out ucretMi) && ucretMi > 0)
                {
                    break;
                }
                Console.WriteLine("Hatalı giriş yapıldı. Tekrar deneyin.");
            }
            Console.WriteLine();
            ARAC_TIPI aracTipiSonuc;
            while (true)
            {
                Console.Write("Araç Tipi(SUV/Hatchback/Sedan): ");
                string tip = Console.ReadLine();
                aracTipiSonuc = OG.AracTipiBul(tip);
                if (aracTipiSonuc != ARAC_TIPI.Empty)
                {
                    break;
                }
                Console.WriteLine("Hatalı giriş yapıldı. Tekrar deneyin.");
            }

            OG.ArabaEkle(plaka, marka, ucretMi, aracTipiSonuc);
            Console.WriteLine("Araç başarılı bir şekilde eklendi.");
        }
        public void ArabaSilme()
        {
            Console.WriteLine("-Araba Sil-");
            string plaka;
            while (true)
            {
                Console.Write("Silinmek istenen araç plakasını girin: ");
                plaka = Console.ReadLine().ToUpper();

                DURUM sonuc = OG.AracKontrol(plaka);
                if (OG.PlakaMi(plaka))
                {
                    foreach (Araba item in OG.Arabalar)
                    {
                        if (item.Plaka != plaka)
                        {
                            Console.WriteLine("Bu plakaya ait araç bulunmamaktadır.");
                            break;
                        }
                        else
                        {
                            if (sonuc == DURUM.Kirada)
                            {
                                Console.WriteLine("Araç kirada olduğu için silinme işlemi yapılamaz.");
                            }

                            else
                            {
                                OG.ArabaSil(plaka);
                                Console.WriteLine("Araç silindi.");
                                break;
                            }

                        }
                    }
                }
                else
                {
                    Console.WriteLine("Bu şekilde plaka girişi yapamazsınız. Tekrar deneyin.");
                    continue;
                }
            }
        }
        public void BilgileriYazdir()
        {
            Console.WriteLine("-Galeri Bilgileri -");
            Console.WriteLine("Toplam Araç Sayısı: " + OG.ToplamAracSayisi);
            Console.WriteLine("Kiradaki Araç Sayısı: " + OG.KiradakiAracSayisi);
            Console.WriteLine("Bekleyen Araç Sayısı: " + OG.GaleridekiAracSayisi);
            Console.WriteLine("Toplam araç kiralama süresi: " + OG.ToplamKiralamaSuresi);
            Console.WriteLine("Toplam araç kiralama adedi: " + OG.ToplamKiralamaSayisi);
            Console.WriteLine("Ciro: " + OG.Ciro);

        }
        public int SayiAl(string text)
        {
            int sayi;
            while (true)
            {
                Console.Write(text);
                string giris = Console.ReadLine();

                if (int.TryParse(giris, out sayi))
                {
                    return sayi;
                }
                Console.WriteLine("Hatalı giriş yapıldı, tekrar deneyin.");
            }
        }
    }


}
