using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Matriz con las 5 categorías 
        String[][] etiquetas = {
            // 0: Longitud (Base: Metro)
            new string[]{"Metros", "Cm", "Pulgadas", "Pies", "Varas", "Yardas", "Km", "Millas"},
            
            // 1: Monedas (Base: Dólar)
            new string[]{"Dolar", "Quetzal", "Lempira", "Cordobas", "Colon CR", "Euro", "Peso MX", "Yen"},

            // 2: Masa (Base: Kilogramo)
            new string[]{"Kilogramo", "Gramo", "Miligramo", "Libra", "Onza", "Tonelada", "Quintal", "Arroba"},

            // 3: Almacenamiento (Base: Byte)
            new string[]{"Byte", "Bit", "Kilobyte (KB)", "Megabyte (MB)", "Gigabyte (GB)", "Terabyte (TB)", "Kilobit (Kb)", "Megabit (Mb)"},

            // 4: Tiempo (Base: Segundo)
            new string[]{"Segundo", "Milisegundo", "Minuto", "Hora", "Día", "Semana", "Mes (30d)", "Año (365d)"}
        };

        // Matriz de factores de conversión exactos
        Double[][] valores = {
            // 0: Longitud
            new double[]{1, 100, 39.3701, 3.28084, 1.1963, 1.09361, 0.001, 0.000621371},

            // 1: Monedas
            new double[]{1, 7.63, 26.81, 36.80, 449.23, 0.92, 19.50, 155.0},

            // 2: Masa
            new double[]{1, 1000, 1000000, 2.20462, 35.274, 0.001, 0.0220462, 0.0881849},

            // 3: Almacenamiento
            new double[]{1, 8, 0.001, 0.000001, 0.000000001, 0.000000000001, 0.008, 0.000008},

            // 4: Tiempo
            new double[]{1, 1000, 0.0166666666666667, 0.000277777777777778, 0.0000115740740740741, 0.00000165343915343915, 0.000000385802469135802, 0.0000000317097919837646}
        };

        private void Form1_Load(object sender, EventArgs e)
        {
            // Carga las 5 categorías principales al abrir la aplicación
            cbo1.Items.Clear();
            cbo1.Items.Add("Longitud");
            cbo1.Items.Add("Monedas");
            cbo1.Items.Add("Masa");
            cbo1.Items.Add("Almacenamiento");
            cbo1.Items.Add("Tiempo");

            cbo1.SelectedIndex = 0; // Selecciona "Longitud" de inicio
        }

        // Evento que actualiza las opciones de comboBox2 y comboBox3
        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            cbo2.SelectedIndex = -1;
            cbo3.SelectedIndex = -1;
            cbo2.Text = "";
            cbo3.Text = ""; 
            cbo2.Items.Clear();
            cbo3.Items.Clear();

            int opcion = cbo1.SelectedIndex;

            if (opcion >= 0 && opcion < etiquetas.Length)
            {
                cbo2.Items.AddRange(etiquetas[opcion]);
                cbo3.Items.AddRange(etiquetas[opcion]);

                // Selecciona por defecto la primera opción de la lista
                cbo2.SelectedIndex = -1;
                cbo3.SelectedIndex = -1;
            }
        }

        // Método alternativo por si en tu diseñador no tiene el "_1" al final
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1_SelectedIndexChanged_1(sender, e);
        }

        // Evento del botón Calcular
        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                int de = cbo2.SelectedIndex;
                int a = cbo3.SelectedIndex;
                int opcion = cbo1.SelectedIndex;

                if (de != -1 && a != -1 && opcion != -1)
                {
                    double cantidad = Double.Parse(txt1.Text);
                    double respuesta = valores[opcion][a] / valores[opcion][de] * cantidad;

                    label5.Text = respuesta.ToString();
                }
                else
                {
                    MessageBox.Show("Selecciona las opciones para realizar la conversión.");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Ingresa una cantidad numérica válida.");
            }
        }

        // Método alternativo por si en tu diseñador no tiene el "_1" al final
        private void button1_Click(object sender, EventArgs e)
        {
            button1_Click_1(sender, e);
        }
    }
}