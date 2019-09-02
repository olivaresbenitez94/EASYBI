using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace EasyBI
{
    public class DBOSql
    {
        protected virtual string[] ignoreColumnsIds() {
            return new string[0];
        }

        #region MIEMBROS PRIVADOS

        /* Hashtable donde se almacenan las columnas */
        protected Hashtable hashDatos;
        /* Nombre de la columna que contiene el ID */
        protected string idColumn;
        /* Nombre de la tabla a la que pertenece el objeto */
        protected string strTabla;

        public String getTableName()
        {
            return strTabla;
        }

        public void setTableName(string TableName)
        {
            strTabla = TableName;
        }

        private bool cargado;

        #endregion

        #region Propiedades
        public bool Cargado
        {
            get { return this.cargado; }
            set { this.cargado = value; }
        }
        #endregion

        /* Debe devolver un objeto de la clase */
        protected virtual object getObject()
        {
            return Activator.CreateInstance(this.GetType());
        }
        /* Debe devolver un objeto Collection de la clase */
        protected virtual object getCollection()
        {
            return new DBOList();
        }

        #region CONSTRUCTORES

        public DBOSql()
        {
            this.idColumn = "ID";
            this.strTabla = this.GetType().Name;

            this.hashDatos = new Hashtable();

            this.setColumn(idColumn, 0);
        }

        public DBOSql(object ID)
        {
            this.idColumn = "ID";
            this.strTabla = this.GetType().Name;

            this.hashDatos = new Hashtable();

            this.cargado = this.Load(ID);
        }

        public DBOSql(string key, object value, bool load = true)
        {
            this.idColumn = key;
            this.strTabla = this.GetType().Name;

            this.hashDatos = new Hashtable();
            if (load)
            {
                this.cargado = this.Load(value);
            }
            else
            {
                this.setColumn(idColumn, 0);
            }
        }

        #endregion

        #region Métodos Genéricos
        public object getColumn(string column)
        {
            return this.hashDatos[column];
        }

        public void setColumn(string column, object val)
        {
            this.hashDatos[column] = val;
        }

        //Setear el nombre de la columna ID cuando esta no se llama "ID"
        public void setColumnId(string idColumn)
        {
            this.idColumn = idColumn;
        }
        #endregion

        #region Métodos Delete

        /// <summary>
        ///	 Elimina un registro de la base de datos pasandole como parámetro el ID
        /// </summary>
        /// <param name="Id">Identificador que queremos eliminar</param>
        /// <returns>True si la operación es correcta</returns>
        public static bool Eliminar(DBOSql instance, object ID)
        {

            string cmdTxt = "DELETE FROM " + instance.strTabla + " WHERE " + instance.idColumn + " = @ID ";

            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@ID", ID);

            bool eliminado = DataAccessSql.ExecuteNonQuery(Constantes.CONNECTIONSTRING, CommandType.Text, cmdTxt, parameters) > 0;

            return eliminado;
        }

        /// <summary>
        ///		Eliminar un objeto de la base de datos previamente instanciado
        /// </summary>
        /// <returns>True si la operación es correcta</returns>
        public virtual bool Eliminar()
        {
            string cmdTxt = "DELETE FROM " + this.strTabla + " WHERE " + this.idColumn + " = @ID ";

            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@ID", this.getColumn(idColumn));

            try
            {
                bool eliminado = DataAccessSql.ExecuteNonQuery(Constantes.CONNECTIONSTRING, CommandType.Text, cmdTxt, parameters) > 0;
                return eliminado;
            }
            catch (SqlException)
            {
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public virtual bool Eliminar(SqlTransaction transaccion)
        {
            string cmdTxt = "DELETE FROM " + this.strTabla + " WHERE " + this.idColumn + " = @ID ";

            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@ID", this.getColumn(idColumn));

            try
            {
                bool eliminado = DataAccessSql.ExecuteNonQuery(transaccion, CommandType.Text, cmdTxt, parameters) > 0;
                return eliminado;
            }
            catch (SqlException)
            {
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion

        #region Métodos 


        /// <summary>
        ///		Inserta un registro en la base de datos. Requiere haber instanciado el objeto
        /// </summary>
        /// <returns>True si la operación se realiza correctamente</returns>
        public virtual bool Insertar()
        {
            string cmdTxt = "INSERT INTO " + this.strTabla + "(";
            string values = "";

            List<SqlParameter> parameters = new List<SqlParameter>();
            int i = 0;
            foreach (string key in this.hashDatos.Keys)
            {
                if (key.ToLower().CompareTo(this.idColumn.ToLower()) == 0
                    || ignoreColumnsIds().Contains(key.ToLower())) continue;
                cmdTxt += key + ",";
                values += "@" + key + ",";
                if (this.hashDatos[key] == null)
                    parameters.Add(new SqlParameter("@" + key, DBNull.Value));
                else
                    parameters.Add(new SqlParameter("@" + key, this.hashDatos[key]));

                i++;
            }

            cmdTxt = cmdTxt.Substring(0, cmdTxt.Length - 1);
            values = values.Substring(0, values.Length - 1);
            cmdTxt += ") VALUES (" + values + ")";
            cmdTxt += "; SELECT SCOPE_IDENTITY()";

            //Ejecutamos la consulta y si ha insertado algún registro, devolvemos true. Si no, devolvemos false.
            try
            {
                object scalar = DataAccessSql.ExecuteScalar(Constantes.CONNECTIONSTRING, CommandType.Text, cmdTxt, parameters.ToArray());
                if (scalar == null)
                {
                    return false;
                }
                this.setColumn(this.idColumn, scalar);
                return true;
            }
            catch (SqlException)
            {
                return false;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public virtual bool Insertar(bool excluirClaveIdentidad)
        {
            string cmdTxt = "INSERT INTO " + this.strTabla + "(";
            string values = "";
            List<SqlParameter> parameters = new List<SqlParameter>();
            
            int i = 0;
            foreach (string key in this.hashDatos.Keys)
            {
                if (((key.ToLower().CompareTo(this.idColumn.ToLower()) == 0) && (excluirClaveIdentidad))
                    || ignoreColumnsIds().Contains(key.ToLower()))
                    continue;
                cmdTxt += key + ",";
                values += "@" + key + ",";
                if (this.hashDatos[key] == null)
                    parameters.Add(new SqlParameter("@" + key, DBNull.Value));
                else
                    parameters.Add(new SqlParameter("@" + key, this.hashDatos[key]));

                i++;
            }

            cmdTxt = cmdTxt.Substring(0, cmdTxt.Length - 1);
            values = values.Substring(0, values.Length - 1);
            cmdTxt += ") VALUES (" + values + ")";
            if (excluirClaveIdentidad)
                cmdTxt += "; SELECT SCOPE_IDENTITY()";

            //Ejecutamos la consulta y si ha insertado algún registro, devolvemos true. Si no, devolvemos false.
            try
            {
                if (excluirClaveIdentidad)
                {
                    object scalar;
                    if ((scalar = DataAccessSql.ExecuteScalar(Constantes.CONNECTIONSTRING, CommandType.Text, cmdTxt, parameters.ToArray())) == null)
                    {
                        return false;
                    }
                    this.setColumn(this.idColumn, scalar);
                    return true;
                }
                else
                {
                    DataAccessSql.ExecuteNonQuery(Constantes.CONNECTIONSTRING, CommandType.Text, cmdTxt, parameters.ToArray());
                    return true;
                }
            }
            catch (SqlException)
            {
                return false;
            }
            catch (Exception)
            {
                return false;
            }

        }
 

        public virtual bool Insertar(SqlTransaction transaccion)
        {
            string cmdTxt = "INSERT INTO " + this.strTabla + "("; //+ " (Codigo, Nombre, bColorPanan) VALUES (@Codigo, @Nombre, @bColorPanan)";
            string values = "";

            List<SqlParameter> parameters = new List<SqlParameter>();
            int i = 0;
            foreach (string key in this.hashDatos.Keys)
            {
                if (key.ToLower().CompareTo(this.idColumn.ToLower()) == 0
                    || ignoreColumnsIds().Contains(key.ToLower())) continue;
                cmdTxt += key + ",";
                values += "@" + key + ",";
                if (this.hashDatos[key] == null)
                    parameters.Add(new SqlParameter("@" + key, DBNull.Value));
                else
                    parameters.Add(new SqlParameter("@" + key, this.hashDatos[key]));

                i++;
            }

            cmdTxt = cmdTxt.Substring(0, cmdTxt.Length - 1);
            values = values.Substring(0, values.Length - 1);
            cmdTxt += ") VALUES (" + values + ")";
            cmdTxt += "; SELECT SCOPE_IDENTITY()";

            //Ejecutamos la consulta y si ha insertado algún registro, devolvemos true. Si no, devolvemos false.
            try
            {
                //object scalar = DataAccessSql.ExecuteScalar(Constantes.CONNECTIONSTRING, CommandType.Text, cmdTxt, parameters );
                object scalar = DataAccessSql.ExecuteScalar(transaccion, CommandType.Text, cmdTxt, parameters.ToArray());
                if (scalar == null)
                {
                    return false;
                }
                this.setColumn(this.idColumn, scalar);
                return true;
            }
            catch (SqlException)
            {
                return false;
            }
            catch (Exception)
            {
                return false;
            }

        }

        #endregion

        #region Métodos Update

        public virtual bool Modificar()
        {
            string cmdTxt = "UPDATE " + this.strTabla + " SET ";

            List<SqlParameter> parameters = new List<SqlParameter>();
            int i = -1;
            foreach (string key in this.hashDatos.Keys)
            {
                if (ignoreColumnsIds().Contains(key.ToLower())) continue;

                i++;

                if (this.hashDatos[key] == null)
                    parameters.Add(new SqlParameter("@" + key, DBNull.Value));
                else
                    parameters.Add(new SqlParameter("@" + key, this.hashDatos[key]));

                if (key.ToLower().CompareTo(this.idColumn.ToLower()) == 0) continue;

                cmdTxt += key + " = " + "@" + key + ",";

            }
            cmdTxt = cmdTxt.Substring(0, cmdTxt.Length - 1);
            cmdTxt += " WHERE " + this.idColumn + " = @" + this.idColumn;

            try
            {
                //Ejecutamos la consulta y si ha actualizado algún registro, devolvemos true. Si no, devolvemos false.
                bool actualizado = (DataAccessSql.ExecuteNonQuery(Constantes.CONNECTIONSTRING, CommandType.Text, cmdTxt, parameters.ToArray()) > 0);
                return actualizado;
            }
            catch(SqlException)
            {
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public virtual bool Modificar(SqlTransaction transaccion)
        {
            string cmdTxt = "UPDATE " + this.strTabla + " SET ";

            List<SqlParameter> parameters = new List<SqlParameter>();
            int i = -1;
            foreach (string key in this.hashDatos.Keys)
            {
                if (ignoreColumnsIds().Contains(key.ToLower()))
                    continue;

                i++;

                if (this.hashDatos[key] == null)
                    parameters.Add(new SqlParameter("@" + key, DBNull.Value));
                else
                    parameters.Add(new SqlParameter("@" + key, this.hashDatos[key]));

                if (key.ToLower().CompareTo(this.idColumn.ToLower()) == 0) continue;

                cmdTxt += key + " = " + "@" + key + ",";

            }
            cmdTxt = cmdTxt.Substring(0, cmdTxt.Length - 1);
            cmdTxt += " WHERE " + this.idColumn + " = @" + this.idColumn;

            try
            {
                bool actualizado = (DataAccessSql.ExecuteNonQuery(transaccion, CommandType.Text, cmdTxt, parameters.ToArray()) > 0);
                return actualizado;
            }
            catch (SqlException)
            {
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion

        #region Métodos (No tocar)

        /// <summary>
        /// Se encarga de rellenar los campos del objeto (this) con los datos almacenados en el registro "row".
        /// </summary>
        /// <param name="ds">Puntero al objeto DataSet que contiene todos los registros obtenidos en la consulta. (Se utiliza para poder conocer las columnas que contiene)</param>
        /// <param name="row">Registro del que queremos extraer toda la información que rellenará el objeto "this".</param>
        protected virtual void FillObjectFromDataRow(DataTable dt, DataRow row)
        {
            if (row != null) //Si el registro está creado...
            {
                foreach (DataColumn column in dt.Columns) //Para cada campo (columna) de la tabla...
                {
                    if (!Convert.IsDBNull(row[column.ColumnName]))
                    {
                        this.hashDatos[column.ColumnName] = row[column.ColumnName];
                    }
                    else
                    {
                        this.hashDatos[column.ColumnName] = null;
                    }

                }
            }
        }


        /// <summary>
        /// Ejecuta una consulta de selección genérica (SELECT ...). Nos devuelve un objeto de tipo DBOCollection, por tanto lo hacemos static.
        /// </summary>
        /// <param name="cmdTxt">Consulta SQL a ejecutar.</param>
        /// <returns>Devuelve los resultados obtenidos en una lista de objetos de tipo Noticia</returns>
        protected static DBOCollection ExecuteSelectQuery(DBOSql instance, string cmdTxt)
        {
            return ExecuteSelectQuery(instance, cmdTxt, null); //Devolvemos la lista .
        }


        /// <summary>
        /// Ejecuta una consulta de selección genérica (SELECT ...). Nos devuelve un objeto de tipo DBOCollection, por tanto lo hacemos static.
        /// </summary>
        /// <param name="cmdTxt">Consulta SQL a ejecutar.</param>
        /// <returns>Devuelve los resultados obtenidos en una lista de objetos de tipo Noticia</returns>
        protected static DBOCollection ExecuteSelectQuery(String connectionString, DBOSql instance, string cmdTxt)
        {
            return ExecuteSelectQuery(connectionString, instance, cmdTxt, null); //Devolvemos la lista .
        }

        /// <summary>
        /// Ejecuta una consulta de selección genérica (SELECT ...) en la que intervienen parámetros. Nos devuelve un objeto de tipo NoticiasCollection, por tanto lo hacemos static.
        /// </summary>
        /// <param name="cmdTxt">Consulta SQL a ejecutar.</param>
        /// <param name="parameters">Parémetros que intervienen en la consulta.</param>
        /// <returns>Devuelve los resultados obtenidos en una lista de objetos de tipo Usuario</returns>
        protected static DBOCollection ExecuteSelectQuery(DBOSql instance, string cmdTxt, SqlParameter[] parameters)
        {
            return ExecuteSelectQuery(Constantes.CONNECTIONSTRING, instance, cmdTxt, parameters); //Ejecutamos la consulta y obtenemos el DataSet correspondiente.

        }

        /// <summary>
        /// Ejecuta una consulta de selección genérica (SELECT ...) en la que intervienen parámetros. Nos devuelve un objeto de tipo NoticiasCollection, por tanto lo hacemos static.
        /// </summary>
        /// <param name="cmdTxt">Consulta SQL a ejecutar.</param>
        /// <param name="parameters">Parémetros que intervienen en la consulta.</param>
        /// <returns>Devuelve los resultados obtenidos en una lista de objetos de tipo Usuario</returns>
        protected static DBOCollection ExecuteSelectQuery(String connectionString, DBOSql instance, string cmdTxt, SqlParameter[] parameters)
        {
            DataTable dt = DataAccessSql.ExecuteDataTable(connectionString, CommandType.Text, cmdTxt, parameters); //Ejecutamos la consulta y obtenemos el DataSet correspondiente.

            //Preparamos un contenedor para los resultados, es decir, una lista.
            DBOCollection rowList = (DBOCollection)instance.getCollection();

            foreach (DataRow row in dt.Rows) //Para cada uno de los registros obtenidos...
            {
                DBOSql dbo = (DBOSql)instance.getObject();
                dbo.FillObjectFromDataRow(dt, row);
                //Añadimos el objeto a la lista (contenedor).
                rowList.Add(row[instance.idColumn].ToString(), dbo);
            }

            return rowList; //Devolvemos la lista .
        }
        #endregion

        #region Métodos Públicos

        /// <summary>
        /// Carga (obtiene los datos) del objeto.
        /// </summary>
        /// <returns>Devuelve true si ha podido ser cargado, o false si no existe o ha habido algún problema.</returns>
        public virtual bool Load()
        {
            string cmdTxt = "SELECT * FROM " + this.strTabla + " WHERE " + this.idColumn + "  = @ID";

            //Creamos los parámetros.
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@ID", this.getColumn(this.idColumn));

            //Si el usuario no existe o hay algún problema saltará una excepción.
            try
            {
                DataTable dt = DataAccessSql.ExecuteDataTable(Constantes.CONNECTIONSTRING, CommandType.Text, cmdTxt, parameters);
                DataRow row = dt.Rows[0];

                FillObjectFromDataRow(dt, row);
                return true; //Delvolvemos true.
            }
            catch (Exception)
            {
                return false; //...devolvemos false.
            }
        }

        /// <summary>
        /// Carga (obtiene los datos) de un colores.
        /// </summary>
        /// <param name="id">Identificador del usuario que queremos cargar.</param>
        /// <returns>Devuelve true si el usuario ha podido ser cargado, o false si no existe o ha habido algún problema.</returns>
        public virtual bool Load(object ID)
        {
            this.setColumn(this.idColumn, ID);

            return this.Load();
        }

        public static DBOCollection getAllFromQuery(DBOSql instance, string strSql, List<SqlParameter> param)
        {
            return ExecuteSelectQuery(instance, strSql, param.ToArray());
        }

        /// <summary>
        /// Obtiene todos los registros.
        /// </summary>
        /// <returns>Devuelve una lista de objetos de tipo Usuario con los resultados obtenidos.</returns>
        /// 
        public static DBOCollection getAll(DBOSql instance, string where, string order, bool ascending, List<SqlParameter> param)
        {
            string cmdTxt = "SELECT * FROM " + instance.strTabla;
            if (where != null && where != "")
            {
                cmdTxt += " WHERE " + where;
            }
            if (order != null && order != "")
            {
                cmdTxt += " ORDER BY " + order;
                if (ascending)
                    cmdTxt += " ASC";
                else
                    cmdTxt += " DESC";
            }
            return ExecuteSelectQuery(instance, cmdTxt, param.ToArray());
        }

        public static DBOCollection getAll(DBOSql instance, string where, string order, bool ascending)
        {
            return getAll(instance, where, order, ascending, new List<SqlParameter>());
        }


        public static DBOCollection getAll(DBOSql instance, string order, bool ascending)
        {
            return getAll(instance, null, order, ascending);
        }

        public static DBOCollection getAll(DBOSql instance, string where)
        {
            return getAll(instance, where, null, true);
        }

        public static DBOCollection getAll(DBOSql instance)
        {
            return getAll(instance, null, true);
        }

        public static List<T> getAllFromQuery<T>(string strSql, List<SqlParameter> param) 
            where T : DBOSql, new()
        {
            return ((DBOList)getAllFromQuery(new T(), strSql, param)).OfType<T>().ToList();
        }

        public static List<T> getAll<T>(string where, string order, bool ascending, List<SqlParameter> param) 
            where T : DBOSql, new()
        {
            return ((DBOList)getAll(new T(), where, order, ascending, param)).OfType<T>().ToList();
        }

        public static List<T> getAll<T>(string where, string order, bool ascending)
            where T : DBOSql, new()
        {
            return getAll<T>(where, order, ascending, new List<SqlParameter>());
        }

        public static List<T> getAll<T>(string order, bool ascending)
            where T : DBOSql, new()
        {
            return getAll<T>(null, order, ascending);
        }

        public static List<T> getAll<T>(string where, List<SqlParameter> param)
            where T : DBOSql, new()
        {
            return getAll<T>(where, null, true, param);
        }

        public static List<T> getAll<T>(string where)
            where T : DBOSql, new()
        {
            return getAll<T>(where, null, true);
        }

        public static List<T> getAll<T>()
            where T : DBOSql, new()
        {
            return getAll<T>(null, true);
        }

        public static DataTable getAllDS(DBOSql instance, string order, bool ascending)
        {
            string cmdTxt = "SELECT * FROM " + instance.strTabla;
            if (order != null && order != "")
            {
                cmdTxt += " ORDER BY " + order;
                if (ascending)
                    cmdTxt += " ASC";
                else
                    cmdTxt += " DESC";
            }
            return DataAccessSql.ExecuteDataTable(Constantes.CONNECTIONSTRING, CommandType.Text, cmdTxt);
        }

        public static DataTable getAllDS(DBOSql instance)
        {
            return getAllDS(instance, null, true);
        }


        public static SqlDataReader getAllDR(DBOSql instance, string order, bool ascending)
        {
            string cmdTxt = "SELECT * FROM " + instance.strTabla;
            if (order != null && order != "")
            {
                cmdTxt += " ORDER BY " + order;
                if (ascending)
                    cmdTxt += " ASC";
                else
                    cmdTxt += " DESC";
            }
            return DataAccessSql.ExecuteReader(Constantes.CONNECTIONSTRING, CommandType.Text, cmdTxt);
        }

        public static SqlDataReader getAllDR(DBOSql instance)
        {
            return getAllDR(instance, null, true);
        }


        public static bool existeValor(DBOSql instance, string columnName, object val)
        {
            //Creamos los parámetros.
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@val", val);

            string cmdTxt = "SELECT COUNT(*) FROM " + instance.strTabla + " WHERE " + columnName + " = @val";
            return (Convert.ToInt32(DataAccessSql.ExecuteScalar(Constantes.CONNECTIONSTRING, CommandType.Text, cmdTxt, parameters)) > 0);
        }

        #endregion

        public static void CreateTable(string strSql)
        {
            DataAccessSql.ExecuteScalar(Constantes.CONNECTIONSTRING, CommandType.Text, strSql);
        }

        public static void  DeleteTable(string table)
        {
            string strSql = string.Format("DROP TABLE {0}", table);
            DataAccessSql.ExecuteScalar(Constantes.CONNECTIONSTRING, CommandType.Text, strSql);
        }

        public static DataTable getAll (string tableName,string connection)
        {
            string strSql = string.Format("SELECT * FROM {0}", tableName);
            return DataAccessSql.ExecuteDataTable(connection, CommandType.Text, strSql);
        }

        public static DataTable getTablesInfo(string connectionString, string tableFilter = "")
        {
            string strSql = " SELECT DISTINCT TABLE_NAME,COLUMN_NAME, IS_NULLABLE,DATA_TYPE,CHARACTER_MAXIMUM_LENGTH,NUMERIC_PRECISION,ORDINAL_POSITION FROM INFORMATION_SCHEMA.COLUMNS ORDER BY TABLE_NAME,ORDINAL_POSITION ";

            DataTable data = DataAccessSql.ExecuteDataTable(connectionString, CommandType.Text, strSql);

            return data;
        }
            public static List<string> getTables(string connectionString,string tableFilter = "")
        {
            string strSql = "SELECT DISTINCT TABLE_NAME FROM INFORMATION_SCHEMA.COLUMNS";
            DataTable data =  DataAccessSql.ExecuteDataTable(connectionString, CommandType.Text, strSql);

            List<string> tableNames = new List<string>();
            foreach(DataRow row in data.Rows)
            {
                tableNames.Add(row[0].ToString());
            }

            if (string.IsNullOrEmpty(tableFilter))
            {
                return tableNames;
            }
            else
            {
                return getAllCoincidence(tableNames,tableFilter.Replace(" ","").Split(',').ToList());
            }
        }
        

        public static List<string> getAllCoincidence(List<string> list, List<string> comparators)
        {
            List<string> coincidences = new List<string>();

            foreach (string linea in list)
            {
                bool containsAll = true;
                foreach (string comparator in comparators)
                {
                    if (!linea.Contains(comparator))
                    {
                        containsAll = false;
                        break;
                    }
                }
                if (containsAll)
                {
                    coincidences.Add(linea);
                }
            }
            return coincidences;
        }

       private static List<List<T>> ChunkBy<T>(List<T> source, int chunkSize)
       {
           return source
               .Select((x, i) => new { Index = i, Value = x })
               .GroupBy(x => x.Index / chunkSize)
               .Select(x => x.Select(v => v.Value).ToList())
               .ToList();
       }

        /// <summary>
        ///		Inserta un registro en la base de datos. Requiere haber instanciado el objeto
        /// </summary>
        /// <returns>True si la operación se realiza correctamente</returns>
        /// 
        public static bool InsertarBatch<T>(List<T> objs, int batchSize = 10) where T : DBOSql
       {
           bool result = true;

           foreach(List<T> subList in ChunkBy(objs, batchSize))
           {
               string strSql = "";
               string cmdTxt = "INSERT INTO " + objs.First().strTabla + " (";

               int i = 0;
               T obj = subList.FirstOrDefault();
               List<SqlParameter> parameters = new List<SqlParameter>();
                List<string> guardarKeys = new List<string>();

               foreach (string key in obj.hashDatos.Keys)
               {
                   if (key.ToLower().CompareTo(obj.idColumn.ToLower()) == 0
                        || obj.ignoreColumnsIds().Contains(key.ToLower())) continue;

                    guardarKeys.Add(key);
                   i++;
               }
               cmdTxt += string.Join(",", guardarKeys) +  ") VALUES ";
               List<string> values = new List<string>();

               for (int o = 0; o < subList.Count; o++)
                {
                    T objeto = subList[o];
                    i = 0;
                    List<string> valuesAux = new List<string>();
                    foreach (string key in objeto.hashDatos.Keys)
                    {
                        if (key.ToLower().CompareTo(objeto.idColumn.ToLower()) == 0) continue;
                        valuesAux.Add("@" + key + o);

                        if (objeto.hashDatos[key] == null)
                            parameters.Add(new SqlParameter("@" + key + o, DBNull.Value));
                        else
                            parameters.Add(new SqlParameter("@" + key + o, objeto.hashDatos[key]));

                    }
                    values.Add("(" + string.Join(",", valuesAux) + ")");
                }
               strSql += cmdTxt + string.Join(",", values) + "; SELECT SCOPE_IDENTITY()";

               //Ejecutamos la consulta y si ha insertado algún registro, devolvemos true. Si no, devolvemos false.
               try
               {
                   object scalar = DataAccessSql.ExecuteScalar(Constantes.CONNECTIONSTRING, CommandType.Text, strSql, parameters.ToArray());
                   if (scalar == null)
                   {
                       result = false;
                   }
                   //subList.FirstOrDefault().setColumn(this.idColumn, scalar);
               }
               catch (SqlException)
               {
                   result = false;
               }
               catch (Exception)
               {
                   result = false;
               }
           }
           return result;
       }
   }

        /// <summary>
        /// Clase que representa a una lista de objetos de tipo Colores. Hereda de ArrayList y por tanto 
        /// implementa el interface IEnumerator, lo que permite utilizarla como DataSource de un control.
        /// También permite ordenar la lista de objetos según los criterios que vayamos añadiendo.
        /// </summary>
        public interface DBOCollection : ICollection
    {
        void Add(string key, object val);
    }

    public class DBOList : ArrayList, DBOCollection
    {
        public void Add(string key, object val)
        {
            this.Add(val);
        }
    }

    public class DBOHashtable : Hashtable, DBOCollection
    {
        public void Add(string key, object val)
        {
            this.Add((object)key, val);
        }
    }
}
