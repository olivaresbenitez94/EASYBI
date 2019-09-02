using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBI.DAL.OBJECTS
{ // en la version local no se usa
    public class Folders : DBOSql
    {
        #region Propiedades
        public int ID
        {
            get
            {
                return Convert.ToInt32(getColumn("ID"));
            }
            set
            {
                setColumn("ID", value);
            }
        }

        public int USER_ID
        {
            get
            {
                return Convert.ToInt32(getColumn("USER_ID"));
            }
            set
            {
                setColumn("USER_ID", value);
            }
        }

        public int? PARENT_ID
        {
            get
            {
                return (int?) Convert.ToInt32(getColumn("PARENT_ID"));
            }
            set
            {
                setColumn("PARENT_ID", value);
            }
        }

        public string NAME
        {
            get
            {
                return Convert.ToString(getColumn("NAME"));
            }
            set
            {
                setColumn("NAME", value);
            }
        }

        public DateTime CREATED_DATE
        {
            get
            {
                return Convert.ToDateTime(getColumn("CREATED_DATE"));
            }
            set
            {
                setColumn("CREATED_DATE", value);
            }
        }

        public bool ACTIVE
        {
            get
            {
                return Convert.ToBoolean(getColumn("ACTIVE"));
            }
            set
            {
                setColumn("ACTIVE", value);
            }
        }

        #endregion

        #region Métodos

        public bool createFolder(int? parent, string name)
        {
            return new Folders
            {
                PARENT_ID = parent,
                NAME = name,
                ACTIVE = true,
                CREATED_DATE = DateTime.UtcNow,
                USER_ID = Users.getUserID(System.Security.Principal.WindowsIdentity.GetCurrent().Name)
            }.Insertar();
        }

        public void createFolder(string name)
        {
             createFolder(null, name);
        }

        public static List<Folders> getFolders()
        {
            int USER_ID = Users.getUserID(Environment.UserName);
            string where = " USER_ID = @userID ";

            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@userID", USER_ID));

            return Folders.getAll<Folders>(where, param).OrderBy(fold => fold.NAME).ToList();
        }

        public static bool deleteFolders(List<int> ids) 
        {
            bool result = true;

            foreach (int id in ids)
            {
                result = new Folders
                {
                    ID = id
                }.Eliminar();
            }

            return result;
        }

        #endregion
    }
}
