
Imports System
Imports System.Data
Imports System.Drawing
Imports System.Windows.Forms

Public Class login
    Inherits Form

    Private ReadOnly db As New Conexion_base_datos()

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents Button1 As Button

    Public Sub New()
        MyBase.New()

        ' Mostrar el diseño hecho en Visual Studio
        InitializeComponent()

        Me.StartPosition = FormStartPosition.CenterScreen
        Me.Text = "Inicio de sesión - Sistema Académico"

        ' Ocultar la contraseña
        TextBox2.Multiline = False
        TextBox2.UseSystemPasswordChar = True

        ' Permitir ingresar con Enter
        Me.AcceptButton = Button1
    End Sub

    Private Sub Button1_Click(sender As Object,
                              e As EventArgs) Handles Button1.Click

        If String.IsNullOrWhiteSpace(TextBox1.Text) OrElse
           String.IsNullOrWhiteSpace(TextBox2.Text) Then

            MessageBox.Show(
                "Ingresa tu usuario y contraseña.",
                "Campos obligatorios",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            )
            Return
        End If

        Try
            Dim sql As String =
                "SELECT idUsuario FROM usuarios_USTR025826 " &
                "WHERE usuario=@u AND clave=@c"

            Dim t As DataTable = db.tabla(
                sql,
                Conexion_base_datos.P("@u", TextBox1.Text.Trim()),
                Conexion_base_datos.P("@c", TextBox2.Text)
            )

            If t.Rows.Count = 0 Then
                MessageBox.Show(
                    "Usuario o contraseña incorrectos.",
                    "Acceso denegado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                )
                Return
            End If

            Me.Hide()

            Using f As New frm_principal(TextBox1.Text.Trim())
                f.ShowDialog()
            End Using

            TextBox2.Clear()
            Me.Show()
            TextBox1.Focus()

        Catch ex As Exception
            Me.Show()

            MessageBox.Show(
                "No se pudo completar el inicio de sesión." &
                Environment.NewLine &
                ex.Message,
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            )
        End Try

    End Sub

    Private Sub InitializeComponent()

        Me.Label1 = New Label()
        Me.Label2 = New Label()
        Me.Label3 = New Label()
        Me.TextBox1 = New TextBox()
        Me.TextBox2 = New TextBox()
        Me.Button1 = New Button()

        Me.SuspendLayout()

        ' Título
        Me.Label1.AutoSize = True
        Me.Label1.Font = New Font(
            "Microsoft Sans Serif",
            12.0!,
            FontStyle.Regular
        )
        Me.Label1.Location = New Point(223, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Text = "Iniciar Sesion"

        ' Usuario
        Me.Label2.AutoSize = True
        Me.Label2.Location = New Point(87, 73)
        Me.Label2.Name = "Label2"
        Me.Label2.Text = "Usuario"

        ' Contraseña
        Me.Label3.AutoSize = True
        Me.Label3.Location = New Point(87, 169)
        Me.Label3.Name = "Label3"
        Me.Label3.Text = "Contraseña"

        ' Cuadro de usuario
        Me.TextBox1.Location = New Point(90, 93)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New Size(225, 34)

        ' Cuadro de contraseña
        Me.TextBox2.Location = New Point(90, 189)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New Size(225, 34)

        ' Botón ingresar
        Me.Button1.Location = New Point(193, 279)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New Size(161, 53)
        Me.Button1.Text = "INGRESAR"
        Me.Button1.UseVisualStyleBackColor = True

        ' Formulario
        Me.ClientSize = New Size(576, 369)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.TextBox2)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)

        Me.Name = "login"

        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

End Class

