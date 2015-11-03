
namespace SLua
{
    using LuaInterface;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;


    public class LuaModule : LuaObject
    {
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static public int RegModule(IntPtr l)
        {
            try
            {
                string script;
                LuaObject.checkType(l, 1, out script);
                LuaState L = LuaState.get(l);
                L.doString(script);
                return 0;
            }
            catch (Exception e)
            {
                return error(l, e);
            }
        }

        static public void reg(IntPtr l)
        {
            reg(l, RegModule, "UnityEngine");
        }
    }
}
