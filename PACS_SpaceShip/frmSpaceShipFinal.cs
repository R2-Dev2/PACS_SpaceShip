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
using Workflow;
using static Workflow.PACSMessage;
using FTP;

namespace PACS_SpaceShip
{
    public partial class frmSpaceShipFinal : Form
    {
        private bool isListening = false;
        private SpaceShipWorkflow workflow;
        private SpaceShip spaceShip;
        private Client ftpClient;

        public frmSpaceShipFinal()
        {
            InitializeComponent();
        }

        private void loadPlanetData()
        {
            spaceShip = new SpaceShip();
            if (!spaceShip.loadConfig())
            {
                MessageBox.Show("Error loading configuration data. The program cannot start");
                this.Close();
            }

            DataRow shipRow = DatabaseHelper.SpaceShipInfo(spaceShip.CodeShip);

            spaceShip.IdShip = shipRow["idSpaceShip"].ToString();
            string portListenStr = shipRow["PortSpaceShipL"].ToString();
            if (int.TryParse(portListenStr, out int portListen))
            {
                spaceShip.PortListen = portListen;
            }
            else
            {
                MessageBox.Show("Error loading configuration data. The program cannot start");
                this.Close();
            }

        }

        public void OnMessageReceived(object sender, EventArgs e)
        {
            string msg = ((Client.MessageEventArgs)e).msg;
            AddToListBox("New message received");
            ProcessMessage(msg);
        }

        private void ProcessMessage(string msg)
        {
            PACSMessage message = PACSMessage.ParseMessage(msg);
            if (MessageType.VR.Equals(message.type))
            {
                ValidationMessage valMsg = (ValidationMessage)message;
                if (valMsg.result.Equals(ValidationResult.VP))
                {
                    updateLabel(lblTitle1, "Acces Aproved");
                    tabControl.Invoke((MethodInvoker)delegate
                    {
                        if (tabControl.SelectedIndex < tabControl.TabCount - 1)
                        {
                            tabControl.SelectedIndex = tabControl.SelectedIndex + 1;
                        }
                    });
                }
                else
                {
                    updateLabel(lblTitle1, "Acces Denied");
                }
            }
        }

        private void OcultarEncabezados(TabControl tabControl1)
        {
            tabControl1.Appearance = TabAppearance.FlatButtons;
            tabControl1.ItemSize = new Size(0, 1);
            tabControl1.SizeMode = TabSizeMode.Fixed;
        }

        private void updateLabel(Label lbl, string message, Color? color = null)
        {
            if (color is null)
            {
                color = Color.Black;
            }
            if (lbl.InvokeRequired)
            {
                lbl.Invoke((MethodInvoker)delegate
                {
                    lbl.Visible = true;
                    lbl.ForeColor = (Color)color;
                    lbl.Text = message;
                    lbl.Refresh();
                });
            }
            else
            {
                lbl.Visible = true;
                lbl.ForeColor = (Color)color;
                lbl.Text = message;
                lbl.Refresh();
            }
        }

        private void AddToListBox(string msg)
        {
            if (lbxInfo.InvokeRequired)
            {
                lbxInfo.Invoke((MethodInvoker)delegate
                {
                    lbxInfo.Items.Add(msg);
                });
            }
            else lbxInfo.Items.Add(msg);
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

        private void frmSpaceShipFinal_Load(object sender, EventArgs e)
        {
            OcultarEncabezados(tabControl);
            MakeButtonCircular(btn1);
            MakeButtonCircular(btn2);
            MakeButtonCircular(btn3);

            loadPlanetData();
            this.txtCodeSpaceShip.Text = spaceShip.CodeShip;
            this.ftpClient = new Client();
            this.ftpClient.listenPort = spaceShip.PortListen;
            this.ftpClient.MessageReceived += new System.EventHandler(OnMessageReceived);
            lblTitle.Text = spaceShip.CodeShip;
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
                ftpClient.StartListening();
                AddToListBox($"Listening to messages on port {ftpClient.listenPort}");
            }
            else
            {
                isListening = false;

                btnStartListening.Text = null;

                //btnStartListening.BackgroundImage = Properties.Resources.stop_listen;

                btnStartListening.BackgroundImageLayout = ImageLayout.Stretch;

                // Aturar escolta...
                ftpClient.StopListening();
                AddToListBox("Stopped listening to messages");
            }
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            tabControl.SelectedTab = tabPage1;
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            tabControl.SelectedTab = tabPage2;
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
            AddToListBox(workflow.planetCode);
            string encryptedKey = this.workflow.EncrypKey();
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
