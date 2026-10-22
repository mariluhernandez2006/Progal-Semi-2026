
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Public Class frm_principal
    Inherits Form

    Private usuarioActual As String

    Private WithEvents Button1 As Button
    Private WithEvents Button2 As Button
    Private WithEvents Button3 As Button
    Private WithEvents Button4 As Button
    Private WithEvents Button5 As Button

    Private Label1 As Label
    Private Label2 As Label
    Private Label3 As Label
    Private Label4 As Label
    Private Label5 As Label

    Private PictureBox1 As PictureBox
    Private WithEvents Timer1 As Timer

    Public Sub New(usuario As String)
        MyBase.New()

        usuarioActual = usuario

        InitializeComponent()

        Label3.Text = "Usuario: " & usuarioActual

        ActualizarFechaHora()
        Timer1.Start()
    End Sub

    Private Sub ActualizarFechaHora()
        Label4.Text = "Fecha: " & DateTime.Now.ToString("dd/MM/yyyy")
        Label5.Text = "Hora: " & DateTime.Now.ToString("HH:mm")
    End Sub

    Private Sub Timer1_Tick(
        sender As Object,
        e As EventArgs) Handles Timer1.Tick

        ActualizarFechaHora()
    End Sub

    ' BOTÓN USUARIOS
    Private Sub Button1_Click(
        sender As Object,
        e As EventArgs) Handles Button1.Click

        Using ventana As New frm_usuario()
            ventana.ShowDialog(Me)
        End Using
    End Sub

    ' BOTÓN ALUMNOS
    Private Sub Button2_Click(
        sender As Object,
        e As EventArgs) Handles Button2.Click

        Using ventana As New frm_alumnos()
            ventana.ShowDialog(Me)
        End Using
    End Sub

    ' BOTÓN MATERIAS
    Private Sub Button3_Click(
        sender As Object,
        e As EventArgs) Handles Button3.Click

        Using ventana As New frm_materias()
            ventana.ShowDialog(Me)
        End Using
    End Sub

    ' BOTÓN CONFIGURACIÓN - CORREGIDO
    Private Sub Button4_Click(
        sender As Object,
        e As EventArgs) Handles Button4.Click

        Using ventana As New frm_configuracion()
            ventana.ShowDialog(Me)
        End Using
    End Sub

    ' BOTÓN CERRAR SESIÓN
    Private Sub Button5_Click(
        sender As Object,
        e As EventArgs) Handles Button5.Click

        Me.Close()
    End Sub

    Private Sub InitializeComponent()

        Me.Button1 = New Button()
        Me.Button2 = New Button()
        Me.Button3 = New Button()
        Me.Button4 = New Button()
        Me.Button5 = New Button()

        Me.Label1 = New Label()
        Me.Label2 = New Label()
        Me.Label3 = New Label()
        Me.Label4 = New Label()
        Me.Label5 = New Label()

        Me.PictureBox1 = New PictureBox()
        Me.Timer1 = New Timer()

        CType(Me.PictureBox1,
              System.ComponentModel.ISupportInitialize).BeginInit()

        Me.SuspendLayout()

        ' FORMULARIO PRINCIPAL
        Me.ClientSize = New Size(690, 500)
        Me.Name = "frm_principal"
        Me.Text = "Sistema Académico - Menú principal"
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.BackColor = Color.FromArgb(240, 240, 240)

        ' BOTÓN USUARIOS
        Me.Button1.Location = New Point(50, 55)
        Me.Button1.Size = New Size(123, 47)
        Me.Button1.Name = "Button1"
        Me.Button1.Text = "Usuarios"
        Me.Button1.UseVisualStyleBackColor = True

        ' BOTÓN ALUMNOS
        Me.Button2.Location = New Point(50, 112)
        Me.Button2.Size = New Size(123, 47)
        Me.Button2.Name = "Button2"
        Me.Button2.Text = "Alumnos"
        Me.Button2.UseVisualStyleBackColor = True

        ' BOTÓN MATERIAS
        Me.Button3.Location = New Point(50, 169)
        Me.Button3.Size = New Size(123, 47)
        Me.Button3.Name = "Button3"
        Me.Button3.Text = "Materias"
        Me.Button3.UseVisualStyleBackColor = True

        ' BOTÓN CONFIGURACIÓN
        Me.Button4.Location = New Point(50, 226)
        Me.Button4.Size = New Size(123, 65)
        Me.Button4.Name = "Button4"
        Me.Button4.Text = "Configuración"
        Me.Button4.UseVisualStyleBackColor = True

        ' BOTÓN CERRAR SESIÓN
        Me.Button5.Location = New Point(50, 301)
        Me.Button5.Size = New Size(123, 65)
        Me.Button5.Name = "Button5"
        Me.Button5.Text = "Cerrar sesión"
        Me.Button5.UseVisualStyleBackColor = True

        ' BIENVENIDO
        Me.Label1.AutoSize = True
        Me.Label1.Font = New Font(
            "Microsoft Sans Serif",
            14.0!,
            FontStyle.Regular)

        Me.Label1.Location = New Point(226, 55)
        Me.Label1.Name = "Label1"
        Me.Label1.Text = "Bienvenido"

        ' SELECCIONE UNA OPCIÓN
        Me.Label2.AutoSize = True
        Me.Label2.Location = New Point(226, 90)
        Me.Label2.Name = "Label2"
        Me.Label2.Text = "Seleccione una opcion de menu"

        ' LOGO DEL SISTEMA
        Me.PictureBox1.Location = New Point(220, 115)
        Me.PictureBox1.Size = New Size(322, 295)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.SizeMode = PictureBoxSizeMode.Zoom
        Me.PictureBox1.BackColor = Color.White

        Me.PictureBox1.Image =
            Global.AcademicaVB2019.My.Resources.Resources.db974c68_625f_4e08_b920_29d528db164d1

        ' USUARIO CONECTADO
        Me.Label3.AutoSize = True
        Me.Label3.Font = New Font(
            "Microsoft Sans Serif",
            12.0!,
            FontStyle.Regular)

        Me.Label3.Location = New Point(48, 450)
        Me.Label3.Name = "Label3"
        Me.Label3.Text = "Usuario:"

        ' FECHA
        Me.Label4.AutoSize = True
        Me.Label4.Location = New Point(455, 455)
        Me.Label4.Name = "Label4"
        Me.Label4.Text = "Fecha:"

        ' HORA
        Me.Label5.AutoSize = True
        Me.Label5.Location = New Point(590, 455)
        Me.Label5.Name = "Label5"
        Me.Label5.Text = "Hora:"

        ' TEMPORIZADOR
        Me.Timer1.Interval = 1000

        ' AGREGAR CONTROLES
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.Button5)

        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label5)

        Me.Controls.Add(Me.PictureBox1)

        CType(Me.PictureBox1,
              System.ComponentModel.ISupportInitialize).EndInit()

        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

End Class
