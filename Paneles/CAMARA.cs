using Emgu.CV;
using Emgu.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing;


namespace Login_Los_2_chinos
{
    public partial class CAMARA : Form
    {
        public CAMARA()
        {
            InitializeComponent();
        }
        private Mat Frame;
        private VideoCapture Camara;
        private BarcodeReader Reader;

        private void CAMARA_Load(object sender, EventArgs e)
        {
            Frame = new Mat();
            Camara = new VideoCapture(0);
            Reader = new BarcodeReader();
            TimerCodigoDeBarra.Interval = 250;
            timerCamara.Interval = 40;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            Camara.Stop();
        }

        private void btnEncender_Click(object sender, EventArgs e)
        {
            try
            {
                Camara.Start();
                if (!timerCamara.Enabled)
                {
                    timerCamara.Enabled = true;
                }
                if (!TimerCodigoDeBarra.Enabled)
                {
                    TimerCodigoDeBarra.Enabled = true;
                }
                timerCamara.Start();
                TimerCodigoDeBarra.Start();
            }
            catch (Exception)
            {

            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            //Apagamos las 2 cosas
            TimerCodigoDeBarra.Stop();
            TimerCodigoDeBarra.Enabled = false;
            Camara.Start();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                Result resultado;
                if (pictureBox1.Image != null)
                {
                    resultado = Reader.Decode((Bitmap)pictureBox1.Image);
                    if (resultado != null)
                    {
                        txtBuscarCodigoBarra.Text = resultado.Text;
                        btnApagarCamara.PerformClick();
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        private void timerCamara_Tick(object sender, EventArgs e)
        {
            try
            {
                if (Camara.IsOpened)
                {
                    Camara.Read(Frame);
                    pictureBox1.Image = Frame.ToBitmap();
                }
            }
            catch (Exception)
            {

            }
        }

    }
}
