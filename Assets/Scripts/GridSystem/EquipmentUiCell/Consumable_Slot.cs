using data;
using EquipmentSystem;

namespace GridSystem
{
    public class Consumable_Slot : PlayerEquipmentSlot
    {
        public int index = 1;
        private Consumable_Data consumableData => packageItemData as Consumable_Data;
        public override PackageItemType ItemType => PackageItemType.Consumable;

        protected override void Init()
        {
            LocalPlayerDataThing localPlayerDataThing = LocalPlayerDataThing.GetData();
            switch (index)
            {
                case 1:
                    packageItemData = localPlayerDataThing.consumable_1;
                    break;
                case 2:
                    packageItemData = localPlayerDataThing.consumable_2;
                    break;
                case 3:
                    packageItemData = localPlayerDataThing.consumable_3;
                    break;
                case 4:
                    packageItemData = localPlayerDataThing.consumable_4;
                    break;
            }
            base.Init();
        }

        public override void SavePlayerEquipmentSlotData()
        {
            LocalPlayerDataThing localPlayerDataThing = LocalPlayerDataThing.GetData();
            switch (index)
            {
                case 1:
                    localPlayerDataThing.consumable_1 = consumableData;
                    break;
                case 2:
                    localPlayerDataThing.consumable_2 = consumableData;
                    break;
                case 3:
                    localPlayerDataThing.consumable_3 = consumableData;
                    break;
                case 4:
                    localPlayerDataThing.consumable_4 = consumableData;
                    break;
            }
            LocalPlayerDataThing.Save();
        }
    }
    
}
