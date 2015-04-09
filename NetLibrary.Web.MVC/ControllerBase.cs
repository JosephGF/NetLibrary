using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Reflection;
using System.Data.Entity;
using System.Collections;
using System.Data.Entity.Infrastructure;
using System.IO;

using NetLibrary.EntityFramework;
using NetLibrary.Debugger;


namespace NetLibrary.Web.MVC
{
    /// <summary>
    /// Proporciona una gestión genérica sobre el Controlador/Entity configurado
    /// </summary>
    /// <typeparam name="TEntity">Etity qu se gestionara</typeparam>
    public abstract class ControllerBase<TEntity> : Controller where TEntity : class
    {
        /// <summary>
        /// Cofiguracion para ControllerGeneric<TEntity>
        /// </summary>
        public class Setting
        {
            private string _movileView = "m_{ACTION}";

            public Setting()
            {
                this.ActionForbbiden = new List<string>();
                this.ActionPermitted = new List<string>();
                this.HandlePermisosEntity = CheckPermisos;
            }
            public Setting(DbContext context)
            {
                this.DbContext = context;
                this.ActionForbbiden = new List<string>();
                this.ActionPermitted = new List<string>();
            }
            /// <summary>
            /// Contexto sobre el que trabajará el Entity Framework (Obligatorio)
            /// </summary>
            public DbContext DbContext { get; set; }
            /// <summary>
            /// Actions cuyo acceso estará prohibido
            /// 
            /// Nota: Si la acción del controlador se encuentra en esta lista redirecciona a Home/Index
            /// </summary>
            public List<string> ActionForbbiden { get; set; }
            /// <summary>
            /// Actions cuyo acceso se permitira desde origen externo.
            ///     No comprueba que estas acciones tengan Url Referida (Url anterior)
            ///    
            /// Nota:
            ///     Si no tiene url referida significa que se ha referenciado desde la barra de direcciones o desde una página externa.
            /// </summary>
            public List<string> ActionPermitted { get; set; }
            /// <summary>
            /// Definicion del controlador de permisos para la Consuta/Creacion/Modificacion/Eliminacion de entidades
            /// </summary>
            public PermisosEntity HandlePermisosEntity { get; set; }
            /// <summary>
            /// Establece la regla para transformar vistas para dispositivos móviles
            /// 
            /// Nota:
            ///     Si no se encuentra el fichero .cshtml esperado devuleve la vista origen
            /// 
            /// Ejemplo: 
            ///     Dando el valor 'movil_{ACTION}' buscara la vista 'movil_Index' dentro de la ruta del cotrolador actual (Views/{Controller}/movil_{ACTION})
            /// </summary>
            public string MobileView { get { return _movileView; } set { _movileView = value; } }

            private bool CheckPermisos(Accion accion, object model)
            {
                if (_setting.HandlePermisosEntity != null)
                    return _setting.HandlePermisosEntity(accion, model);

                return true;
            }
        }

        /// <summary>
        /// Configura los valores necesarios para el funcionamiento genérico del controlador
        /// </summary>
        /// <returns>Objeto de configuracion</returns>
        protected internal abstract Setting Configuration();

        /// <summary>
        /// Controla los permisos para las acciones de Creacion/Modificacion/Eliminacion/Consluta
        /// </summary>
        /// <param name="accion">Tipo de accion que se va a realizar</param>
        /// <param name="model">Objeto sobre el que se va a realizar la acción</param>
        /// <returns>Booleano indicando si tiene permisos</returns>
        public delegate bool PermisosEntity(Accion accion, object model);
        private static string _invalidPermisosText = "No tiene permisos para realizar esta acción";
        private static Setting _setting = new Setting();

        protected internal DbContext db
        {
            get { return _setting.DbContext; }
        }

        protected internal Tdb getDb<Tdb>() where Tdb : DbContext
        {
            return (Tdb)_setting.DbContext;
        }

        /// <summary>
        /// Texto que se usara para asignar en caso de que HandlePermisos devueva false
        /// 
        /// Este texto es introducido en el ModelState con la Key "Exception"
        /// </summary>
        public static string InvalidPermisosText
        {
            get { return _invalidPermisosText; }
            set { _invalidPermisosText = value; }
        }

        public ControllerBase()
        {
            _setting = Configuration();
            GenericEntity.DbContext = _setting.DbContext;
        }

        /// <summary>
        /// Último model que se ha Creado, Modificado o Eliminado
        /// </summary>
        public TEntity LastEntityModel
        {
            get { return (TEntity)GenericEntity.LastEntityModel; }
        }

        private bool checkPermisos(Accion accion, object model)
        {
            if (_setting.HandlePermisosEntity != null)
                return _setting.HandlePermisosEntity(accion, model);

            return true;
        }

