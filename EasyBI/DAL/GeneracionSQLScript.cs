using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EasyBI.DAL
{
    public class GeneracionSQLScript
    {
        #region objetos
        [XmlRoot("POWERMART")]
        public class POWERMART
        {
            [XmlElement("REPOSITORY")]
            public List<REPOSITORY> Repositories { get; set; }
        }

        public class REPOSITORY
        {
            [XmlAttribute("NAME")]
            public string Name { get; set; }

            [XmlElement("FOLDER")]
            public List<FOLDER> Folders { get; set; }

        }

        public class FOLDER
        {
            [XmlAttribute("NAME")]
            public string Name { get; set; }

            [XmlElement("SOURCE")]
            public List<SOURCE> Sources { get; set; }

        }

        public class SOURCE
        {
            [XmlAttribute("NAME")]
            public string Name { get; set; }

            [XmlElement("SOURCEFIELD")]
            public List<SOURCEFIELD> SourceFields { get; set; }
        }

        public class SOURCEFIELD
        {
            [XmlAttribute("DATATYPE")]
            public string DataType { get; set; }

            [XmlAttribute("NAME")]
            public string Name { get; set; }

            [XmlAttribute("PRECISION")]
            public string Precision { get; set; }

        }
        #endregion

        #region métodos

        public static string createSQLScript(string Fichero)
        {
            string sqls = "";
            string sql = " SET ANSI_NULLS ON" + Environment.NewLine +
                         " GO" + Environment.NewLine +
                         " SET QUOTED_IDENTIFIER ON" + Environment.NewLine +
                         " CREATE TABLE {0} (" + Environment.NewLine + "{1} " + Environment.NewLine + ") " + Environment.NewLine
                         + " ON[PRIMARY]" + Environment.NewLine + " GO " + Environment.NewLine + Environment.NewLine;


        
                string contents = File.ReadAllText(Fichero);
                XmlSerializer serializer = new XmlSerializer(typeof(POWERMART));
                using (TextReader reader = new StringReader(contents))
                {
                    POWERMART result = (POWERMART)serializer.Deserialize(reader);
                    foreach (REPOSITORY rep in result.Repositories)
                    {
                        foreach (FOLDER fold in rep.Folders)
                        {
                            foreach (SOURCE sour in fold.Sources)
                            {
                                List<string> columns = new List<string>();
                                columns.Add("\t[ID_REGISTRO] [numeric](19, 0) NOT NULL");
                                columns.Add("\t[ID_EJECUCION][numeric](19, 0) NOT NULL");
                                columns.Add("\t[FE_CARGA] [datetime] NOT NULL");
                                columns.Add("\t[ID_FICHERO] [varchar] (255) NOT NULL");

                                foreach (SOURCEFIELD srf in sour.SourceFields)
                                {
                                    columns.Add(string.Format("\t[{0}] [varchar] ({1}) NULL", srf.Name.ToUpper(), srf.Precision));
                                }

                                sqls += string.Format(sql, sour.Name, string.Join("," + Environment.NewLine, columns));
                            }
                        }
                    }
                }
            return sqls;
        }

        #endregion
    }
}
