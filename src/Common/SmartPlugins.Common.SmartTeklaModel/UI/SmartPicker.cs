using Tekla.Structures.Model;
using Tekla.Structures.Model.UI;

namespace SmartTeklaModel.UI
{
    /// <summary>
    /// Picker for tekla model objects
    /// </summary>
    public class SmartPicker
    {
        private readonly Picker _picker;

        /// <summary>
        /// .ctor
        /// </summary>
        public SmartPicker()
        {
            _picker = new Picker();
        }

        /// <summary>
        /// Pick one object by user input type
        /// </summary>
        /// <param name="pickObjectEnum"></param>
        /// <returns></returns>
        public ModelObject PickObject(Picker.PickObjectEnum pickObjectEnum)
        {
            return _picker.PickObject(pickObjectEnum);
        }
    }
}
