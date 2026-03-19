using System.Collections.Generic;
using Banx.Utility.Guid;
using Eflatun.SceneReference;

namespace SystemsGrimoire.SaveSystem {
    public class SaveCampaignData {
        public int SaveId;
        public System.DateTime SaveTime;

        #region Quest
        public List<QuestSaveData> Quests;
        #endregion

        #region World
        public SerializableGUID LastSavePointGUID;
        public SceneReference LastSaveScene;
        #endregion

        public SaveCampaignData() {
            Quests = new List<QuestSaveData>();
        }

        public SaveCampaignData(int saveId) : this() {
            SaveId = saveId;
            SaveTime = System.DateTime.Now;
        }

        public void SetQuestInfo(QuestSaveData saveData) {
            if (Quests.IndexOf(saveData) == -1) {
                Quests.Add(saveData);
            }
            else {
                Quests[Quests.IndexOf(saveData)] = saveData;
            }
        }
    }
}