        #region Entity Framework
        /// <summary>
        /// Busca el objeto por el identificador seleccionado
        /// 
        /// 
        /// Validaciones: Ejecuta el delegado HandlePermisos(Accion.View)
        ///            -> True  | Devuelve la instancia al objeto
        ///            -> False | Devuelve una nueva instaccia al objeto
        /// </summary>
        /// <param name="id">Identificador único del objeto</param>
        /// <param name="accion">Tipo de acción para comprobar los permisos</param>
        /// <returns>Instancia nueva si no se tienen permisos y el objeto correspodiente al identificador o "null"</returns>
        public virtual TEntity Find(int id, Accion accion)
        {
            object model = this.Find(id);
            if (_setting.HandlePermisosEntity(accion, model))
            {
                return (TEntity)model;
            }

            ModelState.AddModelError("Exception", _invalidPermisosText);
            return (TEntity)Activator.CreateInstance(typeof(TEntity));
        }

        /// <summary>
        /// Busca el objeto por el identificador seleccionado
        /// 
        /// 
        /// Nota: No comprueba si el usuario tiene permisos (No ejecuta HandlePermisos)
        /// </summary>
        /// <param name="id">Identificador único del objeto</param>
        /// <returns>Objeto correspodiente al identificador o "null"</returns>
        public virtual TEntity Find(int id)
        {
            return this.Find<TEntity>(id);
        }

        /// <summary>
        /// Busca el objeto por el identificador seleccionado
        /// 
        /// 
        /// Nota: No comprueba si el usuario tiene permisos (No ejecuta HandlePermisos)
        /// <typeparam name="TEntityAux">Tipo de entidad que se buscará</typeparam>
        /// <param name="id">Identificador único del objeto</param>
        /// <returns>Objeto correspodiente al identificador o "null"</returns>
        public virtual TEntityAux Find<TEntityAux>(int id) where TEntityAux : class
        {
            object model = GenericEntity.Find(typeof(TEntityAux), id);
            return (TEntityAux)model;
        }

        /// <summary>
        /// Obtiene una coleccion con todos los elementos del contexto
        /// 
        /// 
        /// Validaciones (checkPermisos = true): Ejecuta el delegado HandlePermisos(Accion.ViewList) 
        ///            -> True  | Incluye el elemento en la coleccion
        ///            -> False | Excluye el elemento de la coleccion
        /// </summary>
        /// <param name="checkPermisos">Indica si descartara los elementos que no cumpla los permisos necesarios</param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> Select(bool checkPermisos = true)
        {
            /*DbSet dbSet = this.Select(typeof(TEntity));
            DbSet<TEntity> dbSetEntity = dbSet.Cast<TEntity>();

            if (!checkPermisos)
                return dbSetEntity;

            foreach (TEntity entityObject in dbSet)
            {
                if (!this.HandlePermisos(Accion.ViewList, entityObject))
                {
                    dbSetEntity.Remove(entityObject);
                }
            }

            return dbSetEntity;*/
            return this.Select<TEntity>(checkPermisos);
        }

        /// <summary>
        /// Obtiene una coleccion con todos los elementos del contexto
        /// 
        /// 
        /// Validaciones (checkPermisos = true): Ejecuta el delegado HandlePermisos(Accion.ViewList) 
        ///            -> True  | Incluye el elemento en la coleccion
        ///            -> False | Excluye el elemento de la coleccion
        /// </summary>
        /// <typeparam name="TEntityAux">Tipode Etity sobre el que se trabajará</typeparam>
        /// <param name="checkPermisos">Indica si descartara los elementos que no cumpla los permisos necesarios</param>
        /// <returns></returns>
        public virtual IEnumerable<TEntityAux> Select<TEntityAux>(bool checkPermisos = true) where TEntityAux : class
        {
            DbSet dbSet = GenericEntity.Select(typeof(TEntityAux));
            DbSet<TEntityAux> dbSetEntity = dbSet.Cast<TEntityAux>();

            List<TEntityAux> listResult = new List<TEntityAux>();
            if (!checkPermisos)
                return dbSetEntity;

            foreach (TEntityAux entityObject in dbSetEntity)
            {
                if (_setting.HandlePermisosEntity(Accion.ViewList, entityObject))
                {
                    listResult.Add(entityObject);
                }
            }

            return listResult;
        }

        internal virtual DbSet<TEntityAux> GetDbSet<TEntityAux>(bool checkPermisos = true) where TEntityAux : class
        {
            DbSet dbSet = GenericEntity.Select(typeof(TEntityAux));
            DbSet<TEntityAux> dbSetEntity = dbSet.Cast<TEntityAux>();
            return dbSetEntity;
        }

