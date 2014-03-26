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
using System.Web;

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
            List<String> htmlOutput = new List<String>();
            Console.WriteLine("User browsing for files.\n");

            
            dir = getFolderDirectory();                 

            if (dir != null)
            {
                folderName = getFolderName(dir);        //get selected directory name
            }

            filePaths = getAllJPGFiles(dir);            //Store .jpg filenames into string[]

            changeJPGNames(filePaths,folderName);

            folderDirectoryLabel.Text = folderDirectoryLabel.Text + " \\" +folderName;

            htmlOutput = generateHTML(filePaths);

            writeTextFile(htmlOutput);

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
            List<String> files = Directory.GetFiles(@dir, "*").ToList();



            foreach (string values in files)
            {
                if (values.EndsWith(".png") || values.EndsWith(".gif") || values.EndsWith(".jpg"))
                {
                    Console.WriteLine("File Found: " + values);
                }
                else
                {
                    files.Remove(values);
                }
            }
            String[] output = files.ToArray();
            filesFound.Text = "Files Found: " + output.Length;

            return output;

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

                    resizeImage(530,path,newPath);

                    File.Delete(path);
                }
            }//end for
        }//end changeJPGNames





        private void resizeImage(int resizeWidth, String oldPath, String newPath)
        {

            //original height/ original width * new width = new height
            Image img = Image.FromFile(oldPath);

            int width = img.Width;
            int height = img.Height;

            if (width < 530)
            {
                String errors = errorLabel.Text;
                img.Dispose();
                errors = errors + "\nImage width is less than 530. No conversion was made on " + oldPath;
                errorLabel.Text = errors;
                //add to error data structure 
                return; 
            }
            
            double ratio = (double)height / (double)width;
            double newHeight = ratio * (double)resizeWidth;

            var newImage = new Bitmap(resizeWidth, (int)newHeight);
            Graphics.FromImage(newImage).DrawImage(img, 0, 0, resizeWidth, (int)newHeight);

            newImage.Save(@newPath);

            img.Dispose();
            newImage.Dispose();
           
        }//end resizeImage 


        private List<String> generateHTML(String[] filePaths)
        {
            /**
            <div class="separator" style="clear: both; text-align: center;">
            <a href="" imageanchor="1" style="margin-left: 1em; margin-right: 1em;">
            <img border="0" src="http://images.dailydawdle.com/FileNameHERE" alt="funny pic, humor, cool videos, cool art, cool designs, funny photos, cute dogs, funny cats, fail, weird photos, daily dawdle" title="funny pic, humor, cool videos, cool art, cool designs, funny photos, cute dogs, funny cats, fail, weird photos, daily dawdle" />
            </a> 
            </div>
             **/

            List<String> htmlOutput = new List<String>();
            StringWriter stringWriter = new StringWriter();
            String divOpen = "<div class=\"separator\" style=\"clear: both; text-align:center;\">\n";
            String anchorOpen = "<a href=\"\" imageanchor=\"\" style=\"margin-left:1em; margin-right:1em;\">\n";
            String imgOpen = "<img border=\"0\" src=\"";
            String imgClose = "\" alt=\"funny pic, humor, cool videos, cool art, cool designs, funny photos, cute dogs, funny cats, fail, weird photos, daily dawdle\" title=\"funny pic, humor, cool videos, cool art, cool designs, funny photos, cute dogs, funny cats, fail, weird photos, daily dawdle\"/>\n";
            String anchorClose = "</a>\n";
            String divClose = "</div>\n";


            for (int i = 0; i < filePaths.Length; i++)
            {

                String fileName = getFolderName(filePaths[i]);  //reuse the getfoldername() function to get the file name 
                String htmlItem = divOpen + anchorOpen + imgOpen + "http://images.dailydawdle.com/" + fileName + imgClose + anchorClose + divClose;
                htmlOutput.Add(htmlItem);
            }

            return htmlOutput;

        }




        private void writeTextFile(List<String> htmlOutput)
        {

            String username = Environment.UserName;
            Console.WriteLine("User: " + username);
            int moreCounter = 0;
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\\Users\\" + username + "\\Desktop\\DailyDawdleOutput.txt"))
            {
                foreach (string htmlItem in htmlOutput)
                {
                    
                    file.WriteLine(htmlItem);
                    moreCounter++;

                    if (moreCounter == 3)
                    {
                        file.WriteLine("\n<!--more-->\n");
                        //moreCounter = 0;
                    }
                }

            }



        }









    }


}
