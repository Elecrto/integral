namespace Integral
{
    public partial class Form1 : Form
    {
        private Painter painter;

        public Form1()
        {
            InitializeComponent();
            painter= new Painter(mainPanel.CreateGraphics());

        }

        private void mainPanel_Paint(object sender, PaintEventArgs e)
        {
            painter.paint();
        }
    }
}