        internal virtual DbSet<TEntity> GetDbSet(bool checkPermisos = true)
        {
            return this.GetDbSet<TEntity>(checkPermisos);
        }

        /// <summary>
        /// Obtiene una coleccion con todos los elementos del contexto
        /// 
        /// 
        /// Validaciones: Ejecuta el delegado HandlePermisos(Accion.ViewList)
        ///            -> True  | Incluye el elemento en la coleccion
        ///            -> False | Excluye el elemento de la coleccion
        /// </summary>
        /// <param name="filter">Filtro que se aplicara a la colección</param>
        /// <param name="checkPermisos">Indica si descartara los elementos que no cumpla los permisos necesarios</param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> Select(Func<TEntity, bool> filter, bool checkPermisos = true)
        {
            return this.Select(checkPermisos).Where(filter);
        }

        /// <summary>
        /// Obtiene una coleccion con todos los elementos del contexto
        /// 
        /// 
        /// Validaciones: Ejecuta el delegado HandlePermisos(Accion.ViewList)
        ///            -> True  | Incluye el elemento en la coleccion
        ///            -> False | Excluye el elemento de la coleccion
        /// </summary>
        /// <param name="filter">Filtro que se aplicara a la colección</param>
        /// <param name="order">Orden que se aplicará a la coleccion</param>
        /// <param name="desc">Orden Ascendete/Descendete</param>
        /// <param name="checkPermisos">Indica si descartara los elementos que no cumpla los permisos necesarios</param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> Select(Func<TEntity, bool> filter, Func<TEntity, object> order, bool desc = false, bool checkPermisos = true)
        {
            var items = this.Select(checkPermisos).Where(filter);
            if (desc)
                items = items.OrderByDescending(order);
            else
                items = items.OrderBy(order);

            return items;
        }

        protected internal virtual bool ValidateEntity(TEntity model)
        {
            foreach (var error in db.Entry<TEntity>(model).GetValidationResult().ValidationErrors)
            {
                if (ModelState[error.PropertyName] == null || ModelState[error.PropertyName].Errors.Where(err => err.ErrorMessage.Equals(error.ErrorMessage)).Count() == 0)
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }

            return ModelState.IsValid;
        }
        #endregion

        #region Acciones
        /// <summary>
        /// Controla la accion Index, devolviendo una coleccion de objetos TEntity como Model
        /// 
        /// 
        /// Validaciones: Ejecuta el delegado HandlePermisos(Accion.ViewList)
        ///            -> True  | Incluye el elemento en la coleccion
        ///            -> False | Excluye el elemento de la coleccion
        /// </summary>
        /// <returns></returns>
        public virtual ActionResult Index()
        {
            IEnumerable<TEntity> dbSetEntity = this.Select();
            return View(dbSetEntity);
        }

        /// <summary>
        /// Controla la accion Details, devuelve la instancia TEntity del elemento encontrado como Model
        /// 
        /// 
        /// Validaciones: Ejecuta el delegado HandlePermisos(Accion.View)
        ///            -> True  | Devuelve la instancia al objeto
        ///            -> False | Devuelve una nueva instaccia al objeto
        ///                     | Crea un error en el ViewModel con la key "Exception"
        /// </summary>
        /// <param name="id">Identificador que se deesea buscar</param>
        /// <returns></returns>
        public virtual ActionResult Details(int id)
        {
            var item = this.Find(id, Accion.View);
            return View(item);
        }

        /// <summary>
        /// Controla la accion Edit, devuelve la instancia TEntity del elemento encontrado como Model
        /// 
        /// 
        /// Validaciones: Ejecuta el delegado HandlePermisos(Accion.View)
        ///            -> True  | Devuelve la instancia al objeto
        ///            -> False | Devuelve una nueva instaccia al objeto
        ///                     | Crea un error en el ViewModel con la key "Exception"
        /// </summary>
        /// <param name="id">Identificador que se deesea buscar</param>
        /// <returns></returns>
        public virtual ActionResult Edit(int id)
        {
            var item = this.Find(id, Accion.Edit);
            return View(item);
        }

        /// <summary>
        /// Controla la accion Edit [HttpPost] para guardar el contenido del formulario
        /// 
        /// Validaciones: Ejecuta el delegado HandlePermisos(Accion.Edit)
        ///            -> True  | Devuelve un ActionResut del Index
        ///            -> False | Devuelve un ActionResut de la vista Actual
        ///                     | Crea un error en el ViewModel con la key "Exception"
        /// </summary>
        /// <param name="collection">Coleccion de datos recibida por desde el navegador</param>
        /// <returns>
        /// Devuelve un ActionResut del Index
        ///         Si el ModelState no es válido devuelve la vista Manager con la instancia al TEntity actual
        /// </returns>
        [HttpPost]
        public virtual ActionResult Edit(FormCollection collection)
        {
            var entityObject = CollectionToModel(collection);
            return Edit((TEntity)entityObject);
        }

