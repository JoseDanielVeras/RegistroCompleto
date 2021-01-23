using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;


namespace RegistroCompleto.Entidades
{
    public class Roles
    {
        [Key]
        
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }

        //Constructor
        public Roles()
        {
            Id = 0;
            Fecha = DateTime.Now;
            Descripcion = string.Empty;
        }
    }
}
