using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using Autodesk.Revit.Attributes;
using Nice3point.Revit.Toolkit.External;
using UIFramework;
using Xceed.Wpf.AvalonDock.Controls;
using Xceed.Wpf.AvalonDock.Layout;
using Control = System.Windows.Controls.Control;
using TaskDialog = Autodesk.Revit.UI.TaskDialog;

namespace arca.Commands;

/// <summary>
///     External command entry point invoked from the Revit interface
/// </summary>
[UsedImplicitly]
[Transaction(TransactionMode.Manual)]
public class StartupCommand : ExternalCommand
{
    public override void Execute()
    {
        /*var viewModel = new arcaViewModel();
        var view = new arcaView(viewModel);
        view.ShowDialog();

        var ribbon = ComponentManager.Ribbon;*/

        try
        {
            var windowHandle = UiApplication.MainWindowHandle;

            if (windowHandle == IntPtr.Zero)
            {
                //show revit error modal and return
                TaskDialog.Show("Window Handle", "Window handle is not zero.");
                return;
            }

            var windowSource = HwndSource.FromHwnd(windowHandle);
            if (windowSource == null)
            {
                TaskDialog.Show("Window Source", "Window source is null.");
                return;
            }

            var windowRoot = (MainWindow)windowSource.RootVisual;
            if (windowRoot == null)
            {
                TaskDialog.Show("Window Root", "Window root is null.");
                return;
            }

            var documentPaneGroup = MainWindow.FindFirstChild<LayoutDocumentPaneGroupControl>(windowRoot);

            if (documentPaneGroup == null)
            {
                TaskDialog.Show("Document Tab Group", "Document tab group is null.");
                return;
            }

            var documentPaneTabPanel = documentPaneGroup.FindVisualChildren<DocumentPaneTabPanel>()?.FirstOrDefault();

            var tabs = documentPaneGroup.FindVisualChildren<TabItem>();

            Style tabStyle = new Style(typeof(TabItem), tabs.First().Style);


            tabStyle.Setters.Add(
                new Setter(Control.BackgroundProperty, new SolidColorBrush(Colors.Teal)));

            foreach (var tab in tabs)
            {
                string title = ((LayoutDocument)tab.Header).Title;
                Console.WriteLine(title);
                tab.Style = tabStyle;
            }
        }
        catch (Exception e)
        {
            TaskDialog.Show("Error", e.Message);
            //print stacktrace to console
            Console.WriteLine(e.StackTrace);
            Console.WriteLine(e.Source);
            Console.WriteLine(e.TargetSite);
        }
    }
}