using SmartPlugins.Common.Abstractions.ModelObjects;
using System;
using Tekla.Structures.Model;

namespace SmartPlugins.Common.TeklaLibrary.Entities
{
    public sealed class SmartBeam : SmartPart, IBeam
    {
        private readonly Beam _beam;

        private IPoint _startPoint;
        private IPoint _endPoint;

        public SmartBeam(Beam beam) : base(beam)
        {
            ModelObject = _beam = beam;
            _startPoint = new SmartPoint(beam.StartPoint);
            _startPoint = new SmartPoint(beam.EndPoint);
        }

        public IPoint StartPoint { get => _startPoint; }
        public IPoint EndPoint { get => _endPoint; }
    }
}
