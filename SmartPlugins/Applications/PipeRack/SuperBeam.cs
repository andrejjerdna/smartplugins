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
    public abstract class SuperBeam 
    {
        public Attributes AttBeam { get; set; }
        public Point StartPoint { get; set; }
        public Point EndPoint { get; set; }
        public Beam _beam { get; set; }

        protected SuperBeam() { }

        public SuperBeam(Attributes attBeam, Point startPoint, Point endPoint) 
        {
            AttBeam = attBeam;
            StartPoint = startPoint;
            EndPoint = endPoint;
        }

        public Beam GetBeam()
        {
            return _beam;
        }

        public bool Insert()
        {
            Beam beam = new Beam(StartPoint, EndPoint);

           beam.Profile.ProfileString = "I30K1_20_93";

            if (beam.Insert())
            {
                SetAtt(beam, AttBeam);
                beam.Modify();
                _beam = beam;
                return true;
            }
            else
            {
                return false;
            }

        }
        public bool Modify()
        {
            _beam.StartPoint = StartPoint;
            _beam.EndPoint = EndPoint;
            _beam.Modify();
            return true;
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
                beam.SetUserProperty("RType", _attributes.RType);
            }
            beam.Modify();
        }
    }
}



