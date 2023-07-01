using NUnit.Framework;
using Autodesk.Revit.DB;
using Revit.Elements;
using Revit.GeometryConversion;
using RevitServices.Persistence;
using RevitServices.Transactions;
using RTF.Framework;
using Curve = Autodesk.DesignScript.Geometry.Curve;
using ElementType = Revit.Elements.Element;
using Level = Revit.Elements.Level;
using Point = Autodesk.DesignScript.Geometry.Point;
using Wall = Autodesk.Revit.DB.Wall;

namespace CreateElement.Tests
{
    [TestFixture]
    public class CreateModelTests
    {
        [SetUp]
        public void SetUp()
        {
            DocumentManager.Instance.CurrentUIApplication =
                RTF.Applications.RevitTestExecutive.CommandData.Application;
            DocumentManager.Instance.CurrentUIDocument =
                RTF.Applications.RevitTestExecutive.CommandData.Application.ActiveUIDocument;
        }
        [Test]
        [TestModel("Resources/Rebar.rvt")]
        public void TestCreateNewWall()
        {
            Document doc = DocumentManager.Instance.CurrentDBDocument;
            // Arrange
            Point p1 = Point.ByCoordinates(0,0,0);
            Point p2 = Point.ByCoordinates(10,0,0);
            Curve locationCurve = Autodesk.DesignScript.Geometry.Line.ByStartPointEndPoint(p1, p2);
            //Curve locationCurve = Line.ByStartPointEndPoint(Point.ByCoordinates(0, 0, 0), Point.ByCoordinates(10, 0, 0));
            //Curve locationCurve = ...; // Tạo giá trị Curve
            ElementId wallTypeId = new ElementId(600634);
            ElementId LevelId = new ElementId(311);
            ElementType wallType = doc.GetElement(wallTypeId).ToDSType(true); // Tạo giá trị ElementType
            Level level = doc.GetElement(LevelId).ToDSType(true) as Level; // Tạo giá trị Level
            double wallHeight = 3000; // Tạo giá trị chiều cao tường
            double offsetFromLocation = 0; // Tạo giá trị khoảng cách tới vị trí
            bool flip = false; // Tạo giá trị flip
            bool structural = false; // Tạo giá trị structural

            // Act
            Revit.Elements.Element createdWall = CreateModel.CreateNewWall(locationCurve, wallType, level, wallHeight, offsetFromLocation, flip, structural);

            // Assert
            Assert.IsNotNull(createdWall);
            Assert.AreEqual(typeof(Revit.Elements.Element), createdWall.GetType());
        }
    }
}