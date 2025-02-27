using System.Reflection;
using System.Windows.Media.Imaging;
using Nice3point.Revit.Toolkit.External;
using arca.Commands;
using arca.Interfaces;
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
        Application.CreateRibbonTab("Arca");
        InitModules();
    }

    private void InitModules()
    {
        IModule alignModul = new AlignModule();
        alignModul.Initialize();
    }
    
}