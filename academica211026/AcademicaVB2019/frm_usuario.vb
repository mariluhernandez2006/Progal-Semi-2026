
Imports System
Imports System.Data
Imports System.Drawing
Imports System.Text.RegularExpressions
Imports System.Windows.Forms

Public Class frm_usuario
    Inherits Form

    Private ReadOnly db As New Conexion_base_datos()

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label

    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents TextBox3 As TextBox
    Friend WithEvents TextBox4 As TextBox
    Friend WithEvents TextBox5 As TextBox

    Friend WithEvents DataGridView1 As DataGridView

    Friend WithEvents id As DataGridViewTextBoxColumn
    Friend WithEvents usuario As DataGridViewTextBoxColumn
    Friend WithEvents nombre As DataGridViewTextBoxColumn
    Friend WithEvents direccion As DataGridViewTextBoxColumn
    Friend WithEvents telefono As DataGridViewTextBoxColumn

    Friend WithEvents btnNuevo As Button
    Friend WithEvents btnGuardar As Button
    Friend WithEvents btnModificar As Button
    Friend WithEvents btnEliminar As Button

    Friend WithEvents lblConfirmarClave As Label
    Friend WithEvents txtConfirmarClave As TextBox
    Friend WithEvents btnPrimero As Button
    Friend WithEvents btnAnterior As Button
    Friend WithEvents btnSiguiente As Button
    Friend WithEvents btnUltimo As Button

    Private idSeleccionado As String = ""

    Public Sub New()
        MyBase.New()

        ' Mostrar el diseño original
        InitializeComponent()

        Me.StartPosition = FormStartPosition.CenterScreen

        ' Ocultar los caracteres de la contraseña
        TextBox2.Multiline = False
        TextBox2.UseSystemPasswordChar = True
        txtConfirmarClave.UseSystemPasswordChar = True

        ' Preparar la tabla
        DataGridView1.ReadOnly = True
        DataGridView1.AllowUserToAddRows = False
        DataGridView1.AllowUserToDeleteRows = False
        DataGridView1.SelectionMode =
            DataGridViewSelectionMode.FullRowSelect
        DataGridView1.MultiSelect = False
        DataGridView1.AutoGenerateColumns = False

        id.DataPropertyName = "idUsuario"
        usuario.DataPropertyName = "usuario"
        nombre.DataPropertyName = "nombre"
        direccion.DataPropertyName = "direccion"
        telefono.DataPropertyName = "telefono"

        AddHandler Me.Shown, AddressOf FormularioMostrado
    End Sub

    Private Sub FormularioMostrado(
        sender As Object,
        e As EventArgs)

        CargarUsuarios()
    End Sub

    ' CARGAR USUARIOS
    Private Sub CargarUsuarios()
        Try
            Dim ds As DataSet = db.crearDs()

            DataGridView1.DataSource =
                ds.Tables("usuarios_USTR025826")

        Catch ex As Exception
            MessageBox.Show(
                "No se pudieron cargar los usuarios." &
                Environment.NewLine & ex.Message,
                "Error de base de datos",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error)
        End Try
    End Sub

    ' NUEVO
    Private Sub btnNuevo_Click(
        sender As Object,
        e As EventArgs) Handles btnNuevo.Click

        LimpiarCampos()
    End Sub

    Private Sub LimpiarCampos()
        idSeleccionado = ""

        TextBox1.Clear()
        TextBox2.Clear()
        txtConfirmarClave.Clear()
        TextBox3.Clear()
        TextBox4.Clear()
        TextBox5.Clear()

        DataGridView1.ClearSelection()
        TextBox1.Focus()
    End Sub

    ' GUARDAR
    Private Sub btnGuardar_Click(
        sender As Object,
        e As EventArgs) Handles btnGuardar.Click

        GuardarUsuario("nuevo")
    End Sub

    ' MODIFICAR
    Private Sub btnModificar_Click(
        sender As Object,
        e As EventArgs) Handles btnModificar.Click

        If String.IsNullOrWhiteSpace(idSeleccionado) Then
            MessageBox.Show(
                "Selecciona un usuario de la tabla.")
            Return
        End If

        GuardarUsuario("modificar")
    End Sub

    ' VALIDAR Y GUARDAR
    Private Sub GuardarUsuario(accion As String)

        Dim nombreUsuario As String = TextBox1.Text.Trim()
        Dim clave As String = TextBox2.Text
        If clave <> txtConfirmarClave.Text Then
            MessageBox.Show("Las contraseñas no coinciden.")
            Return
        End If
        If clave.Length > 35 Then
            MessageBox.Show("La contraseña no puede superar 35 caracteres.")
            Return
        End If
        Dim nombreCompleto As String = TextBox3.Text.Trim()
        Dim dir As String = TextBox4.Text.Trim()
        Dim tel As String = TextBox5.Text.Trim()

        If nombreUsuario.Length < 6 OrElse
           nombreUsuario.Length > 16 Then

            MessageBox.Show(
                "El usuario debe tener entre 6 y 16 caracteres.")
            Return
        End If

        If Not Regex.IsMatch(clave, "[A-Za-z]") OrElse
           Not Regex.IsMatch(clave, "[0-9]") Then

            MessageBox.Show(
                "La contraseña debe contener letras y números.")
            Return
        End If

        If nombreCompleto.Length > 65 OrElse dir.Length > 85 Then
            MessageBox.Show("El nombre o la dirección supera la longitud permitida.")
            Return
        End If

        If String.IsNullOrWhiteSpace(nombreCompleto) Then
            MessageBox.Show("Escribe el nombre.")
            Return
        End If

        If tel.Length > 9 Then
            MessageBox.Show(
                "El teléfono debe tener máximo 9 caracteres.")
            Return
        End If

        Try
            Dim datos As String() = {
                idSeleccionado,
                nombreUsuario,
                clave,
                nombreCompleto,
                dir,
                tel
            }

            db.mantenimiento_usuarios_USTR025826(
                datos, accion)

            MessageBox.Show(
                "Operación realizada correctamente.",
                "Sistema Académico",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information)

            CargarUsuarios()
            LimpiarCampos()

        Catch ex As Exception
            MessageBox.Show(
                ex.Message,
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error)
        End Try
    End Sub

    ' SELECCIONAR USUARIO
    Private Sub DataGridView1_CellClick(
        sender As Object,
        e As DataGridViewCellEventArgs
        ) Handles DataGridView1.CellClick

        If e.RowIndex < 0 Then Return

        Dim fila As DataGridViewRow =
            DataGridView1.Rows(e.RowIndex)

        idSeleccionado =
            Convert.ToString(fila.Cells("id").Value)

        TextBox1.Text =
            Convert.ToString(fila.Cells("usuario").Value)

        TextBox3.Text =
            Convert.ToString(fila.Cells("nombre").Value)

        TextBox4.Text =
            Convert.ToString(fila.Cells("direccion").Value)

        TextBox5.Text =
            Convert.ToString(fila.Cells("telefono").Value)

        ' No mostrar la contraseña almacenada
        TextBox2.Clear()
        txtConfirmarClave.Clear()
    End Sub

    ' Navegación por los usuarios de la tabla sin modificar su diseño original
    Private Sub SeleccionarFila(indice As Integer)
        If DataGridView1.Rows.Count = 0 Then Return
        indice = Math.Max(0, Math.Min(indice, DataGridView1.Rows.Count - 1))
        DataGridView1.ClearSelection()
        DataGridView1.Rows(indice).Selected = True
        DataGridView1.CurrentCell = DataGridView1.Rows(indice).Cells("usuario")
        Dim fila As DataGridViewRow = DataGridView1.Rows(indice)
        idSeleccionado = Convert.ToString(fila.Cells("id").Value)
        TextBox1.Text = Convert.ToString(fila.Cells("usuario").Value)
        TextBox3.Text = Convert.ToString(fila.Cells("nombre").Value)
        TextBox4.Text = Convert.ToString(fila.Cells("direccion").Value)
        TextBox5.Text = Convert.ToString(fila.Cells("telefono").Value)
        TextBox2.Clear()
        txtConfirmarClave.Clear()
    End Sub

    Private Sub btnPrimero_Click(sender As Object, e As EventArgs) Handles btnPrimero.Click
        SeleccionarFila(0)
    End Sub
    Private Sub btnAnterior_Click(sender As Object, e As EventArgs) Handles btnAnterior.Click
        SeleccionarFila(If(DataGridView1.CurrentRow Is Nothing, 0, DataGridView1.CurrentRow.Index - 1))
    End Sub
    Private Sub btnSiguiente_Click(sender As Object, e As EventArgs) Handles btnSiguiente.Click
        SeleccionarFila(If(DataGridView1.CurrentRow Is Nothing, 0, DataGridView1.CurrentRow.Index + 1))
    End Sub
    Private Sub btnUltimo_Click(sender As Object, e As EventArgs) Handles btnUltimo.Click
        SeleccionarFila(DataGridView1.Rows.Count - 1)
    End Sub

    ' ELIMINAR
    Private Sub btnEliminar_Click(
        sender As Object,
        e As EventArgs) Handles btnEliminar.Click

        If String.IsNullOrWhiteSpace(idSeleccionado) Then
            MessageBox.Show(
                "Selecciona un usuario para eliminar.")
            Return
        End If

        Dim respuesta As DialogResult =
            MessageBox.Show(
                "¿Deseas eliminar este usuario?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question)

        If respuesta <> DialogResult.Yes Then Return

        Try
            Dim datos As String() = {
                idSeleccionado, "", "", "", "", ""
            }

            db.mantenimiento_usuarios_USTR025826(
                datos, "eliminar")

            MessageBox.Show(
                "Usuario eliminado correctamente.")

            CargarUsuarios()
            LimpiarCampos()

        Catch ex As Exception
            MessageBox.Show(
                ex.Message,
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error)
        End Try
    End Sub

    ' DISEÑO ORIGINAL DEL FORMULARIO
    Private Sub InitializeComponent()

        Me.Label1 = New Label()
        Me.Label2 = New Label()
        Me.Label3 = New Label()
        Me.Label4 = New Label()
        Me.Label5 = New Label()

        Me.TextBox1 = New TextBox()
        Me.TextBox2 = New TextBox()
        Me.TextBox3 = New TextBox()
        Me.TextBox4 = New TextBox()
        Me.TextBox5 = New TextBox()

        Me.DataGridView1 = New DataGridView()

        Me.id = New DataGridViewTextBoxColumn()
        Me.usuario = New DataGridViewTextBoxColumn()
        Me.nombre = New DataGridViewTextBoxColumn()
        Me.direccion = New DataGridViewTextBoxColumn()
        Me.telefono = New DataGridViewTextBoxColumn()

        Me.btnNuevo = New Button()
        Me.btnGuardar = New Button()
        Me.btnModificar = New Button()
        Me.btnEliminar = New Button()
        Me.lblConfirmarClave = New Label()
        Me.txtConfirmarClave = New TextBox()
        Me.btnPrimero = New Button()
        Me.btnAnterior = New Button()
        Me.btnSiguiente = New Button()
        Me.btnUltimo = New Button()

        CType(Me.DataGridView1,
              System.ComponentModel.ISupportInitialize).BeginInit()

        Me.SuspendLayout()

        ' USUARIO
        Me.Label1.AutoSize = True
        Me.Label1.Location = New Point(12, 26)
        Me.Label1.Name = "Label1"
        Me.Label1.Text = "Usuario"

        Me.TextBox1.Location = New Point(12, 46)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New Size(133, 27)

        ' CONTRASEÑA
        Me.Label2.AutoSize = True
        Me.Label2.Location = New Point(12, 87)
        Me.Label2.Name = "Label2"
        Me.Label2.Text = "Contraseña"

        Me.TextBox2.Location = New Point(15, 107)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New Size(130, 30)

        ' NOMBRE
        Me.Label3.AutoSize = True
        Me.Label3.Location = New Point(12, 152)
        Me.Label3.Name = "Label3"
        Me.Label3.Text = "Nombre"

        Me.TextBox3.Location = New Point(15, 172)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New Size(130, 31)

        ' DIRECCIÓN
        Me.Label4.AutoSize = True
        Me.Label4.Location = New Point(12, 218)
        Me.Label4.Name = "Label4"
        Me.Label4.Text = "Dirección"

        Me.TextBox4.Location = New Point(15, 238)
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.Size = New Size(130, 31)

        ' TELÉFONO
        Me.Label5.AutoSize = True
        Me.Label5.Location = New Point(12, 282)
        Me.Label5.Name = "Label5"
        Me.Label5.Text = "Teléfono"

        Me.TextBox5.Location = New Point(15, 302)
        Me.TextBox5.Name = "TextBox5"
        Me.TextBox5.Size = New Size(130, 32)

        ' TABLA
        Me.DataGridView1.Location = New Point(151, 26)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New Size(668, 285)

        Me.DataGridView1.ColumnHeadersHeightSizeMode =
            DataGridViewColumnHeadersHeightSizeMode.AutoSize

        Me.DataGridView1.Columns.AddRange(
            New DataGridViewColumn() {
                Me.id,
                Me.usuario,
                Me.nombre,
                Me.direccion,
                Me.telefono
            })

        Me.id.Name = "id"
        Me.id.HeaderText = "ID"

        Me.usuario.Name = "usuario"
        Me.usuario.HeaderText = "Usuario"

        Me.nombre.Name = "nombre"
        Me.nombre.HeaderText = "Nombre"

        Me.direccion.Name = "direccion"
        Me.direccion.HeaderText = "Dirección"

        Me.telefono.Name = "telefono"
        Me.telefono.HeaderText = "Teléfono"

        ' BOTÓN NUEVO
        Me.btnNuevo.Location = New Point(151, 335)
        Me.btnNuevo.Name = "btnNuevo"
        Me.btnNuevo.Size = New Size(95, 47)
        Me.btnNuevo.Text = "Nuevo"
        Me.btnNuevo.UseVisualStyleBackColor = True

        ' BOTÓN GUARDAR
        Me.btnGuardar.Location = New Point(341, 335)
        Me.btnGuardar.Name = "btnGuardar"
        Me.btnGuardar.Size = New Size(95, 47)
        Me.btnGuardar.Text = "Guardar"
        Me.btnGuardar.UseVisualStyleBackColor = True

        ' BOTÓN MODIFICAR
        Me.btnModificar.Location = New Point(537, 335)
        Me.btnModificar.Name = "btnModificar"
        Me.btnModificar.Size = New Size(95, 47)
        Me.btnModificar.Text = "Modificar"
        Me.btnModificar.UseVisualStyleBackColor = True

        ' BOTÓN ELIMINAR
        Me.btnEliminar.Location = New Point(724, 335)
        Me.btnEliminar.Name = "btnEliminar"
        Me.btnEliminar.Size = New Size(95, 47)
        Me.btnEliminar.Text = "Eliminar"
        Me.btnEliminar.UseVisualStyleBackColor = True

        ' Controles nuevos en el espacio libre, sin desplazar los originales
        Me.lblConfirmarClave.AutoSize = True
        Me.lblConfirmarClave.Location = New Point(12, 352)
        Me.lblConfirmarClave.Name = "lblConfirmarClave"
        Me.lblConfirmarClave.Text = "Confirmar contraseña"
        Me.txtConfirmarClave.Location = New Point(15, 375)
        Me.txtConfirmarClave.Name = "txtConfirmarClave"
        Me.txtConfirmarClave.Size = New Size(130, 27)
        Me.txtConfirmarClave.MaxLength = 35

        Me.btnPrimero.Location = New Point(305, 411)
        Me.btnPrimero.Name = "btnPrimero"
        Me.btnPrimero.Size = New Size(112, 32)
        Me.btnPrimero.Text = "Primero"
        Me.btnAnterior.Location = New Point(423, 411)
        Me.btnAnterior.Name = "btnAnterior"
        Me.btnAnterior.Size = New Size(112, 32)
        Me.btnAnterior.Text = "Anterior"
        Me.btnSiguiente.Location = New Point(541, 411)
        Me.btnSiguiente.Name = "btnSiguiente"
        Me.btnSiguiente.Size = New Size(112, 32)
        Me.btnSiguiente.Text = "Siguiente"
        Me.btnUltimo.Location = New Point(659, 411)
        Me.btnUltimo.Name = "btnUltimo"
        Me.btnUltimo.Size = New Size(112, 32)
        Me.btnUltimo.Text = "Último"

        ' FORMULARIO
        Me.ClientSize = New Size(840, 470)
        Me.Name = "frm_usuario"
        Me.Text = "Gestión de usuarios"

        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label5)

        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.TextBox2)
        Me.Controls.Add(Me.TextBox3)
        Me.Controls.Add(Me.TextBox4)
        Me.Controls.Add(Me.TextBox5)

        Me.Controls.Add(Me.DataGridView1)

        Me.Controls.Add(Me.btnNuevo)
        Me.Controls.Add(Me.btnGuardar)
        Me.Controls.Add(Me.btnModificar)
        Me.Controls.Add(Me.btnEliminar)
        Me.Controls.Add(Me.lblConfirmarClave)
        Me.Controls.Add(Me.txtConfirmarClave)
        Me.Controls.Add(Me.btnPrimero)
        Me.Controls.Add(Me.btnAnterior)
        Me.Controls.Add(Me.btnSiguiente)
        Me.Controls.Add(Me.btnUltimo)

        CType(Me.DataGridView1,
              System.ComponentModel.ISupportInitialize).EndInit()

        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

End Class

