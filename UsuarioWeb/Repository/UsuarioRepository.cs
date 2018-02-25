using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UsuarioWeb.Models;

namespace UsuarioWeb.Repository
{
    public class UsuarioRepository
    {
        public List<Usuario> getUsuariosAll()
        {
            List<Usuario> usuarios = new List<Usuario>();

            string connectionString = ConfigurationManager.ConnectionStrings["UsuarioStringConnection"].ToString();//Esta es la alinea del archivo web.config de conexion! 

            string sqlString = "Select * From Usuario ";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sqlString, conn))
                {
                    cmd.CommandType = CommandType.Text;

                    conn.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Usuario u1 = new Usuario();
                        u1.Id = Convert.ToInt32(reader["Id"].ToString());
                        u1.Nombre = reader["Nombre"].ToString();                        
                        u1.Telefono = reader["Telefono"].ToString();
                        u1.Email = reader["Email"].ToString();

                        usuarios.Add(u1);
                    }
                    reader.Close();
                }
            }

            return usuarios;
        }

        public int insertUsuario(Usuario x)//Sql_inyeccion!
        {
            //out of the box = (librerias) por defecto = ya viene incluido
            string connectionString = ConfigurationManager.ConnectionStrings["UsuarioStringConnection"].ToString();

            string sqlString = "Insert into Usuario (Nombre,Telefono, Email) values (@nombre, @telefono, @email) ";
            int retorno = 0;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sqlString, conn))
                {
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@nombre", x.Nombre);
                    cmd.Parameters.AddWithValue("@telefono", x.Telefono);
                    cmd.Parameters.AddWithValue("@email", x.Email);
                    

                    conn.Open();

                    retorno = cmd.ExecuteNonQuery(); //esta funcion es para INSERT,UPDATE,DELETE y me devuelve el total de filas afectadas
                }
            }

            return retorno;
        }


        public int insertUsuario1(Usuario x)// no Sql_inyeccion!
        {
            //out of the box = (librerias) por defecto = ya viene incluido
            string connectionString = ConfigurationManager.ConnectionStrings["UsuarioStringConnection"].ToString();

            string sqlString = "Insert into Usuario (Nombre,Telefono, Email) values ('" + x.Nombre + "', '" + x.Telefono + "', '" + x.Email + "') ";
            int retorno = 0;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sqlString, conn))
                {
                    cmd.CommandType = CommandType.Text;

                    conn.Open();

                    retorno = cmd.ExecuteNonQuery(); //esta funcion es para INSERT,UPDATE,DELETE y me devuelve el total de filas afectadas
                }
            }

            return retorno;
        }


        public Usuario getUsuarioById(int x) //Con seguridad contra inyeccion SQL
        {

            string connectionString = ConfigurationManager.ConnectionStrings["UsuarioStringConnection"].ToString();

            Usuario u1 = new Usuario();
            //Inyeccion SQL
            string sqlString = "Select Id, Nombre, Telefono, Email From Usuario WHERE Id = @id";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sqlString, conn))
                {
                    cmd.CommandType = CommandType.Text;

                    //Inyeccion SQL
                    cmd.Parameters.AddWithValue("@id", x);

                    conn.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {

                        u1.Id = Convert.ToInt32(reader["Id"].ToString());
                        u1.Nombre = reader["Nombre"].ToString();
                        u1.Telefono = reader["Telefono"].ToString();
                        u1.Email = reader["Email"].ToString();
                    }
                    else
                    {
                        throw new Exception("El Id " + x + " no existe en la tabla de Usuarios");
                    }
                    reader.Close();
                }
            }

            return u1;
        }

        public int updateUsuario(Usuario x) //Sql_inyeccion
        {
            //out of the box = (librerias) por defecto = ya viene incluido
            string connectionString = ConfigurationManager.ConnectionStrings["UsuarioStringConnection"].ToString();

            string sqlString = "UPDATE Usuario SET Nombre = @nombre, Telefono = @telefono, Email = @email WHERE Id = @id ";
            int retorno = 0;

            
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sqlString, conn))
                {
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@id", x.Id);
                    cmd.Parameters.AddWithValue("@nombre", x.Nombre);
                    cmd.Parameters.AddWithValue("@telefono", x.Telefono);
                    cmd.Parameters.AddWithValue("@email", x.Email);
                    

                    conn.Open();

                    retorno = cmd.ExecuteNonQuery(); //esta funcion es para INSERT,UPDATE,DELETE y me devuelve el total de filas afectadas
                }
            }

            return retorno;
        }

        public int updateUsuario1(Usuario x) // no Sql_inyeccion!
        {
            //out of the box = (librerias) por defecto = ya viene incluido
            string connectionString = ConfigurationManager.ConnectionStrings["UsuarioStringConnection"].ToString();

            string sqlString = "UPDATE Usuario SET Nombre='" + x.Nombre + "', Telefono='" + x.Telefono + "', Email='" + x.Email + "' WHERE ID= '" + x.Id + "'";
            int retorno = 0;


            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sqlString, conn))
                {
                    cmd.CommandType = CommandType.Text;

                    conn.Open();

                    retorno = cmd.ExecuteNonQuery(); //esta funcion es para INSERT,UPDATE,DELETE y me devuelve el total de filas afectadas
                }
            }

            return retorno;
        }

        public int deleteUsuario(int id) //Sql_inyeccion!
        {
            //out of the box = (librerias) por defecto = ya viene incluido
            string connectionString = ConfigurationManager.ConnectionStrings["UsuarioStringConnection"].ToString();

            string sqlString = "DELETE FROM Usuario WHERE Id= @id";  //PENDIENTE CORREGIR CON SQL INJECCION
            int retorno = 0;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sqlString, conn))
                {
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@id", id);
                    conn.Open();

                    retorno = cmd.ExecuteNonQuery(); //esta funcion es para INSERT,UPDATE,DELETE y me devuelve el total de filas afectadas
                }
            }

            return retorno;
        }
    }
}