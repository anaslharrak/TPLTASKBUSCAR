using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TPLTASKBUSCAR
{
    class trolo
    {
        public static String buscar(string texto, int tipo) {
            var reader = new StreamReader(File.OpenRead(@"data\arxiuUsers.csv"));
            string final = "";
            while (!reader.EndOfStream && final.Equals(""))
            {
                var line = reader.ReadLine();
                var values = line.Split(',');
                if (texto.Equals(values[tipo])) {
                    final = line;
                }
            }
            return final;
        }
    }
}
