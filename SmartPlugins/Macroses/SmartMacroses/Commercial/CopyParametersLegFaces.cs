using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMacroses
{
    public class CopyParametersLegFaces
    {
        private double _delta;
        private bool _reversed;
        private int _layerOrderNumber;

        public double Delta { get => _delta; }
        public bool Reversed { get => _reversed; }
        public int LayerOrderNumber { get => _layerOrderNumber; }

        public CopyParametersLegFaces(double delta, bool reversed, int layerOrderNumber)
        {
            _delta = delta;
            _reversed = reversed;
            _layerOrderNumber = layerOrderNumber;
        }

        public static void Run()
        {
            var window = new CopyParametersLegFacesWindow();
            window.ShowDialog();
        }
    }
}
