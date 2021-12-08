using SmartPlugins.Common.Abstractions.TeklaStructures;
using SmartPlugins.Common.TeklaLibrary.Extensions;
using Tekla.Structures.Model;

namespace SmartPlugins.Common.TeklaLibrary.Welds
{
    public class ContourWeldByProfileBeam
    {
        private readonly ISmartModel _smartModel;

        public ContourWeldByProfileBeam(ISmartModel smartModel)
        {
            _smartModel = smartModel;
        }

        public void Get(Part mainPart, Beam secondaryPart)
        {
            var model = _smartModel.GetParentObject<Model>();

            var contourPoints = secondaryPart.GetAllPointsProfile(model);



            ////var weld = new PolygonWeld()
            ////{
            ////    Polygon = contourPoints,
            ////    MainObject = mainPart,
            ////    SecondaryObject = secondaryPart,
            ////    SizeAbove = 10
            ////};

            ////weld.Insert();
        }

    }
}
