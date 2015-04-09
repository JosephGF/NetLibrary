using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace NetLibrary.EntityFramework
{
    public static class GenericEntity
    {
        /// <summary>
        /// Utilizado para debuggear, si esta a false no actualiza la base de datos
        /// </summary>
        private static bool _saveChangesOnDB = true;
        private static object _lastObject = null;

        //public static ModelStateDictionary ModelState { get; }

        public static DbContext DbContext
        {
            get;
            set;
        }
        internal static DbContext GetDbContext()
        {
            return GenericEntity.DbContext;
        }
        public static object LastEntityModel
        {
            get { return _lastObject; }
        }

        /// <summary>
        /// Obtiene todos los elementos
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="tipo">Tipo de Entity Model del que se obtendrán los elementos</param>
        /// <returns>DbSet con los elementos</returns>
        public static DbSet Select(Type tipo)
        {
            DbSet customer = DbContext.Set(tipo);
            return customer;
        }

        /// <summary>
        /// Obtiene el elemento cuyo id sea el especificado
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="tipo">Tipo de Entity Model del que se obtendrán eñ elemento</param>
        /// <param name="id">Identificado del elemento que se desea obtener</param>
        /// <returns>Elemento resultante, null si no se encuentra</returns>
        public static object Find(Type tipo, int id)
        {
            DbSet customer = DbContext.Set(tipo);

            return customer.Find(id);
        }

        /// <summary>
        /// Crea un nuevo registro en la base de datos del contexto especificado
        /// </summary>
        /// <param name="controller">Controlador de origen</param>
        /// <param name="model">Registo que se desea insertar</param>
        /// <returns>Booleano que indica si se han producido errores (Los errores están contenidos en el ModelState)</returns>
        public static ModelStateDictionary CreateModel(object model)
        {
            ModelStateDictionary modelState = new ModelStateDictionary();

            modelState.Merge(GetModelState(model));
            if (modelState.IsValid)
            {
                try
                {
                    Type tipo = model.GetType();
                    DbSet customer = DbContext.Set(tipo);

                    /*
                     
                    if (DbContext.Entry(model).State == EntityState.Detached)
                    {
                        if (DbContext.Set(model.GetType()).Local.Contains(model))
                        {
                            var attachedEntry = DbContext.Entry(model);
                            attachedEntry.CurrentValues.SetValues(model);
                        }
                        else
                        {
                            DbContext.Set(model.GetType()).Attach(model);
                        }
                    }
                     
                    */

                    customer.Add(model);

                    if (_saveChangesOnDB)
                        DbContext.SaveChanges();

                    _lastObject = model;
                }
                catch (Exception ex)
                {
                    modelState.Merge(GestionExcepcion(ex));
                }
            }

            return modelState;
        }

        /// <summary>
        /// Crea un nuevo registro en la base de datos del contexto especificado
        /// </summary>
        /// <param name="controller">Controlador de origen</param>
        /// <param name="model">Registo que se desea insertar</param>
        /// <returns>Booleano que indica si se han producido errores (Los errores están contenidos en el ModelState)</returns>
        public static ModelStateDictionary CreateArray(object[] model)
        {
            ModelStateDictionary modelState = new ModelStateDictionary();
            if (model.Length == 0)
                return modelState;

            //if (!controller.ModelState.IsValid)
            //    modelErrors = false;
            //else
            //{
            try
            {
                Type tipo = model[0].GetType();
                DbSet customer = DbContext.Set(tipo);
                customer.AddRange(model);

                if (_saveChangesOnDB)
                    DbContext.SaveChanges();

                _lastObject = model;
            }
            catch (Exception ex)
            {
                modelState.Merge(GestionExcepcion(ex));
            }
            //}

            return modelState;
        }

        /// <summary>
        /// Actualiza un registro existente en la base de datos del contexto especificado
        /// </summary>
        /// <param name="controller">Controlador de origen</param>
        /// <param name="model">Registo que se desea actualizar</param>
        /// <returns>Booleano que indica si se han producido errores (Los errores están contenidos en el ModelState)</returns>
        public static ModelStateDictionary EditModel(object model)
        {
            ModelStateDictionary modelState = new ModelStateDictionary();
            modelState.Merge(GetModelState(model));
            if (modelState.IsValid)
            {
                try
                {
                    // La de arriba es la forma correcta
                    if (DbContext.Entry(model).State == EntityState.Detached)
                    {
                        int idModel = PrimaryKey(model);
                        var modelAux = Find(model.GetType(), idModel);

                        if (DbContext.Set(model.GetType()).Local.Contains(modelAux))
                        {
                            DbContext.Entry(modelAux).CurrentValues.SetValues(model);
                            model = modelAux;
                        }
                        else
                        {
                            DbContext.Set(model.GetType()).Attach(model);
                            //DbContext.Set(model.GetType()).Attach(model);
                        }
                    }

                    DbContext.Entry(model).State = EntityState.Modified;


                    if (_saveChangesOnDB)
                        DbContext.SaveChanges();

                    _lastObject = model;
                }
                catch (Exception ex)
                {
                    modelState.Merge(GestionExcepcion(ex));
                }
            }
            return modelState;
        }

        /// <summary>
        /// Obtiene la clave primaria de un elemento TModel
        /// 
        /// Nota: Este método solo funciona para Models cuya clave primaria sea una única columna de tipo integer
        /// </summary>
        /// <param name="model">Elemento sobre el que se obtendrá la clave primaria</param>
        /// <returns>Clave del elemento</returns>
        static public int PrimaryKey(object model)
        {
            Int32 id = -1;
            if (model == null)
                return id;

            Type t = model.GetType();
            DbContext db = DbContext;
            var objectContext = ((IObjectContextAdapter)db).ObjectContext;


            MethodInfo m = objectContext.GetType().GetMethod("CreateObjectSet", new Type[] { });
            MethodInfo generic = m.MakeGenericMethod(t);
            object set = generic.Invoke(objectContext, null);

            PropertyInfo entitySetPI = set.GetType().GetProperty("EntitySet");
            System.Data.Entity.Core.Metadata.Edm.EntitySet entitySet = (System.Data.Entity.Core.Metadata.Edm.EntitySet)entitySetPI.GetValue(set, null);

            string[] keyProperties = entitySet.ElementType.KeyMembers.Select(k => k.Name).ToArray();

            if (keyProperties.Count() == 0)
                return id;

            id = Reflection.Manager.GetPropertyValue<int>(model, keyProperties[0]);

            return id;
        }

        /// <summary>
        /// Elimina un registro existente en la base de datos del contexto especificado
        /// </summary>
        /// <param name="controller">Controlador de origen</param>
        /// <param name="modelType">Tipo del modelo/tabla donde se realizara la eliminación</param>
        /// <param name="id">Identificador del registro a eliminar</param>
        /// <returns>Booleano que indica si se han producido errores (Los errores están contenidos en el ModelState)</returns>
        public static ModelStateDictionary DeleteModel(Type modelType, int id)
        {
            ModelStateDictionary modelState = new ModelStateDictionary();
            try
            {
                DbSet customer = DbContext.Set(modelType);
                var model = customer.Find(id);
                return DeleteModel(model);
            }
            catch (Exception ex)
            {
                modelState.Merge(GestionExcepcion(ex));
            }

            return modelState;
        }

        /// <summary>
        /// Elimina un registro existente en la base de datos del contexto especificado
        /// </summary>
        /// <param name="controller">Controlador de origen</param>
        /// <param name="model">Modelo entity que se eliminará</param>
        /// <returns>Booleano que indica si se han producido errores (Los errores están contenidos en el ModelState)</returns>
        public static ModelStateDictionary DeleteModel(object model)
        {
            ModelStateDictionary modelState = new ModelStateDictionary();
            modelState.Merge(GetModelState(model));

            if (modelState.IsValid)
            {
                try
                {
                    DbSet customer = DbContext.Set(model.GetType());

                    customer.Remove(model);
                    if (_saveChangesOnDB)
                        DbContext.SaveChanges();

                    _lastObject = model;
                }
                catch (Exception ex)
                {
                    modelState.Merge(GestionExcepcion(ex));
                }
            }
            return modelState;
        }

        /// <summary>
        /// Obtiene la clave primaria de un elemento TModel
        /// 
        /// Nota: Este método solo funciona para Models cuya clave primaria sea una única columna
        /// </summary>
        /// <param name="model">Elemento sobre el que se obtendrá la clave primaria</param>
        /// <returns>Clave del elemento</returns>
        static internal T PrimaryKey<T>(this Controller controller, object model)
        {
            T id = default(T);
            if (model == null)
                return id;

            Type t = model.GetType();
            DbContext db = DbContext;
            var objectContext = ((IObjectContextAdapter)db).ObjectContext;


            MethodInfo m = objectContext.GetType().GetMethod("CreateObjectSet", new Type[] { });
            MethodInfo generic = m.MakeGenericMethod(t);
            object set = generic.Invoke(objectContext, null);

            PropertyInfo entitySetPI = set.GetType().GetProperty("EntitySet");
            System.Data.Entity.Core.Metadata.Edm.EntitySet entitySet = (System.Data.Entity.Core.Metadata.Edm.EntitySet)entitySetPI.GetValue(set, null);

            string[] keyProperties = entitySet.ElementType.KeyMembers.Select(k => k.Name).ToArray();

            if (keyProperties.Count() == 0)
                return id;

            id = NetLibrary.Reflection.Manager.GetPropertyValue<T>(model, keyProperties[0]);

            return id;
        }

        /// <summary>
        /// Gestina las excepiciones devuelta del DbContext.SaveChange()
        /// </summary>
        /// <param name="controller">Controlador del modelState</param>
        /// <param name="ex">Excepicion capturada</param>
        /// <returns>Booleano que indica si se han encontrado errores o no (Genealmete será false porque si hay una excepción se habrá</returns>
        private static ModelStateDictionary GestionExcepcion(Exception ex)
        {
            ModelStateDictionary modelErrors = new ModelStateDictionary();
            if (ex is DbEntityValidationException)
            {
                DbEntityValidationException exAux = (DbEntityValidationException)ex;
                foreach (DbEntityValidationResult entityVadiation in exAux.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityVadiation.ValidationErrors)
                    {
                        string propertyName = "Exception";
                        if (error.PropertyName != null && error.PropertyName.Length > 0)
                        {
                            propertyName = error.PropertyName;
                        }
                        //controller.ModelState.AddModelError(propertyName, error.ErrorMessage);
                        modelErrors.AddModelError(propertyName, error.ErrorMessage);
                    }
                }
            }
            else
            {
                Exception exAux = ex;
                //controller.ModelState.AddModelError("Exception", exAux.Message);
                while (exAux.InnerException != null)
                {
                    exAux = exAux.InnerException;
                    if (exAux.InnerException == null)
                        //controller.ModelState.AddModelError("Exception", exAux.Message);
                        modelErrors.AddModelError("Exception", exAux.Message);
                }
            }

            return modelErrors;
        }

        /// <summary>
        /// Obtiene el resultado de validación para un objeto
        /// </summary>
        /// <param name="model">Objeto Entity a validar</param>
        /// <returns>Resultado de la validación</returns>
        public static ModelStateDictionary GetModelState(object model)
        {
            ModelStateDictionary modelState = new ModelStateDictionary();
            DbEntityValidationResult validResult = DbContext.Entry(model).GetValidationResult();

            foreach (DbValidationError error in validResult.ValidationErrors)
            {
                modelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }
            return modelState;
        }
    }
}
