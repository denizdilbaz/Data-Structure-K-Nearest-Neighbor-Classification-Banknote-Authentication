using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KNNClassification
{
    class Program
    {
        public static object varyans;
        public static object basiklik;
        public static object carpiklik;
        public static object entropi;
        public static object tur;
        public static ArrayList varyanslar = new ArrayList();
        public static ArrayList carpikliklar = new ArrayList();
        public static ArrayList basikliklar = new ArrayList();
        public static ArrayList entropiler = new ArrayList();
        public static ArrayList turler = new ArrayList();
        public static ArrayList basari_orani = new ArrayList();


        public Program(object varyans, object carpiklik, object basiklik, object entropi, object tur)
        {

            Program.varyans = varyans;
            Program.carpiklik = carpiklik;
            Program.basiklik = basiklik;
            Program.entropi = entropi;
            Program.tur = tur;
            varyanslar.Add(varyans);
            carpikliklar.Add(carpiklik);
            basikliklar.Add(basiklik);
            entropiler.Add(entropi);
            turler.Add(tur);

        }

        public static void KNN(int k, double varyanss, double carpiklikk, double basiklikk, double entropii, int turr)
        {


            NumberFormatInfo provider = new NumberFormatInfo();
            provider.NumberDecimalSeparator = ".";

            ArrayList uzakliklar = new ArrayList();
            for (int i = 0; i < varyanslar.Count; i++)
            {
                double uzaklik;
                uzaklik = Math.Sqrt(Math.Pow((varyanss - Convert.ToDouble(varyanslar[i], provider)), 2) +
                    Math.Pow((carpiklikk - Convert.ToDouble(carpikliklar[i], provider)), 2) +
                    Math.Pow((basiklikk - Convert.ToDouble(basikliklar[i], provider)), 2) +
                    Math.Pow((entropii - Convert.ToDouble(entropiler[i], provider)), 2));

                uzakliklar.Add(uzaklik);
            }



            ArrayList uzakliklar_kopya = new ArrayList(uzakliklar);
            uzakliklar_kopya.Sort();

            ArrayList en_kucuk = new ArrayList();
            for (int i = 0; i < k; i++)
            {
                for (int j = 0; j < uzakliklar.Count; j++)
                {
                    if (uzakliklar_kopya[i] == uzakliklar[j])
                    {
                        en_kucuk.Add(j);
                    }

                }
            }
            int sayac1 = 0;
            int sayac0 = 0;
            Console.WriteLine("Varyans değeri: " + varyanss + "\t Çarpıklık değeri: " + carpiklikk +
                " \t Basıklık değeri: " + basiklik + "\t Entropi değeri: " + entropii + " olan banknota en yakın " + k + " tane banknotlar");
            for (int i = 0; i < en_kucuk.Count; i++)
            {                
                Console.WriteLine(String.Format("{0}. en yakın Varyans:{1}     Çarpıklık:{2}      Basiklik:{3}     Entropi:{4}     Türü:{5}     Uzaklıkları:{6}",i+1, varyanslar[(int)en_kucuk[i]], carpikliklar[(int)en_kucuk[i]], basikliklar[(int)en_kucuk[i]], entropiler[(int)en_kucuk[i]], turler[(int)en_kucuk[i]], uzakliklar[(int)en_kucuk[i]]));
                if ((Convert.ToInt32(turler[(int)en_kucuk[i]]) == 1))
                {
                    sayac1++;
                }
                else sayac0++;
                Console.WriteLine();
            }
            if (sayac1 > sayac0)
            {
                Console.WriteLine("Tahminlenen türü = 1");
                if (turr == 1)
                {
                    basari_orani.Add(1);
                }
            }
            else if (sayac0 > sayac1)
            {
                Console.WriteLine("Tahminlenen türü = 0");

                if (turr == 0)
                {
                    basari_orani.Add(1);
                }
            }
            else
            {
                Console.WriteLine("Tahminlenen türü =", turler[(int)en_kucuk[0]]);
                if (turr == Convert.ToInt32(turler[(int)en_kucuk[0]]))
                {
                    basari_orani.Add(1);
                }

            }


            Console.WriteLine("--------------------------------------------------------------------------------------------------------------------------------------------------------");

        }
        public static void SonYuzKarsilastir()
        {
            NumberFormatInfo provider2 = new NumberFormatInfo();
            provider2.NumberDecimalSeparator = ".";


            ArrayList sifirlar = new ArrayList();
            ArrayList birler = new ArrayList();
            for (int i = 0; i < turler.Count; i++)
            {
                if ((Convert.ToInt32(turler[i])) == 0)
                {
                    sifirlar.Add(i);
                }
                else
                {
                    birler.Add(i);
                }
            }
            sifirlar.RemoveRange(0, sifirlar.Count - 100);
            birler.RemoveRange(0, birler.Count - 100);
            Console.WriteLine("Lütfen son yüz değerleri için k değerini giriniz: ");
            int k = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < sifirlar.Count; i++)
            {

                KNN(k, Convert.ToDouble(varyanslar[(int)sifirlar[i]], provider2), Convert.ToDouble(carpikliklar[(int)sifirlar[i]], provider2),
                    Convert.ToDouble(basikliklar[(int)sifirlar[i]], provider2), Convert.ToDouble(entropiler[(int)sifirlar[i]], provider2), 0);
            }

            for (int i = 0; i < birler.Count; i++)
            {

                KNN(k, Convert.ToDouble(varyanslar[(int)birler[i]], provider2), Convert.ToDouble(carpikliklar[(int)birler[i]], provider2),
                    Convert.ToDouble(basikliklar[(int)birler[i]], provider2), Convert.ToDouble(entropiler[(int)birler[i]], provider2), 1);
            }
            int basari_orani2 = basari_orani.Count / 2;

            Console.WriteLine("Başarı Oranı %:" + basari_orani2);
        }
        public static void TextiYazdir()
        {
            Console.WriteLine("\t\t\t\t\t\t\t\t\t\t ----Textteki Banknotlar ----");
            for (int i = 0; i < varyanslar.Count; i++)
            {

                Console.Write((i + 1) + ".ci banknotun değerleri => \t\tVaryans: {0,10} ", varyanslar[i]);
                Console.Write("\t\tÇarpıklık: {0,10} ", carpikliklar[i]);
                Console.Write("\t\tBasıklık: {0,10}", basikliklar[i]);
                Console.Write("\t\tEntropi: {0,10}", entropiler[i]); ;
                Console.Write("\t\tTür: {0,10}", turler[i]);
                Console.WriteLine();

            }
        }





        public static void Main(string[] args)
        {
            char[] delimiterChars = { ' ', ',' };
            ArrayList Varyans = new ArrayList();
            ArrayList Carpiklik = new ArrayList();
            ArrayList Basiklik = new ArrayList();
            ArrayList Entropi = new ArrayList();
            ArrayList Tur = new ArrayList();
            String[] oku = new String[] { };
            oku = File.ReadAllLines(@"C:\Users\05414015011\source\repos\KNNClassification\data_banknote_authentication.txt");
            for (int i = 0; i < oku.Length; i++)
            {
                String[] degerler = new String[4];
                degerler = oku[i].Split(delimiterChars);
                Varyans.Add(degerler[0]);
                Carpiklik.Add(degerler[1]);
                Basiklik.Add(degerler[2]);
                Entropi.Add(degerler[3]);
                Tur.Add(degerler[4]);
            }

            for (int i = 0; i < Varyans.Count; i++)
            {

                Program elemani = new Program(Varyans[i], Carpiklik[i], Basiklik[i], Entropi[i], Tur[i]);

            }




            KNN(5, 1.89, -2.05, 0.93, 1.24, 2);
            KNN(3, 2.43, 2.82, -2.79, -2.81, 2);
            KNN(1, -2.24, 2.74, 2.09, -1.34, 2);
            KNN(1, 2.48, -0.09, 2.60, -2.72, 2);
            KNN(3, 0.79, 1.80, 1.07, -2.42, 2);
            KNN(5, -2.41, -2.15, 1.55, 1.94, 2);
            KNN(3, -1.33, -0.47, 1.46, 1.17, 2);
            KNN(3, 0.28, 2.49, -0.65, -1.10, 2);
            KNN(1, 2.75, 1.75, 0.93, 2.70, 2);
            KNN(5, 2.79, 2.76, -1.97, -2.79, 2);

            SonYuzKarsilastir();

            TextiYazdir();
            Console.ReadKey();

        }
    }
}