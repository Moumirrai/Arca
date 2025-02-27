using System.Reflection;
using arca.Helpers;
using arca.Interfaces;
using Autodesk.Revit.UI;
using Nice3point.Revit.Toolkit.External;

namespace arca.Commands;

public class AlignModule : IModule
{
    public void Initialize()
    {
        string assemblyPath = Assembly.GetExecutingAssembly().Location;

        var panel = Context.UiApplication.CreateRibbonPanel("Arca", "Alignment");


        var alignLeftButton = ButtonHelper.CreateIconButton<AlignLeftCommand>(
            "/arca;component/Resources/Icons/AlignLeft16.png", "/arca;component/Resources/Icons/AlignLeft16.png",
            "Align Left");

        //var alignLeftButton = new PushButtonData("Align Left", "Align Left", assemblyPath, typeof(AlignLeftCommand).ToString());    
        var alignRightButton = ButtonHelper.CreateIconButton<AlignRightCommand>(
            "/arca;component/Resources/Icons/AlignLeft16.png", "/arca;component/Resources/Icons/AlignLeft32.png",
            "Align Right");
        var alignBottomButton = ButtonHelper.CreateIconButton<AlignBottomCommand>(
            "/arca;component/Resources/Icons/AlignLeft16.png", "/arca;component/Resources/Icons/AlignLeft32.png",
            "Align Bottom");

        var alignTopButton = ButtonHelper.CreateIconButton<AlignTopCommand>(
            "/arca;component/Resources/Icons/AlignLeft16.png", "/arca;component/Resources/Icons/AlignLeft32.png",
            "Align Top");
        var alignHorizontallyButton = ButtonHelper.CreateIconButton<AlignHorizontalCommand>(
            "/arca;component/Resources/Icons/AlignLeft16.png", "/arca;component/Resources/Icons/AlignLeft32.png",
            "Align Horizontally");
        var alignCenterButton = ButtonHelper.CreateIconButton<AlignCenterCommand>(
            "/arca;component/Resources/Icons/AlignLeft16.png", "/arca;component/Resources/Icons/AlignLeft32.png",
            "Align to Center");

        panel.AddStackedItems(alignLeftButton, alignRightButton, alignBottomButton);
        panel.AddStackedItems(alignTopButton, alignHorizontallyButton, alignCenterButton);

        ButtonHelper.RemoveTextFromButton(panel.GetItems(), "Arca", "Alignment");
    }
}