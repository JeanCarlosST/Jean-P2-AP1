using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Jean_P2_AP1.Entidades
{
    public class TiposTarea
    {
        [Key]
        public int TipoId { get; set; }
        public string Descripcion { get; set; }

        [ForeignKey("TipoId")]
        public virtual List<ProyectosDetalle> Detalle { get; set; }

        public TiposTarea()
        {
            Detalle = new List<ProyectosDetalle>();
        }

    }
}
