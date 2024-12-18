using DailyRoutines.Abstracts;
using Dalamud.Plugin.Services;
using OmenTools;
using OmenTools.Helpers;

namespace DailyRoutines.ModuleTemplate;

public class GPoseObjectBorder : DailyModuleBase
{
    public override ModuleInfo Info => new()
    {
        Title = "自动调整边缘高亮效果",
        Description = "进入集体动作之后自动关闭，退出后恢复",
        Category = ModuleCategories.General,
        Author = ["Due"],
    };

    public override void Init()
    {
        TaskHelper ??= new TaskHelper { TimeLimitMS = 30_000 };

        DService.Framework.Update += ToggleConfig;
    }

    public static void ToggleConfig(IFramework framework)
    {
        var config = DService.GameConfig.UiControl.GetBool("ObjectBorderingType");
        if (DService.ClientState.IsGPosing && config == true)
        {
            DService.GameConfig.UiControl.Set("ObjectBorderingType", false);
        }
        else if (!DService.ClientState.IsGPosing && config == false)
        {
            DService.GameConfig.UiControl.Set("ObjectBorderingType", true);
        }
    }

    public override void Uninit()
    {
        DService.Framework.Update -= ToggleConfig;
        DService.GameConfig.UiControl.Set("ObjectBorderingType", false);

        base.Uninit();
    }
}
