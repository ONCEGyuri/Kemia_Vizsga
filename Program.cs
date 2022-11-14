//Év; Elem; Vegyjel; Rendszám; Felfedező

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Nemszpesz
{
    class Kemia
    {
        public string ev { set; get; }
        public string elem { set; get; }
        public string vegyjel { set; get; }
        public string rendszam { set; get; }
        public string felfedezo { set; get; }

        public Kemia(string sor)
        {
            var s = sor.Trim().Split(';');
            ev = s[0];
            elem = s[1];
            vegyjel = s[2];
            rendszam = s[3];
            felfedezo = s[4];
        }
    }
    class Program
    {

        public static bool Secondary(string sor)
        {
            var abc = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            if(sor.Length > 2 || sor.Length < 1)
            {
                return false;
            }
            foreach (var fut in sor.ToUpper())
            {
                if(!abc.Contains(fut))
                {
                    return false;
                }
            }
            return true;
            
        }


        public static void Main(string[] args)
        {
            var lista = new List<Kemia>();
            var sr = new StreamReader("felfedezesek.txt", Encoding.UTF8);
            var elsosor = sr.ReadLine();

            while (!sr.EndOfStream)
            {
                lista.Add(new Kemia(sr.ReadLine()));
            }
            sr.Close();

            //3. feladat: Kémia elemek száma
            Console.WriteLine($"3. feladat: Elemek száma: {lista.Count}");

            //4. feladat: Felfedezések száma az Ókorban
            var okor = (
                from sor in lista
                where sor.ev == "Ókor"
                select sor
            );
            Console.WriteLine($"4. feladat: Felfedezések száma az Ókorban: {okor.Count()}");

            var raktar = "";
            while (true)
            {
                Console.Write("5. Feladat:");
                raktar = Console.ReadLine().ToUpper();
                if (Secondary(raktar)) 
                { 
                    break; 
                }
            }

            //6. feladat: Keresés \n \t Az elem vegyjele: {a.vegyjel} \n \t Az elem neve: {a.elem} \n \t Rendszáma: {a.rendszam} \n \t Felfedezés éve: {a.ev} \n \t Felfedező: {a.felfedezo}

            var kereses = (
                from sor in lista
                where sor.vegyjel.ToUpper() == raktar
                select sor
            );
            Console.WriteLine($"6. feladat: Keresés");
            if (kereses.Any())
            {
                var a = kereses.First();
                Console.WriteLine($"\t \t Az elem vegyjele: {a.vegyjel} \n \t\t Az elem neve: {a.elem} \n \t\t Rendszáma: {a.rendszam} \n \t\t Felfedezés éve: {a.ev} \n \t\t Felfedező: {a.felfedezo}");
            }
            else
            {
                Console.WriteLine($"\t Nincs ilyen elem az adatforrásban!");
            }

            //7. feladat: Leghosszabb időszak két elem felfedezése között az ókor után

            var ev = (
                from sor in lista
                where sor.ev != "Ókor"
                select int.Parse(sor.ev)
            ).ToList();

            var idoszak = new List<int>();

            for (int i = 0; i < ev.Count() - 1; i++ )
            {
                idoszak.Add(ev[i + 1] - ev[i]);
            }

            Console.WriteLine($"7. feladat: {idoszak.Max()} év volt a leghosszabb időszak két elem felfedezése között.");

            //8. feladat: Jelenítse meg azokat az éveket, amelyekben több mint három elemet fedeztek fel!

            var nem_buzi = (
            from sor in lista
            where sor.ev != "Ókor"
            group sor by sor.ev
            );

            var stat = (
                from sor in nem_buzi
                where sor.Count() > 3
                select sor
            );

            Console.WriteLine("8.feladat: Statiszika");
            foreach (var s in stat)
            {
                Console.WriteLine($" \t\t {s.Key} - {s.Count()}");
            }

            Console.Read();
        }
    }
}
