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
using System.Text.RegularExpressions;

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
            String[] filePaths;
            Console.WriteLine("User browsing for files.\n");

            dir = getFolderDirectory();                 

            if (dir != null)
            {
                folderName = getFolderName(dir);        //get selected directory name
            }

            filePaths = getAllJPGFiles(dir);            //Store .jpg filenames into string[]

            changeJPGNames(filePaths,folderName);
            
         

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
        private String[] getAllJPGFiles(String dir)
        {
            //TODO: need to convert any .gifs or .pngs to .jpg. so we do need multiple extension filter

            String[] filePaths = Directory.GetFiles(@dir, "*.jpg");                         //works for JPG as well
            Console.WriteLine("The number of files ending with .jpg is {0}.", filePaths.Length);

            for (int i = 0; i < filePaths.Length; i++)
            {
                Console.WriteLine("File: " + filePaths[i]);

            }

            return filePaths;

        }//end getAllJPGFiles()



        private void changeJPGNames(String[] filePaths,String dirName)
        {
            string path = "";
            string newPath = "";
            string prefixString = "";


            for (int i = 0; i < filePaths.Length; i++)
            {
                path = @filePaths[i];
                prefixString = new FileInfo(path).Directory.FullName;
                newPath = prefixString + "\\" + dirName + i + ".jpg";
                Console.WriteLine("Copied to: " + newPath);

                if (File.Exists(path) && !File.Exists(newPath))
                {

                     resizeImage(path,newPath);

                      //file already exists exception 
                   File.Delete(path);
                }
            }//end for
        }//end changeJPGNames





        private void resizeImage(String oldPath, String newPath)
        {

            //original height/ original width * new width = new height
            Image img = Image.FromFile(oldPath);

            int width = img.Width;
            int height = img.Height;
            double ratio = (double)height / (double)width;
            double newHeight = ratio * (double)530;

            var newImage = new Bitmap(530, (int)newHeight);
            Graphics.FromImage(newImage).DrawImage(img, 0, 0, 530, (int)newHeight);

            newImage.Save(@newPath);

            img.Dispose();
            newImage.Dispose();

        }




    }


}
