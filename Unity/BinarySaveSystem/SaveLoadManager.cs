using System;
using UnityEngine;
using System.IO;
using Banx.Utility.Guid;
using Eflatun.SceneReference;

namespace SystemsGrimoire.SaveSystem {
    public static class SaveLoadManager {
        private static readonly string s_SaveVersion = "1";

        public static void WriteSave(SaveCampaignData saveData) {
            try {
                RecreateSaveDirectory();

                using var fs = new FileStream(GenerateSaveFileName(saveData.SaveId), FileMode.Create, FileAccess.Write);
                using var bw = new BinaryWriter(fs);

                bw.Write(s_SaveVersion);
                bw.Write(saveData.SaveId);
                bw.Write(saveData.SaveTime.ToBinary());

                #region Quest
                saveData.Quests.Write(bw);
                #endregion

                #region World
                bw.Write(saveData.LastSavePointGUID.ToString());
                bw.Write(saveData.LastSaveScene.Guid);
                #endregion
            }
            catch (Exception ex) {
                Debug.LogError($"Error while writing save slot {saveData.SaveId}! {ex.Message} Inner: {ex.InnerException?.Message ?? "None"}");
            }
        }

        public static void DeleteSave(int slotId) {
            File.Delete(GenerateSaveFileName(slotId));
        }

        public static SaveCampaignData ReadSave(int slotId) {
            if (!File.Exists(GenerateSaveFileName(slotId)))
                return null;

            try {
                RecreateSaveDirectory();

                var saveData = new SaveCampaignData();

                using var fs = new FileStream(GenerateSaveFileName(slotId), FileMode.Open, FileAccess.Read);
                using var br = new BinaryReader(fs);

                if (br.ReadString() != s_SaveVersion) {
                    // TODO: Implement an updater for old save files
                    throw new NotImplementedException("Updater not implemented for old save files");
                }
                saveData.SaveId = br.ReadInt32();
                saveData.SaveTime = DateTime.FromBinary(br.ReadInt64());

                #region Quest
                saveData.Quests.Read(br);
                #endregion

                #region World
                saveData.LastSavePointGUID = new SerializableGUID(br.ReadString());
                saveData.LastSaveScene = new SceneReference(br.ReadString());
                #endregion

                return saveData;
            }
            catch (Exception ex) {
                Debug.LogError($"Error while reading save slot {slotId}! {ex.Message} Inner: {ex.InnerException?.Message ?? "None"}");
                DeleteSave(slotId);
                return null;
            }
        }

        public static SaveCampaignData WriteTestingSave(SaveCampaignData saveData) {
            saveData.SaveId = 1;
            saveData.SaveTime = DateTime.Now;
            saveData.Quests.Add(new QuestSaveData(new SerializableGUID(Guid.NewGuid().ToString()), 0, 0, 0));
            saveData.LastSavePointGUID = new SerializableGUID(Guid.NewGuid().ToString());
            saveData.LastSaveScene = new SceneReference(Guid.NewGuid().ToString());
            return saveData;
        }

        private static void RecreateSaveDirectory() {
            var savePath = $"{Application.persistentDataPath}{Path.AltDirectorySeparatorChar}Saves";
            if (!Directory.Exists(savePath))
                Directory.CreateDirectory(savePath);
        }

        private static string GenerateSaveFileName(int slotId) {
            return $"{Application.persistentDataPath}{Path.AltDirectorySeparatorChar}Saves{Path.AltDirectorySeparatorChar}BanxSaves{slotId}.banxsave";
        }
    }
}
