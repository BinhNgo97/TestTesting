using Autodesk.DesignScript.Runtime;
using Autodesk.Revit.DB;
using Revit.Elements;
using Revit.GeometryConversion;
using RevitServices.Persistence;
using RevitServices.Transactions;
using Curve = Autodesk.DesignScript.Geometry.Curve;
using ElementType = Revit.Elements.Element;
using Level = Revit.Elements.Level;
using Wall = Autodesk.Revit.DB.Wall;

namespace CreateElement
{
    public class CreateModel
    {
        [IsVisibleInDynamoLibrary(false)]
        private CreateModel()
        {
        }

        //public static Wall Create(
        //          Document    document,
        //          Curve       curve,
        //          ElementId   wallTypeId,
        //          ElementId   levelId,
        //          double      height,
        //          double      offset,
        //          bool        flip,
        //          bool        structural
        //      )
        [IsVisibleInDynamoLibrary(true)]
        public static Revit.Elements.Element CreateNewWall(Curve locationCurve, ElementType WallType,
            Revit.Elements.Level Level, double WallHeight,
            double OffsetFromLocation, bool flip, bool structural)
        {
            Document doc = DocumentManager.Instance.CurrentDBDocument;

            //Convert data from dynamo to revit
            int levelId = Level.Id;

            //ElementType elementType =
            ElementId levelElementId = new ElementId(levelId);

            ElementId wallTypeid = WallType.InternalElement.Id;

            Autodesk.Revit.DB.Curve rvlocation = locationCurve.ToRevitType(true);

            TransactionManager.Instance.EnsureInTransaction(doc);

            Wall newWall = Wall.Create(doc, rvlocation, wallTypeid, levelElementId, WallHeight, OffsetFromLocation,
                flip, structural);

            TransactionManager.Instance.TransactionTaskDone();

            Revit.Elements.Element dyWall = ElementWrapper.ToDSType(newWall, true);

            return dyWall;
        }
    }
}