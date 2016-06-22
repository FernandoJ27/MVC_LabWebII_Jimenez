namespace Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;
    using System.Data.Entity;
    

    [Table("CATEGORIA")]
    public partial class CATEGORIA
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CATEGORIA()
        {
            PRODUCTO = new HashSet<PRODUCTO>();
        }

        [Key]
        public int IDCATEGORIA { get; set; }

        [Required]
        [StringLength(20)]
        public string NOMBRE { get; set; }

        [Column(TypeName = "text")]
        public string DESCRIPCION { get; set; }

        [Required]
        [StringLength(1)]
        public string ESTADO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRODUCTO> PRODUCTO { get; set; }

        /// <summary>
        /// Listar registros
        /// </summary>
        /// <returns>Retorna una colección de datos</returns>
        public List<CATEGORIA> Listar()
        {
            var categorias = new List<CATEGORIA>();

            try{
                using (var dbventas = new BasedeDatos())
                {
                    categorias = dbventas.CATEGORIA.ToList();
                }
            }
            catch(Exception e){
                throw e;
            }

            return categorias;
        }

        /// <summary>
        /// Buscar registro
        /// </summary>
        /// <returns>Retorna una categoría</returns>
        public CATEGORIA Obtener(int id)
        {
            var categoria = new CATEGORIA();

            try
            {
                using (var dbventas = new BasedeDatos())
                {
                    categoria = dbventas.CATEGORIA
                        .Include("PRODUCTO")
                        .Where(x => x.IDCATEGORIA == id)
                        .SingleOrDefault();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return categoria;
        }

        public void Guardar()
        {
            try
            {
                using (var dbventas = new BasedeDatos())
                {
                    if(this.IDCATEGORIA > 0)
                    {
                        dbventas.Entry(this).State = EntityState.Modified;
                    }else
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

        public List<CATEGORIA> Buscar(string criterio)
        {
            var categorias = new List<CATEGORIA>();
            string estado = "";

            estado = (criterio == "Activo") ? "A" : (criterio == "Inactivo") ? "I" : "";

            try
            {
                using (var dbventas = new BasedeDatos())
                {
                    categorias = dbventas.CATEGORIA
                                //.Where(x => x.NOMBRE.Contains(criterio))
                                .Where(x => x.NOMBRE.Contains(criterio) | x.ESTADO == estado)
                                .ToList();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return categorias;
        }

        public List<CATEGORIA> ListarProductoGroupBy()
        {
            var categorias = new List<CATEGORIA>();

            try
            {
                using (var dbventas = new BasedeDatos())
                {
                    categorias = dbventas.CATEGORIA.ToList();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return categorias;
        }

    }
}
