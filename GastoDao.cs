using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using GestorGastosApp.Models;

namespace GestorGastosApp.Data;

public class GastoDao
{
    private readonly ConexionSqlite _conexionSqlite;

    public GastoDao(ConexionSqlite conexionSqlite)
    {
        _conexionSqlite = conexionSqlite;
    }

    public void Insertar(Gasto gasto)
    {
        using var conexion = _conexionSqlite.ObtenerConexion();
        conexion.Open();

        string sql = @"
            INSERT INTO Gastos (Descripcion, Categoria, Monto, Fecha)
            VALUES (@descripcion, @categoria, @monto, @fecha);";

        using var comando = new SqliteCommand(sql, conexion);
        comando.Parameters.AddWithValue("@descripcion", gasto.Descripcion);
        comando.Parameters.AddWithValue("@categoria", gasto.Categoria);
        comando.Parameters.AddWithValue("@monto", Convert.ToDouble(gasto.Monto));
        comando.Parameters.AddWithValue("@fecha", gasto.Fecha);
        comando.ExecuteNonQuery();
    }

    public List<Gasto> ObtenerTodos()
    {
        var lista = new List<Gasto>();

        using var conexion = _conexionSqlite.ObtenerConexion();
        conexion.Open();

        string sql = "SELECT Id, Descripcion, Categoria, Monto, Fecha FROM Gastos ORDER BY Id DESC;";
        using var comando = new SqliteCommand(sql, conexion);
        using var lector = comando.ExecuteReader();

        while (lector.Read())
        {
            var gasto = new Gasto(
                lector.GetInt32(0),
                lector.GetString(1),
                lector.GetString(2),
                Convert.ToDecimal(lector.GetDouble(3)),
                lector.GetString(4)
            );
            lista.Add(gasto);
        }

        return lista;
    }

    public void Actualizar(Gasto gasto)
    {
        using var conexion = _conexionSqlite.ObtenerConexion();
        conexion.Open();

        string sql = @"
            UPDATE Gastos
            SET Descripcion = @descripcion,
                Categoria = @categoria,
                Monto = @monto,
                Fecha = @fecha
            WHERE Id = @id;";

        using var comando = new SqliteCommand(sql, conexion);
        comando.Parameters.AddWithValue("@descripcion", gasto.Descripcion);
        comando.Parameters.AddWithValue("@categoria", gasto.Categoria);
        comando.Parameters.AddWithValue("@monto", Convert.ToDouble(gasto.Monto));
        comando.Parameters.AddWithValue("@fecha", gasto.Fecha);
        comando.Parameters.AddWithValue("@id", gasto.Id);
        comando.ExecuteNonQuery();
    }

    public void Eliminar(int id)
    {
        using var conexion = _conexionSqlite.ObtenerConexion();
        conexion.Open();

        string sql = "DELETE FROM Gastos WHERE Id = @id;";
        using var comando = new SqliteCommand(sql, conexion);
        comando.Parameters.AddWithValue("@id", id);
        comando.ExecuteNonQuery();
    }
}