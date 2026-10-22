
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration

Public Class Conexion_base_datos

    Private ReadOnly cadena As String =
        ConfigurationManager.ConnectionStrings("Academica").ConnectionString

    Public Function abrir() As SqlConnection
        Dim conexion As New SqlConnection(cadena)
        conexion.Open()
        Return conexion
    End Function

    Public Function obtenerDatos() As DataSet
        Return crearDs()
    End Function

    Public Function crearDs() As DataSet
        Dim ds As New DataSet()

        Using conexion As SqlConnection = abrir()
            For Each nombreTabla As String In
                New String() {"alumnos", "materias", "usuarios_USTR025826"}

                Dim consulta As String =
                    "SELECT * FROM [" & nombreTabla & "]"

                Using adaptador As New SqlDataAdapter(consulta, conexion)
                    adaptador.Fill(ds, nombreTabla)
                End Using
            Next
        End Using

        Return ds
    End Function

    Public Function tabla(sql As String,
                          ParamArray parametros() As SqlParameter) As DataTable

        Dim resultado As New DataTable()

        Using conexion As SqlConnection = abrir()
            Using comando As New SqlCommand(sql, conexion)

                If parametros IsNot Nothing AndAlso parametros.Length > 0 Then
                    comando.Parameters.AddRange(parametros)
                End If

                Using adaptador As New SqlDataAdapter(comando)
                    adaptador.Fill(resultado)
                End Using
            End Using
        End Using

        Return resultado
    End Function

    Public Function procesar(sql As String,
                             ParamArray parametros() As SqlParameter) As Integer

        Using conexion As SqlConnection = abrir()
            Using comando As New SqlCommand(sql, conexion)

                If parametros IsNot Nothing AndAlso parametros.Length > 0 Then
                    comando.Parameters.AddRange(parametros)
                End If

                Return comando.ExecuteNonQuery()
            End Using
        End Using
    End Function

    Public Function mantenimiento_usuarios_USTR025826(
        datos As String(), accion As String) As Integer

        Select Case accion.ToLowerInvariant()

            Case "nuevo"
                Return procesar(
                    "INSERT INTO usuarios_USTR025826 " &
                    "(usuario,clave,nombre,direccion,telefono) " &
                    "VALUES (@u,@c,@n,@d,@t)",
                    P("@u", datos(1)),
                    P("@c", datos(2)),
                    P("@n", datos(3)),
                    P("@d", datos(4)),
                    P("@t", datos(5)))

            Case "modificar"
                Return procesar(
                    "UPDATE usuarios_USTR025826 SET " &
                    "usuario=@u,clave=@c,nombre=@n," &
                    "direccion=@d,telefono=@t " &
                    "WHERE idUsuario=@id",
                    P("@u", datos(1)),
                    P("@c", datos(2)),
                    P("@n", datos(3)),
                    P("@d", datos(4)),
                    P("@t", datos(5)),
                    P("@id", Integer.Parse(datos(0))))

            Case "eliminar"
                Return procesar(
                    "DELETE FROM usuarios_USTR025826 WHERE idUsuario=@id",
                    P("@id", Integer.Parse(datos(0))))

            Case Else
                Throw New ArgumentException("Acción no válida")
        End Select
    End Function

    Public Function administrarDatosAlumnos(
        datos As String(), accion As String) As Integer

        Select Case accion.ToLowerInvariant()

            Case "nuevo"
                Return procesar(
                    "INSERT INTO alumnos " &
                    "(codigo,nombre,direccion,telefono,email) " &
                    "VALUES (@c,@n,@d,@t,@e)",
                    P("@c", datos(1)),
                    P("@n", datos(2)),
                    P("@d", datos(3)),
                    P("@t", datos(4)),
                    P("@e", datos(5)))

            Case "modificar"
                Return procesar(
                    "UPDATE alumnos SET codigo=@c,nombre=@n," &
                    "direccion=@d,telefono=@t,email=@e " &
                    "WHERE idAlumno=@id",
                    P("@c", datos(1)),
                    P("@n", datos(2)),
                    P("@d", datos(3)),
                    P("@t", datos(4)),
                    P("@e", datos(5)),
                    P("@id", Integer.Parse(datos(0))))

            Case "eliminar"
                Return procesar(
                    "DELETE FROM alumnos WHERE idAlumno=@id",
                    P("@id", Integer.Parse(datos(0))))

            Case Else
                Throw New ArgumentException("Acción no válida")
        End Select
    End Function

    Public Function administrarDatosMaterias(
        datos As String(), accion As String) As Integer

        Select Case accion.ToLowerInvariant()

            Case "nuevo"
                Return procesar(
                    "INSERT INTO materias(codigo,nombre,uv) " &
                    "VALUES (@c,@n,@v)",
                    P("@c", datos(1)),
                    P("@n", datos(2)),
                    P("@v", Integer.Parse(datos(3))))

            Case "modificar"
                Return procesar(
                    "UPDATE materias SET codigo=@c,nombre=@n,uv=@v " &
                    "WHERE idMateria=@id",
                    P("@c", datos(1)),
                    P("@n", datos(2)),
                    P("@v", Integer.Parse(datos(3))),
                    P("@id", Integer.Parse(datos(0))))

            Case "eliminar"
                Return procesar(
                    "DELETE FROM materias WHERE idMateria=@id",
                    P("@id", Integer.Parse(datos(0))))

            Case Else
                Throw New ArgumentException("Acción no válida")
        End Select
    End Function

    Public Shared Function P(nombre As String,
                             valor As Object) As SqlParameter

        Return New SqlParameter(nombre, valor)
    End Function

End Class
