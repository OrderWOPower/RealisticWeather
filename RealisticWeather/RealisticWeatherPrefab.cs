using Bannerlord.UIExtenderEx.Attributes;
using Bannerlord.UIExtenderEx.Prefabs2;

namespace RealisticWeather
{
    public class RealisticWeatherPrefab
    {
        // Add the Rain Density selection button.
        [PrefabExtension("CustomBattleScreen", "descendant::Widget[@DataSource='{MapSelectionGroup}' and .//@Parameter.SelectorDataSource='{TimeOfDaySelection}']")]
        [PrefabExtension("NavalCustomBattleScreen", "descendant::Widget[@DataSource='{MapSelectionGroup}' and .//@Parameter.SelectorDataSource='{TimeOfDaySelection}']")]
        public class RainDensityPrefab : PrefabExtensionInsertPatch
        {
            public override InsertType Type => InsertType.Append;

            [PrefabExtensionText]
            public string Text => "<Widget DataSource=\"{MapSelectionGroup}\" WidthSizePolicy=\"Fixed\" SuggestedWidth=\"590\" HorizontalAlignment=\"Center\" HeightSizePolicy=\"CoverChildren\" MarginLeft=\"!InnerPanel.LeftRight.Padding\" MarginRight=\"!InnerPanel.LeftRight.Padding\" MarginTop=\"15\"><Children><TextWidget WidthSizePolicy=\"CoverChildren\" HeightSizePolicy=\"CoverChildren\" HorizontalAlignment=\"Left\" VerticalAlignment=\"Center\" Brush=\"CustomBattle.Value.Text\" Text=\"@RainDensityText\" /><Standard.DropdownWithHorizontalControl HorizontalAlignment=\"Right\" Parameter.SelectorDataSource=\"{RainDensitySelection}\" /></Children></Widget>";
        }

        // Add the Fog Density selection button.
        [PrefabExtension("CustomBattleScreen", "descendant::Widget[@DataSource='{MapSelectionGroup}' and .//@Parameter.SelectorDataSource='{RainDensitySelection}']")]
        [PrefabExtension("NavalCustomBattleScreen", "descendant::Widget[@DataSource='{MapSelectionGroup}' and .//@Parameter.SelectorDataSource='{RainDensitySelection}']")]
        public class FogDensityPrefab : PrefabExtensionInsertPatch
        {
            public override InsertType Type => InsertType.Append;

            [PrefabExtensionText]
            public string Text => "<Widget DataSource=\"{MapSelectionGroup}\" WidthSizePolicy=\"Fixed\" SuggestedWidth=\"590\" HorizontalAlignment=\"Center\" HeightSizePolicy=\"CoverChildren\" MarginLeft=\"!InnerPanel.LeftRight.Padding\" MarginRight=\"!InnerPanel.LeftRight.Padding\" MarginTop=\"15\"><Children><TextWidget WidthSizePolicy=\"CoverChildren\" HeightSizePolicy=\"CoverChildren\" HorizontalAlignment=\"Left\" VerticalAlignment=\"Center\" Brush=\"CustomBattle.Value.Text\" Text=\"@FogDensityText\" /><Standard.DropdownWithHorizontalControl HorizontalAlignment=\"Right\" Parameter.SelectorDataSource=\"{FogDensitySelection}\" /></Children></Widget>";
        }
    }
}
