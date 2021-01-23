using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RegistroCompleto.Entidades;
using RegistroCompleto.DAL;

namespace RegistroCompleto
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //Esta funcion verifica el id ya existe.
        public static bool Existe(int id)
        {
            Contexto contexto = new Contexto();
            bool encontrado = false;

            try
            {
                encontrado = contexto.Roles.Any(e => e.Id == id);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return encontrado;
        }
        
        public static bool validarDescripcion(string cadena)
        {
            Contexto contexto = new Contexto();
            bool encontrado = false;

            try
            {
                encontrado = contexto.Roles.Any(e => e.Descripcion == cadena);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            if(encontrado == true)
                MessageBox.Show("La descripsion es la misma, eso no se puede repetir");

            return encontrado;
        }
        
        //Esta funcion busca mediante el boton guardar el id a modificar.
        public static bool Modificar(Roles rol)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                //Marcar la entidad como modificada para que el contexto sepa como proceder
                contexto.Entry(rol).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                paso = contexto.SaveChanges() > 0;

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return paso;
        }

        //Esta funcion mediante el boton eliminar busca y elimina.
        public static bool Eliminar(int id)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                //Busca la entidad que decea eliminar
                var rol = contexto.Roles.Find(id);

                if (rol != null)
                {
                    contexto.Roles.Remove(rol); //Se elimina la entidad
                    paso = contexto.SaveChanges() > 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return paso;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Contexto contexto = new Contexto();
            Roles rol = new Roles();

            if (!Existe(Convert.ToInt32(IdRolTextBox.Text))) //Si no existe el id devuelve False, ! de Falce es true y continua el if.
            {
                if(!validarDescripcion(DescripcionTextBox.Text))
                {
                    rol.Id = Convert.ToInt32(IdRolTextBox.Text);
                    rol.Fecha = Convert.ToDateTime(FechaCreacionMaskedTextBox.Text);
                    rol.Descripcion = DescripcionTextBox.Text;

                    contexto.Roles.Add(rol);
                    contexto.SaveChanges();
                    dataGridView1.DataSource = contexto.Roles.ToList();
                    contexto.Dispose();
                }
               
            }
            else //Si el id existe lo niega y continua el else.
            {
                MessageBox.Show("Ya existe ese Id");
            }
        }

        private void EditarButton_Click(object sender, EventArgs e)
        {
            Contexto contexto = new Contexto();
            Roles rol = new Roles();

            if (Existe(Convert.ToInt32(IdRolTextBox.Text))) //Si encuentra el id continua el if y se realizan los cambios
            {
                rol.Id = Convert.ToInt32(IdRolTextBox.Text);
                rol.Fecha = Convert.ToDateTime(FechaCreacionMaskedTextBox.Text);
                rol.Descripcion = DescripcionTextBox.Text;
                
                if(!validarDescripcion(DescripcionTextBox.Text))
                {
                    Modificar(rol);
                    contexto.SaveChanges();
                    dataGridView1.DataSource = contexto.Roles.ToList();
                    contexto.Dispose();
                }                
            }
            else //Si no encuentra el id continua el else
            {
                MessageBox.Show("Este Id no existe");
            }
        }

        private void EliminarButton_Click(object sender, EventArgs e)
        {
            Contexto contexto = new Contexto();
            Eliminar(Convert.ToInt32(IdRolTextBox.Text));
            dataGridView1.DataSource = contexto.Roles.ToList();
        }
    }
}
