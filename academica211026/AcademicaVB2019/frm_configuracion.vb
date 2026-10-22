
Imports System
Imports System.Windows.Forms

Public Class frm_configuracion

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub frm_configuracion_Load(
        sender As Object,
        e As EventArgs) Handles MyBase.Load

        Me.Text = "Configuración - Sistema Académico"
        Me.StartPosition = FormStartPosition.CenterParent

    End Sub

End Class
