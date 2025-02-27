using System.Reflection;
using System.Windows.Media.Imaging;
using Autodesk.Revit.UI;
using Autodesk.Windows;
using AW = Autodesk.Windows;
using RibbonItem = Autodesk.Revit.UI.RibbonItem;

namespace arca.Helpers;

public class ButtonHelper
{
    public static AW.RibbonItem GetButton(string tabName, string panelName, string buttonName)
    {
        var ribbonControl = ComponentManager.Ribbon;
        var tab = ribbonControl.Tabs.FirstOrDefault(t => t.Name == tabName);
        if (tab == null)
        {
            Console.WriteLine($"Tab {tabName} not found.");
            return null;
        }

        var panel = tab.Panels.FirstOrDefault(p => p.Source.Title == panelName);
        if (panel == null)
        {
            Console.WriteLine($"Panel {panelName} not found.");
            return null;
        }

        var button = panel.FindItem("CustomCtrl_%CustomCtrl_%"
                                    + tabName + "%" + panelName + "%" + buttonName,
            true);

        if (button == null)
        {
            Console.WriteLine($"Button {buttonName} not found.");
            return null;
        }

        return button;
    }

    public static void RemoveTextFromButton(IList<RibbonItem> ribbonItems, string tabName, string panelName)
    {
        var buttonCache = new Dictionary<string, AW.RibbonItem>();

        foreach (var ribbonItem in ribbonItems)
        {
            if (!buttonCache.TryGetValue(ribbonItem.Name, out var button))
            {
                button = GetButton(tabName, panelName, ribbonItem.Name);
                buttonCache.Add(ribbonItem.Name, button);
            }

            if (button != null) button.ShowText = false;
        }
    }

    public static PushButtonData CreateIconButton<T>(string imagePath, string largeImagePath, string name)
        where T : IExternalCommand
    {
        string className = typeof(T).ToString();
        var pushButtonData =
            new PushButtonData(className, name, Assembly.GetExecutingAssembly().Location, className);
        pushButtonData.LargeImage = new BitmapImage(new Uri(largeImagePath, UriKind.Relative));
        pushButtonData.Image = new BitmapImage(new Uri(imagePath, UriKind.Relative));
        return pushButtonData;
    }
}