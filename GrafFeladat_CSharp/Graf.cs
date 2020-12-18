using System;
using System.Collections.Generic;

namespace GrafFeladat_CSharp
{
    /// <summary>
    /// Irányítatlan, egyszeres gráf.
    /// </summary>
    class Graf
    {
        int csucsokSzama;
        /// <summary>
        /// A gráf élei.
        /// Ha a lista tartalmaz egy(A, B) élt, akkor tartalmaznia kell
        /// a(B, A) vissza irányú élt is.
        /// </summary>
        readonly List<El> elek = new List<El>();
        /// <summary>
        /// A gráf csúcsai.
        /// A gráf létrehozása után új csúcsot nem lehet felvenni.
        /// </summary>
        readonly List<Csucs> csucsok = new List<Csucs>();

        /// <summary>
        /// Létehoz egy úgy, N pontú gráfot, élek nélkül.
        /// </summary>
        /// <param name="csucsok">A gráf csúcsainak száma</param>
        public Graf(int csucsok)
        {
            this.csucsokSzama = csucsok;

            // Minden csúcsnak hozzunk létre egy új objektumot
            for (int i = 0; i < csucsok; i++)
            {
                this.csucsok.Add(new Csucs(i));
            }
        }

        /// <summary>
        /// Hozzáad egy új élt a gráfhoz.
        /// Mindkét csúcsnak érvényesnek kell lennie:
        /// 0 &lt;= cs &lt; csúcsok száma.
        /// </summary>
        /// <param name="cs1">Az él egyik pontja</param>
        /// <param name="cs2">Az él másik pontja</param>
        public void Hozzaad(int cs1, int cs2)
        {
            if (cs1 < 0 || cs1 >= csucsokSzama ||
                cs2 < 0 || cs2 >= csucsokSzama)
            {
                throw new ArgumentOutOfRangeException("Hibas csucs index");
            }

            // Ha már szerepel az él, akkor nem kell felvenni
            foreach (var el in elek)
            {
                if (el.Csucs1 == cs1 && el.Csucs2 == cs2)
                {
                    return;
                }
            }

            elek.Add(new El(cs1, cs2));
            elek.Add(new El(cs2, cs1));
        }

        public void Torles(int cs1, int cs2)
        {
            if (cs1 < 0 || cs1 >= csucsokSzama ||
                cs2 < 0 || cs2 >= csucsokSzama)
            {
                throw new ArgumentOutOfRangeException("Hibas csucs index");
            }

            
            int i = 0;
            int ind1 = -1;
            int ind2 = -1;
            foreach (var el in elek)
            {
                if (el.Csucs1 == cs1 && el.Csucs2 == cs2)
                {
                    ind1 = i;
                }
                else if (el.Csucs2 == cs1 && el.Csucs1 == cs2)
                {
                    ind2 = i;
                }
                i++;
            }

            if (ind2 != -1)
            {
                elek.RemoveAt(ind2);
            }
            if (ind1 != -1)
            {
                elek.RemoveAt(ind1);
            }
   
        }

        public void SzelessegiBejar(int kezdopont)
        {
            List<int> bejart = new List<int>();

            Queue<int> kovetkezok = new Queue<int>();
            kovetkezok.Enqueue(kezdopont);
            bejart.Add(kezdopont);

            while (kovetkezok.Count>0)
            {
                int k = kovetkezok.Dequeue();

                Console.WriteLine("A csúcs: " + k);

                foreach (var item in elek)
                {
                    if (item.Csucs1 == k && !(bejart.Contains(item.Csucs2)))
                    {
                        kovetkezok.Enqueue(item.Csucs2);
                        bejart.Add(item.Csucs2);
                    }
                }

            }
        }

        public void MelysegiBejar(int kezdopont) 
        {
            List<int> bejart = new List<int>();

            Stack<int> kovetkezok = new Stack<int>();
            kovetkezok.Push(kezdopont);
            bejart.Add(kezdopont);

            while (kovetkezok.Count>0)
            {
                int k = kovetkezok.Pop();

                Console.WriteLine("A csúcs: " + k);

                foreach (var item in elek)
                {
                    if (item.Csucs1 == k && !(bejart.Contains(item.Csucs2)))
                    {
                        kovetkezok.Push(item.Csucs2);
                        bejart.Add(item.Csucs2);
                    }
                }
            }
        }

        public bool Osszefuggo()
        {
            List<int> bejart = new List<int>();

            Queue<int> kovetkezok = new Queue<int>();
            kovetkezok.Enqueue(0);
            bejart.Add(0);

            while (kovetkezok.Count > 0)
            {
                int k = kovetkezok.Dequeue();

                foreach (var item in elek)
                {
                    if (item.Csucs1 == k && !(bejart.Contains(item.Csucs2)))
                    {
                        kovetkezok.Enqueue(item.Csucs2);
                        bejart.Add(item.Csucs2);
                    }
                }
            }
            if (bejart.Count == this.csucsokSzama)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Graf Feszitofa() 
        {
            Graf fa = new Graf(this.csucsokSzama);

            List<int> bejart = new List<int>();
            Queue<int> kovetkezok = new Queue<int>();

            kovetkezok.Enqueue(0);
            bejart.Add(0);

            while (kovetkezok.Count>0)
            {
                int aktualisCucs = kovetkezok.Dequeue();

                foreach (var item in elek)
                {
                    if (item.Csucs1 == aktualisCucs &&!(bejart.Contains(item.Csucs2)))
                    {
                        bejart.Add(item.Csucs2);
                        kovetkezok.Enqueue(item.Csucs2);
                        fa.Hozzaad(item.Csucs1, item.Csucs2);
                    }
                }
            }
            return fa;
        }

        public override string ToString()
        {
            string str = "Csucsok:\n";
            foreach (var cs in csucsok)
            {
                str += cs + "\n";
            }
            str += "Elek:\n";
            foreach (var el in elek)
            {
                str += el + "\n";
            }
            return str;
        }
    }
}