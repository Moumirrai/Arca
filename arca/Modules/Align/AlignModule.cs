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
            "/arca;component/Resources/Icons/Align/AlignLeftIcon16.png",
            "/arca;component/Resources/Icons/Align/Align/AlignLeftIcon32.png",
            "Align Left");

        //var alignLeftButton = new PushButtonData("Align Left", "Align Left", assemblyPath, typeof(AlignLeftCommand).ToString());    
        var alignRightButton = ButtonHelper.CreateIconButton<AlignRightCommand>(
            "/arca;component/Resources/Icons/Align/AlignRightIcon16.png",
            "/arca;component/Resources/Icons/Align/AlignRightIcon32.png",
            "Align Right");
        var alignBottomButton = ButtonHelper.CreateIconButton<AlignBottomCommand>(
            "/arca;component/Resources/Icons/Align/AlignBottomIcon16.png",
            "/arca;component/Resources/Icons/Align/AlignBottomIcon32.png",
            "Align Bottom");

        var alignTopButton = ButtonHelper.CreateIconButton<AlignTopCommand>(
            "/arca;component/Resources/Icons/Align/AlignTopIcon16.png",
            "/arca;component/Resources/Icons/Align/AlignTopIcon32.png",
            "Align Top");
        var alignHorizontallyButton = ButtonHelper.CreateIconButton<AlignHorizontalCommand>(
            "/arca;component/Resources/Icons/Align/AlignHorizontalIcon16.png",
            "/arca;component/Resources/Icons/Align/AlignHorizontalIcon32.png",
            "Align Horizontally");
        var alignCenterButton = ButtonHelper.CreateIconButton<AlignCenterCommand>(
            "/arca;component/Resources/Icons/Align/AlignCenterIcon16.png",
            "/arca;component/Resources/Icons/Align/AlignCenterIcon32.png",
            "Align to Center");

        //panel.AddStackedItems(alignLeftButton, alignRightButton);
        panel.AddStackedItems(alignLeftButton, alignTopButton, alignBottomButton);
        panel.AddStackedItems(alignRightButton, alignHorizontallyButton, alignCenterButton);

        ButtonHelper.RemoveTextFromButton(panel.GetItems(), "Arca", "Alignment");
    }
}