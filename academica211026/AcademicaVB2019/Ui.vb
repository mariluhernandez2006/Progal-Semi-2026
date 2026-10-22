Imports System.Drawing
Imports System.Windows.Forms
Public Module Ui
    Public ReadOnly Azul As Color = Color.FromArgb(25, 52, 89)
    Public ReadOnly Claro As Color = Color.FromArgb(243, 247, 252)
    Public Function Boton(t As String, Optional ancho As Integer = 112) As Button
        Dim b As New Button With {.Text=t, .Width=ancho, .Height=38, .BackColor=Color.FromArgb(28, 105, 192), .ForeColor=Color.White, .FlatStyle=FlatStyle.Flat, .Margin=New Padding(5)}
        b.FlatAppearance.BorderSize=0
        Return b
    End Function
    Public Function Campo(etiqueta As String, panel As TableLayoutPanel, Optional oculto As Boolean=False, Optional soloLectura As Boolean=False) As TextBox
        Dim l As New Label With {.Text=etiqueta, .AutoSize=True, .Font=New Font("Segoe UI",10,FontStyle.Bold), .Anchor=AnchorStyles.Left, .Margin=New Padding(6,10,6,3)}
        Dim t As New TextBox With {.Width=285, .Font=New Font("Segoe UI",11), .UseSystemPasswordChar=oculto, .ReadOnly=soloLectura, .Anchor=AnchorStyles.Left Or AnchorStyles.Right, .Margin=New Padding(6,5,6,8)}
        panel.Controls.Add(l)
        panel.Controls.Add(t)
        Return t
    End Function
    Public Function Formulario(titulo As String, Optional ancho As Integer=950, Optional alto As Integer=690) As Form
        Return New Form With {.Text=titulo, .Size=New Size(ancho,alto), .StartPosition=FormStartPosition.CenterScreen, .BackColor=Claro, .Font=New Font("Segoe UI",10)}
    End Function
    Public Function TablaCampos(contenedor As Control) As TableLayoutPanel
        Dim t As New TableLayoutPanel With {.ColumnCount=2, .AutoSize=True, .Dock=DockStyle.Top, .Padding=New Padding(12), .BackColor=Color.White}
        t.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute,170))
        t.ColumnStyles.Add(New ColumnStyle(SizeType.Percent,100))
        contenedor.Controls.Add(t)
        Return t
    End Function
    Public Function Rejilla() As DataGridView
        Return New DataGridView With {.Dock=DockStyle.Fill, .ReadOnly=True, .AllowUserToAddRows=False, .AllowUserToDeleteRows=False, .SelectionMode=DataGridViewSelectionMode.FullRowSelect, .MultiSelect=False, .AutoSizeColumnsMode=DataGridViewAutoSizeColumnsMode.Fill, .BackgroundColor=Color.White, .RowHeadersVisible=False}
    End Function
End Module