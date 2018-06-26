using System;
using System.Collections.Generic;
using System.Text;
using Utils.DOD;
using UtilsMain.DOD;

namespace UtilsExperimental
{
    public static class Program
    {
        static DataStream<int> Entities;
        public static int Main()
        {
            MainLoop loop = new MainLoop();
            loop.Update();
            return 0;
        }
    }
    public static class Entity
    {
        static HashSet<int> IDs;

        
    }

    //public static class StreamManager
    //{
    //    enum SType
    //    {
    //        pos,
    //        phys,
    //        hp,
    //        donkey,
    //    }
    //    static Dictionary<SType, DataStream> streams = new Dictionary<SType, Utils.DOD.DataStream>();
    //}
}
