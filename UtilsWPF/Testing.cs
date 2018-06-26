using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.DOD;
using UtilsMain.DOD;

namespace Utils
{
    public static class Program
    {
        static DataStream<int> Entities;
        public static void Main()
        {
            MainLoop loop = new MainLoop();
            int two = FoldMapUCase.Zumbro.AlwaysTwo;
            loop.Update();

            dynamic poo = 2;
            if (poo is int i)
            {
                i++;
            }
        }
    }
    public static class Entity
    {
        static HashSet<int> IDs;

        
    }
}
