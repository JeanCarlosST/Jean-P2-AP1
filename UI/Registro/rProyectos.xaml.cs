using Jean_P2_AP1.BLL;
using Jean_P2_AP1.Entidades;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Jean_P2_AP1.UI.Registro
{
    /// <summary>
    /// Interaction logic for r.xaml
    /// </summary>
    public partial class rProyectos : Window
    {
        Proyectos proyecto;
        List<TareaDetalle> detalle;

        public rProyectos()
        {
            InitializeComponent();

            TareasComboBox.ItemsSource = TiposTareaBLL.ObtenerLista(t => true);
            TareasComboBox.SelectedValuePath = "TipoId";
            TareasComboBox.DisplayMemberPath = "Descripcion";

            detalle = new List<TareaDetalle>();

            proyecto = new Proyectos();
            DataContext = null;
            DataContext = proyecto;
        }

        public void Limpiar()
        {
            proyecto = new Proyectos();
            DataContext = null;
            DataContext = proyecto;
            detalle = new List<TareaDetalle>();

            Actualizar();
        }

        public void Actualizar()
        {
            DescripcionTextBox.Text = proyecto.Descripcion;
            TiempoTotalTextBox.Text = proyecto.TiempoTotal.ToString();

            TareasDataGrid.ItemsSource = null;
            TareasDataGrid.ItemsSource = detalle;

            TareasComboBox.SelectedIndex = -1;
            RequerimientoTextBox.Clear();
            TiempoTextBox.Clear();
        }

        private void BuscarBoton_Click(object sender, RoutedEventArgs e)
        {
            var p = ProyectosBLL.Buscar(Utilities.ToInt(ProyectoIdTextBox.Text));

            if(p != null)
            {
                proyecto = p;

                foreach (ProyectosDetalle t in proyecto.Detalle)
                {
                    detalle.Add(new TareaDetalle(t));
                }

                Actualizar();
            }
            else
            {
                MessageBox.Show("Proyecto no encontrado", "Registro de proyectos");
            }
             
        }

        private void AgregarBoton_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidarTarea())
                return;

            ProyectosDetalle detalle = new ProyectosDetalle()
            {
                ProyectoId = Utilities.ToInt(ProyectoIdTextBox.Text),
                TipoId = Utilities.ToInt(TareasComboBox.SelectedValue.ToString()),
                Requerimiento = RequerimientoTextBox.Text,
                Tiempo = Utilities.ToDouble(TiempoTextBox.Text)
            };

            this.detalle.Add(new TareaDetalle(detalle));

            proyecto.Detalle.Add(detalle);
            proyecto.TiempoTotal += detalle.Tiempo;

            Actualizar();
        }

        private void RemoverBoton_Click(object sender, RoutedEventArgs e)
        {
            if(TareasDataGrid.SelectedIndex >= TareasDataGrid.Items.Count - 1)
            {
                TareaDetalle tarea = (TareaDetalle)TareasDataGrid.SelectedItem;

                proyecto.Detalle.Remove(
                    proyecto.Detalle.Find(t =>   
                        t.TipoId == tarea.TipoId &&
                        t.Requerimiento.Equals(tarea.Requerimiento)
                    )
                );

                proyecto.TiempoTotal -= tarea.Tiempo;

                detalle.Remove(tarea);

                Actualizar();
            }
        }

        private void NuevoBoton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void GuardarBoton_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidarProyecto())
                return;

            if(ProyectosBLL.Guardar(proyecto))
            {
                Limpiar();
                MessageBox.Show("Proyecto guardado correctamente", "Registro de proyectos");
            }
            else
            {
                MessageBox.Show("Hubo un error, no se puedo guardar", "Registro de proyectos", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EliminarBoton_Click(object sender, RoutedEventArgs e)
        {
            int id = Utilities.ToInt(ProyectoIdTextBox.Text);

            Limpiar();

            if (ProyectosBLL.Eliminar(id))
                MessageBox.Show("Proyecto eliminado correctamente", "Registro de proyectos");
            else
                MessageBox.Show("Hubo un error, no se puedo eliminar", "Registro de proyectos", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public bool ValidarTarea()
        {
            if(TareasComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un tipo de tarea", "Registro de proyectos");
                return false;
            }
            if(RequerimientoTextBox.Text.Length == 0)
            {
                MessageBox.Show("Ingrese un requerimiento para esta tarea", "Registro de proyectos");
                return false;
            }
            if(TiempoTextBox.Text.Length == 0)
            {
                MessageBox.Show("Ingrese un tiempo para esta tarea", "Registro de proyectos");
                return false;
            }
            if(Utilities.ToInt(TiempoTextBox.Text) == 0)
            {
                MessageBox.Show("Ingrese un tiempo para esta tarea que sea válido o mayor a 0", "Registro de proyectos");
                return false;
            }

            return true;
        }

        public bool ValidarProyecto()
        {
            if(ProyectoIdTextBox.Text.Length == 0 || ProyectoIdTextBox.Text.Any(char.IsLetter))
            {
                MessageBox.Show("Ingrese un ID para el proyecto que sea válido o mayor a 0", "Registro de proyectos");
                return false;
            }
            if(DescripcionTextBox.Text.Length == 0)
            {
                MessageBox.Show("Ingrese un descripción para este proyecto", "Registro de proyectos");
                return false;
            }
            if(proyecto.Detalle.Count == 0)
            {
                MessageBox.Show("Ingrese por lo menos una tarea", "Registro de proyectos");
                return false;
            }
        
            return true;
        }
    }

    public class TareaDetalle
    {
        public int ProyectosDetalleId { get; set; }
        public int ProyectoId { get; set; }
        public int TipoId { get; set; }
        public string Tipo { get; set; }
        public string Requerimiento { get; set; }
        public double Tiempo { get; set; }


        public TareaDetalle(ProyectosDetalle detalle)
        {
            ProyectosDetalleId = detalle.ProyectosDetalleId;
            ProyectoId = detalle.ProyectoId;
            TipoId = detalle.TipoId;
            Tipo = TiposTareaBLL.Buscar(TipoId).Descripcion;
            Requerimiento = detalle.Requerimiento;
            Tiempo = detalle.Tiempo;
        }
    }
}