        /// <summary>
        /// Controla la accion Edit [HttpPost] para guardar el contenido del formulario
        /// 
        /// Validaciones: Ejecuta el delegado HandlePermisos(Accion.Edit)
        ///            -> True  | Devuelve un ActionResut del Index
        ///            -> False | Devuelve un ActionResut de la vista Actual
        ///                     | Crea un error en el ViewModel con la key "Exception"
        /// </summary>
        /// <typeparam name="TEntityAux">Tipo de entidad</typeparam>
        /// <param name="entityObject">Model que se desea actualizar</param>
        /// <returns>
        /// Devuelve un ActionResut del Index
        ///         Si el ModelState no es válido devuelve la vista Manager con la instancia al TEntity actual
        /// </returns>
        [HttpPost]
        protected internal virtual ActionResult Edit<TEntityAux>(TEntityAux entityObject) where TEntityAux : class
        {

            if (db.Entry(entityObject).State == EntityState.Detached)
            {
                int idModel = GenericEntity.PrimaryKey(entityObject);
                TEntityAux modelAux = this.Find<TEntityAux>(idModel);

                if (db.Set<TEntityAux>().Local.Contains(modelAux))
                {
                    db.Entry(modelAux).CurrentValues.SetValues(entityObject);
                    entityObject = modelAux;
                }
                else
                {
                    db.Set<TEntityAux>().Attach(entityObject);
                    //DbContext.Set(model.GetType()).Attach(model);
                }
            }

            TEntityAux originalValues = (TEntityAux)this.db.Entry<TEntityAux>(entityObject).OriginalValues.ToObject();
            if (!_setting.HandlePermisosEntity(Accion.Edit, originalValues))
                ModelState.AddModelError("Exception", _invalidPermisosText);

            ModelState.Merge(GenericEntity.EditModel(entityObject));
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            return View(entityObject);
        }

        /// <summary>
        /// Controla la accion Edit [HttpPost] para guardar el contenido del formulario
        /// 
        /// Validaciones: Ejecuta el delegado HandlePermisos(Accion.Edit)
        ///            -> True  | Devuelve un ActionResut del Index
        ///            -> False | Devuelve un ActionResut de la vista Actual
        ///                     | Crea un error en el ViewModel con la key "Exception"
        /// </summary>
        /// <param name="entityObject">Model que se desea actualizar</param>
        /// <returns>
        /// Devuelve un ActionResut del Index
        ///         Si el ModelState no es válido devuelve la vista Manager con la instancia al TEntity actual
        /// </returns>
        [HttpPost]
        protected internal virtual ActionResult Edit(TEntity entityObject)
        {
            if (!ValidateEntity(entityObject))
            {
                return View(entityObject);
            }

            return this.Edit<TEntity>(entityObject);
        }

        /// <summary>
        /// Controla la accion Create, devuelve la vista sin ninguna instancia
        /// </summary>
        /// <returns></returns>
        public virtual ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Controla la accion Create [HttpPost] para guardar el contenido del formulario
        /// 
        /// Validaciones: Ejecuta el delegado HandlePermisos(Accion.Create)
        ///            -> True  | Devuelve un ActionResut del Index
        ///            -> False | Devuelve un ActionResut de la vista Actual
        ///                     | Crea un error en el ViewModel con la key "Exception"
        /// </summary>
        /// <param name="collection">Coleccion de datos recibida por desde el navegador</param>
        /// <returns>
        /// Devuelve un ActionResut del Index
        ///         Si el ModelState no es válido devuelve la vista Manager con la instancia al TEntity actual
        /// </returns>
        [HttpPost]
        public virtual ActionResult Create(FormCollection collection)
        {
            var entityObject = CollectionToModel(typeof(TEntity), collection);
            return Create((TEntity)entityObject);
        }

