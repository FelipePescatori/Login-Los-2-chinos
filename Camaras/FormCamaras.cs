using AForge.Video;
using AForge.Video.DirectShow;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace Login_Los_2_chinos
{
    public partial class FormCamaras : Form
    {
        private FilterInfoCollection videoDevices;
        private List<VideoCaptureDevice> videoSources;

        public FormCamaras()
        {
            InitializeComponent();
            videoSources = new List<VideoCaptureDevice>(4); // Inicializar la lista con un tamaño inicial de 4
        }

        private void FormCamaras_Load(object sender, EventArgs e)
        {
            // Obtener la lista de dispositivos de video disponibles
            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            if (videoDevices.Count == 0)
            {
                MessageBox.Show("No se encontraron cámaras disponibles.");
                return;
            }

            // Inicializar cada elemento de la lista con null
            for (int i = 0; i < 4; i++)
            {
                videoSources.Add(null);
            }

            // Llenar cada ComboBox con los nombres de las cámaras
            foreach (FilterInfo device in videoDevices)
            {
                comboBoxCamaras1.Items.Add(device.Name);
                comboBoxCamaras2.Items.Add(device.Name);
                comboBoxCamaras3.Items.Add(device.Name);
                comboBoxCamaras4.Items.Add(device.Name);
            }

            // Agregar la opción "None" a cada ComboBox
            comboBoxCamaras1.Items.Add("None");
            comboBoxCamaras2.Items.Add("None");
            comboBoxCamaras3.Items.Add("None");
            comboBoxCamaras4.Items.Add("None");

            // Dejar cada ComboBox en blanco al inicio
            comboBoxCamaras1.SelectedIndex = 1;
            comboBoxCamaras2.SelectedIndex = 1;
            comboBoxCamaras3.SelectedIndex = 1;
            comboBoxCamaras4.SelectedIndex = 1;
        }

        private void comboBoxCamaras1_SelectedIndexChanged(object sender, EventArgs e)
        {
            HandleComboBoxSelection(comboBoxCamaras1, pictureBox1, 0);
        }

        private void comboBoxCamaras2_SelectedIndexChanged(object sender, EventArgs e)
        {
            HandleComboBoxSelection(comboBoxCamaras2, pictureBox2, 1);
        }

        private void comboBoxCamaras3_SelectedIndexChanged(object sender, EventArgs e)
        {
            HandleComboBoxSelection(comboBoxCamaras3, pictureBox3, 2);
        }

        private void comboBoxCamaras4_SelectedIndexChanged(object sender, EventArgs e)
        {
            HandleComboBoxSelection(comboBoxCamaras4, pictureBox4, 3);
        }

        private void HandleComboBoxSelection(ComboBox comboBox, PictureBox pictureBox, int index)
        {
            if (videoSources.Count <= index)
            {
                videoSources.Add(null);
            }

            if (comboBox.SelectedIndex == comboBox.Items.Count - 1)
            {
                // Si se selecciona "None," apaga la cámara correspondiente
                if (videoSources[index] != null && videoSources[index].IsRunning)
                {
                    videoSources[index].SignalToStop();
                    videoSources[index].WaitForStop();
                }

                // Muestra una imagen para "None" en el PictureBox correspondiente
                pictureBox.Image = Image.FromFile("D:\\VIOLETA\\Login Los 2 chinos\\Camaras\\NoneCamra.jpg");
            }
            else
            {
                // Enciende la cámara seleccionada
                StartCamera(comboBox.SelectedIndex, pictureBox, index);
            }
        }

        private void StartCamera(int cameraIndex, PictureBox pictureBox, int videoSourceIndex)
        {
            // Apaga la cámara correspondiente antes de iniciar una nueva
            if (videoSources[videoSourceIndex] != null && videoSources[videoSourceIndex].IsRunning)
            {
                videoSources[videoSourceIndex].SignalToStop();
                videoSources[videoSourceIndex].WaitForStop();
            }

            // Configura y enciende la cámara seleccionada
            videoSources[videoSourceIndex] = new VideoCaptureDevice(videoDevices[cameraIndex].MonikerString);
            videoSources[videoSourceIndex].NewFrame += new NewFrameEventHandler((s, e) => videoSource_NewFrame(s, e, pictureBox));
            videoSources[videoSourceIndex].Start();
        }

        private void FormCamaras_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Apaga todas las cámaras al cerrar el formulario
            foreach (var source in videoSources)
            {
                if (source != null && source.IsRunning)
                {
                    source.SignalToStop();
                    source.WaitForStop();
                }
            }
        }

        private void videoSource_NewFrame(object sender, NewFrameEventArgs eventArgs, PictureBox pictureBox)
        {
            // Mostrar el fotograma de la cámara en el PictureBox correspondiente
            pictureBox.Image = (Bitmap)eventArgs.Frame.Clone();
        }
    }
}


