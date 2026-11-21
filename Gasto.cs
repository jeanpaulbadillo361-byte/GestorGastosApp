using System;

namespace GestorGastosApp.Models;

public class Gasto
{
    private decimal _monto;

    public int Id { get; set; }
    public string Descripcion { get; set; } = string.Empty;
    public string Categoria { get; set; } = string.Empty;
    public string Fecha { get; set; } = DateTime.Now.ToString("yyyy-MM-dd");

    public decimal Monto
    {
        get => _monto;
        set
        {
            if (value < 0)
                throw new ArgumentException("El monto no puede ser negativo.");
            _monto = value;
        }
    }

    public Gasto() { }

    public Gasto(string descripcion, string categoria, decimal monto, string fecha)
    {
        Descripcion = descripcion;
        Categoria = categoria;
        Monto = monto;
        Fecha = fecha;
    }

    public Gasto(int id, string descripcion, string categoria, decimal monto, string fecha)
        : this(descripcion, categoria, monto, fecha)
    {
        Id = id;
    }
}