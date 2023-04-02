namespace Integral
{
    public partial class Form1 : Form
    {
        private Painter painter;

        public Form1()
        {
            InitializeComponent();
            painter = new Painter(mainPanel.CreateGraphics());

        }

        private void mainPanel_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void resize(object sender, EventArgs e)
        {
            mainPanel.Height = this.Height;
            mainPanel.Width = this.Width;
            painter = new Painter(mainPanel.CreateGraphics());
            painter.paint();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            painter.X_MIN = Convert.ToInt32(textBox1.Text);
            painter.X_MAX = Convert.ToInt32(textBox2.Text);
            painter.Y_MIN = Convert.ToInt32(textBox3.Text);
            painter.Y_MAX = Convert.ToInt32(textBox4.Text);
            painter.ACC = Convert.ToInt32(textBox5.Text);
            painter.paint();
            label1.Text = Convert.ToString(Math.Round(painter.S,5));
            painter.S = 0;
        }
    }
}