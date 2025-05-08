using DataAccess;
using FTP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Workflow;
using static Workflow.PACSMessage;

namespace PACS_SpaceShip
{
    public partial class frmSpaceship : Form
    {
        private SpaceShipWorkflow workflow;
        private SpaceShip spaceShip;
        private Client ftpClient;
        public frmSpaceship()
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
            if(int.TryParse(portListenStr, out int portListen))
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

        private void frmSpaceship_Load(object sender, EventArgs e)
        {
            loadPlanetData();
            this.txtCodeSpaceShip.Text = spaceShip.CodeShip;
            this.ftpClient = new Client();
            this.ftpClient.listenPort = spaceShip.PortListen;
            this.ftpClient.MessageReceived += new System.EventHandler(OnMessageReceived);
            lblTitle.Text = spaceShip.CodeShip;
            OcultarEncabezados(tabControl);
        }

        private void pbClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnStartListening_Click(object sender, EventArgs e)
        {
            ftpClient.StartListening();
            AddToListBox($"Listening to messages on port {ftpClient.listenPort}");
        }

        private void btnStopListening_Click(object sender, EventArgs e)
        {
            ftpClient.StopListening();
            AddToListBox("Stopped listening to messages");
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
        }
    }
}
