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

        public virtual bool Eliminar()
        {
            string cmdTxt = "DELETE FROM " + this.strTabla + " WHERE " + this.idColumn + " = @ID ";

            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@ID", this.getColumn(idColumn));

            try
            {
                bool eliminado = DataAccessSql.ExecuteNonQuery(Constantes.sqlConnectionString, CommandType.Text, cmdTxt, parameters) > 0;
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
                object scalar = DataAccessSql.ExecuteScalar(Constantes.sqlConnectionString, CommandType.Text, cmdTxt, parameters.ToArray());
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

        #region Métodos 
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
        protected static DBOCollection ExecuteSelectQuery(DBOSql instance, string cmdTxt, SqlParameter[] parameters)
        {
            return ExecuteSelectQuery(Constantes.sqlConnectionString, instance, cmdTxt, parameters); //Ejecutamos la consulta y obtenemos el DataSet correspondiente.

        }
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
        public virtual bool Load()
        {
            string cmdTxt = "SELECT * FROM " + this.strTabla + " WHERE " + this.idColumn + "  = @ID";

            //Creamos los parámetros.
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@ID", this.getColumn(this.idColumn));

            //Si el usuario no existe o hay algún problema saltará una excepción.
            try
            {
                DataTable dt = DataAccessSql.ExecuteDataTable(Constantes.sqlConnectionString, CommandType.Text, cmdTxt, parameters);
                DataRow row = dt.Rows[0];

                FillObjectFromDataRow(dt, row);
                return true; //Delvolvemos true.
            }
            catch (Exception)
            {
                return false; //...devolvemos false.
            }
        }
        public virtual bool Load(object ID)
        {
            this.setColumn(this.idColumn, ID);

            return this.Load();
        }

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

        public static List<T> getAll<T>(string where, string order, bool ascending, List<SqlParameter> param) 
            where T : DBOSql, new()
        {
            return ((DBOList)getAll(new T(), where, order, ascending, param)).OfType<T>().ToList();
        }

        public static List<T> getAll<T>(string where, List<SqlParameter> param)
            where T : DBOSql, new()
        {
            return getAll<T>(where, null, true, param);
        }
     
        #endregion

   }

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
