using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EasyBI.DAL.OBJECTS;

namespace EasyBI.EasyBI
{
    public class tabExtraction
    {
        #region Clases

        #endregion

        #region Métodos

        public static List<TreeNode> GetTreeNodes(Folders folder, List<Folders> folders)
        {
            List<TreeNode> nodes = new List<TreeNode>();
            List<TreeNode> auxNodes = new List<TreeNode>();
            TreeNode aux;
            List<Folders> childFolders = folders.Where(obj => obj.PARENT_ID == folder.ID).ToList();

            if (childFolders.Any())
            {
                foreach (Folders fold in childFolders)
                {
                    auxNodes.AddRange(GetTreeNodes(fold, folders));
                }
                aux = new TreeNode(folder.NAME, auxNodes.ToArray());
                aux.Tag = folder.ID;
               
                nodes.Add(aux);
            }
            else
            {
                aux = new TreeNode(folder.NAME);
                aux.Tag = folder.ID;

                nodes.Add(aux);
            }

            return nodes;

        }

        public static List<int> getAllChildrenNodes(TreeNode node)
        {
            List<int> nodes = new List<int>();
            
            if (node.Nodes.Count > 0)
            {
                foreach (TreeNode nod in node.Nodes)
                {
                    nodes.AddRange(getAllChildrenNodes(nod));
                }
                nodes.Add(Convert.ToInt32(node.Tag));
            }
            else
            {
                nodes.Add(Convert.ToInt32(node.Tag));
            }

            return nodes;
        }

        public static void readCSVFile()
        {
            
            

        }
        #endregion
    }
}
