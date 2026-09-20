using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace salud
{
        public partial class Form1 : Form
        {
            public Form1()
            {
                InitializeComponent();
                AsignarValidacionesTeclas();
            }

            private void AsignarValidacionesTeclas()
            {
                // Solo letras en el nombre
                textBox1.KeyPress += TextBox1_KeyPress;

                // Solo números enteros en edad, presión y saturación
                textBox2.KeyPress += SoloNumeros_KeyPress;
                textBox5.KeyPress += SoloNumeros_KeyPress;
                textBox6.KeyPress += SoloNumeros_KeyPress;

                // Solo números/decimales en temperatura
                textBox4.KeyPress += SoloNumerosYDecimal_KeyPress;
            }

            private void Form1_Load(object sender, EventArgs e)
            {
                if (comboBox1.Items.Count == 0)
                {
                    comboBox1.Items.Add("Estable");
                    comboBox1.Items.Add("Urgente");
                    comboBox1.Items.Add("Grave");
                    comboBox1.SelectedIndex = 0;
                }
            }

            // --- VALIDACIONES DE TECLAS EN TIEMPO REAL ---

            // Permite solo letras, espacios y borrar
            private void TextBox1_KeyPress(object sender, KeyPressEventArgs e)
            {
                if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
                {
                    e.Handled = true;
                }
            }

            // Permite solo números y borrar
            private void SoloNumeros_KeyPress(object sender, KeyPressEventArgs e)
            {
                if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                {
                    e.Handled = true;
                }
            }

            // Permite números y decimales para la temperatura
            private void SoloNumerosYDecimal_KeyPress(object sender, KeyPressEventArgs e)
            {
                TextBox txt = sender as TextBox;
                if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != ',')
                {
                    e.Handled = true;
                }

                if ((e.KeyChar == '.' || e.KeyChar == ',') && (txt.Text.Contains(".") || txt.Text.Contains(",")))
                {
                    e.Handled = true;
                }
            }

            // --- LÓGICA DE EVALUACIÓN ---
            private void ProcesarTriaje()
            {
                if (!int.TryParse(textBox2.Text, out int edad) ||
                    !double.TryParse(textBox4.Text, out double temperatura) ||
                    !int.TryParse(textBox6.Text, out int saturacion))
                {
                    MessageBox.Show("Por favor, completa los campos numéricos correctamente (Edad, Temperatura y Saturación).",
                                    "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Límite máximo de 45°C en la temperatura
                if (temperatura > 45.0)
                {
                    MessageBox.Show("La temperatura no puede sobrepasar los 45 °C.",
                                    "Límite excedido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox4.Focus();
                    return;
                }

                string estadoGeneral = comboBox1.SelectedItem != null ? comboBox1.SelectedItem.ToString() : "Estable";

                if (estadoGeneral == "Grave" || temperatura >= 39.5 || saturacion < 90)
                {
                    button3.Text = "PRIORIDAD I / ROJO (Emergencia)";
                    button3.BackColor = Color.Red;
                    button3.ForeColor = Color.White;
                }
                else if (estadoGeneral == "Urgente" || temperatura >= 38.0 || saturacion <= 94 || edad < 1 || edad >= 60)
                {
                    button3.Text = "PRIORIDAD II / AMARILLO (Urgencia)";
                    button3.BackColor = Color.Yellow;
                    button3.ForeColor = Color.Black;
                }
                else
                {
                    button3.Text = "PRIORIDAD III / VERDE (No Urgente)";
                    button3.BackColor = Color.Green;
                    button3.ForeColor = Color.White;
                }
            }

            // --- LÓGICA DE LIMPIEZA ---
            private void EjecutarLimpieza()
            {
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                textBox5.Clear();
                textBox6.Clear();

                if (comboBox1.Items.Count > 0)
                {
                    comboBox1.SelectedIndex = 0;
                }

                button3.Text = "Resultado";
                button3.BackColor = SystemColors.Control;
                button3.ForeColor = SystemColors.ControlText;

                textBox1.Focus();
            }

            // --- EVENTOS DE EVALUAR ---
            private void button1_Click(object sender, EventArgs e) => ProcesarTriaje();
            private void button1_Click_1(object sender, EventArgs e) => ProcesarTriaje();
            private void button3_Click_1(object sender, EventArgs e) => ProcesarTriaje();
            private void label8_Click(object sender, EventArgs e) => ProcesarTriaje();

            // --- EVENTOS DE LIMPIAR (Incluyendo el error del diseñador button2_Click_2) ---
            private void button2_Click(object sender, EventArgs e) => EjecutarLimpieza();
            private void button2_Click_1(object sender, EventArgs e) => EjecutarLimpieza();
            private void button2_Click_2(object sender, EventArgs e) => EjecutarLimpieza();
            private void button4_Click(object sender, EventArgs e) => EjecutarLimpieza();
            private void button4_Click_1(object sender, EventArgs e) => EjecutarLimpieza();

            // Métodos auxiliares
            private void button3_Click(object sender, EventArgs e) { }
            private void Form1_Load_1(object sender, EventArgs e) => Form1_Load(sender, e);
        }
    }