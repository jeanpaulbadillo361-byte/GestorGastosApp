using Microsoft.Data.Sqlite;

namespace GestorGastosApp.Data;

public class ConexionSqlite
{
    private readonly string _cadenaConexion;

    public ConexionSqlite(string nombreArchivo = "gastos.db")
    {
        _cadenaConexion = $"Data Source={nombreArchivo}";
        InicializarBaseDatos();
    }

    public SqliteConnection ObtenerConexion()
    {
        return new SqliteConnection(_cadenaConexion);
    }

    private void InicializarBaseDatos()
    {
        using var conexion = ObtenerConexion();
        conexion.Open();

        string sql = @"
            CREATE TABLE IF NOT EXISTS Gastos (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Descripcion TEXT NOT NULL,
                Categoria TEXT NOT NULL,
                Monto REAL NOT NULL,
                Fecha TEXT NOT NULL
            );";

        using var comando = new SqliteCommand(sql, conexion);
        comando.ExecuteNonQuery();
    }
}