using System;
using System.IO;
using Banx.Utility.Guid;

namespace SystemsGrimoire.SaveSystem {
    [Serializable]
    public class QuestSaveData : IEquatable<QuestSaveData>, IBinarySaveData {
        public SerializableGUID QuestGUID;
        public int State;
        public int CurrentStageIndex;
        public int CurrentStageAmount;

        public QuestSaveData() { }

        public QuestSaveData(SerializableGUID questGUID, int state, int currentStageIndex, int currentStageAmount) {
            QuestGUID = questGUID;
            State = state;
            CurrentStageIndex = currentStageIndex;
            CurrentStageAmount = currentStageAmount;
        }

        public bool Equals(QuestSaveData other) {
            return QuestGUID.Equals(other.QuestGUID)
                && State == other.State
                && CurrentStageIndex == other.CurrentStageIndex
                && CurrentStageAmount == other.CurrentStageAmount;
        }

        public override bool Equals(object obj) {
            return obj is QuestSaveData other && Equals(other);
        }

        public override int GetHashCode() {
            return HashCode.Combine(QuestGUID, State, CurrentStageIndex, CurrentStageAmount);
        }

        public bool IsMeetingSaveCriteria => true;

        public void Write(BinaryWriter bw) {
            bw.Write(QuestGUID.ToString());
            bw.Write(State);
            bw.Write(CurrentStageIndex);
            bw.Write(CurrentStageAmount);
        }

        public void Read(BinaryReader br) {
            QuestGUID = new SerializableGUID(br.ReadString());
            State = br.ReadInt32();
            CurrentStageIndex = br.ReadInt32();
            CurrentStageAmount = br.ReadInt32();
        }
    }
}
