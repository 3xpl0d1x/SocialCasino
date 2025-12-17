using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoGame.SaveLoadData
{
    internal interface ISaveLoadService<T>
    {
        void SaveData(T data, string name);

        T LoadData(string name);
    }
}
