using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LudoGui
{
    class Field : Button
    {
        private readonly int fieldId;
        private GameColor color;
        private int height = 32;
        private int width = 32;

        public Field(int id, GameColor color)
        {
            this.fieldId = id;
            this.color = color;
            InitializeField();
        }

        private void InitializeField()
        {
            this.SetBounds(50, 50, width, height);
        }
    }
}
