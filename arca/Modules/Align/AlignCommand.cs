using Autodesk.Revit.Attributes;
using Nice3point.Revit.Toolkit.External;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using System.Collections.Generic;
using System.Linq;

namespace arca.Commands;

/// <summary>
///     External command entry point for aligning elements
/// </summary>
[Transaction(TransactionMode.Manual)]
public class AlignCommand : AlignBaseCommand
{
    public override void Execute() => AlignElements(Alignment.Left);
}

[Transaction(TransactionMode.Manual)]
public class AlignLeftCommand : AlignBaseCommand
{
    public override void Execute() => AlignElements(Alignment.Left);
}

[Transaction(TransactionMode.Manual)]
public class AlignRightCommand : AlignBaseCommand
{
    public override void Execute() => AlignElements(Alignment.Right);
}

[Transaction(TransactionMode.Manual)]
public class AlignTopCommand : AlignBaseCommand
{
    public override void Execute() => AlignElements(Alignment.Top);
}

[Transaction(TransactionMode.Manual)]
public class AlignBottomCommand : AlignBaseCommand
{
    public override void Execute() => AlignElements(Alignment.Bottom);
}

[Transaction(TransactionMode.Manual)]
public class AlignCenterCommand : AlignBaseCommand
{
    public override void Execute() => AlignElements(Alignment.Center);
}

[Transaction(TransactionMode.Manual)]
public class AlignHorizontalCommand : AlignBaseCommand
{
    public override void Execute() => AlignElements(Alignment.Horizontal);
}

public abstract class AlignBaseCommand : ExternalCommand
{
    protected enum Alignment { Left, Right, Top, Bottom, Center, Horizontal }
    
    protected void AlignElements(Alignment alignment)
    {
        var selectedElements = UiDocument.Selection.GetElementIds();
        if (!selectedElements.Any())
        {
            TaskDialog.Show("Selection", "No elements selected.");
            return;
        }
        
        var coordinateDictionary = GetCoordinatesDictionary(selectedElements);
        if (!coordinateDictionary.Any()) return;
        
        double targetCoordinate = alignment switch
        {
            Alignment.Left => coordinateDictionary.Min(x => x.Value.X),
            Alignment.Right => coordinateDictionary.Max(x => x.Value.X),
            Alignment.Top => coordinateDictionary.Max(x => x.Value.Y),
            Alignment.Bottom => coordinateDictionary.Min(x => x.Value.Y),
            Alignment.Center => coordinateDictionary.Average(x => x.Value.X),
            Alignment.Horizontal => coordinateDictionary.Average(x => x.Value.Y),
            _ => 0
        };
        
        using (var transaction = new Transaction(Document, $"Align {alignment}"))
        {
            transaction.Start();
            foreach (var elementId in selectedElements)
            {
                var element = Document.GetElement(elementId);
                if (element.Location is LocationPoint locationPoint)
                {
                    var point = locationPoint.Point;
                    point = alignment switch
                    {
                        Alignment.Left or Alignment.Right or Alignment.Center => new XYZ(targetCoordinate, point.Y, point.Z),
                        Alignment.Top or Alignment.Bottom or Alignment.Horizontal => new XYZ(point.X, targetCoordinate, point.Z),
                        _ => point
                    };
                    locationPoint.Point = point;
                }
            }
            transaction.Commit();
        }
    }
    
    private Dictionary<ElementId, XYZ> GetCoordinatesDictionary(ICollection<ElementId> elements)
    {
        var coordinateDictionary = new Dictionary<ElementId, XYZ>();
        foreach (var elementId in elements)
        {
            var element = Document.GetElement(elementId);
            //log type of Location to console
            Console.WriteLine(element.Location.GetType());
            if (element.Location is LocationPoint locationPoint)
            {
                coordinateDictionary[elementId] = locationPoint.Point;
            }
            /*else if (element.Location is LocationCurve locationCurve && locationCurve.Curve is Line line)
            {
                coordinateDictionary[elementId] = line.GetEndPoint(0);
            }*/
        }
        return coordinateDictionary;
    }
}
