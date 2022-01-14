using SmartPlugins.Common.Abstractions.ModelObjects;
using Tekla.Structures.Model;

namespace SmartPlugins.Common.TeklaLibrary.Entities
{
    public class SmartPart : SmartBaseObject, IPart
    {
        private readonly Part _part;

        public SmartPart(Part part)
        {
            ModelObject = _part = part;
        }

        public string Class 
        { 
            get => _part.Class;
            set => _part.Class = value;
        }

        public void Modify() => _part.Modify();
    }
}
