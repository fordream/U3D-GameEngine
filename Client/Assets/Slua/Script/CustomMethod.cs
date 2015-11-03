using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SLua
{

    public class CustomMethodAttribute : System.Attribute
    {
        public CustomMethodAttribute(bool isStatic)
        {
            IsStatic = isStatic;
        }
        public bool IsStatic;
    }

    public class CustomMethod_BinaryWriter
    {
        [CustomMethod(true)]
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
        [CustomMethod(true)]
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

    public class CustomMethod_GameObject
    {
        static Type GetExportTypes(string classname)
        {
            return LuaHelper.GetType(classname);
        }

        [CustomMethod(false)]
        public static Component AddComponent(ref GameObject self,string componentname)
        {
            Type t = GetExportTypes(componentname);
            if (null != t)
                return self.AddComponent(t);
            return null;
        }
    }

    public class CustomMethod_UnityEngineObject
    {
        [CustomMethod(false)]
        public static UnityEngine.Object Instantiate(UnityEngine.Object self, string name = null)
        {
            UnityEngine.Object obj = UnityEngine.Object.Instantiate(self);
            if (!string.IsNullOrEmpty(name))
                obj.name = name;

            return obj;
        }
    }

}
