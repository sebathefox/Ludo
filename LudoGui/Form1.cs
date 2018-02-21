using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LudoGui
{
    enum GameColor { Yellow, Blue, Red, Green, White}
    enum GameState { InPlay, Finished}

    public partial class Form1 : Form
    {

        private Field[] fields;
        public Form1()
        {
            InitializeComponent();

            CreateField();

            //Field felt = new Field(1, GameColor.White);

            //this.Controls.Add(felt);
        }

        private void CreateField()
        {
            fields = new Field[56];

            for (int i = 0; i < fields.Length; i++)
            {
                fields[i] = new Field(i, GameColor.White);
                //fields[i].Width += i * 39; 
                fields[i].w
            }

            AddComponents(fields);
        }

        private void AddComponents(Control[] obj)
        {
            foreach (var ob in obj)
            {
                this.Controls.Add(ob);
            }
        }
    }
}
