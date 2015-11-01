// Copyright (c) 2015 hugula
// direct https://github.com/tenvick/hugula

using UnityEngine;
using System.Collections;
[SLua.CustomLuaClass]
public class DESHelper : MonoBehaviour {

	public KeyVData KEYData;

	public KeyVData IVData;


	// Use this for initialization
	void Awake() {
		_desHlper = this;
	}

	public byte[] Key
	{
		get{
            if (null != KEYData)
                return KEYData.KEY;
            else
                return null;
		}
	}

	public byte[] IV
	{
		get{
            if (null != IVData)
                return IVData.IV;
            else
                return null;
		}
	}

	private static DESHelper _desHlper; 

	public static DESHelper instance{
		get{
			return _desHlper;
		}
	}

}
