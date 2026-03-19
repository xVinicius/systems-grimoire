using System;
using System.Collections.Generic;
using System.IO;

namespace SystemsGrimoire.SaveSystem {
    public interface IBinarySaveData {
        bool IsMeetingSaveCriteria { get; }

        void Write(BinaryWriter bw);
        void Read(BinaryReader br);
    }

    public static class IBinarySaveDataExtensions {
        public static void Write<T>(this List<T> binaryDataList, BinaryWriter bw) where T : class, IBinarySaveData, new() {
            var dataToSave = binaryDataList.FindAll(item => item.IsMeetingSaveCriteria);
            bw.Write(dataToSave.Count);

            for (var i = 0; i < dataToSave.Count; i++) {
                var item = dataToSave[i];
                item.Write(bw);
            }
        }

        public static void Read<T>(this List<T> binaryDataList, BinaryReader br)
            where T : class, IBinarySaveData, new() {
            binaryDataList.Clear();
            var itemCount = br.ReadInt32();
            for (var i = 0; i < itemCount; i++) {
                binaryDataList.Add(Create<T>(br));
            }
        }

        // TODO: Add Writer and Reader for List<List<T>>

        public static T Create<T>(BinaryReader br) where T : class, IBinarySaveData {
            T newBinaryData = Activator.CreateInstance<T>();
            newBinaryData.Read(br);
            return newBinaryData;
        }
    }
}
