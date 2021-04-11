using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace CidDupFinder
{
    public class SFile
    {
        public FileInfo FileInfo { get; set; }
        public long CodeProgress { get; set; }
        public string Code { get; set; }
        public SearchFileStatus Status { get; set; }


        public SFile(FileInfo FileInfo)
        {
            CodeProgress = 0;
            this.FileInfo = FileInfo;
            this.Status = SearchFileStatus.ToCheck;
        }

        public SFile(FileInfo fileInfo, long codeProgress, string code, SearchFileStatus status) : this(fileInfo)
        {
            CodeProgress = codeProgress;
            Code = code;
            Status = status;
        }

        public string CalculateHash(int byteCount = 0)
        {
            int bytesToRead;

            if (byteCount <= 0)
                return CalculateFullHash();

            try
            {
                if (FileInfo.Length < byteCount)
                    bytesToRead = (int)FileInfo.Length;
                else
                    bytesToRead = byteCount;

                byte[] buffer = new byte[bytesToRead];
                using (FileStream fs = new FileStream(FileInfo.FullName, FileMode.Open, FileAccess.Read))
                {
                    fs.Read(buffer, 0, bytesToRead);
                    fs.Close();
                }

                byte[] hash;
                using (var md5 = System.Security.Cryptography.MD5.Create())
                {
                    md5.TransformFinalBlock(buffer, 0, buffer.Length);
                    hash = md5.Hash;
                }

                this.CodeProgress = byteCount;
                this.Code = ToHex(hash, true);

            }
            catch (Exception ex)
            {
                return "Exception for File: " + FileInfo.Name + Environment.NewLine + "Exception Message: " + ex.Message;
            }
            
            return "OK";
        }

        public string CalculateFullHash()
        {
            try
            {
                using (var md5 = MD5.Create())
                {
                    using (var stream = File.OpenRead(FileInfo.FullName))
                    {
                        byte[] hash = md5.ComputeHash(stream);

                        this.CodeProgress = -1;
                        this.Code = ToHex(hash, true);
                    }
                }
            }
            catch (Exception ex)
            {
                return "Exception for File: " + FileInfo.Name + Environment.NewLine + "Exception Message: " + ex.Message;
            }

            return "OK";
        }

        public string ToHex(byte[] bytes, bool upperCase)
        {
            StringBuilder result = new StringBuilder(bytes.Length * 2);

            for (int i = 0; i < bytes.Length; i++)
                result.Append(bytes[i].ToString(upperCase ? "X2" : "x2"));

            return result.ToString();
        }
    }
}
