using Blazorise;

namespace AdminAssistant.Blazor;

internal static class Themes
{
    private static class Common
    {
        internal static readonly bool IsRounded = true;
        internal static readonly bool IsGradient = false;
    }

    public static Theme Light => new Theme()
    {
        IsRounded = Common.IsRounded,
        IsGradient = Common.IsGradient,
        // LuminanceThreshold = ,
        White = "",
        Black = "",
        //BodyOptions = new() { },
        //BreakpointOptions = new() { },
        //ContainerMaxWidthOptions = new() { },
        //ColorOptions = new() { },
        //BackgroundOptions = new() { },
        //TextColorOptions = new() { },
        //ThemeColorInterval = ,
        //ButtonOptions = new() { },
        //DropdownOptions = new() { },
        //InputOptions = new() { },
        //CardOptions = new() { },
        //ModalOptions = new() { },
        //TabsOptions = new() { },
        //ProgressOptions = new() { },
        //RatingOptions = new() { },
        //AlertOptions = new() { },
        //TableOptions = new() { },
        //BreadcrumbOptions = new() { },
        //BadgeOptions = new() { },
        //SwitchOptions = new() { },
        //PaginationOptions = new() { },
        //SidebarOptions = new() { },
        //SnackbarOptions = new() { },
        //StepsOptions = new() { },
        //BarOptions = new() { },
        //DividerOptions = new() { },
        //TooltipOptions = new() { },
        //SpinKitOptions = new() { },
        //ListGroupItemOptions = new() { },
        //SpacingOptions = new() { }
    };

    public static Theme Dark => new()
    {
        IsRounded = Common.IsRounded,
        IsGradient = Common.IsGradient,
        // LuminanceThreshold = ,
        //White = "#111",
        //Black = "#fff",
        //BodyOptions = new() { },
        //BreakpointOptions = new() { },
        //ContainerMaxWidthOptions = new() { },

        ColorOptions = new()
        {
            Primary = "#3b82f6",
            Secondary = "#6B7280",
            Success = "#0E9F6E",
            Danger = "#F05252",
            Warning = "#C27803",
            Info = "#03A9F4",
            Light = "#F3F4F6",
            Dark = "#1F2937",
        },
        BackgroundOptions = new()
        {
            Primary = "#3b82f6",
            Secondary = "#6B7280",
            Success = "#0E9F6E",
            Danger = "#F05252",
            Warning = "#C27803",
            Info = "#03A9F4",
            Light = "#F3F4F6",
            Dark = "#1F2937",
        },
        TextColorOptions = new()
        {
            Primary = "#3b82f6",
            Secondary = "#6B7280",
            Success = "#0E9F6E",
            Danger = "#F05252",
            Warning = "#C27803",
            Info = "#03A9F4",
            Light = "#F3F4F6",
            Dark = "#1F2937",
        },
        //ThemeColorInterval = ,
        //ButtonOptions = new() { },
        //DropdownOptions = new() { },
        //InputOptions = new() { },
        //CardOptions = new() { },
        //ModalOptions = new() { },
        //TabsOptions = new() { },
        //ProgressOptions = new() { },
        //RatingOptions = new() { },
        //AlertOptions = new() { },
        //TableOptions = new() { },
        //BreadcrumbOptions = new() { },
        //BadgeOptions = new() { },
        //SwitchOptions = new() { },
        //PaginationOptions = new() { },
        //SidebarOptions = new() { },
        //SnackbarOptions = new() { },
        //StepsOptions = new() { },
        //BarOptions = new() { },
        //DividerOptions = new() { },
        //TooltipOptions = new() { },
        //SpinKitOptions = new() { },
        //ListGroupItemOptions = new() { },
        //SpacingOptions = new() { }
    };
}
