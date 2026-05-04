using MudBlazor;
using MudBlazor.Utilities;

namespace Agri.ReadModel.Blazor.Theme
{
    public class AgriTheme : MudTheme
    {
        public AgriTheme()
        {
            PaletteLight = new PaletteLight()
            {
                Primary = new MudColor("#2E7D32"), // A rich, dark green
                Secondary = new MudColor("#FFC107"), // A vibrant amber/yellow
                AppbarBackground = new MudColor("#2E7D32"),
                AppbarText = Colors.Shades.White,
                DrawerBackground = Colors.Shades.White,
                DrawerText = Colors.Shades.Black,
                Surface = Colors.Shades.White,
                ActionDefault = new MudColor("#2E7D32"),
            };

            PaletteDark = new PaletteDark()
            {
                Primary = new MudColor("#4CAF50"), // A slightly lighter green for dark mode
                Secondary = new MudColor("#FFB300"), // A slightly darker amber for dark mode
                AppbarBackground = new MudColor("#2E7D32"),
                AppbarText = Colors.Shades.White,
                DrawerBackground = new MudColor("#272727"),
                DrawerText = Colors.Shades.White,
                Surface = new MudColor("#373737"),
                ActionDefault = new MudColor("#4CAF50"),
            };

            LayoutProperties = new LayoutProperties()
            {
                DrawerWidthLeft = "260px",
                DrawerWidthRight = "300px"
            };
        }
    }
}
