using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using exercicisEnricEx9.Models;

namespace exercicisEnricEx9
{
    class Program
    {
       
      public static List<Article> Articles = new List<Article>();
       public static StreamReader rutaFinal;
        public static Boolean changeFunction = false;

        static void readFile(StreamReader ruta)
        {
            //poner data en la List de Articles si el file ya está creado(copiamos la datat del file la
            //ponemos en la list y trabajamos con la data de la List
            string line;
       
            try
            {
                line = ruta.ReadLine();
                string[] splitline;
               if(line!= null) { 
                   if(changeFunction == false) { 
                        while (line != null)
                         {
                 
                            splitline = line.Split('|');
                            if(Articles.Contains( new Article(int.Parse(splitline[0]), splitline[1], float.Parse(splitline[2]))))
                            {
                            line = ruta.ReadLine();
                            }
                             else
                            {
                            Articles.Add(new Article(int.Parse(splitline[0]), splitline[1], float.Parse(splitline[2])));

                            line = ruta.ReadLine();
                            }
                   
                        }
                        printList();
                        
                    }
                    else {
                        printList();
                    } 
                }
            }
            catch (Exception e) { Console.WriteLine("Exception: " + e.Message); }
            changeFunction = true;
        }
        public static void crearArxiu()
        {

            //Articles.Add(new Article(1, "CocaCola", 1.95f));
            //Articles.Add(new Article(2, "VichyCatala", 2.21f));
            //Articles.Add(new Article(3, "Nestea", 2.30f));
            //Articles.Add(new Article(4, "Sprite", 2.15f));
            //Articles.Add(new Article(5, "Soda", 2.45f));
            //Articles.Add(new Article(6, "AiguaMineral", 1.50f));
            //Articles.Add(new Article(7, "FantaTaronja", 2.50f));

            //List<string> newlines = new List<string>();
            ////string[] splitLine;
            ////string articuloparseado;
            //for (int i = 0; i < Articles.Count; i++)
            //{

            //    newlines.Add(Articles[i].escribirData());

            //    //splitLine = articuloparseado.Split('|');
            //}
            //try
            //{ 
            //System.IO.File.WriteAllLines(@"C:\DATOS\ANDREU\articles.txt", newlines);
            //}
            //catch { Console.WriteLine("No s'ha pogut crear l'arxiu, potser la ubicació és incorrecte.");  }

            try
            {
                System.IO.File.WriteAllText(@"C:\DATOS\ANDREU\articles.txt", "");
            }
            catch
            {
                Console.WriteLine("No s'ha pogut crear l'arxiu, potser la ubicació és incorrecte.");
            }
        }
       
