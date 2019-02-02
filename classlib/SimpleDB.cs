using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace simpledb2
{
    [Serializable]
    internal class DataWrapper{
        public object SerializableValue { get; set; }
    }

    internal class Mtuple{
        public int _OffSet { get; set; }
        public int _DataLen { get; set; }
    }
    public class SimpleDB
    {
        Dictionary<object, Mtuple> map;
        string SimpleDBPath;
        BinaryFormatter BinF;

        public SimpleDB()
        {
            map = new Dictionary<object, Mtuple>();
            BinF = new BinaryFormatter();

            if(RuntimeInformation.IsOSPlatform(OSPlatform.Windows)){
                SimpleDBPath = @"..\simpledb.bin";
            }else{
                SimpleDBPath = @"../simpledb.bin";
            }    
        }
        
        /*
        Sets data in key - value storage file simpledb.bin
         */        
         public void SetData(object key, object data){
             using (FileStream fs = new FileStream(SimpleDBPath, FileMode.Append, FileAccess.Write))
             {
                 using (BinaryWriter bw = new BinaryWriter(fs))
                 {
                     //object breakdown
                    bw.Write(Encoding.UTF8.GetBytes("\n")); // start of data indicator
                    bw.Write(ObjToByteArr(new DataWrapper(){SerializableValue=key})); // key
                    bw.Write(Encoding.UTF8.GetBytes(":")); // key-data divider
                    bw.Write(ObjToByteArr(new DataWrapper(){SerializableValue=data})); //data
                    bw.Write(Encoding.UTF8.GetBytes("#")); // End of data indicator 
                 }
             }
         }


        public void GetData(object key){
            using (FileStream fs = new FileStream(SimpleDBPath, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    if (map.TryGetValue(key, out Mtuple DataOffsets)){
                        fs.Seek(DataOffsets._OffSet,SeekOrigin.Begin);
                        var res = br.ReadBytes(DataOffsets._DataLen-DataOffsets._OffSet);
                        var tmp = ByteArrToObj(res);
                        System.Console.WriteLine("value : " + tmp.SerializableValue.ToString());
                    }
                    else{
                        System.Console.WriteLine("Key not found");
                    }
                }
            }
        }


        /*Maintains in-memory offset dictionary */
        public void CreateMap(){
            using(FileStream fs = new FileStream(SimpleDBPath, FileMode.Open, FileAccess.Read)){
                using (BinaryReader br = new BinaryReader(fs)){
                    int offSet = 0;
                    while(br.BaseStream.Position!= br.BaseStream.Length){
                        byte SingleByte = br.ReadByte();
                        offSet++;
                        //looking for start of key
                        // 10 = "\n"
                        if(SingleByte == 10){
                            List<byte> keyBuilder = new List<byte>();
                            List<byte> dataBuilder = new List<byte>();
                            SingleByte = br.ReadByte();
                            offSet++;
                            //looking for key-data divider
                            // 58 = ":"
                            while(SingleByte != 58){
                                keyBuilder.Add(SingleByte);
                                SingleByte=br.ReadByte();
                                offSet++;
                            }
                            var key = ByteArrToObj(keyBuilder.ToArray());
                            var keylen = offSet;//keyBuilder.ToArray().Length;

                            //looking fr end of data
                            // 35 = "#"
                            while(SingleByte != 35 && br.BaseStream.Position!= br.BaseStream.Length){
                                dataBuilder.Add(SingleByte);
                                SingleByte=br.ReadByte();
                                offSet++;
                            }
                            var datalen = dataBuilder.ToArray().Length;
                            map[key.SerializableValue]=new Mtuple(){_OffSet=keylen, _DataLen=offSet};
                            //offSet +=  keylen + 3 + datalen;
                        }    
                    }
                    
                }
            }
        }


        /*
        Print all keys and offset-values in Dictionary(Hashmap)
         */
        public void printMap(){
            foreach (var item in map){
                System.Console.WriteLine($"Key [{item.Key}] - OffSet [{item.Value._OffSet}] - DataLen [{item.Value._DataLen}]");
            }
        }

        /*
        Byte converters - turning an object into an array of byte and vice visa
         */
        private byte[] ObjToByteArr(object obj){
            using (MemoryStream ms = new MemoryStream())
            {
                BinF.Serialize(ms, obj);
                return ms.ToArray();    
            }
        }

        private DataWrapper ByteArrToObj(byte[] ByteArr){
            using (MemoryStream ms = new MemoryStream(ByteArr))
            {
                var res = BinF.Deserialize(ms);
                return res as DataWrapper;
            }
        }
    }
}