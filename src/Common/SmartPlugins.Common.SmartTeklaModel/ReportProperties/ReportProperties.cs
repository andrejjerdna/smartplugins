using Fusion;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Model;

namespace SmartTeklaModel.ReportProperties
{
    public class ReportProperties
    {
        private ModelObject _modelObject;
        private Hashtable _values;

        private ArrayList _stringAttributes;
        private ArrayList _doubleAttributes;
        private ArrayList _integerAttributes;
        public ReportProperties(ModelObject modelObject)
        {
            _modelObject = modelObject;
            _stringAttributes = new ArrayList();
            _doubleAttributes = new ArrayList();
            _integerAttributes = new ArrayList();
        }

        public void AddStringAttribut(string attribute) => _stringAttributes.Add(attribute);

        public void AddDoubleAttribut(string attribute) => _doubleAttributes.Add(attribute);

        public void AddIntegerAttribut(string attribute) => _integerAttributes.Add(attribute);

        public void RefreshReportProperties()
        {
            if (_modelObject == null)
            {
                _values = new Hashtable();
                return;
            }

            var valuesCount = _stringAttributes.Count + _doubleAttributes.Count + _integerAttributes.Count;

            _values = new Hashtable(valuesCount);

            _modelObject.GetAllReportProperties(_stringAttributes, _doubleAttributes, _integerAttributes, ref _values);
        }

        public T GetAttribute<T>(string attribute)
        {
            if (_values.Contains(attribute))
                return (T)_values[attribute];
            else
                return default(T);
        }
    }
}
