using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exercicisEnricEx9.Models
{
    class Article
    {
        public int Codi { get; set; }
        public string Descripcio { get; set; }
        public float Preu { get; set; }

        public Article(int c, string d, float p)
        {
            Codi = c;
            Descripcio = d;
            Preu = p;
        }
        public string escribirData()
        {
           return Codi + " | " + Descripcio + " | " + Preu;
        }
        public void modificarDescripcio(string newdescription)
        {
            Descripcio = newdescription;
        }
        public void modificarPreu(float newprice)
        {
            Preu = newprice;
        }
    }
}
