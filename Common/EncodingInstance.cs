using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public class EncodingInstance
    {
        private static Encoding instance;
        public static Encoding Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = UTF32Encoding.UTF8;
                }
                return instance;
            }
        }
    }
}
