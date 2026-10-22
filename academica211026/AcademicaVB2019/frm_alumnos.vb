
Imports System
Imports System.Data
Imports System.Drawing
Imports System.Windows.Forms

Public Class frm_alumnos
    Inherits Form

    Private ReadOnly db As New Conexion_base_datos()
    Private campos As TextBox()
    Private idAlumnoSeleccionado As String = ""

    Friend WithEvents Label1 As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label

    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents TextBox3 As TextBox
    Friend WithEvents TextBox4 As TextBox
    Friend WithEvents TextBox5 As TextBox
    Friend WithEvents TextBox6 As TextBox

    Friend WithEvents Button2 As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents Button4 As Button
    Friend WithEvents Button5 As Button

    Friend WithEvents Label8 As Label
    Friend WithEvents DataGridView1 As DataGridView

    Friend WithEvents codigo As DataGridViewTextBoxColumn
    Friend WithEvents nombre As DataGridViewTextBoxColumn
    Friend WithEvents apellido As DataGridViewTextBoxColumn
    Friend WithEvents fechanacimiento As DataGridViewTextBoxColumn
    Friend WithEvents telefono As DataGridViewTextBoxColumn
    Friend WithEvents direccion As DataGridViewTextBoxColumn

    Public Sub New()
        MyBase.New()

        ' Cargar el diseño original
        InitializeComponent()

        Me.StartPosition = FormStartPosition.CenterScreen
        Me.Text = "Gestión de alumnos"

        ' Orden requerido por la base de datos:
        ' ID, Código, Nombre, Dirección, Teléfono, Email
        '
        ' Los seis campos visibles conservan las
        ' etiquetas del diseño original.
        campos = New TextBox() {
            TextBox1,
            TextBox2,
            TextBox3,
            TextBox4,
            TextBox5,
            TextBox6
        }

        DataGridView1.AutoGenerateColumns = True
        DataGridView1.Columns.Clear()
        DataGridView1.ReadOnly = True
        DataGridView1.AllowUserToAddRows = False
        DataGridView1.AllowUserToDeleteRows = False
        DataGridView1.SelectionMode =
            DataGridViewSelectionMode.FullRowSelect
        DataGridView1.MultiSelect = False

        AddHandler Me.Shown, AddressOf MostrarFormulario
        AddHandler DataGridView1.SelectionChanged,
            AddressOf SeleccionarAlumno
    End Sub

    Private Sub MostrarFormulario(
        sender As Object,
        e As EventArgs)

        Cargar()
    End Sub

    Private Sub Cargar()
        Try
            Dim ds As DataSet = db.obtenerDatos()

            DataGridView1.DataSource =
                ds.Tables("alumnos")

        Catch ex As Exception
            MessageBox.Show(
                ex.Message,
                "Error al cargar alumnos",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub SeleccionarAlumno(
        sender As Object,
        e As EventArgs)

        If DataGridView1.CurrentRow Is Nothing Then Return
        If DataGridView1.CurrentRow.IsNewRow Then Return

        Dim fila As DataGridViewRow =
            DataGridView1.CurrentRow

        If fila.Cells.Count < 6 Then Return

        idAlumnoSeleccionado =
            Convert.ToString(fila.Cells(0).Value)

        ' Mantener los valores visibles en el
        ' mismo orden de la tabla.
        For i As Integer = 0 To 5
            campos(i).Text =
                Convert.ToString(fila.Cells(i).Value)
        Next
    End Sub

    Private Sub Limpiar()
        idAlumnoSeleccionado = ""

        For Each t As TextBox In campos
            t.Clear()
        Next

        DataGridView1.ClearSelection()
        TextBox1.Focus()
    End Sub

    Private Sub Ejecutar(accion As String)

        If accion <> "nuevo" AndAlso
           String.IsNullOrWhiteSpace(
               idAlumnoSeleccionado) Then

            MessageBox.Show(
                "Selecciona primero un alumno.")
            Return
        End If

        If accion <> "eliminar" Then
            For i As Integer = 0 To campos.Length - 1
                If String.IsNullOrWhiteSpace(
                    campos(i).Text) Then

                    MessageBox.Show(
                        "Completa todos los campos.")
                    campos(i).Focus()
                    Return
                End If
            Next
        End If

        If accion = "eliminar" Then
            Dim respuesta As DialogResult =
                MessageBox.Show(
                    "¿Deseas eliminar este alumno?",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question)

            If respuesta <> DialogResult.Yes Then Return
        End If

        Try
            Dim datos As String() =
                Array.ConvertAll(
                    campos,
                    Function(t As TextBox) t.Text.Trim())

            db.administrarDatosAlumnos(datos, accion)

            MessageBox.Show(
                "Operación realizada correctamente.",
                "Sistema Académico",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information)

            Cargar()
            Limpiar()

        Catch ex As Exception
            MessageBox.Show(
                ex.Message,
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error)
        End Try
    End Sub

    ' REGRESAR
    Private Sub Button1_Click(
        sender As Object,
        e As EventArgs) Handles Button1.Click

        Me.Close()
    End Sub

    ' NUEVO
    Private Sub Button2_Click(
        sender As Object,
        e As EventArgs) Handles Button2.Click

        Limpiar()
    End Sub

    ' GUARDAR
    Private Sub Button3_Click(
        sender As Object,
        e As EventArgs) Handles Button3.Click

        Ejecutar("nuevo")
    End Sub

    ' MODIFICAR
    Private Sub Button4_Click(
        sender As Object,
        e As EventArgs) Handles Button4.Click

        Ejecutar("modificar")
    End Sub

    ' ELIMINAR
    Private Sub Button5_Click(
        sender As Object,
        e As EventArgs) Handles Button5.Click

        Ejecutar("eliminar")
    End Sub

    Private Sub InitializeComponent()

        Me.Label1 = New Label()
        Me.Button1 = New Button()
        Me.Label2 = New Label()
        Me.Label3 = New Label()
        Me.Label4 = New Label()
        Me.Label5 = New Label()
        Me.Label6 = New Label()
        Me.Label7 = New Label()

        Me.TextBox1 = New TextBox()
        Me.TextBox2 = New TextBox()
        Me.TextBox3 = New TextBox()
        Me.TextBox4 = New TextBox()
        Me.TextBox5 = New TextBox()
        Me.TextBox6 = New TextBox()

        Me.Button2 = New Button()
        Me.Button3 = New Button()
        Me.Button4 = New Button()
        Me.Button5 = New Button()

        Me.Label8 = New Label()
        Me.DataGridView1 = New DataGridView()

        Me.codigo = New DataGridViewTextBoxColumn()
        Me.nombre = New DataGridViewTextBoxColumn()
        Me.apellido = New DataGridViewTextBoxColumn()
        Me.fechanacimiento = New DataGridViewTextBoxColumn()
        Me.telefono = New DataGridViewTextBoxColumn()
        Me.direccion = New DataGridViewTextBoxColumn()

        CType(Me.DataGridView1,
              System.ComponentModel.ISupportInitialize).BeginInit()

        Me.SuspendLayout()

        ' TÍTULO
        Me.Label1.AutoSize = True
        Me.Label1.Font = New Font(
            "Microsoft Sans Serif", 12.0!,
            FontStyle.Regular)

        Me.Label1.Location = New Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Text = "GESTION DE ALUMNOS"

        ' REGRESAR
        Me.Button1.Location = New Point(541, 6)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New Size(89, 37)
        Me.Button1.Text = "Regresar"
        Me.Button1.UseVisualStyleBackColor = True

        ' CÓDIGO
        Me.Label2.AutoSize = True
        Me.Label2.Location = New Point(14, 82)
        Me.Label2.Name = "Label2"
        Me.Label2.Text = "Codigo"

        Me.TextBox1.Location = New Point(17, 102)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New Size(100, 22)

        ' NOMBRE
        Me.Label3.AutoSize = True
        Me.Label3.Location = New Point(14, 183)
        Me.Label3.Name = "Label3"
        Me.Label3.Text = "Nombre"

        Me.TextBox2.Location = New Point(17, 214)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New Size(100, 22)

        ' APELLIDO
        Me.Label4.AutoSize = True
        Me.Label4.Location = New Point(169, 82)
        Me.Label4.Name = "Label4"
        Me.Label4.Text = "Apellido"

        Me.TextBox3.Location = New Point(172, 102)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New Size(100, 22)

        ' FECHA DE NACIMIENTO
        Me.Label5.AutoSize = True
        Me.Label5.Location = New Point(169, 183)
        Me.Label5.Name = "Label5"
        Me.Label5.Text = "Fecha de nacimiento"

        Me.TextBox4.Location = New Point(172, 214)
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.Size = New Size(100, 22)

        ' TELÉFONO
        Me.Label6.AutoSize = True
        Me.Label6.Location = New Point(420, 82)
        Me.Label6.Name = "Label6"
        Me.Label6.Text = "Telefono"

        Me.TextBox5.Location = New Point(423, 102)
        Me.TextBox5.Name = "TextBox5"
        Me.TextBox5.Size = New Size(100, 22)

        ' DIRECCIÓN
        Me.Label7.AutoSize = True
        Me.Label7.Location = New Point(420, 183)
        Me.Label7.Name = "Label7"
        Me.Label7.Text = "Direccion"

        Me.TextBox6.Location = New Point(428, 209)
        Me.TextBox6.Name = "TextBox6"
        Me.TextBox6.Size = New Size(100, 22)

        ' NUEVO
        Me.Button2.Location = New Point(17, 274)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New Size(75, 37)
        Me.Button2.Text = "Nuevo"
        Me.Button2.UseVisualStyleBackColor = True

        ' GUARDAR
        Me.Button3.Location = New Point(118, 274)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New Size(75, 37)
        Me.Button3.Text = "Guardar"
        Me.Button3.UseVisualStyleBackColor = True

        ' MODIFICAR
        Me.Button4.Location = New Point(215, 274)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New Size(75, 37)
        Me.Button4.Text = "Modificar"
        Me.Button4.UseVisualStyleBackColor = True

        ' ELIMINAR
        Me.Button5.Location = New Point(319, 274)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New Size(75, 37)
        Me.Button5.Text = "Eliminar"
        Me.Button5.UseVisualStyleBackColor = True

        ' LISTA DE ALUMNOS
        Me.Label8.AutoSize = True
        Me.Label8.Location = New Point(15, 336)
        Me.Label8.Name = "Label8"
        Me.Label8.Text = "Lista de alumnos"

        ' TABLA
        Me.DataGridView1.Location = New Point(11, 356)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New Size(748, 150)

        Me.DataGridView1.ColumnHeadersHeightSizeMode =
            DataGridViewColumnHeadersHeightSizeMode.AutoSize

        Me.DataGridView1.Columns.AddRange(
            New DataGridViewColumn() {
                Me.codigo,
                Me.nombre,
                Me.apellido,
                Me.fechanacimiento,
                Me.telefono,
                Me.direccion
            })

        Me.codigo.Name = "codigo"
        Me.codigo.HeaderText = "Codigo"
        Me.codigo.Width = 125

        Me.nombre.Name = "nombre"
        Me.nombre.HeaderText = "Nombre"
        Me.nombre.Width = 125

        Me.apellido.Name = "apellido"
        Me.apellido.HeaderText = "Apellido"
        Me.apellido.Width = 125

        Me.fechanacimiento.Name = "fechanacimiento"
        Me.fechanacimiento.HeaderText =
            "Fecha de nacimiento"
        Me.fechanacimiento.Width = 125

        Me.telefono.Name = "telefono"
        Me.telefono.HeaderText = "Telefono"
        Me.telefono.Width = 125

        Me.direccion.Name = "direccion"
        Me.direccion.HeaderText = "Direccion"
        Me.direccion.Width = 125

        ' FORMULARIO ORIGINAL
        Me.ClientSize = New Size(771, 578)
        Me.Name = "frm_alumnos"

        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Button1)

        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label7)

        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.TextBox2)
        Me.Controls.Add(Me.TextBox3)
        Me.Controls.Add(Me.TextBox4)
        Me.Controls.Add(Me.TextBox5)
        Me.Controls.Add(Me.TextBox6)

        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.Button5)

        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.DataGridView1)

        CType(Me.DataGridView1,
              System.ComponentModel.ISupportInitialize).EndInit()

        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

End Class
