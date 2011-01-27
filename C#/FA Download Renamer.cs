using System;
using System.Collections;
using System.IO;
using System.Windows.Forms;

namespace FA_Download_Renamer
{
    internal class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            if (args.Length >= 1)
            {
                string fn = args[0];
                if (string.IsNullOrEmpty(fn) || !Directory.Exists(fn))
                {
                    Console.WriteLine("Cannot load the selected directory.");
                }
                else
                {
                    Environment.CurrentDirectory = fn;
                    DoSearch(fn);
                }
            }
            else
            {
                Console.WriteLine("Select a directory...");
                FolderBrowserDialog fbd = new FolderBrowserDialog();

                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    if (string.IsNullOrEmpty(fbd.SelectedPath) || !Directory.Exists(fbd.SelectedPath))
                    {
                        MessageBox.Show("Cannot load the selected directory.", "FA Download Renamer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        Environment.CurrentDirectory = fbd.SelectedPath;
                        DoSearch(fbd.SelectedPath);
                    }
                }
            }
        }

        private static void DoSearch(string path)
        {
            Console.WriteLine(path);
            string[] fileList = Directory.GetFiles(path);
            Console.WriteLine("Found {0} files", fileList.Length);
            int renamed = 0;
			ArrayList errorFiles = new ArrayList();

            for (int i = 0; i < fileList.Length; i++)
            {
                string bn = Path.GetFileName(fileList[i]);

                Console.WriteLine("Renaming {0}/{1} ({2:P0}) : {3}", i + 1, fileList.Length, (float)i / (float)fileList.Length, bn);

                string nn = null;
                for (int ix = 0; ix < bn.Length; ix++)
                {
                    if (bn[ix] == '.')
                    {
                        nn = bn.Substring(ix + 1);
                        break;
                    }
                    else if (!char.IsDigit(bn[ix]))
                    {
                        break;
                    }
                }

                if (!string.IsNullOrEmpty(nn))
                {
                	try {
                    	File.Move(fileList[i], Path.Combine(path, nn));
                    	renamed++;
                	} catch {
                		errorFiles.Add(bn);
                	}
                }
            }

            MessageBox.Show(renamed.ToString() + " files renamed.", "FA Download Renamer", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (errorFiles.Count > 0){
            	Console.WriteLine();
            	Console.WriteLine("Could not renanme:");
            	foreach (object fn in errorFiles){
            		Console.WriteLine(fn.ToString());
            	}
            	MessageBox.Show(errorFiles.Count.ToString() + " files could not be renamed.", "FA Download Renamer", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
