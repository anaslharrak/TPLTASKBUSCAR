using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TPLTASKBUSCAR
{
    public partial class Form1 : Form
    {
        string hola = "";
        string countEdusResult = "";
        string countInfosResult = "";

        public Form1()
        {
            InitializeComponent();

        }

        private async void btnBuscar_Click(object sender, EventArgs e)
        {
            if (!chkCountry.Checked && !chkEmail.Checked && !chkFirst.Checked && !chkid.Checked && !chkIP.Checked &&
                !chkLast.Checked)
            {
                txtResult.Text = "ERROR: Selecciona un campo mínimo";
            }
            else
            {
                txtResult.Text = "";
                List<Task<string>> tasks = new List<Task<string>>();
                int contador = 0;
                bool encontrado = false;

                if (chkCountry.Checked)
                {
                    tasks.Add(new Task<string>(() => trolo.buscar(txtbuscar.Text, 4)));
                    tasks[tasks.Count - 1].Start();
                    contador++;
                }

                if (chkEmail.Checked)
                {
                    tasks.Add(new Task<string>(() => trolo.buscar(txtbuscar.Text, 3)));
                    tasks[tasks.Count - 1].Start();
                    contador++;
                }

                if (chkFirst.Checked)
                {
                    tasks.Add(new Task<string>(() => trolo.buscar(txtbuscar.Text, 1)));
                    tasks[tasks.Count - 1].Start();
                    contador++;
                }

                if (chkid.Checked)
                {
                    tasks.Add(new Task<string>(() => trolo.buscar(txtbuscar.Text, 0)));
                    tasks[tasks.Count - 1].Start();
                    contador++;
                }

                if (chkIP.Checked)
                {
                    tasks.Add(new Task<string>(() => trolo.buscar(txtbuscar.Text, 5)));
                    tasks[tasks.Count - 1].Start();
                    contador++;
                }

                if (chkLast.Checked)
                {
                    tasks.Add(new Task<string>(() => trolo.buscar(txtbuscar.Text, 2)));
                    tasks[tasks.Count - 1].Start();
                    contador++;
                }

                do
                {
                    await Task.WhenAll(tasks.ToArray());
                    
                    encontrado = check(tasks[contador - 1].Result);
                    contador--;
                } while (contador > 0 && !encontrado);

                if (encontrado)
                {
                    await Task.WhenAll(tasks);

                    for (int i = 0; i <= contador; i++)
                    {
                        txtResult.Text += takeInfo(tasks[i].Result) + Environment.NewLine;
                    }
                    
                    await counter(txtResult.Text);
                    txtResult.Text = "";
                    
                    txtResult.Text += countEdusResult;
                    txtResult.Text += countInfosResult;
                    txtResult.Text += Environment.NewLine;
                    
                    for (int i = 0; i <= contador; i++)
                    {
                        txtResult.Text += takeInfo(tasks[i].Result) + Environment.NewLine;
                    }
                }
                else
                {
                    txtResult.Text = "No Encontrado!";
                }
            }
        }

        private async Task counter(string txtResult)
        {
            await Task.Run(() => countEdus(txtResult));
            await Task.Run(() => countInfos(txtResult));
        }


        private bool check(string check)
        {
            return !check.Equals("");
        }

        private void countEdus(string txtResult)
        {
            string patron = Regex.Escape(".edu");

            int cantidadRepeticiones = Regex.Matches(txtResult, patron).Count;

            countEdusResult = "Counter .edu:" + cantidadRepeticiones + Environment.NewLine;
        }

        private void countInfos(string txtResult)
        {
            string patron = Regex.Escape(".info");

            int cantidadRepeticiones = Regex.Matches(txtResult, patron).Count;

            countInfosResult = "Counter .info:" + cantidadRepeticiones + Environment.NewLine;
        }
        
        private String takeInfo(string info)
        {
            return info;
        }
    }
}