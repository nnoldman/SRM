using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AJson
{
    public class Json
    {
        public delegate string ConverToString(object obj);
        public delegate object ConverToObject(Type type, string content);
        public class Creator
        {
            public ConverToObject OBJCreator;
            public ConverToString STRCreator;
        }
        public T Deserialize<T>(string content) where T : new()
        {
            return new T();
        }

        public T DeserializeFromFile<T>(string path) where T : new()
        {
            string content = File.ReadAllText(path);
            return Deserialize<T>(content);
        }

        static Type TP_Int = typeof(int);
        static Type TP_Int32 = typeof(Int32);
        static Type TP_UInt = typeof(uint);
        static Type TP_UInt32 = typeof(UInt32);
        static Type TP_Int64 = typeof(Int64);
        static Type TP_UInt64 = typeof(UInt64);
        static Type TP_Double = typeof(double);
        static Type TP_Long = typeof(long);
        static Type TP_Float = typeof(float);
        static Type TP_String = typeof(string);
        static Type TP_Char = typeof(char);
        static Type TP_Short = typeof(short);
        static Type TP_UShort = typeof(ushort);
        static Type TP_Byte = typeof(byte);
        static Type TP_SByte = typeof(sbyte);
        static Type TP_Enum = typeof(Enum);
        static Type TP_List = typeof(List<>);
        static Type TP_Array = typeof(Array);
        static Type TP_Dictionary = typeof(Dictionary<,>);

        Dictionary<Type, Creator> mCreators = new Dictionary<Type, Creator>();

        public void Inialize()
        {
            mCreators.Add(TP_Int, new Creator()
            {
                OBJCreator = (type, content) => int.Parse(content),
                STRCreator = (obj) => obj.ToString()
            });
            mCreators.Add(TP_Int32, new Creator()
            {
                OBJCreator = (type, content) => Int32.Parse(content),
                STRCreator = (obj) => obj.ToString()
            });
            mCreators.Add(TP_UInt, new Creator()
            {
                OBJCreator = (type, content) => uint.Parse(content),
                STRCreator = (obj) => obj.ToString()
            });
            mCreators.Add(TP_UInt32, new Creator()
            {
                OBJCreator = (type, content) => UInt32.Parse(content),
                STRCreator = (obj) => obj.ToString()
            });
            mCreators.Add(TP_Int64, new Creator()
            {
                OBJCreator = (type, content) => Int64.Parse(content),
                STRCreator = (obj) => obj.ToString()
            });
            mCreators.Add(TP_UInt64, new Creator()
            {
                OBJCreator = (type, content) => UInt64.Parse(content),
                STRCreator = (obj) => obj.ToString()
            });
            mCreators.Add(TP_Double, new Creator()
            {
                OBJCreator = (type, content) => double.Parse(content),
                STRCreator = (obj) => obj.ToString()
            });
            mCreators.Add(TP_Long, new Creator()
            {
                OBJCreator = (type, content) => long.Parse(content),
                STRCreator = (obj) => obj.ToString()
            });
            mCreators.Add(TP_Float, new Creator()
            {
                OBJCreator = (type, content) => float.Parse(content),
                STRCreator = (obj) => obj.ToString()
            });
            mCreators.Add(TP_String, new Creator()
            {
                OBJCreator = (type, content) => content,
                STRCreator = (obj) => (string)obj
            });
            mCreators.Add(TP_Char, new Creator()
            {
                OBJCreator = (type, content) => char.Parse(content),
                STRCreator = (obj) => obj.ToString()
            });
            mCreators.Add(TP_Short, new Creator()
            {
                OBJCreator = (type, content) => int.Parse(content),
                STRCreator = (obj) => obj.ToString()
            });
            mCreators.Add(TP_UShort, new Creator()
            {
                OBJCreator = (type, content) => ushort.Parse(content),
                STRCreator = (obj) => obj.ToString()
            });
            mCreators.Add(TP_Byte, new Creator()
            {
                OBJCreator = (type, content) => byte.Parse(content),
                STRCreator = (obj) => obj.ToString()
            });
            mCreators.Add(TP_SByte, new Creator()
            {
                OBJCreator = (type, content) => sbyte.Parse(content),
                STRCreator = (obj) => obj.ToString()
            });

            mCreators.Add(TP_Enum, new Creator()
            {
                OBJCreator = (type, content) => Enum.Parse(type, content, true),
                STRCreator = (obj) => obj.ToString()
            });
            mCreators.Add(TP_Array, new Creator()
            {
                OBJCreator = (type, content) => int.Parse(content),
                STRCreator = (obj) => obj.ToString()
            });
            mCreators.Add(TP_Dictionary, new Creator()
            {
                OBJCreator = (type, content) => int.Parse(content),
                STRCreator = (obj) => obj.ToString()
            });
        }

        public string Serialize(object obj)
        {
            var type = obj.GetType();

            Creator c = null;

            if (type == TP_Array)
            {

            }
            else if (type == TP_List)
            {

            }
            else if (type == TP_Dictionary)
            {

            }
            else if (mCreators.TryGetValue(type, out c))
            {
                return c.STRCreator(obj);
            }
            else
            {

            }
            throw new Exception();
        }

        public bool SerializeToFile(object obj, string path)
        {
            string content = Serialize(obj);
            File.WriteAllText(path, content);
            return true;
        }
    }
}
