using System.Windows.Forms;
using CustomGame;

namespace GameFormImplementation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            var t  = new Game1();
            this.Controls.Add(t);
        }

        private void label1_Click(object sender, System.EventArgs e)
        {

        }
    }
}