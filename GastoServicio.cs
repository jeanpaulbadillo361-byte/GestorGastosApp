using System;
using System.Collections.Generic;
using System.Globalization;
using GestorGastosApp.Data;
using GestorGastosApp.Models;

namespace GestorGastosApp.Services;

public class GastoServicio
{
    private readonly GastoDao _gastoDao;

    public GastoServicio(GastoDao gastoDao)
    {
        _gastoDao = gastoDao;
    }

    public (bool Exito, string Mensaje) RegistrarGasto(string descripcion, string? categoria, string montoTexto, string fecha)
    {
        if (!ValidarCampos(descripcion, categoria, montoTexto, fecha, out decimal monto, out string error))
            return (false, error);

        var nuevoGasto = new Gasto(descripcion.Trim(), categoria!, monto, fecha.Trim());
        _gastoDao.Insertar(nuevoGasto);
        return (true, "Gasto registrado correctamente.");
    }

    public List<Gasto> ListarGastos()
    {
        return _gastoDao.ObtenerTodos();
    }

    public (bool Exito, string Mensaje) ActualizarGasto(int id, string descripcion, string? categoria, string montoTexto, string fecha)
    {
        if (id <= 0)
            return (false, "Selecciona primero un registro de la tabla para actualizar.");

        if (!ValidarCampos(descripcion, categoria, montoTexto, fecha, out decimal monto, out string error))
            return (false, error);

        var gastoEditado = new Gasto(id, descripcion.Trim(), categoria!, monto, fecha.Trim());
        _gastoDao.Actualizar(gastoEditado);
        return (true, "Gasto actualizado correctamente.");
    }

    public (bool Exito, string Mensaje) EliminarGasto(int id)
    {
        if (id <= 0)
            return (false, "Selecciona primero un registro de la tabla para eliminar.");

        _gastoDao.Eliminar(id);
        return (true, "Gasto eliminado correctamente.");
    }

    private bool ValidarCampos(string descripcion, string? categoria, string montoTexto, string fecha, out decimal monto, out string error)
    {
        monto = 0;
        error = string.Empty;

        if (string.IsNullOrWhiteSpace(descripcion))
        {
            error = "La descripción es obligatoria.";
            return false;
        }

        if (string.IsNullOrWhiteSpace(categoria))
        {
            error = "Debes seleccionar una categoría.";
            return false;
        }

        if (!decimal.TryParse(montoTexto.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out monto) || monto <= 0)
        {
            error = "Ingresa un monto numérico válido mayor a 0.";
            return false;
        }

        if (string.IsNullOrWhiteSpace(fecha) || !DateTime.TryParse(fecha, out _))
        {
            error = "Ingresa una fecha válida (ejemplo: 2026-11-21).";
            return false;
        }

        return true;
    }
}