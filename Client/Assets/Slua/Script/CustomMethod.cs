using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SLua
{
    public class CustomMethod_BinaryWriter
    {
        public static BinaryWriter Create(byte[] buff = null)
        {
            MemoryStream mms ;
            if (buff != null)
                mms = new MemoryStream(buff);
            else
                mms = new MemoryStream();
            BinaryWriter bw = new BinaryWriter(mms);

            return bw;
        }
    }
    public class CustomMethod_BinaryReader
    {
        public static BinaryReader Create(byte[] buff = null)
        {
            MemoryStream mms;
            if (buff != null)
                mms = new MemoryStream(buff);
            else
                mms = new MemoryStream();
            BinaryReader br = new BinaryReader(mms);

            return br;
        }
    }

}
