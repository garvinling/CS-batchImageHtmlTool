using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String dir = "";
            String folderName = "";
            Console.WriteLine("User browsing for files.\n");

            dir = getFolderDirectory();                 

            if (dir != null)
            {
                folderName = getFolderName(dir);        //get selected directory name
            }

            getAllJPGFiles(dir);
  
            //Check that folder contains only .JPGs. otherwise only store JPG paths.

            //Set name of files.   directoryName_#

            //Resize/scale to aspect ratio

            //output html with filename to .txt on desktop 

        }//end Browse Onclick



        /*
         * getFolderDirectory prompts user with folderbrowser and returns directory path of the selected folder
         * 
         * */
        private String getFolderDirectory()
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            DialogResult result = folderBrowser.ShowDialog();
            String folderDirectory = "";
            if (result == DialogResult.OK)
            {
                //Get the file path here
                folderDirectory = folderBrowser.SelectedPath;
                return folderDirectory;
            }

            return null;
        }//end getFolderDirectory()




        /*
         * getFolderName() parses the directory string and returns the folder-name
         * 
         * */
        private String getFolderName(String folderDirectory)
        {
            String folderName = "";

            Console.WriteLine("Path is: " + folderDirectory);
            int dirNameIndex = folderDirectory.LastIndexOf('\\');
            int lastDirNameIndex = folderDirectory.Length;
            int length = lastDirNameIndex - dirNameIndex;
            folderName = folderDirectory.Substring(dirNameIndex + 1, length - 1);
            Console.WriteLine("Folder Name: " + folderName);

            return folderName;
        }

     
        /*
         * getAllJPGFiles()
         * 
         * */
        private void getAllJPGFiles(String dir)
        {
            string[] filePaths = Directory.GetFiles(@dir, "*.jpg");
            Console.WriteLine("The number of files ending with .jpg is {0}.", filePaths.Length);

            foreach (string dirs in filePaths)
            {
                Console.WriteLine("Array: " +dir);
            }




        }






    }


}
