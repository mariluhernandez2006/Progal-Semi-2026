using System.Data.SqlClient;

namespace academica
{
    class conexion
    {
        public static SqlConnection ObtenerConexion()
        {
            string cadena =
                @"Data Source=(localdb)\MSSQLLocalDB;" +
                "Initial Catalog=db_academica;" +
                "Integrated Security=True;";

            return new SqlConnection(cadena);
        }
    }
}