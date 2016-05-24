namespace Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("USUARIO")]
    public partial class USUARIO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public USUARIO()
        {
            PEDIDO = new HashSet<PEDIDO>();
        }

        [Key]
        [StringLength(20)]
        public string IDUSUARIO { get; set; }

        public int IDTIPOUSUARIO { get; set; }

        [StringLength(20)]
        public string PASSWORD { get; set; }

        [StringLength(20)]
        public string NOMBRE { get; set; }

        [StringLength(20)]
        public string APELLIDOS { get; set; }

        [StringLength(50)]
        public string EMAIL { get; set; }

        [StringLength(1)]
        public string ESTADO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PEDIDO> PEDIDO { get; set; }

        public virtual TIPO_USUARIO TIPO_USUARIO { get; set; }


        /// <summary>
        /// Listar registros
        /// </summary>
        /// <returns>Retorna una colección de datos</returns>
        public List<USUARIO> Listar()
        {
            var usuarios = new List<USUARIO>();

            try
            {
                using (var dbventas = new BasedeDatos())
                {
                    usuarios = dbventas.USUARIO.ToList();
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return usuarios;
        }

        /// <summary>
        /// Buscar registro
        /// </summary>
        /// <returns>Retorna una categoría</returns>
        public USUARIO Obtener(int id)
        {
            var usuario = new USUARIO();

            try
            {
                using (var dbventas = new BasedeDatos())
                {
                    usuario = dbventas.USUARIO
                        .Include("TIPO_USUARIO")
                        .Where(x => x.IDTIPOUSUARIO == id)
                        .SingleOrDefault();
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return usuario;
        }
    }
}
