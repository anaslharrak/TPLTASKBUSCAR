using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TPLTASKBUSCAR
{
    public partial class Form1 : Form
    {
        string hola = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (!chkCountry.Checked && !chkEmail.Checked && !chkFirst.Checked && !chkid.Checked && !chkIP.Checked && !chkLast.Checked)
            {
                txtResult.Text = "ERROR: Selecciona un campo mínimo";
            }
            else {
                List<Task<string>> tasks = new List<Task<string>>();
                int contador = 0;
                bool encontrado = false;

                if (chkCountry.Checked)
                {
                    tasks.Add( new Task<string>(() => trolo.buscar(txtbuscar.Text, 4)));
                    tasks[tasks.Count-1].Start();
                    contador++;
                }
                if (chkEmail.Checked) {
                    tasks.Add( new Task<string>(() => trolo.buscar(txtbuscar.Text, 3)));
                    tasks[tasks.Count - 1].Start();
                    contador++;
                }
                if (chkFirst.Checked)
                {
                    tasks.Add( new Task<string>(() => trolo.buscar(txtbuscar.Text, 1)));
                    tasks[tasks.Count - 1].Start();
                    contador++;
                }
                if (chkid.Checked)
                {
                    tasks.Add( new Task<string>(() => trolo.buscar(txtbuscar.Text, 0)));
                    tasks[tasks.Count - 1].Start();
                    contador++;
                }
                if (chkIP.Checked)
                {
                   tasks.Add( new Task<string>(() => trolo.buscar(txtbuscar.Text, 5)));
                    tasks[tasks.Count - 1].Start();
                    contador++;
                }
                if (chkLast.Checked)
                {
                    tasks.Add( new Task<string>(() => trolo.buscar(txtbuscar.Text, 2)));
                    tasks[tasks.Count - 1].Start();
                    contador++;
                }
                do
                {
                    Task.WaitAll(tasks.ToArray());
                    encontrado = check(tasks[contador - 1].Result);
                    contador--;
                } while (contador > 0 && !encontrado);

                if (encontrado)
                {
                    txtResult.Text = hola;
                }
                else {
                    txtResult.Text = "No Encontrado!";
                }
            }

        }
        private bool check(string check) {
            if (!check.Equals(""))
            {
                hola = check;
                return true;
            }
            else {

                return false;
            }
        }
    }
}
