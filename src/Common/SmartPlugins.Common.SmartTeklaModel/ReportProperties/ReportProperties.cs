using System.Collections;
using Tekla.Structures.Model;

namespace SmartTeklaModel.ReportProperties
{
    /// <summary>
    /// Report properties
    /// </summary>
    public class ReportProperties
    {
        private ModelObject _modelObject;
        private Hashtable _values;

        private ArrayList _stringAttributes;
        private ArrayList _doubleAttributes;
        private ArrayList _integerAttributes;

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="modelObject"></param>
        public ReportProperties(ModelObject modelObject)
        {
            _modelObject = modelObject;
            _stringAttributes = new ArrayList();
            _doubleAttributes = new ArrayList();
            _integerAttributes = new ArrayList();
        }

        /// <summary>
        /// Add string attribute
        /// </summary>
        /// <param name="attribute"></param>
        public void AddStringAttribute(string attribute) => _stringAttributes.Add(attribute);

        /// <summary>
        ///  Add double attribute
        /// </summary>
        /// <param name="attribute"></param>
        public void AddDoubleAttribute(string attribute) => _doubleAttributes.Add(attribute);

        /// <summary>
        /// Add integer attribute
        /// </summary>
        /// <param name="attribute"></param>
        public void AddIntegerAttribute(string attribute) => _integerAttributes.Add(attribute);

        /// <summary>
        /// Refresh report properties
        /// </summary>
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

        /// <summary>
        /// Get attribute
        /// </summary>
        /// <typeparam name="T">Attribute type</typeparam>
        /// <param name="attribute">Attribute of name</param>
        /// <returns></returns>
        public T GetAttribute<T>(string attribute)
        {
            if (_values.Contains(attribute))
                return (T)_values[attribute];
            else
                return default(T);
        }
    }
}
