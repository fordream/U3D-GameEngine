// Copyright (c) 2015 hugula
// direct https://github.com/tenvick/hugula
//
using System.Collections;
using System.IO;
using System;
using System.Text;
[SLua.CustomLuaClass]
public class LMsg  
{

	public LMsg()
	{
		buff = new MemoryStream();
		br = new BinaryReader(buff);
	}

    public LMsg(byte[] bytes)
	{
		buff =new MemoryStream(bytes);
		br = new BinaryReader(buff);
	}
	
	public long Length
	{
		get
        {
			return buff.Length;
		}
	}
	
	public long Position
	{
		get
        {
		    return buff.Position;
		}
		set
        {
			buff.Position=value;
		}
	}
	
	public byte[] ToArray()
    {
        return buff.ToArray();
    }
	
	public string Debug()
	{
        byte[] bts = ToArray();
		
		string bstr="";
		
		foreach(byte i in bts)
		{
			bstr+=" "+i+" ";
		}
        return bstr;
	}
	
	/// <summary>
	/// our message pro
	/// </summary>
	/// <returns>
	/// The C array.
	/// </returns>
	public byte[] ToCArray()
	{
		byte[] data = ToArray();

        short len = (short)(data.Length + 2);//date[].length+type(short)
		short type=(short)this.Type;

		byte[] lenBytes = BitConverter.GetBytes(len);// date.length
		System.Array.Reverse(lenBytes);

		byte[] typeBytes = BitConverter.GetBytes(type);//tyep bytes
		System.Array.Reverse(typeBytes);

        int allLen = lenBytes.Length + typeBytes.Length + data.Length;
		byte[] send = new byte[allLen];
		
		lenBytes.CopyTo(send,0);//len
		typeBytes.CopyTo(send,lenBytes.Length);//type
        data.CopyTo(send, lenBytes.Length + typeBytes.Length);
		
		return send;
	}

    public static LMsg FromCArray(byte[] buf)
    {
        try
        {
            MemoryStream stream = new MemoryStream(buf);

            byte[] dataHeader = new byte[2];
            stream.Read(dataHeader, 0, 2);
            Array.Reverse(dataHeader);
            ushort datalen = BitConverter.ToUInt16(dataHeader, 0);
            byte[] typeHeader = new byte[2];
            stream.Read(typeHeader, 0, 2);
            Array.Reverse(typeHeader);
            ushort type = BitConverter.ToUInt16(typeHeader, 0);

            if (datalen > 0)
            {
                byte[] message = new byte[datalen];
                stream.Read(message, 0, message.Length);

                byte[] lenBytes = BitConverter.GetBytes(datalen);// date.length
                System.Array.Reverse(lenBytes);

                byte[] data = new byte[datalen + lenBytes.Length];
                lenBytes.CopyTo(data, 0);//len

                message.CopyTo(data, lenBytes.Length);
                LMsg msg = new LMsg(data);
                msg.Type = type;

                string s = msg.ReadString();

                return msg;
            }

            return null;
        }
        catch
        {
            return null;
        }
    }
	
	public int Type
	{
		get
        {
			return _type;	
		}
		set
		{
			_type=value;
		}
	}
	
	#region write
    public void Write(byte value)
    {
        buff.WriteByte(value);
    }
	public void Write(byte[] value)
	{
		buff.Write(value,0,value.Length);
	}
	
	public void Write(bool value)
	{
		buff.WriteByte(value ? ((byte)1) : ((byte)0));
	}

    public void Write(char value)
	{
		byte b= Convert.ToByte(value);
        Write(b);
	}
	
	public void Write(ushort value)
	{
		byte[] bytes =BitConverter.GetBytes(value);
		WriteBigEndian(bytes);
	}
    public void Write(short value)
    {
        byte[] bytes = BitConverter.GetBytes(value);
        WriteBigEndian(bytes);
    }

	public void Write(uint value)
	{
		byte[] bytes =BitConverter.GetBytes(value);
		WriteBigEndian(bytes);
	}
    public void Write(int value)
    {
        byte[] bytes = BitConverter.GetBytes(value);
        WriteBigEndian(bytes);
    }
	
	public void Write(ulong value)
	{
		byte[] bytes =BitConverter.GetBytes(value);
		WriteBigEndian(bytes);
	}
    public void Write(long value)
    {
        byte[] bytes = BitConverter.GetBytes(value);
        WriteBigEndian(bytes);
    }
	
    public void Write(float value)
	{
		byte[] bytes = BitConverter.GetBytes(value);			
		WriteBigEndian(bytes);
	}
    public void Write(double value)
    {
        byte[] bytes = BitConverter.GetBytes(value);
        WriteBigEndian(bytes);
    }

    public void Write(string value)
    {
        UTF8Encoding utf8Encoding = new UTF8Encoding();
        short byteCount = (short)utf8Encoding.GetByteCount(value);
        byte[] buffer = utf8Encoding.GetBytes(value);
        Write(byteCount);
        if (buffer.Length > 0)
            Write(buffer);
    }
	
	public void WriteUTFBytes(string value)
	{
		UTF8Encoding utf8Encoding = new UTF8Encoding();
		byte[] buffer = utf8Encoding.GetBytes(value);
		if (buffer.Length > 0)
			Write(buffer);
	}
	
	#endregion
	
	#region read
	
	public bool ReadBoolean()
	{
		return br.ReadBoolean();
	}
	
	public byte ReadByte()
	{
		return br.ReadByte();
	}
	
	public char ReadChar()
	{
		byte byt = br.ReadByte();
		return Convert.ToChar(byt);
	}
		
	public ushort ReadUShort()
	{
		byte[] bytes = br.ReadBytes(2);
		Array.Reverse(bytes);
		return BitConverter.ToUInt16( bytes, 0 );
	}
	
	public uint ReadUInt()
	{
		byte[] bytes = br.ReadBytes(4);
		Array.Reverse(bytes);
		return BitConverter.ToUInt32( bytes, 0 );
	}
	
	public ulong ReadULong()
	{
		byte[] bytes = br.ReadBytes(8);
		Array.Reverse(bytes);
		return BitConverter.ToUInt64( bytes, 0 );
	}
	
	public  short ReadShort()
	{
		byte[] bytes = br.ReadBytes(2);
		Array.Reverse(bytes);
		return BitConverter.ToInt16(bytes,0);
	}
	
	public int ReadInt()
	{
		byte[] bytes = br.ReadBytes(4);
		Array.Reverse(bytes);
		return BitConverter.ToInt32(bytes,0);
	}
	
	public float ReadFloat()
	{
		byte[] bytes = br.ReadBytes(4);
		Array.Reverse(bytes);
		float value = BitConverter.ToSingle(bytes, 0);
		return value;
	}
	
	public string ReadString()
	{
		int length = ReadShort();
		return ReadUTF(length);
	}
	
	public string ReadUTF(int length)
	{
		if( length == 0 )
            return string.Empty;

		byte[] encodedBytes = br.ReadBytes(length);
        string decodedString = Encoding.UTF8.GetString(encodedBytes, 0, encodedBytes.Length);
        return decodedString;
	}
	
	#endregion
	
	#region member

    protected void WriteBigEndian(byte[] bytes)
	{
		if( bytes == null )
			return;
		for(int i = bytes.Length-1; i >= 0; i--) //		for(int i = 0; i < bytes.Length; i++)
		{
			buff.WriteByte( bytes[i] );
		}
	}
	
	protected MemoryStream buff;
    protected BinaryReader br;

    protected int _type;
	
	#endregion
	
}