        /// <summary>
        /// Controla la accion Create [HttpPost] para guardar el contenido del formulario
        /// 
        /// Validaciones: Ejecuta el delegado HandlePermisos(Accion.Create)
        ///            -> True  | Devuelve un ActionResut del Index
        ///            -> False | Devuelve un ActionResut de la vista Actual
        ///                     | Crea un error en el ViewModel con la key "Exception"
        /// </summary>
        /// <typeparam name="TEntityAux">Tipo de entidad</typeparam>
        /// <param name="entityObject">Model que se desea crear</param>
        /// <returns>
        /// Devuelve un ActionResut del Index
        ///         Si el ModelState no es válido devuelve la vista Manager con la instancia al TEntity actual
        /// </returns>
        [HttpPost]
        protected internal virtual ActionResult Create<TEntityAux>(TEntityAux entityObject) where TEntityAux : class
        {
            if (!_setting.HandlePermisosEntity(Accion.Create, entityObject))
                ModelState.AddModelError("Exception", _invalidPermisosText);

            ModelState.Merge(GenericEntity.CreateModel(entityObject));
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            return View(entityObject);
        }
        /// <summary>
        /// Controla la accion Create [HttpPost] para guardar el contenido del formulario
        /// 
        /// Validaciones: Ejecuta el delegado HandlePermisos(Accion.Create)
        ///            -> True  | Devuelve un ActionResut del Index
        ///            -> False | Devuelve un ActionResut de la vista Actual
        ///                     | Crea un error en el ViewModel con la key "Exception"
        /// </summary>
        /// <param name="entityObject">Model que se desea crear</param>
        /// <returns>
        /// Devuelve un ActionResut del Index
        ///         Si el ModelState no es válido devuelve la vista Manager con la instancia al TEntity actual
        /// </returns>
        [HttpPost]
        protected internal virtual ActionResult Create(TEntity entityObject)
        {
            if (!ValidateEntity(entityObject))
            {
                return View(entityObject);
            }

            return this.Create<TEntity>(entityObject);
        }

        /// <summary>
        /// Controla la accion Delete [HttpPost] para guardar el contenido del formulario
        /// 
        /// Nota: Debe recibir en la posición 0 del FormCollection el valor de la clave primaria, el resto de valores se descartan
        /// 
        /// Validaciones: Ejecuta el delegado HandlePermisos(Accion.Delete)
        ///            -> True  | Devuelve un ActionResut del Index
        ///            -> False | Devuelve un ActionResut de la vista Actual
        ///                     | Crea un error en el ViewModel con la key "Exception"
        /// </summary>
        /// <param name="collection">Coleccion de datos recibida por desde el navegador</param>
        /// <returns>
        /// Devuelve un JsonResult con el resultado de la operación
        /// 
        /// Json Sin Incidencias
        ///     {id:[PrimaryKey],success:'1',errors:[]}
        ///     
        /// Json Con Incidencias
        ///     {id:[PrimaryKey],success:'0',errors:[
        ///                                               {key:'',messages:['Messaje 1', 'Message 2', (...) ]}
        ///                                             , {key:'',messages:['Messaje 1', 'Message 2', (...) ]}
        ///                                             , (...)
        ///                                         ]
        ///     }
        /// </returns>
        [HttpPost]
        public virtual ActionResult Delete(FormCollection collection)
        {
            int id = Convert.ToInt32(collection[0]);
            TEntity model = this.Find(id);

            this.Delete(model);
            return ModelStateToJSonResult();
        }

        /// <summary>
        /// Controla la accion Delete [HttpPost] para guardar el contenido del formulario
        /// 
        /// Validaciones: Ejecuta el delegado HandlePermisos(Accion.Delete)
        ///            -> True  | Devuelve un ActionResut del Index
        ///            -> False | Devuelve un ActionResut de la vista Actual
        ///                     | Crea un error en el ViewModel con la key "Exception"
        /// </summary>
        /// <typeparam name="TEntityAux">Tipo de entidad</typeparam>
        /// <param name="entityObject">Model que se desea Eliminar</param>
        /// <returns>
        /// Devuelve un JsonResult con el resultado de la operación
        /// 
        /// Json Sin Incidencias
        ///     {id:[PrimaryKey],success:'1',errors:[]}
        ///     
        /// Json Con Incidencias
        ///     {id:[PrimaryKey],success:'0',errors:[
        ///                                               {key:'',messages:['Messaje 1', 'Message 2', (...) ]}
        ///                                             , {key:'',messages:['Messaje 1', 'Message 2', (...) ]}
        ///                                             , (...)
        ///                                         ]
        ///     }
        /// </returns>
        [HttpPost]
        protected internal virtual ActionResult Delete<TEntityAux>(TEntityAux entityObject) where TEntityAux : class
        {
            if (!_setting.HandlePermisosEntity(Accion.Delete, entityObject))
                ModelState.AddModelError("Exception", _invalidPermisosText);

            ModelState.Merge(GenericEntity.DeleteModel(entityObject));
            return ModelStateToJSonResult();
        }

