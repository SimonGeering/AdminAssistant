using Blazorise;

namespace AdminAssistant.Blazor.Shared;

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
        //White = "",
        //Black = "",
        BodyOptions = new ThemeBodyOptions
        {
            BackgroundColor = "#FFFFFF", // White
         },
        //BreakpointOptions = new() { },
        //ContainerMaxWidthOptions = new() { },
        //ColorOptions = new() { },
        ColorOptions = new ThemeColorOptions
        {

            Primary = ThemeColors.Cyan._500.Value,
            //Secondary = ThemeColors.Blue.Shades["400"].Value
        },
        //BackgroundOptions = new() { },
        //TextColorOptions = new() { },
        // TextColorOptions = new ThemeTextColorOptions
        // {
        //     Primary = ThemeColors.Orange.Shades["400"].Value,
        //     Secondary = ThemeColors.Yellow.Shades["400"].Value
        // }
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
        //White = "#242424",
        //Black = "#fff",
        //BodyOptions = new() { },
        BodyOptions = new ThemeBodyOptions
        {
            BackgroundColor = ThemeColors.Gray.Shades["900"].Value,
        },
        //BreakpointOptions = new() { },
        //ContainerMaxWidthOptions = new() { },

        ColorOptions = new()
        {
            Primary = ThemeColors.DeepPurple._500.Value,
            // Primary = "#3b82f6",
            // Secondary = "#6B7280",
            // Success = "#0E9F6E",
            // Danger = "#F05252",
            // Warning = "#C27803",
            // Info = "#03A9F4",
            // Light = "#F3F4F6",
            // Dark = "#1F2937",
        },
        BackgroundOptions = new()
        {
            Body = ThemeColors.Gray.Shades["400"].Value,
        //     Primary = "#242424",
        //     Secondary = "#6B7280",
        //     Success = "#0E9F6E",
        //     Danger = "#F05252",
        //     Warning = "#C27803",
        //     Info = "#03A9F4",
        //     Light = "#F3F4F6",
        //     Dark = "#1F2937",
        },
        // TextColorOptions = new()
        // {
        //     Primary = "#3b82f6",
        //     Secondary = "#6B7280",
        //     Success = "#0E9F6E",
        //     Danger = "#F05252",
        //     Warning = "#C27803",
        //     Info = "#03A9F4",
        //     Light = "#F3F4F6",
        //     Dark = "#1F2937",
        // },
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
