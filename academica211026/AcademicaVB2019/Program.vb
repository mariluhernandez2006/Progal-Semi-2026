

Imports System
Imports System.Windows.Forms

Friend Module Program

    <STAThread()>
    Public Sub Main()

        Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(False)

        ' Abrir primero el inicio de sesión
        Application.Run(New login())

    End Sub

End Module