        /// <summary>
        /// Controla la accion Delete [HttpPost] para guardar el contenido del formulario
        /// 
        /// Validaciones: Ejecuta el delegado HandlePermisos(Accion.Delete)
        ///            -> True  | Devuelve un ActionResut del Index
        ///            -> False | Devuelve un ActionResut de la vista Actual
        ///                     | Crea un error en el ViewModel con la key "Exception"
        /// </summary>
        /// <param name="entityObject">Model que se desea Eliminar</param>
        /// <returns>
        /// Devuelve un JsonResult con el resultado de la operación
        /// 
        /// Json Sin Incidencias
        ///     {id:[PrimaryKey],success:'1',errors:[]}
        ///     
        /// Json Con Incidencias
        ///     {id:[PrimaryKey],success:'0',errors:[
        ///                                               {key:'',messages:['Messaje 1', 'Message 2', (...) ]}
        ///                                             , {key:'',messages:['Messaje 1', 'Message 2', (...) ]}
        ///                                             , (...)
        ///                                         ]
        ///     }
        /// </returns>
        [HttpPost]
        protected internal virtual ActionResult Delete(TEntity entityObject)
        {
            return this.Delete<TEntity>(entityObject);
            /*if (!_setting.HandlePermisosEntity(Accion.Delete, entityObject))
                ModelState.AddModelError("Exception", _invalidPermisosText);

            this.DeleteModel(entityObject);
            return this.JsonResulModelState(0);*/
        }

        /// <summary>
        /// Cotrola la accion Manager que se encarga tanto de la Edición como de la Creacion
        /// Devuelve una instancia TEntity con el Id encontrado o nueva si el id es nulo
        /// 
        /// 
        /// Validaciones: Ejecuta el delegado HandlePermisos(Accion.Edit) o HandlePermisos(Accion.Create)
        ///            -> True  | Devuelve la instancia al objeto (nueva instancia en caso de Accion.Create)
        ///            -> False | Devuelve una nueva instaccia al objeto
        ///                     | Crea un error en el ViewModel con la key "Exception"
        /// </summary>
        /// <param name="id">Identificador del elemento a modificar o null para crear</param>
        /// <returns></returns>
        public virtual ActionResult Manager(int? id)
        {
            TEntity entity = (TEntity)Activator.CreateInstance(typeof(TEntity));
            if (id != null)
            {
                entity = this.Find(Convert.ToInt32(id), Accion.Edit);
            }
            else
            {
                if (!_setting.HandlePermisosEntity(Accion.Create, entity))
                    ModelState.AddModelError("Exception", InvalidPermisosText);
            }

            return View(entity);
        }

        /// <summary>
        /// Cotrola la accion [HttpPost] Manager que se encarga tanto de la Edición como de la Creacion
        /// 
        /// 
        /// Validaciones: Ejecuta el delegado HandlePermisos(Accion.Edit) o HandlePermisos(Accion.Create)
        ///            -> True  | Devuelve un ActionResut del Index
        ///            -> False | Devuelve un ActionResut de la vista Actual
        ///                     | Crea un error en el ViewModel con la key "Exception"
        /// </summary>
        /// <param name="collection">Elementos del formulario correspondiente al TEntity actual</param>
        /// <returns>
        /// Devuelve un ActionResut del Index
        ///         Si el ModelState no es válido devuelve la vista Manager con la instancia al TEntity actual
        /// </returns>
        [HttpPost]
        public virtual ActionResult Manager(FormCollection collection)
        {
            TEntity entity = this.CollectionToModel(collection);

            ValidateEntity(entity);
            if (!ModelState.IsValid)
                return View(entity);

            return this.Manager(entity);
        }

        /// <summary>
        /// Cotrola la accion [HttpPost] Manager que se encarga tanto de la Edición como de la Creacion
        /// 
        /// 
        /// Validaciones: Ejecuta el delegado HandlePermisos(Accion.Edit) o HandlePermisos(Accion.Create)
        ///            -> True  | Devuelve la instancia al objeto (nueva instancia en caso de Accion.Create)
        ///            -> False | Devuelve una nueva instaccia al objeto
        ///                     | Crea un error en el ViewModel con la key "Exception"
        /// </summary>
        /// <param name="model">Elemento TEntity que se desea gestionar</param>
        /// <returns>
        /// Devuelve un ActionResut del Index
        ///         Si el ModelState no es válido devuelve la vista Manager con la instancia al TEntity actual
        /// </returns>
        [HttpPost]
        protected internal virtual ActionResult Manager(TEntity model)
        {
            if (this.GetPrimaryKey(model) > 0)
            {
                this.Edit(model);
            }
            else
            {
                this.Create(model);
            }

            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
        }
        #endregion

        #region Utilidades
        [NonAction]
        protected internal string ViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }

