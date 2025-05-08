using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PACS_SpaceShip
{
    public partial class frmSpaceShipFinal : Form
    {
        private bool isListening = false;

        public frmSpaceShipFinal()
        {
            InitializeComponent();
        }

        private void OcultarEncabezados(TabControl tabControl1)
        {
            tabControl1.Appearance = TabAppearance.FlatButtons;
            tabControl1.ItemSize = new Size(0, 1);
            tabControl1.SizeMode = TabSizeMode.Fixed;
        }

        private void frmSpaceShipFinal_Load(object sender, EventArgs e)
        {
            OcultarEncabezados(tabControl1);
            MakeButtonCircular(btn1);
            MakeButtonCircular(btn2);
            MakeButtonCircular(btn3);
        }
        private void MakeButtonCircular(Button btn)
        {
            int diameter = Math.Min(btn.Width, btn.Height);
            GraphicsPath path = new GraphicsPath();
            path.AddEllipse(0, 0, diameter, diameter);
            btn.Region = new Region(path);

            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = Color.White;

            btn.TextAlign = ContentAlignment.MiddleCenter;
            btn.Font = new Font(btn.Font.FontFamily, btn.Font.Size, FontStyle.Bold);

            btn.AutoSize = false;
            btn.Size = new Size(diameter, diameter);
            btn.Padding = new Padding(0);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage2;
        }

        private void btnStartListening_Click(object sender, EventArgs e)
        {
            if (!isListening)
            {
                isListening = true;

                btnStartListening.Text = null;

                //btnStartListening.BackgroundImage = Properties.Resources.listen;

                btnStartListening.BackgroundImageLayout = ImageLayout.Stretch;


                // Iniciar escolta...
            }
            else
            {
                isListening = false;

                btnStartListening.Text = null;

                //btnStartListening.BackgroundImage = Properties.Resources.stop_listen;

                btnStartListening.BackgroundImageLayout = ImageLayout.Stretch;

                // Aturar escolta...
            }
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage1;
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage2;
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            tabControl.SelectedTab = tabPage3;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            tabControl.SelectedTab = tabPage2;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            tabControl.SelectedTab = tabPage3;
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            this.workflow = new SpaceShipWorkflow();
            this.workflow.SpaceShipCode = spaceShip.CodeShip;
            this.workflow.SpaceShipId = spaceShip.IdShip;
            this.workflow.DeliveryCode = txtDeliveryCode.Text;
            string msg = workflow.GetEntryMessage();
            if (msg != null)
            {
                ftpClient.ipDestination = this.workflow.planetIp;
                ftpClient.sendPort = this.workflow.planetPortL;
                ftpClient.SendMessage(msg);
                AddToListBox($"Sending message {msg} to IP {ftpClient.ipDestination} via {ftpClient.sendPort}");
            }
            else
            {
                AddToListBox("The message cannot be sent because no DeliveryCode has been requested.");
            }
        }

        private void btnGenCred_Click(object sender, EventArgs e)
        {
            this.workflow.GenerateAesCredentials();
            string encryptedKey = this.workflow.KeyEncripted();
            string encryptedIV = this.workflow.EncrypIV();

            ftpClient.SendMessage(encryptedKey);
            ftpClient.SendMessage(encryptedIV);
        }

        private void btnDescarregarPdf_Click(object sender, EventArgs e)
        {
            string encryptedPdf = this.workflow.EncryptPDF();

            ftpClient.SendMessage(encryptedPdf);
        }
    }
}
