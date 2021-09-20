using CMS.Containers;

namespace AutoRepairBench
{
    public static class ItemExtensions
    {
        public static string ToPrettyString(this GroupItem groupItem)
        {
            if (groupItem == null) return "null";
            
            return ($"(GroupItem) ID: {groupItem.ID}, UID: {groupItem.UID}, " +
                            $"Condition: {groupItem.GetCondition()}, ConditionToShow: {groupItem.GetConditionToShow()}, " +
                            $"Size: {groupItem.Size}, IsNormalGroup: {groupItem.IsNormalGroup}");
        }
        
        public static string ToPrettyString(this BaseItem baseItem)
        {
            if (baseItem == null) return "null";
            return ($"(BaseItem) ID: {baseItem.ID}, UID: {baseItem.UID}, " +
                            $"Condition: {baseItem.GetCondition()}, ConditionToShow: {baseItem.GetConditionToShow()}");
        }

        public static string ToPrettyString(this Item item)
        {
            if (item == null) return "";

            return ($"(Item) ID: {item.ID}, UID: {item.UID}, Condition: {item.Condition}, Dent: {item.Dent}, " +
                    $"Quality: {item.Quality}, IsExamined: {item.IsExamined}, RepairAmount: {item.RepairAmount}, " +
                    $"WashFactor: {item.WashFactor}, OutsideRustEnabled: {item.OutsideRustEnabled}");
        }

        public static void FixItem(this Item item)
        {
            if (item == null) return;
            item.Condition = 1.0f;
            item.Dent = 0.0f;
        }
    }
}