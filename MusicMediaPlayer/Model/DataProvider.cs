using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicMediaPlayer.Model
{
    public class DataProvider
    {
        private static DataProvider _ints;
        public static DataProvider Ints 
        { 
            get 
            { 
                if (_ints == null) 
                    _ints = new DataProvider(); 
                return _ints; 
            } 
            set 
            { 
                _ints = value; 
            } 
        }
        public MusicPlayerEntities DB { get; set; } 
        private DataProvider()
        {
            DB = new MusicPlayerEntities();
        }
    }
}
