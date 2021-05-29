using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using System.Windows.Forms;

namespace PipeRack
{
    abstract class CreateBeam 
    {
        public Attributes _AttBeam { get; set; }
        public Point _StartPoint { get; set; }
        public Point _EndtPoint { get; set; }

        //protected CreateBeam() { }

        public CreateBeam() //Attributes att, Point startPoint, Point endPoint
        {
            _AttBeam = new Attributes();
           // _AttBeam = att;
           // _StartPoint = startPoint;
           // _EndtPoint = endPoint;

        }
        public Beam Insert(Attributes att, Point startPoint, Point endPoint)
        {
            _AttBeam = att;
            _StartPoint = startPoint;
            _EndtPoint = endPoint;

            Beam beam = new Beam(_StartPoint, _EndtPoint);
            beam.Profile.ProfileString = "I30K1_20_93";
            beam.Insert();
            SetAtt(beam, _AttBeam);
            beam.Modify();
            return beam;
        }
        private void SetAtt(Beam beam, Attributes _attributes)
        {
            if (_attributes != null)
            {
                beam.Name = _attributes.Name;
                beam.Profile.ProfileString = _attributes.Profile;
                beam.Material.MaterialString = _attributes.Material;
                beam.Class = _attributes.Class;
                beam.PartNumber.Prefix = _attributes.PrefixSborki;

                int nomerSborki;
                var H = Int32.TryParse(_attributes.NomerSborki.ToString(), out nomerSborki);
                if (!H)
                {
                    MessageBox.Show("Введено не целое число сборки");
                    return;
                }
                beam.PartNumber.StartNumber = nomerSborki;

                //LEFT MIDDLE RIGHT
                if (_attributes.PolojenieGorizontalno == 1) beam.Position.Plane = Position.PlaneEnum.LEFT;
                if (_attributes.PolojenieGorizontalno == 0) beam.Position.Plane = Position.PlaneEnum.MIDDLE;
                if (_attributes.PolojenieGorizontalno == 2) beam.Position.Plane = Position.PlaneEnum.RIGHT;

                // BACK BELOW FRONT TOP
                if (_attributes.PolojeniePovorot == 1) beam.Position.Rotation = Position.RotationEnum.FRONT;
                if (_attributes.PolojeniePovorot == 0) beam.Position.Rotation = Position.RotationEnum.TOP;

                // BEHIND  FRONT MIDLE
                if (_attributes.PolojenieVertikalno == 1) beam.Position.Depth = Position.DepthEnum.MIDDLE;
                if (_attributes.PolojenieVertikalno == 0) beam.Position.Depth = Position.DepthEnum.BEHIND;
                if (_attributes.PolojenieVertikalno == 2) beam.Position.Depth = Position.DepthEnum.FRONT;

                beam.SetUserProperty("RNumberOfYarus", _attributes.RNumberOfYarus);
                beam.SetUserProperty("DirectionOfYarus", _attributes.DirectionOfYarus);
                beam.SetUserProperty("RNazvanie", _attributes.RNazvanie);
            }
            beam.Modify();
        }
    }
}