        /// <summary>
        /// Obtiene la clave primaria de un elemento TModel
        /// 
        /// Nota: Este método solo funciona para Models cuya clave primaria sea una única columna de tipo integer
        /// </summary>
        /// <param name="model">Elemento sobre el que se obtendrá la clave primaria</param>
        /// <returns>Clave del elemento</returns>
        protected internal Int32 GetPrimaryKey(TEntity model)
        {
            Int32 id = -1;
            if (model == null)
                return id;

            var set = ((IObjectContextAdapter)this.db).ObjectContext.CreateObjectSet<TEntity>();
            var entitySet = set.EntitySet;
            string[] keyProperties = entitySet.ElementType.KeyMembers.Select(k => k.Name).ToArray();

            if (keyProperties.Count() == 0)
                return id;

            id = Reflection.Manager.GetPropertyValue<int>(model, keyProperties[0]);

            return id;
        }

        /// <summary>
        /// Conviernte una colección recibida de un formulario en un objeto TEntity
        /// 
        /// 
        /// Nota: Los parámetros no incluidos se escriben en la consola
        /// </summary>
        /// <param name="collection">Coleccion recibida por desde el navegador</param>
        /// <returns>Instancia al objeto TEntity con los valores recibidos</returns>
        protected internal TEntity CollectionToModel(FormCollection collection)
        {
            return (TEntity)CollectionToModel(typeof(TEntity), collection);
        }

        /// <summary>
        /// Conviernte una colección recibida de un formulario en un objeto TEntity
        /// 
        /// 
        /// Nota: Los parámetros no incluidos se escriben en la consola
        /// </summary>
        /// <param name="tipo">Tipo de objeto que se desea convertir</param>
        /// <param name="collection">Coleccion recibida por desde el navegador</param>
        /// <returns>Instancia al objeto con los valores recibidos</returns>
        protected internal object CollectionToModel(Type tipo, FormCollection collection)
        {
            var entityObject = Activator.CreateInstance(tipo);

            foreach (string key in collection)
            {
                System.Reflection.PropertyInfo propertyInfo = tipo.GetProperty(key);
                try
                {
                    if (propertyInfo != null)
                    {
                        object value = null;
                        Type valueType = propertyInfo.PropertyType;
                        if (valueType.GenericTypeArguments.Length > 0)
                            valueType = valueType.GenericTypeArguments[0];

                        value = Convert.ChangeType(collection[key], valueType);
                        this.CollectionValue(collection, key, valueType, ref value);
                        propertyInfo.SetValue(entityObject, value, null);
                    }
                    else
                    {
                        Debug.WriteLine("Property No Found in Model: " + key);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("ConvertFormCollection WARNING" + ex.Message);
                }
            }

            return entityObject;
        }

