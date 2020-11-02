using Jean_P2_AP1.BLL;
using Jean_P2_AP1.Entidades;
using System;
using System.Collections.Generic;
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
        public rProyectos()
        {
            InitializeComponent();

            TareasComboBox.ItemsSource = TiposTareaBLL.ObtenerLista(t => true);
            TareasComboBox.SelectedValuePath = "TipoId";
            TareasComboBox.DisplayMemberPath = "Descripcion";

            proyecto = new Proyectos();
            DataContext = proyecto;
        }

        public void Limpiar()
        {
            proyecto = new Proyectos();
            DataContext = proyecto;

            Actualizar();
        }

        public void Actualizar()
        {
        //    DataContext = null;
        //    DataContext = proyecto;

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

            proyecto.Detalle.Add(detalle);
            proyecto.TiempoTotal += detalle.Tiempo;

            Actualizar();
        }

        private void RemoverBoton_Click(object sender, RoutedEventArgs e)
        {
            if(TareasDataGrid.SelectedIndex >= TareasDataGrid.Items.Count - 1)
            {
                proyecto.Detalle.Remove((ProyectosDetalle)TareasDataGrid.SelectedItem);
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
            return true;
        }

        public bool ValidarProyecto()
        {
            return true;
        }
    }
}
