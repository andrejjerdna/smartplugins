using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Model;
using TSM = Tekla.Structures.Model;

namespace SmartPlugins.Common.TeklaLibrary.SmartObjects
{
    public abstract class ConcretePartBase : IConcreteAssembly
    {
        public enum AssemblyTypeEnum
        {
            ADD_TO_CAST_UNIT,
            NOT_ADD_TO_CAST_UNIT
        }
        public enum CastUnitTypeEnum
        {
            PRECAST,
            CAST_IN_PLACE
        }

        private protected TSM.Model _currentModel { get; set; }
        public string PartName { get; set; }
        public string AssemblyName { get; set; }
        public string Profile { get; set; }
        public string Material { get; set; }
        public string Class { get; set; }
        public string AssemblyPrefix { get; set; }
        public string PartPrefix { get; set; }
        public int AssemblyStartNumber { get; set; }
        public int PartStartNumber { get; set; }
        public int PourPhase { get; set; }
        public string Finish { get; set; }
        public AssemblyTypeEnum AddToCastUnit { get; set; }
        private protected Part _mainPart { get; set; }
        private protected List<Part> _secondaryParts { get; set; }
        public Part.CastUnitTypeEnum CastUnitType;
        public virtual Part GetMainPart()
        {
            return _mainPart;
        }
        public virtual List<Part> GetSecondaryParts()
        {
            return _secondaryParts;
        }
        public virtual void DeleteToAssembly(Part assemblyPart)
        {
            var assembly = assemblyPart.GetAssembly();
            assembly.Remove(_mainPart);
            assembly.Modify();
        }
        public virtual void AddToAssembly(Part assemblyPart)
        {
            var assembly = assemblyPart.GetAssembly();
            assembly.Add(_mainPart);
            assembly.Modify();
        }
        private protected virtual void InsertAssembly()
        {
            var assembly = _mainPart.GetAssembly();

            foreach(Part part in _secondaryParts)
            {
                assembly.Add(part);
            }
            assembly.Modify();
        }
    }
}