        public static void editarArticle(int codiArticle)
        {
            IEnumerable<Article> querycodi =
                from art in Articles
                where art.Codi == codiArticle
                select art;

            Console.WriteLine("Has seleccionado editar el articulo: ");
           
            try { 
            var articulocoincidente = querycodi.ToList()[0];
         
            string coincidente = articulocoincidente.escribirData();
            Console.WriteLine(coincidente);
            Console.WriteLine("\nQué quiere editar? \nPara editar el nombre pulse 1. Y para editar" +
                " el precio pulse 2:");
            int option = int.Parse(Console.ReadLine());
            switch (option)
            {
                case 1:
                    string nuevoNombre;
                        int caso;
                    Console.WriteLine("Introduzca el nuevo Nombre:");
                    nuevoNombre = Console.ReadLine();
                        Console.WriteLine("¿Estas seguro que quiere editar el artículo? Pulse 1 para Editar \n Pulse 0 para Cancelar");
                        try { caso = int.Parse(Console.ReadLine()); } catch { caso = default; }
                        switch (caso)
                        {
                            case 1:
                                articulocoincidente.modificarDescripcio(nuevoNombre);
                                Console.WriteLine("¡Editado con éxito!");
                                break;
                            case 0:
                                Console.WriteLine("La edición ha sido cancelada.");
                                break;
                            default:
                                Console.WriteLine("No ha introducido ninguna opción válida.");
                                break;
                        }
                    
                    break;
                case 2:
                    float nuevoPrecio;
                    Console.WriteLine("Introduzca el nuevo Precio:");
                    nuevoPrecio = float.Parse(Console.ReadLine());
                    articulocoincidente.modificarPreu(nuevoPrecio);
                    break;
                default:
                    Console.WriteLine("Introdueixi una opció vàlida siusplau.");
                    break;
            }
            Console.WriteLine("Premi qualsevol tecla per tonar al menú principal");
            }
            catch { Console.WriteLine("No s'ha trobat ningún article amb aquest codi."); }
        }
        static void crearArticleNou(int codi, string descripcio, float preu)
        {

          if(Articles.Exists(x => x.Codi == codi))
            {
                Console.WriteLine("El codi de l'article que has introduït ja existeix i no es poden repetir.");
                Console.WriteLine("\nL'article NO s'ha creat.");
            }
            else
            {
                Articles.Add(new Article(codi, descripcio, preu));
                Console.WriteLine("\nL'article s'ha creat correctament.");
            }


         //for (int i=0; i<Articles.Count; i++)
         //   {
         //       if (Articles.Contains(Articles[i]))
         //       {
         //           Articles.Add(new Article(codi, descripcio, preu));
         //       }
         //       else if(Articles[i].Codi == codi) { Console.WriteLine("El codi que acabes d'introduir ja existeix tonra ho a intentar amb un codi diferent."); }
         //   }

        }
        static void esborrarArticle(int codi)
        {
            IEnumerable<Article> querycodi =
                from art in Articles
                where art.Codi == codi
                select art;
            try { 
            Console.WriteLine("L'article que desitja esborrar es: "+querycodi.ToList()[0].escribirData());
            Console.WriteLine("Està segur que vol esborrar aquest article?");
            Console.WriteLine("Si es que sí premi el número 1, en cas contrari premi el 0.");
            int opcion = int.Parse(Console.ReadLine());
            switch (opcion)
            {
                case 1:
                    int indexArticle = Articles.IndexOf(querycodi.ToList()[0]);
                    Articles.RemoveAt(indexArticle);
                    Console.WriteLine("Article esborrat correctament.");
                    break;
                case 0:
                    Console.WriteLine("S'ha cancel·lat la operació.");
                    break;
                default:
                    Console.WriteLine("Introdueixi una opció vàlida siusplau.");
                    break;
            }
            }
            catch { Console.WriteLine("No s'ha trobat ningún article amb aquest codi."); }
        }
        static void desarElsCanvisAlFitxer()
        {
            //StreamWriter ruta5 = new StreamWriter("C:\\DATOS\\ANDREU\\articles.txt");
           
            List<string> newlines = new List<string>();

            for (int i = 0; i < Articles.Count; i++)
            {
                newlines.Add(Articles[i].escribirData());
            }
            try
            {
                System.IO.File.WriteAllLines(@"C:\DATOS\ANDREU\articles.txt",newlines);
                Console.WriteLine("S'han desat correctament els canvis.");
            }
            catch { Console.WriteLine("No s'ha pogut crear l'arxiu, potser la ubicació és incorrecte."); }

            //ruta5.Close();
        }
        static void esborrarTotesLesDades()
        {

            Articles.Clear();
            try
            { 
            System.IO.File.WriteAllText(@"C:\DATOS\ANDREU\articles.txt", "");
                Console.WriteLine("S'han esborrat correctament les dades.");
            }
            catch { Console.WriteLine("No s'ha pogut realitzar la operació."); }
            
        } 

