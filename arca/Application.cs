using System.Reflection;
using System.Windows.Media.Imaging;
using Nice3point.Revit.Toolkit.External;
using arca.Commands;
using Autodesk.Revit.UI;

namespace arca;

/// <summary>
///     Application entry point
/// </summary>
[UsedImplicitly]
public class Application : ExternalApplication
{
    public override void OnStartup()
    {
        CreateRibbon();
    }

    private void CreateRibbon()
    {
        var panel = Application.CreatePanel("Alignment", "Arca");

        panel.AddPushButton<StartupCommand>("Debug")
            .SetImage("/arca;component/Resources/Icons/AlignLeft16.png")
            .SetLargeImage("/arca;component/Resources/Icons/AlignLeft32.png")
            .Enabled = true;

        string assemblyPath = Assembly.GetExecutingAssembly().Location;

        // Define stacked buttons
        var alignLeftButton =
            new PushButtonData("Align left", "Align left", assemblyPath, typeof(AlignLeftCommand).ToString())
            {
                Image = new BitmapImage(new Uri("/arca;component/Resources/Icons/AlignLeft16.png", UriKind.Relative)),
                LargeImage = new BitmapImage(new Uri("/arca;component/Resources/Icons/AlignLeft32.png", UriKind.Relative)),
                ToolTip = "Align Left"
            };
        var alignRightButton = new PushButtonData("Align Right", "Align Right", assemblyPath, typeof(AlignRightCommand).ToString());
        var alignBottomButton = new PushButtonData("Align Bottom", "Align Bottom", assemblyPath, typeof(AlignBottomCommand).ToString());
        
        var alignTopButton = new PushButtonData("Align Top", "Align Top", assemblyPath, typeof(AlignTopCommand).ToString());
        var spaceHorizontallyButton = new PushButtonData("Space Horizontally", "Space Horizontally", assemblyPath, typeof(AlignTopCommand).ToString()); //TODO: cahnge
        var spaceVerticallyButton = new PushButtonData("Space Vertically", "Space Vertically", assemblyPath, typeof(AlignTopCommand).ToString()); //TODO: change

        // Add the stacked buttons to the panel
        panel.AddStackedItems(alignLeftButton, alignRightButton, alignBottomButton);
        
        panel.AddStackedItems(alignTopButton, spaceHorizontallyButton, spaceVerticallyButton);
        
        
    }
}