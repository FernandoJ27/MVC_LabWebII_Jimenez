namespace Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;
    using System.Data.Entity;
    using System.IO;
    using System.Web;
    using System.Data.Entity.Validation;
    [Table("USUARIO")]
    public partial class USUARIO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public USUARIO()
        {
            PEDIDO = new HashSet<PEDIDO>();
        }

        [Key]
        public int IDUSUARIO { get; set; }

        [Required]
        public int IDTIPOUSUARIO { get; set; }

        [StringLength(30)]
        public string NOMBREUSU { get; set; }

        [StringLength(50)]
        public string PASSWORD { get; set; }

        [StringLength(20)]
        public string NOMBRE { get; set; }

        [StringLength(50)]
        public string APELLIDOS { get; set; }

        [StringLength(50)]
        public string EMAIL { get; set; }

        [StringLength(1)]
        public string ESTADO { get; set; }

        [StringLength(250)]
        public string FOTO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PEDIDO> PEDIDO { get; set; }

        public virtual TIPO_USUARIO TIPO_USUARIO { get; set; }


        /// <summary>
        /// Listar registros
        /// </summary>
        /// <returns>Retorna una colecci�n de datos</returns>
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
        /// <returns>Retorna un usuario</returns>
        //public USUARIO Obtener(int id)
        //{
        //    var usuario = new USUARIO();

        //    try
        //    {
        //        using (var dbventas = new BasedeDatos())
        //        {
        //            usuario = dbventas.USUARIO
        //                    .Include("TIPO_USUARIO")
        //                    .Where(x => x.IDTIPOUSUARIO == id)
        //                    .SingleOrDefault();
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        throw;
        //    }

        //    return usuario;
        //}

        public USUARIO Obtener(int id)
        {
            var usuario = new USUARIO();
            try
            {
                using (var db_ventas = new BasedeDatos())
                {
                    usuario = db_ventas.USUARIO
                              .Where(x => x.IDUSUARIO == id)
                              .SingleOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return usuario;
        }

        public void Guardar()
        {
            try
            {
                using (var dbventas = new BasedeDatos())
                {
                    if (this.IDUSUARIO == 0)
                    {
                        dbventas.Entry(this).State = EntityState.Added;
                    }
                    else
                    {
                        dbventas.Entry(this).State = EntityState.Modified;
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

        public List<USUARIO> Buscar(string criterio)
        {
            var usuario = new List<USUARIO>();
            string estado = "";

            estado = (criterio == "Activo") ? "A" : (criterio == "Inactivo") ? "I" : "";

            try
            {
                using (var dbventas = new BasedeDatos())
                {
                    usuario = dbventas.USUARIO
                                //.Where(x => x.IDUSUARIO.Contains(criterio) | x.ESTADO == estado)
                                .ToList();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return usuario;
        }

        public ResponseModel Acceder(string Email, string Password)
        {
            var rm = new ResponseModel();

            try
            {
                using (var dbventas = new BasedeDatos())
                {
                    Password = HashHelper.MD5(Password);
                    var usuario = dbventas.USUARIO
                                .Where(x => x.EMAIL == Email)
                                .Where(x => x.PASSWORD == Password)
                                .SingleOrDefault();

                    if (usuario != null)
                    {
                        SessionHelper.AddUserToSession(usuario.IDUSUARIO.ToString());
                        rm.SetResponse(true);
                    }
                    else
                    {
                        rm.SetResponse(false, "Email o Password incorrecto");
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return rm;
        }

        public ResponseModel UploadFoto(HttpPostedFileBase foto)
        {
            var rm = new ResponseModel();

            try
            {
                using (var dbventas = new BasedeDatos())
                {
                    dbventas.Configuration.ValidateOnSaveEnabled = false;

                    var eUsuario = dbventas.Entry(this);
                    eUsuario.State = EntityState.Modified;

                    if (foto != null)
                    {
                        string archivo = Path.GetFileName(foto.FileName) + Path.GetExtension(foto.FileName);

                        foto.SaveAs(HttpContext.Current.Server.MapPath("~/Uploads/" + archivo));

                        this.FOTO = archivo;
                    }
                    else eUsuario.Property(x => x.FOTO).IsModified = false;
                    
                    if (this.NOMBREUSU == null) eUsuario.Property(x => x.NOMBREUSU).IsModified = false;
                    if (this.PASSWORD == null) eUsuario.Property(x => x.PASSWORD).IsModified = false;

                    dbventas.SaveChanges();
                    rm.SetResponse(true);
                }
            } catch(DbEntityValidationException ex)
            {
                throw ex;
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return rm;
        }
    }
}
