using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tekla.Structures.Model;
using Tekla.Structures.Model.UI;
using T3D = Tekla.Structures.Geometry3d;
using SmartGeometry;
using SmartTeklaModel;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace PipeRack
{
    class DataGrids
    {
        public void WorkWithDataGrid(DataGridView dataGridView1, List<Attributes> _attributesProdolnie)
        {
           var curent = AddRowDataGrid(dataGridView1, _attributesProdolnie);

            string srtr = "dataGridColumn";
            if (dataGridView1.Name == srtr)
            {
                var attr = new Form_att_column(_attributesProdolnie, curent);
                attr.ShowDialog();
                _attributesProdolnie[attr.selectY] = attr.GetAttributes();
                dataGridView1.Rows.Clear();
            }
            else
            {
                var attr = new Form_att(_attributesProdolnie, curent);
                attr.ShowDialog();
                _attributesProdolnie[attr.selectY] = attr.GetAttributes();
                dataGridView1.Rows.Clear();

            }

            for (int _count = 0; _count < _attributesProdolnie.Count; _count++)
            {
                var at = _attributesProdolnie[_count];
                if (at == null)
                    continue;
                else
                    dataGridView1.Rows.Add(_count + 1, at.Name, at.Profile, at.Material, at.Class, at.PrefixSborki, at.NomerSborki, at.PolojenieVertikalno, at.PolojeniePovorot, at.PolojenieGorizontalno);
            }
            dataGridView1.Update();
        }

        public int AddRowDataGrid(DataGridView dataGridView1, List<Attributes> _attributesProdolnie)
        {
            var curent = 0; // конкретный ярус в строке
            var index = 0;  // индекс яруса в конкретной строке
            if (dataGridView1.CurrentRow != null)
                index = dataGridView1.CurrentRow.Index;

            if (dataGridView1[0, index].Value != null)
                curent = Convert.ToInt32(dataGridView1[0, index].Value) - 1;
            else
                curent = 0;

            for (int count_r = 0; count_r < dataGridView1.RowCount - 1; count_r++)
            {
                var nomerProleta = Convert.ToInt32(dataGridView1[0, count_r].Value) - 1;

                if (_attributesProdolnie[nomerProleta] == null)
                    _attributesProdolnie[nomerProleta] = new Attributes();

                AttSetGrid(_attributesProdolnie[nomerProleta], count_r, dataGridView1);
            }
            return curent;
        }

        private void AttSetGrid(Attributes _attributesProdolnie, int I, DataGridView dataGridView1)
        {
            _attributesProdolnie.Name = dataGridView1[1, I].Value.ToString();
            _attributesProdolnie.Profile = dataGridView1[2, I].Value.ToString();
            _attributesProdolnie.Material = dataGridView1[3, I].Value.ToString();
            _attributesProdolnie.Class = dataGridView1[4, I].Value.ToString();
            _attributesProdolnie.PrefixSborki = dataGridView1[5, I].Value.ToString();
            _attributesProdolnie.NomerSborki = dataGridView1[6, I].Value.ToString();
            _attributesProdolnie.PolojenieVertikalno = Convert.ToInt32(dataGridView1[7, I].Value.ToString());
            _attributesProdolnie.PolojeniePovorot = Convert.ToInt32(dataGridView1[8, I].Value.ToString());
            _attributesProdolnie.PolojenieGorizontalno = Convert.ToInt32(dataGridView1[9, I].Value.ToString());
        }
        public DataGridViewRow CloneWithValues(DataGridViewRow row)
        {
            DataGridViewRow clonedRow = (DataGridViewRow)row.Clone();
            for (Int32 index = 0; index < row.Cells.Count; index++)
            {
                clonedRow.Cells[index].Value = row.Cells[index].Value;
            }
            return clonedRow;
        }
        public void CreateNewRow(DataGridView dataGridView1)
        {
            int I = 0;
            if (dataGridView1.CurrentRow == null)
                return;
            I = dataGridView1.SelectedCells[0].RowIndex;

            dataGridView1.Rows.Add(CloneWithValues(dataGridView1.Rows[I]));
        }
        public void DeleteeNewRow(DataGridView dataGridView1)
        {
            int I = 0;
            if (dataGridView1.CurrentRow == null)
                return;
            I = dataGridView1.SelectedCells[0].RowIndex;
            

            if (dataGridView1.CurrentRow.Index != dataGridView1.RowCount-1)
                dataGridView1.Rows.RemoveAt(I);
        }
    }
}
