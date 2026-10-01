using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace academica
{
    public partial class Form1 : Form
    {
        private readonly BindingSource bsAlumnos = new BindingSource();

        public Form1()
        {
            InitializeComponent();

            // Conecta los eventos una sola vez.
            Load -= Form1_Load;
            Load += Form1_Load;

            btnagregar.Click -= btnagregar_Click;
            btnagregar.Click += btnagregar_Click;

            btnmodificar.Click -= btnmodificar_Click;
            btnmodificar.Click += btnmodificar_Click;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CargarAlumnos();

            bindingNavigator1.BindingSource = bsAlumnos;

            txtcodigo.DataBindings.Clear();
            txtnombre.DataBindings.Clear();
            txtdireccion.DataBindings.Clear();
            txttelefono.DataBindings.Clear();
            txtemail.DataBindings.Clear();

            txtcodigo.DataBindings.Add(
                "Text", bsAlumnos, "codigo", true,
                DataSourceUpdateMode.OnPropertyChanged);

            txtnombre.DataBindings.Add(
                "Text", bsAlumnos, "nombre", true,
                DataSourceUpdateMode.OnPropertyChanged);

            txtdireccion.DataBindings.Add(
                "Text", bsAlumnos, "direccion", true,
                DataSourceUpdateMode.OnPropertyChanged);

            txttelefono.DataBindings.Add(
                "Text", bsAlumnos, "telefono", true,
                DataSourceUpdateMode.OnPropertyChanged);

            txtemail.DataBindings.Add(
                "Text", bsAlumnos, "email", true,
                DataSourceUpdateMode.OnPropertyChanged);
        }

        private void CargarAlumnos()
        {
            DataTable tabla = new DataTable();

            using (SqlConnection conexionSql = conexion.ObtenerConexion())
            using (SqlDataAdapter adaptador = new SqlDataAdapter(
                "SELECT idAlumno, codigo, nombre, direccion, telefono, email " +
                "FROM dbo.alumnos ORDER BY idAlumno",
                conexionSql))
            {
                adaptador.Fill(tabla);
            }

            bsAlumnos.DataSource = tabla;
        }

        private void btnagregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtcodigo.Text) ||
                string.IsNullOrWhiteSpace(txtnombre.Text))
            {
                MessageBox.Show("Escribe el código y el nombre.");
                return;
            }

            using (SqlConnection conexionSql = conexion.ObtenerConexion())
            using (SqlCommand comando = new SqlCommand(
                "INSERT INTO dbo.alumnos " +
                "(codigo, nombre, direccion, telefono, email) " +
                "VALUES (@codigo, @nombre, @direccion, @telefono, @email)",
                conexionSql))
            {
                comando.Parameters.AddWithValue("@codigo", txtcodigo.Text.Trim());
                comando.Parameters.AddWithValue("@nombre", txtnombre.Text.Trim());
                comando.Parameters.AddWithValue("@direccion", ValorONull(txtdireccion.Text));
                comando.Parameters.AddWithValue("@telefono", ValorONull(txttelefono.Text));
                comando.Parameters.AddWithValue("@email", ValorONull(txtemail.Text));

                conexionSql.Open();
                comando.ExecuteNonQuery();
            }

            CargarAlumnos();
            bsAlumnos.MoveLast();
            MessageBox.Show("Registro agregado.");
        }

        private void btnmodificar_Click(object sender, EventArgs e)
        {
            DataRowView registro = bsAlumnos.Current as DataRowView;

            if (registro == null)
            {
                MessageBox.Show("No hay un registro seleccionado. Usa la barra de navegación.");
                return;
            }

            int idAlumno = Convert.ToInt32(registro["idAlumno"]);

            using (SqlConnection conexionSql = conexion.ObtenerConexion())
            using (SqlCommand comando = new SqlCommand(
                "UPDATE dbo.alumnos SET codigo = @codigo, nombre = @nombre, " +
                "direccion = @direccion, telefono = @telefono, email = @email " +
                "WHERE idAlumno = @idAlumno",
                conexionSql))
            {
                comando.Parameters.AddWithValue("@codigo", txtcodigo.Text.Trim());
                comando.Parameters.AddWithValue("@nombre", txtnombre.Text.Trim());
                comando.Parameters.AddWithValue("@direccion", ValorONull(txtdireccion.Text));
                comando.Parameters.AddWithValue("@telefono", ValorONull(txttelefono.Text));
                comando.Parameters.AddWithValue("@email", ValorONull(txtemail.Text));
                comando.Parameters.AddWithValue("@idAlumno", idAlumno);

                conexionSql.Open();
                comando.ExecuteNonQuery();
            }

            CargarAlumnos();
            MessageBox.Show("Registro modificado.");
        }

        private object ValorONull(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
                return DBNull.Value;

            return texto.Trim();
        }
    }
}