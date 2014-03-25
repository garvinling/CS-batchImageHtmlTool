using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

            Console.Write("User browsing for files.\n");

            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            DialogResult result = folderBrowser.ShowDialog();
            String folderDirectory = "";


            if (result == DialogResult.OK)
            {
                //Get the file path here
                folderDirectory = folderBrowser.SelectedPath;
                Console.Write("Path is: " + folderDirectory);

            }


            //Get Directory Name

            //Check that folder contains only .JPGs. otherwise only store JPG paths.

            //Set name of files.   directoryName_#

            //Resize/scale to aspect ratio

            //output html with filename to .txt on desktop 












            /**
            Console.Write("User browsing for files.");
            
            OpenFileDialog fileDialog = new OpenFileDialog();
            try
            {
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    Console.Write("Dialog OK.");
                    String path = fileDialog.FileName;
                    Console.Write("Path is:" + path);
                }
            }
            catch (Exception ex)
            {

            }
             **/



        }

     
    }
}
