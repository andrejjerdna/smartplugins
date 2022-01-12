using SmartPlugins.Common.Abstractions.ModelObjects;
using SmartPlugins.Common.TeklaLibrary.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using Tekla.Structures.Model;

namespace SmartPlugins.Common.TeklaLibrary.Entities
{
    public class SmartAssembly : SmartBaseObject, IAssembly
    {
        private readonly Assembly _assembly;

        public SmartAssembly(Assembly assembly)
        {
            ModelObject = _assembly = assembly;
        }
        
        public IPart GetMainPart()
        {
            var mo = _assembly.GetMainPart();

            if(mo is Part part)
                return new SmartPart(part);
            else
                return null;
        }

        public IEnumerable<IPart> GetSecondaries()
        {
            return _assembly.GetAllParts(false).Where(part => part != null).Select(part => new SmartPart(part));
        }

        public void Modify() => _assembly.Modify();

        public void SetMainPart(IPart part)
        {
            var mainPart = part.GetOriginObject<Part>();

            if(mainPart != null)
                _assembly.SetMainPart(mainPart);
        }
    }
}