        /// <summary>
        /// Obtiene el valor para la key especificada y lo asigna al parametro por referencia value
        /// 
        /// 
        /// Nota:
        ///     Si el valor no existe o se produce una excepción no cambia el valor pasado como parámetro
        ///     
        ///     Si se produce una excepción escribe en la consola de debug
        /// <typeparam name="T">Tipo de valor esperado</typeparam>
        /// <param name="collection">Coleccion de origen</param>
        /// <param name="key">Nombre del parámetro buscado</param>
        /// <param name="type">Tipo de valor esperado</param>
        /// <param name="value">Valor por defecto/devuelto</param>
        protected internal void CollectionValue(FormCollection collection, string key, Type type, ref object value)
        {
            if (collection.AllKeys.Contains(key))
            {
                try
                {
                    Type valueType = type;
                    if (valueType.GenericTypeArguments.Length > 0)
                        valueType = valueType.GenericTypeArguments[0];

                    object valueAux = collection[key];
                    if (valueType.IsArray)
                    {

                        Type elementType = type.GetElementType();
                        string[] valueArray = collection[key].Split(",".ToCharArray());
                        Array valueArrayAux = Array.CreateInstance(elementType, valueArray.Length);
                        for (int idx = 0; idx < valueArray.Length; idx++)
                        {
                            object val = null;
                            try
                            {
                                val = Convert.ChangeType(valueArray[idx], elementType);
                            }
                            catch (Exception ex)
                            {
                                Debug.WriteLine("Error " + ex.Message);
                            }

                            if (val != null)
                            {
                                valueArrayAux.SetValue(val, idx);
                            }
                        }

                        value = valueArrayAux;
                    }
                    else
                    {
                        value = Convert.ChangeType(collection[key], valueType);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("ConvertFormCollection WARNING: " + ex.Message);
                }
            }
        }

        /// <summary>
        /// Obtiene el valor para la key especificada y lo asigna al parametro por referencia value
        /// 
        /// 
        /// Nota:
        ///     Si el valor no existe o se produce una excepción no cambia el valor pasado como parámetro
        ///     
        ///     Si se produce una excepción escribe en la consola de debug
        /// </summary>
        /// <typeparam name="T">Tipo de valor esperado</typeparam>
        /// <param name="collection">Coleccion de origen</param>
        /// <param name="key">Nombre del parámetro buscado</param>
        /// <param name="value">Valor por defecto/devuelto</param>
        protected internal void CollectionValue<T>(FormCollection collection, string key, ref T value)
        {
            Type tipo = typeof(T);
            object valueAux = value;
            this.CollectionValue(collection, key, tipo, ref valueAux);
            value = (T)valueAux;
        }

        /// <summary>
        /// Conviernte una colección en un objeto TEntity
        /// 
        /// 
        /// Nota: Los parámetros no incluidos se escriben en la consola
        /// </summary>
        /// <param name="values">Coleccion recibidar</param>
        /// <returns>Instancia al objeto TEntity con los valores recibidos</returns>
        protected internal TEntity DbPropertyValuesToModel(DbPropertyValues values)
        {
            return (TEntity)DbPropertyValuesToModel(typeof(TEntity), values);
        }

        /// <summary>
        /// Conviernte una colección recibida en un objeto TEntity
        /// 
        /// 
        /// Nota: Los parámetros no incluidos se escriben en la consola
        /// </summary>
        /// <param name="tipo">Tipo de objeto que se desea convertir</param>
        /// <param name="values">Coleccion recibida</param>
        /// <returns>Instancia al objeto con los valores recibidos</returns>
        protected internal object DbPropertyValuesToModel(Type tipo, DbPropertyValues values)
        {
            var entityObject = Activator.CreateInstance(tipo);

            /*            PropertyInfo[] properties = tipo.GetProperties();

                        foreach (PropertyInfo property in properties)
                        {
                            try
                            {
                                if (propertyInfo != null)
                                {
                                    var value = values.g
                                    property.SetValue(entityObject, value, null);
                                }
                                else
                                {
                                    Debugger.Log(0, "ConvertFormCollection WARNING", "Property No Found in Model: " + key);
                                }
                            }
                            catch (Exception ex)
                            {
                                Debugger.Log(0, "ConvertFormCollection WARNING", ex.Message);
                            }
                        }*/

            return entityObject;
        }

        protected internal JsonResult ModelStateToJSonResult()
        {
            var state = new List<Object>();
            foreach (ModelState mState in this.ModelState.Values)
            {
                if (mState.Errors.Count > 0)
                {
                    foreach (var error in mState.Errors)
                    {
                        state.Add(new { key = mState.Value.AttemptedValue, msg = error.ErrorMessage });
                    }
                }
            }

            JsonResult result = new JsonResult();
            result.Data = System.Web.Helpers.Json.Encode(state);
            return result;
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            RedirectToRouteResult redirection = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Home" }, { "action", "Index" } });
            // Acciones Prohibidas
            if (_setting.ActionForbbiden.Contains(filterContext.ActionDescriptor.ActionName))
                filterContext.Result = redirection;

            // Acciones Permitidas sin validaciones
            if (_setting.ActionPermitted.Contains(filterContext.ActionDescriptor.ActionName))
            {
                base.OnActionExecuting(filterContext);
                return;
            }

            // Tratamiento por defecto (No se permite el acceso desde fuera de la aplicacion (Barra de direcciones o link externo)
            if (Request.UrlReferrer != null)
            {
                base.OnActionExecuting(filterContext);
            }
            else
            {
                filterContext.Result = redirection;
            }
        }

        protected internal virtual ActionResult CreateActionResult(AccionResultType type, string strInfo, object model)
        {
            ActionResult action = View();

            switch (type)
            {
                case AccionResultType.JsonResult:
                    action = Json(model, JsonRequestBehavior.AllowGet);
                    break;
                case AccionResultType.PartialViewResult:
                    action = PartialView(strInfo, model);
                    break;
                case AccionResultType.ViewResult:
                    string actionName = strInfo;
                    if (Request.Browser.IsMobileDevice)
                    {
                        actionName = _setting.MobileView.Replace("{ACTION}", actionName);
                        //if (
                    }

                    action = View(actionName, model);
                    break;
                case AccionResultType.ContentResult:
                    action = Content(strInfo);
                    break;
                case AccionResultType.JavaScriptResult:
                    action = JavaScript(strInfo);
                    break;
                case AccionResultType.FormResult:
                    throw new NotImplementedException();
            }

            return action;
        }

        /// <summary>
        /// Obtiene el FormCollection actual, si existe
        /// </summary>
        /// <returns>Colencción con los valores obtenidos</returns>
        protected internal virtual FormCollection GetFormCollection()
        {
            FormCollection collection = new FormCollection();

            for (int i = 0; i < this.Request.Form.Count; i++)
            {
                var value = this.Request.Form[i];
                var key = this.Request.Form.Keys[i];
                collection.Add(key, value);
            }

            return collection;
        }
        #endregion
    }
}