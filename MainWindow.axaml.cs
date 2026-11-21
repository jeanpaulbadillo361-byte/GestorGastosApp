using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using GestorGastosApp.Data;
using GestorGastosApp.Models;
using GestorGastosApp.Services;

namespace GestorGastosApp;

public partial class MainWindow : Window
{
    private readonly GastoServicio _servicio;
    private int _idSeleccionado = 0;

    public MainWindow()
    {
        InitializeComponent();

        var conexion = new ConexionSqlite();
        var dao = new GastoDao(conexion);
        _servicio = new GastoServicio(dao);

        LimpiarFormulario();
        CargarTabla();
    }

    private void CargarTabla()
    {
        DgGastos.ItemsSource = _servicio.ListarGastos();
    }

    private void BtnGuardar_Click(object? sender, RoutedEventArgs e)
    {
        try
        {
            string? categoria = (CmbCategoria.SelectedItem as ComboBoxItem)?.Content?.ToString();
            var resultado = _servicio.RegistrarGasto(
                TxtDescripcion.Text ?? string.Empty,
                categoria,
                TxtMonto.Text ?? string.Empty,
                TxtFecha.Text ?? string.Empty
            );

            MostrarMensaje(resultado.Mensaje, resultado.Exito);

            if (resultado.Exito)
            {
                LimpiarFormulario();
                CargarTabla();
            }
        }
        catch (Exception ex)
        {
            MostrarMensaje($"Error al guardar: {ex.Message}", false);
        }
    }

    private void BtnActualizar_Click(object? sender, RoutedEventArgs e)
    {
        try
        {
            string? categoria = (CmbCategoria.SelectedItem as ComboBoxItem)?.Content?.ToString();
            var resultado = _servicio.ActualizarGasto(
                _idSeleccionado,
                TxtDescripcion.Text ?? string.Empty,
                categoria,
                TxtMonto.Text ?? string.Empty,
                TxtFecha.Text ?? string.Empty
            );

            MostrarMensaje(resultado.Mensaje, resultado.Exito);

            if (resultado.Exito)
            {
                LimpiarFormulario();
                CargarTabla();
            }
        }
        catch (Exception ex)
        {
            MostrarMensaje($"Error al actualizar: {ex.Message}", false);
        }
    }

    private void BtnEliminar_Click(object? sender, RoutedEventArgs e)
    {
        try
        {
            var resultado = _servicio.EliminarGasto(_idSeleccionado);
            MostrarMensaje(resultado.Mensaje, resultado.Exito);

            if (resultado.Exito)
            {
                LimpiarFormulario();
                CargarTabla();
            }
        }
        catch (Exception ex)
        {
            MostrarMensaje($"Error al eliminar: {ex.Message}", false);
        }
    }

    private void BtnLimpiar_Click(object? sender, RoutedEventArgs e)
    {
        LimpiarFormulario();
        MostrarMensaje("Formulario limpio.", true);
    }

    private void DgGastos_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (DgGastos.SelectedItem is Gasto seleccionado)
        {
            _idSeleccionado = seleccionado.Id;
            TxtDescripcion.Text = seleccionado.Descripcion;
            TxtMonto.Text = seleccionado.Monto.ToString("0.##");
            TxtFecha.Text = seleccionado.Fecha;

            foreach (var item in CmbCategoria.Items)
            {
                if (item is ComboBoxItem comboItem && comboItem.Content?.ToString() == seleccionado.Categoria)
                {
                    CmbCategoria.SelectedItem = comboItem;
                    break;
                }
            }
        }
    }

    private void LimpiarFormulario()
    {
        _idSeleccionado = 0;
        TxtDescripcion.Text = string.Empty;
        CmbCategoria.SelectedIndex = 0;
        TxtMonto.Text = string.Empty;
        TxtFecha.Text = DateTime.Now.ToString("yyyy-MM-dd");
        DgGastos.SelectedItem = null;
    }

    private void MostrarMensaje(string texto, bool esExito)
    {
        TxtMensaje.Text = texto;
        TxtMensaje.Foreground = esExito ? Brushes.DarkGreen : Brushes.DarkRed;
    }
}