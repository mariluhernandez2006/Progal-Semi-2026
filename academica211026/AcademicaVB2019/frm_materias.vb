
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Public Class frm_materias
    Inherits Form

    Private ReadOnly db As New Conexion_base_datos()
    Private campos As TextBox()
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents TextBox3 As TextBox
    Friend WithEvents TextBox4 As TextBox
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents Button4 As Button
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents idMateria As DataGridViewTextBoxColumn
    Friend WithEvents codigo As DataGridViewTextBoxColumn
    Friend WithEvents nombre As DataGridViewTextBoxColumn
    Friend WithEvents uv As DataGridViewTextBoxColumn
    Private dgv As DataGridView

    Public Sub New()
        Me.Text = "Gestión de materias"
        Me.Size = New Size(900, 600)
        Me.StartPosition = FormStartPosition.CenterParent
        Me.BackColor = Ui.Claro

        Dim layout As New TableLayoutPanel With {
            .Dock = DockStyle.Fill,
            .RowCount = 3,
            .ColumnCount = 1,
            .Padding = New Padding(15)
        }

        layout.ColumnStyles.Add(
            New ColumnStyle(SizeType.Percent, 100))

        layout.RowStyles.Add(
            New RowStyle(SizeType.Absolute, 220))

        layout.RowStyles.Add(
            New RowStyle(SizeType.Absolute, 60))

        layout.RowStyles.Add(
            New RowStyle(SizeType.Percent, 100))

        Me.Controls.Add(layout)

        Dim tabla As TableLayoutPanel = Ui.TablaCampos(layout)

        Dim etiquetas As String() = {
            "ID materia",
            "Código",
            "Nombre",
            "UV"
        }

        campos = New TextBox(3) {}

        For i As Integer = 0 To 3
            campos(i) = Ui.Campo(
                etiquetas(i),
                tabla,
                False,
                i = 0)
        Next

        Dim barra As New FlowLayoutPanel With {
            .Dock = DockStyle.Fill,
            .AutoScroll = True
        }

        layout.Controls.Add(barra, 0, 1)

        Dim btnNuevo As Button = Ui.Boton("Nuevo")
        Dim btnGuardar As Button = Ui.Boton("Guardar")
        Dim btnModificar As Button = Ui.Boton("Modificar")
        Dim btnEliminar As Button = Ui.Boton("Eliminar")

        barra.Controls.AddRange(New Control() {
            btnNuevo,
            btnGuardar,
            btnModificar,
            btnEliminar
        })

        AddHandler btnNuevo.Click,
            Sub(sender, e) Limpiar()

        AddHandler btnGuardar.Click,
            Sub(sender, e) Ejecutar("nuevo")

        AddHandler btnModificar.Click,
            Sub(sender, e) Ejecutar("modificar")

        AddHandler btnEliminar.Click,
            Sub(sender, e) Ejecutar("eliminar")

        dgv = Ui.Rejilla()
        dgv.Dock = DockStyle.Fill

        layout.Controls.Add(dgv, 0, 2)

        AddHandler dgv.SelectionChanged,
            AddressOf SeleccionarMateria

        AddHandler Me.Shown,
            Sub(sender, e) Cargar()
    End Sub

    Private Sub SeleccionarMateria(
        sender As Object,
        e As EventArgs)

        If dgv.CurrentRow Is Nothing Then Return
        If dgv.CurrentRow.IsNewRow Then Return
        If dgv.CurrentRow.Cells.Count < campos.Length Then Return

        For i As Integer = 0 To campos.Length - 1
            campos(i).Text =
                Convert.ToString(dgv.CurrentRow.Cells(i).Value)
        Next
    End Sub

    Private Sub Limpiar()
        For Each t As TextBox In campos
            t.Clear()
        Next

        campos(1).Focus()
    End Sub

    Private Sub Cargar()
        Try
            dgv.DataSource =
                db.obtenerDatos().Tables("materias")

        Catch ex As Exception
            MessageBox.Show(
                ex.Message,
                "Error al cargar materias",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Ejecutar(accion As String)

        If accion <> "nuevo" AndAlso
           String.IsNullOrWhiteSpace(campos(0).Text) Then

            MessageBox.Show(
                "Selecciona primero una materia.")
            Return
        End If

        If accion <> "eliminar" Then
            For i As Integer = 1 To 3
                If String.IsNullOrWhiteSpace(campos(i).Text) Then
                    MessageBox.Show(
                        "Completa todos los campos.")
                    campos(i).Focus()
                    Return
                End If
            Next

            Dim uv As Integer

            If Not Integer.TryParse(campos(3).Text, uv) OrElse uv <= 0 Then
                MessageBox.Show(
                    "Las unidades valorativas deben ser un número mayor que cero.")
                campos(3).Focus()
                Return
            End If
        End If

        If accion = "eliminar" Then
            Dim respuesta As DialogResult =
                MessageBox.Show(
                    "¿Deseas eliminar esta materia?",
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

            db.administrarDatosMaterias(datos, accion)

            Cargar()

            MessageBox.Show(
                "Operación realizada correctamente.",
                "Sistema Académico",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information)

        Catch ex As Exception
            MessageBox.Show(
                ex.Message,
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.TextBox4 = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.idMateria = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.codigo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.nombre = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.uv = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(76, 17)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "ID Materia:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 83)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(56, 17)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Codigo:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 167)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(62, 17)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Nombre:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 223)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(31, 17)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "UV:"
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(94, 23)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(262, 32)
        Me.TextBox1.TabIndex = 4
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(94, 83)
        Me.TextBox2.Multiline = True
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(262, 34)
        Me.TextBox2.TabIndex = 5
        '
        'TextBox3
        '
        Me.TextBox3.Location = New System.Drawing.Point(94, 167)
        Me.TextBox3.Multiline = True
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(262, 31)
        Me.TextBox3.TabIndex = 6
        '
        'TextBox4
        '
        Me.TextBox4.Location = New System.Drawing.Point(94, 223)
        Me.TextBox4.Multiline = True
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.Size = New System.Drawing.Size(262, 31)
        Me.TextBox4.TabIndex = 7
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(15, 317)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(85, 38)
        Me.Button1.TabIndex = 8
        Me.Button1.Text = "Nuevo"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(120, 319)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(85, 36)
        Me.Button2.TabIndex = 9
        Me.Button2.Text = "Guardar"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(228, 318)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(85, 36)
        Me.Button3.TabIndex = 10
        Me.Button3.Text = "Modiicar"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(329, 319)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(85, 36)
        Me.Button4.TabIndex = 11
        Me.Button4.Text = "Eliminar"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToOrderColumns = True
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.idMateria, Me.codigo, Me.nombre, Me.uv})
        Me.DataGridView1.Location = New System.Drawing.Point(15, 373)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowHeadersWidth = 51
        Me.DataGridView1.RowTemplate.Height = 24
        Me.DataGridView1.Size = New System.Drawing.Size(551, 150)
        Me.DataGridView1.TabIndex = 12
        '
        'idMateria
        '
        Me.idMateria.HeaderText = "ID materia"
        Me.idMateria.MinimumWidth = 6
        Me.idMateria.Name = "idMateria"
        Me.idMateria.Width = 125
        '
        'codigo
        '
        Me.codigo.HeaderText = "Codigo"
        Me.codigo.MinimumWidth = 6
        Me.codigo.Name = "codigo"
        Me.codigo.Width = 125
        '
        'nombre
        '
        Me.nombre.HeaderText = "Nombre"
        Me.nombre.MinimumWidth = 6
        Me.nombre.Name = "nombre"
        Me.nombre.Width = 125
        '
        'uv
        '
        Me.uv.HeaderText = "UV"
        Me.uv.MinimumWidth = 6
        Me.uv.Name = "uv"
        Me.uv.Width = 125
        '
        'frm_materias
        '
        Me.ClientSize = New System.Drawing.Size(626, 551)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.TextBox4)
        Me.Controls.Add(Me.TextBox3)
        Me.Controls.Add(Me.TextBox2)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "frm_materias"
        Me.Text = "ccccccccccc"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

    End Sub
End Class
