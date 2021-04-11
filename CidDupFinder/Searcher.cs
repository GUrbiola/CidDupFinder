using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CidDupFinder
{
    public partial class Searcher : Form
    {
        public string Path { get; set; }
        public List<string> Filters { get; set; }

        public Searcher(string Path, List<string> Filters)
        {
            InitializeComponent();
            this.Path = Path;
            this.Filters = Filters ?? new List<string>();
        }

        private void Searcher_Load(object sender, EventArgs e)
        {
            
        }

        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker helperBW = sender as BackgroundWorker;
            DoSearch();
        }
        private void bgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int visibleItems = listLog.ClientSize.Height / listLog.ItemHeight;
            listLog.Items.Add(e.UserState.ToString());
            listLog.TopIndex = Math.Max(listLog.Items.Count - visibleItems + 1, 0);
        }

        private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private void DoSearch()
        {
            bgWorker.ReportProgress(0, "Searching for Files....");
            List<SFile> files = new List<SFile>();
            List<string> paths = new List<string>();
            Dictionary<long, int> Sizes = new Dictionary<long, int>();
            Dictionary<string, int> Partials = new Dictionary<string, int>();
            Dictionary<string, int> Partials2 = new Dictionary<string, int>();
            Dictionary<string, int> Fulls = new Dictionary<string, int>();
            string filter;

            int filterIndex = 0;
            do
            {
                filter = (Filters != null && Filters.Count > 0) ? Filters[filterIndex] : "";
                string[] fileList;

                if (String.IsNullOrEmpty(filter))
                    fileList = Directory.GetFiles(this.Path, "*.*", SearchOption.AllDirectories);
                else
                    fileList = Directory.GetFiles(this.Path, filter, SearchOption.AllDirectories);

                foreach (string path in fileList)
                {
                    if (!paths.Contains(path.ToUpper()))
                    {
                        FileInfo buff = new FileInfo(path);
                        files.Add(new SFile(buff));
                        if (Sizes.ContainsKey(buff.Length))
                            Sizes[buff.Length]++;
                        else
                            Sizes.Add(buff.Length, 1);

                        paths.Add(path.ToUpper());
                    }
                }

                filterIndex++;
            } while (Filters != null && filterIndex < Filters.Count);

            bgWorker.ReportProgress(0, "Files Found: " + files.Count.ToString("0,0"));

            bgWorker.ReportProgress(0, "First test, Uniqueness by size." + files.Count.ToString("0,0"));
            int totalFiles = files.Count, totalUnique = 0;
            foreach (SFile sf in files)
            {
                if (Sizes.ContainsKey(sf.FileInfo.Length) && Sizes[sf.FileInfo.Length] == 1)
                { 
                    sf.Status = SearchFileStatus.Unique;
                    totalUnique++;
                }
            }
            bgWorker.ReportProgress(0, "Found: " + totalUnique.ToString("0,0") + " unique files." + (totalFiles - totalUnique).ToString("0,0") + " files need to be further inspected." );

            bgWorker.ReportProgress(0, "Second test, Uniqueness by first MB's Hash(MD5).");
            int totalFiles1 = 0, totalUnique1 = 0; 
            foreach (SFile sf in files.Where(x => x.Status != SearchFileStatus.Unique))
            {
                totalFiles1++;
                sf.CalculateHash(1024);
                if (Partials.ContainsKey(sf.Code))
                    Partials[sf.Code]++;
                else
                    Partials.Add(sf.Code, 1);
            }
            foreach (SFile sf in files.Where(x => x.Status != SearchFileStatus.Unique))
            {
                if (Partials.ContainsKey(sf.Code) && Partials[sf.Code] == 1)
                {
                    sf.Status = SearchFileStatus.Unique;
                    totalUnique1++;
                }
            }
            bgWorker.ReportProgress(0, "Found: " + totalUnique1.ToString("0,0") + " unique files." + (totalFiles1 - totalUnique1).ToString("0,0") + " files need to be further inspected.");

            bgWorker.ReportProgress(0, "Third test, Uniqueness by first 5 MB's Hash(MD5).");
            int totalFiles2 = 0, totalUnique2 = 0;
            foreach (SFile sf in files.Where(x => x.Status != SearchFileStatus.Unique))
            {
                totalFiles2++;
                sf.CalculateHash(5120);
                if (Partials2.ContainsKey(sf.Code))
                    Partials2[sf.Code]++;
                else
                    Partials2.Add(sf.Code, 1);
            }
            foreach (SFile sf in files.Where(x => x.Status != SearchFileStatus.Unique))
            {
                if (Partials2.ContainsKey(sf.Code) && Partials2[sf.Code] == 1)
                {
                    sf.Status = SearchFileStatus.Unique;
                    totalUnique2++;
                }
            }
            bgWorker.ReportProgress(0, "Found: " + totalUnique2.ToString("0,0") + " unique files." + (totalFiles2 - totalUnique2).ToString("0,0") + " files need to be further inspected.");

            bgWorker.ReportProgress(0, "Last test, Uniqueness by Hash(MD5).");
            int totalFiles3 = 0, totalUnique3 = 0;
            foreach (SFile sf in files.Where(x => x.Status != SearchFileStatus.Unique))
            {
                totalFiles3++;
                sf.CalculateHash(10240);
                if (Fulls.ContainsKey(sf.Code))
                    Fulls[sf.Code]++;
                else
                    Fulls.Add(sf.Code, 1);
            }
            foreach (SFile sf in files.Where(x => x.Status != SearchFileStatus.Unique))
            {
                if (Fulls.ContainsKey(sf.Code) && Fulls[sf.Code] == 1)
                {
                    sf.Status = SearchFileStatus.Unique;
                    totalUnique3++;
                }
                else
                {
                    sf.Status = SearchFileStatus.Duplicate;
                }
            }
            bgWorker.ReportProgress(0, "Found: " + totalUnique3.ToString("0,0") + " unique files." + (totalFiles3 - totalUnique3).ToString("0,0") + " are duplicated files, creating list of them.");

            Dictionary<string, Duplicate> dups = new Dictionary<string, Duplicate>();
            foreach (SFile sf in files.Where(x => x.Status == SearchFileStatus.Duplicate))
            {
                if (dups.ContainsKey(sf.Code))
                    dups[sf.Code].AddFile(sf);
                else
                    dups.Add(sf.Code, new Duplicate(sf.Code, sf));
            }

            bgWorker.ReportProgress(0, "Found: " + dups.Count.ToString("0,0") + " unique duplicated files.");
            bgWorker.ReportProgress(0, "**********************************************************************************************");

            int dupNumber = 1;
            List<long> dupSizes = new List<long>();
            foreach (Duplicate dup in dups.Values)
            {
                if (!dupSizes.Contains(dup.Files[0].FileInfo.Length))
                    dupSizes.Add(dup.Files[0].FileInfo.Length);
            }
            dupSizes.Sort();
            for (int i = dupSizes.Count - 1; i >= 0; i--)
            {
                var ldups = dups.Values.ToList();
                foreach (Duplicate dup in ldups.Where(x => x.Files[0].FileInfo.Length == dupSizes[i]))
                {
                    bgWorker.ReportProgress(0, String.Format("Duplicates Number {0}: {1} Files were found to be copies.", dupNumber, dup.Files.Count));
                    foreach (SFile sf in dup.Files)
                    {
                        bgWorker.ReportProgress(0, sf.FileInfo.FullName);
                    }
                    bgWorker.ReportProgress(0, "**********************************************************************************************");
                    dupNumber++;
                }
            }

            //foreach (Duplicate dup in dups.Values)
            //{
            //    bgWorker.ReportProgress(0, String.Format("Duplicates Number {0}: {1} Files were found to be copies.", dupNumber, dup.Files.Count));
            //    foreach (SFile sf in dup.Files)
            //    {
            //        bgWorker.ReportProgress(0, sf.FileInfo.FullName);
            //    }
            //    bgWorker.ReportProgress(0, "**********************************************************************************************");
            //    dupNumber++;
            //}
        }

        private void Searcher_Shown(object sender, EventArgs e)
        {
            bgWorker.RunWorkerAsync();
        }

        private void Searcher_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
    }

    public class Duplicate
    {
        public List<SFile> Files { get; set; }
        public string Code { get; set; }

        public Duplicate(string Code, SFile File)
        {
            this.Files = new List<SFile>();
            this.Files.Add(File);
            this.Code = Code;
        }

        internal void AddFile(SFile File)
        {
            this.Files.Add(File);
        }
    }

}