        static void llistatAlfabetic()
        {
            var sortedList = Articles.OrderBy(x => x.Descripcio).ToList();
            
           
            //query para ordenar?
            for (int i=0; i<sortedList.Count; i++)
            {

                Console.WriteLine(sortedList[i].escribirData());
            }

            //Intentando dejar la ListArticles ordenada ya
            Articles = sortedList;      
        }
        static void printList()
        {
            for (int i = 0; i < Articles.Count; i++)
            {
                Console.WriteLine(Articles[i].escribirData());
            }
        }
        static void Main(string[] args)
        {
            //Aqui si el file està creat simplement el llegeixo i poso les dades dins la List de Articles
            //Sinó el creo  posant primer articles dins la List de Articles, i després creo un file on hi
            //poso la data de la LISTArtices
            //string rutafile = "C:\\DATOS\\ANDREU\\articles.txt";

            //Menú switch case per les opcions
            Boolean menu = true;
            do
            {
                Console.WriteLine("Menú Exercici 9");
                Console.WriteLine("1.Carregar o crear fitxer:");
                Console.WriteLine("2.Editar Article:");
                Console.WriteLine("3.Crear Article:");
                Console.WriteLine("4.Esborrar article:");
                Console.WriteLine("5.Grabar canvis realitzats al fitxer:");
                Console.WriteLine("6.Llistat del articles ordenat alfabèticament per (descripció):");
                Console.WriteLine("7.Esborrar totes les dades.");
                Console.WriteLine("8.Sortir.");
                int option;
                try
                {
                    option = int.Parse(Console.ReadLine());

                }
                catch { option = default; }


                switch (option)
                {
                    case 1:
                        Console.WriteLine("Imprimint la data del doc o Comprovant si el fitxer existeix...\n En cas de que no " +
                            "existeixi es crearà en cas de que ja existeixi simplement es carregarà el fitxer:\n");
                        try
                        {
                            StreamReader ruta = new StreamReader("C:\\DATOS\\ANDREU\\articles.txt");
                            readFile(ruta);
                            rutaFinal = ruta;
                            Console.WriteLine("\nFitxer CARREGAT amb èxit.");
                        }
                        catch
                        {
                            crearArxiu();
                            StreamReader ruta = new StreamReader("C:\\DATOS\\ANDREU\\articles.txt");
                            rutaFinal = ruta;
                            Console.WriteLine("\nFitxer CREAT amb èxit.");
                        }
                        rutaFinal.Close();
                     
                        break;
                    case 2:
                        Console.WriteLine("Edició d'article...");
                        Console.WriteLine("Introdueixi el codi del article que desitja editar:");
                        try { 
                        int codiArticle = int.Parse(Console.ReadLine());
                        editarArticle(codiArticle);
                        }
                        catch { Console.WriteLine("Has d'assegurar-te d'introduir un número enter.\n"); }
                        break;
                    case 3:
                        Console.WriteLine("Creant un nou article...");
                        Console.WriteLine("Introdueixi un CODI per el nou Article");
                        try { 
                        int codi = int.Parse(Console.ReadLine());
                        Console.WriteLine("Introdueixi un NOM per el nou Article");
                        string nom = Console.ReadLine();
                        Console.WriteLine("Introdueixi un PREU per el nou Article");
                        float preu = float.Parse(Console.ReadLine());
                        crearArticleNou(codi, nom, preu);
                        }
                        catch { Console.WriteLine("Has d'assegurar-te d'introduir un número enter per al Codi, lletres per la Despcició de l'article,\n i un número decimal pel Preu.\n" +
                            "Exemple:  1  Soda  2,12  \n"); }
                        break;
                    case 4:
                        Console.WriteLine("Esborrant un article existent...");
                        Console.WriteLine("Introdueixi el codi de l'article que desitja esborrar:");
                        try { 
                        int codiArticleEsborrar = int.Parse(Console.ReadLine());
                        esborrarArticle(codiArticleEsborrar);
                        }
                        catch { Console.WriteLine("Has d'assegurar-te d'introduir un número enter.\n"); }
                        break;
                    case 5:
                        Console.WriteLine("S'estan grabant els nous canvis al fitxer...");
                        desarElsCanvisAlFitxer();
                        break;
                    case 6:
                        Console.WriteLine("Mostrant llistat dels articles per ordre alfabètic de la descripció...");
                        llistatAlfabetic();
                        break;
                    case 7:
                        Console.WriteLine("Preparant per esborrar totes les dades...");
                        esborrarTotesLesDades();
                        break;
                    case 8:
                        Console.WriteLine("Sortint...");
                        menu = false;
                        break;
                    default:
                        Console.WriteLine("Introdueixi una opció vàlida siusplau.");
                        break;
                }
                Console.ReadKey();
            } while (menu == true);
        }
    }
}