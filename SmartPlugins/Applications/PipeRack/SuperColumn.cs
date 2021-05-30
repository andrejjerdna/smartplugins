using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;

namespace PipeRack
{
    public abstract class SuperBase
    {
        public Attributes Attributes { get => _attributes; }
        private protected Attributes _attributes { get; set; }
        private protected Point _p1 { get; set; }
        private protected Point _p2 { get; set; }
    }

    public class SuperColumn : SuperBase
    {
        private Beam _beam;

        public SuperColumn(Attributes attributes, Point p1, Point p2)
        {
            _attributes = attributes;
            _p1 = p1;
            _p2 = p2;
        }

        public void Insert()
        {
            _beam = Beam_main(_attributes, _p1, _p2, "", "");
        }

        public Beam GetBeam()
        {
            return _beam;
        }

        private Beam Beam_main(Attributes attributes, Point startPoint, Point endPoint, string RNumberOfYarus, string DirectionOfYarus)
        {
            Beam newBeam = new Beam(startPoint, endPoint);
            newBeam.Profile.ProfileString = "I30K1_20_93";
            newBeam.Insert();
            SetAtt(newBeam, attributes);

            newBeam.SetUserProperty("RNumberOfYarus", RNumberOfYarus);
            newBeam.SetUserProperty("DirectionOfYarus", DirectionOfYarus);
            newBeam.Modify();
            return newBeam;
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

                if (_attributes.NomerSborki != null)
                {
                    int nomerSborki;
                    var H = Int32.TryParse(_attributes.NomerSborki.ToString(), out nomerSborki);
                    if (!H)
                    {
                        MessageBox.Show("Введено не целое число сборки");
                        return;
                    }

                    beam.PartNumber.StartNumber = nomerSborki;
                }


                //LEFT MIDDLE RIGHT
                if (_attributes.PolojenieGorizontalno == 1) beam.Position.Plane = Position.PlaneEnum.LEFT;
                if (_attributes.PolojenieGorizontalno == 0) beam.Position.Plane = Position.PlaneEnum.MIDDLE;
                if (_attributes.PolojenieGorizontalno == 2) beam.Position.Plane = Position.PlaneEnum.RIGHT;

                // BACK BELOW FRONT TOP
                if (_attributes.PolojeniePovorot == 1) beam.Position.Rotation = Position.RotationEnum.FRONT;
                if (_attributes.PolojeniePovorot == 0) beam.Position.Rotation = Position.RotationEnum.TOP;
                // if (_attributes.PolojeniePovorot == 0) beam.Position.Rotation = Position.RotationEnum.BACK;
                // if (_attributes.PolojeniePovorot == 1) beam.Position.Rotation = Position.RotationEnum.BELOW;

                // BEHIND  FRONT MIDLE
                if (_attributes.PolojenieVertikalno == 1) beam.Position.Depth = Position.DepthEnum.MIDDLE;
                if (_attributes.PolojenieVertikalno == 0) beam.Position.Depth = Position.DepthEnum.BEHIND;
                if (_attributes.PolojenieVertikalno == 2) beam.Position.Depth = Position.DepthEnum.FRONT;

            }

            beam.Modify();
        }
    }

    public class SuperBeam : SuperBase
    {

    }
}
