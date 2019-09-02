using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBI.DAL.OBJECTS
{
    public class Users : DBOSql
    {
        #region propiedades
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

        #region métodos
        public static int getUserID(string userName)
        {
            string where = "NAME = @userName AND ACTIVE = 1";

            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@userName", userName));

            List<Users> users = Users.getAll<Users>(where, param);

            return users.FirstOrDefault()?.ID ?? -1;
        }

        #endregion
    }
}
