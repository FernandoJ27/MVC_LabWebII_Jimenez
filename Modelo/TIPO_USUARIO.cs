namespace Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class TIPO_USUARIO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TIPO_USUARIO()
        {
            USUARIO = new HashSet<USUARIO>();
        }

        [Key]
        public int IDTIPOUSUARIO { get; set; }

        [StringLength(20)]
        public string NOMBRE { get; set; }

        [StringLength(1)]
        public string ESTADO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<USUARIO> USUARIO { get; set; }

        public List<TIPO_USUARIO> Listar()
        {
            var tipos = new List<TIPO_USUARIO>();

            try
            {
                using (var dbventas = new BasedeDatos())
                {
                    tipos = dbventas.TIPO_USUARIO.ToList();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return tipos;
        }

        public AnexGRIDResponde ListarAnexGRID(AnexGRID grilla)
        {
           
            try
            {
                using (var dbventas = new BasedeDatos())
                {
                    grilla.Inicializar();

                    var tipos = dbventas.TIPO_USUARIO.Where(x => x.IDTIPOUSUARIO > 0);

                    //ordernar por columnas
                    if (grilla.columna == "IDTIPOUSUARIO")
                    {
                        tipos = grilla.columna_orden == "DESC" ? tipos.OrderByDescending(x => x.IDTIPOUSUARIO) 
                                                               : tipos.OrderBy(x => x.IDTIPOUSUARIO);
                    }

                    if (grilla.columna == "NOMBRE")
                    {
                        tipos = grilla.columna_orden == "DESC" ? tipos.OrderByDescending(x => x.NOMBRE)
                                                               : tipos.OrderBy(x => x.NOMBRE);
                    }

                    if (grilla.columna == "ESTADO")
                    {
                        tipos = grilla.columna_orden == "DESC" ? tipos.OrderByDescending(x => x.ESTADO)
                                                               : tipos.OrderBy(x => x.ESTADO);
                    }


                    var tipousuario = tipos.Skip(grilla.pagina)
                                           .Take(grilla.limite)
                                           .ToList();


                    grilla.SetData(from t in tipousuario
                                   select new
                                   {
                                       t.IDTIPOUSUARIO,
                                       t.NOMBRE,
                                       t.ESTADO
                                   },
                                  //resultset)
                                  tipos.Count()
                                  );

                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return grilla.responde();
        }

        public List<TIPO_USUARIO> Buscar(string criterio)
        {
            var tipos = new List<TIPO_USUARIO>();
            string estado = "";

            estado = (criterio == "Activo") ? "A" : (criterio == "Inactivo") ? "I" : "";

            try
            {
                using (var dbventas = new BasedeDatos())
                {
                    tipos = dbventas.TIPO_USUARIO
                                .Where(x => x.NOMBRE.Contains(criterio) | x.ESTADO == estado)
                                .ToList();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return tipos;
        }

        public TIPO_USUARIO Obtener(int id)
        {
            var tipo = new TIPO_USUARIO();

            try
            {
                using (var dbventas = new BasedeDatos())
                {
                    tipo = dbventas.TIPO_USUARIO
                        .Where(x => x.IDTIPOUSUARIO == id)
                        .SingleOrDefault();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return tipo;
        }

        public void Guardar()
        {
            try
            {
                using (var dbventas = new BasedeDatos())
                {
                    if (this.IDTIPOUSUARIO > 0)
                    {
                        dbventas.Entry(this).State = EntityState.Modified;
                    }
                    else
                    {
                        dbventas.Entry(this).State = EntityState.Added;
                    }
                    dbventas.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Eliminar()
        {
            try
            {
                using (var dbventas = new BasedeDatos())
                {
                    dbventas.Entry(this).State = EntityState.Deleted;
                    dbventas.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
