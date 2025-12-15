using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casino.SaveLoadData
{
    internal class FileSystemSaveLoadService : ISaveLoadService<string>
    {
        private string _path;

        public FileSystemSaveLoadService(string path)
        {
            
            _path = path;
        }

        public string LoadData(string name)
        {
            string fullPath = Path.Combine(_path, name);
            bool isFileExist = File.Exists(fullPath);
            if (!isFileExist)
            {
                File.Create(fullPath);
            }
            return File.ReadAllText(fullPath);
        }

        public void SaveData(string data, string name)
        {
            string fullPath = Path.Combine(_path, name);
            bool isFileExist = File.Exists(fullPath);
            if (!isFileExist)
            {
                File.Create(fullPath);
            }

            File.WriteAllText(fullPath, data);
        }
    }
}
