using SmartPlugins.Common.TeklaLibrary.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;

namespace SmartPlugins.Applications.SmartCheckAssembliesML
{
    public class DetailsInfo
    {
        private Assembly _assembly;

        private List<CheckData> _checkDatas { get; set;}

        public DetailsInfo(Assembly assembly)
        {
            _assembly = assembly;
            GetDetails();
        }

        public void GetDetails()
        {
            var mainPart = _assembly.GetMainPart() as Part;

            if (mainPart == null)
                return;

            _checkDatas = new List<CheckData>();

            var solidMainPart = mainPart.GetSolid();

            var maxPoint = solidMainPart.MaximumPoint;
            var minPoint = solidMainPart.MinimumPoint;

            var secondariesParts = _assembly.GetSecondaries().OfType<Part>().ToList();

            foreach (var part in secondariesParts)
            {
                var bolts = part.GetBolts().ToIEnumerable<BoltGroup>().ToList();

                var welds = part.GetWelds().ToIEnumerable<BaseWeld>().ToList();

                var allConnectDetails = GetConnectDetails(bolts, welds).ToList();

                var solidPart = part.GetSolid();

                var maxPointPart = solidPart.MaximumPoint;
                var minPointPart = solidPart.MinimumPoint;

                var distances1 = GetMinDist(maxPointPart, maxPoint, minPoint);
                var distances2 = GetMinDist(minPointPart, maxPoint, minPoint);

                var errorStatus = false;

                if(part.Class == "1234")
                    errorStatus = true;

                var checkData = GetCheckDatas(bolts.Count, welds.Count, errorStatus, distances1, distances2, allConnectDetails);

                _checkDatas.Add(checkData);
            }
        }

        private IEnumerable<Part> GetConnectDetails(IEnumerable<BoltGroup> boltGroups, IEnumerable<BaseWeld> baseWelds)
        {
            var result = new List<Part>();

            var detailsBolts = boltGroups.SelectMany(bg => bg.GetAllParts());
            var detailsWelds = baseWelds.SelectMany(bw => bw.GetAllParts());

            result.AddRange(detailsBolts);
            result.AddRange(detailsWelds);

            return result;
        }

        private CheckData GetCheckDatas(int bolts,
            int welds,
            bool errorStatus,
            Tuple<float, float, float> dist1,
            Tuple<float, float, float> dist2,
            List<Part> allConnectDetails)
        {
            return new CheckData
            {
                Label = errorStatus,
                Features = new float[] { 
                    bolts, 
                    welds, 
                    allConnectDetails.Count(), 
                    dist1.Item1, 
                    dist1.Item2, 
                    dist1.Item3, 
                    dist2.Item1, 
                    dist2.Item2, 
                    dist2.Item3 }

                /*
            Bolts = bolts,
                Welds = welds,
                ConnectDetails = allConnectDetails.Count(),
                DistancesX1 = dist1.Item1,
                DistancesY1 = dist1.Item2,
                DistancesZ1 = dist1.Item3,
                DistancesX2 = dist2.Item1,
                DistancesY2 = dist2.Item2,
                DistancesZ2 = dist2.Item3
                */
            };
        }

        private Tuple<float,float, float> GetMinDist(Point pointDetail, Point maxPointAssembly, Point minPointAssembly)
        {
            var distX = new List<double>
            {
                Distance.PointToPoint(new Point(maxPointAssembly.X, 0, 0), new Point(pointDetail.X, 0, 0)),
                Distance.PointToPoint(new Point(maxPointAssembly.X, 0, 0), new Point(pointDetail.X, 0, 0)),
                Distance.PointToPoint(new Point(minPointAssembly.X, 0, 0), new Point(pointDetail.X, 0, 0)),
                Distance.PointToPoint(new Point(minPointAssembly.X, 0, 0), new Point(pointDetail.X, 0, 0))
            }
            .OrderBy(d => d)
            .ToList();

            var distY = new List<double>
            {
                Distance.PointToPoint(new Point(0, maxPointAssembly.Y, 0), new Point(0, pointDetail.Y, 0)),
                Distance.PointToPoint(new Point(0, maxPointAssembly.Y, 0), new Point(0, pointDetail.Y, 0)),
                Distance.PointToPoint(new Point(0, minPointAssembly.Y, 0), new Point(0, pointDetail.Y, 0)),
                Distance.PointToPoint(new Point(0, minPointAssembly.Y, 0), new Point(0, pointDetail.Y, 0))
            }
            .OrderBy(d => d)
            .ToList();

            var distZ = new List<double>
            {
                Distance.PointToPoint(new Point(0, 0, maxPointAssembly.Z), new Point(0, 0, pointDetail.Z)),
                Distance.PointToPoint(new Point(0, 0, maxPointAssembly.Z), new Point(0, 0, pointDetail.Z)),
                Distance.PointToPoint(new Point(0, 0, minPointAssembly.Z), new Point(0, 0, pointDetail.Z)),
                Distance.PointToPoint(new Point(0, 0, minPointAssembly.Z), new Point(0, 0, pointDetail.Z))
            }
            .OrderBy(d => d)
            .ToList();

            var result = new List<float>
            {
                (float)distX.First(),
                (float)distY.First(),
                (float)distZ.First(),
            }
            .OrderBy(d => d);

            return new Tuple<float, float, float>(result.ElementAt(0), result.ElementAt(1), result.ElementAt(2));
        }

        public List<CheckData> GetChangeDatas()
        {
            return _checkDatas;
        }
    }
}
