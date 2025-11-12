using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TariasS5.Models;

namespace TariasS5.Utiles
{
    public class PersonRepo
    {
        string dbRuta;
        private SQLiteConnection conn;
  
        public string mensaje { get; set; }

        public void init()
        { 
                if (conn is not null)
                    return;
                conn = new(dbRuta);
                conn.CreateTable<Persona>();
            
               
        }
        public PersonRepo(string ruta)
        {
            dbRuta = ruta;
        }

        //CRUD
        public void AddNewPerson(string _nombre)
        {
            int result = 0;
            try
            {
                //insertar la perosna
                init();
                if (string.IsNullOrEmpty(_nombre))
                    throw new Exception("EL NOMBRE ES REQUERIDO");
                Persona person = new() { Nombre = _nombre };
                result = conn.Insert(person);
                mensaje = string.Format("Dato Ingresado correctamente"+" "+ result+" "+_nombre);
            }
            catch (Exception ex)
            {

                mensaje = string.Format("Error ", ex.Message);
            }
        }

        //MOSTRAR /LISTAR
        public List<Persona> GetAllPeople()
        {
            try
            {
                init();
                return conn.Table<Persona>().ToList();
                mensaje =string.Format("Elementos Listados Correctamente");
            }
            catch (Exception ex)
            {
                mensaje = string.Format("Error ", ex.Message);

            }
            return new List<Persona>();
        }

        //UPDATE

        public void UpdatePersonByName(string oldName, string newName)
        {
            try
            {
                init();
                if (string.IsNullOrWhiteSpace(oldName) || string.IsNullOrWhiteSpace(newName))
                {
                    mensaje = "Nombre viejo y nuevo son requeridos.";
                    return;
                }

                var persona = conn.Table<Persona>().FirstOrDefault(p => p.Nombre == oldName);
                if (persona == null)
                {
                    mensaje = $"No se encontró persona con nombre '{oldName}'.";
                    return;
                }

                persona.Nombre = newName.Trim();
                int result = conn.Update(persona);
                mensaje = $"Persona actualizada correctamente ({result}) {oldName} = {persona.Nombre}";
            }
            catch (Exception ex)
            {
                mensaje = $"Error al actualizar: {ex.Message}";
            }
        }

        // DELETE
        public void DeletePersonByName(string name)
        {
            try
            {
                init();
                if (string.IsNullOrWhiteSpace(name))
                {
                    mensaje = "El nombre es requerido para eliminar.";
                    return;
                }

                var persona = conn.Table<Persona>().FirstOrDefault(p => p.Nombre == name);
                if (persona == null)
                {
                    mensaje = $"No se encontró persona con nombre '{name}'.";
                    return;
                }

                int result = conn.Delete(persona);
                mensaje = $"Persona eliminada correctamente ({result}) Nombre: {name}";
            }
            catch (Exception ex)
            {
                mensaje = $"Error al eliminar: {ex.Message}";
            }
        }
    }
}

