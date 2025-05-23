﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Model;
using Workflow;
using CodificationUtils;
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
        private FileGenerator fileGenerator;


        public frmSpaceShipFinal()
        {
            InitializeComponent();
        }

        private void loadSpaceShipData()
        {
            spaceShip = new SpaceShip();
            if (!spaceShip.loadConfig())
            {
                MessageBox.Show("Error loading configuration data. The program cannot start");
                this.Close();
            }

            DataRow shipRow = DatabaseHelper.SpaceShipInfo(spaceShip.CodeShip);
            try
            {
                spaceShip.IdShip = shipRow["idSpaceShip"].ToString();
                string messagePortStr = shipRow["PortSpaceShipL"].ToString();
                string filePortStr = shipRow["PortSpaceShipS"].ToString();
                spaceShip.MessagePort = int.Parse(messagePortStr);
                spaceShip.FilePort = int.Parse(filePortStr);
            }
            catch(Exception)
            {
                MessageBox.Show("Error loading configuration data. The program cannot start");
                this.Close();
            }

        }

        public void OnMessageReceived(object sender, EventArgs e)
        {
            string msg = ((Client.MessageEventArgs)e).msg;
            AddToListBox("Message received");
            ProcessMessage(msg);
        }

        public void OnFileReceived(object sender, EventArgs e)
        {
            string path = ((Client.FileMessageEventArgs)e).FileName;
            AddToListBox("ZIP received");
            enableButton(btnDecodificar, true);
        }

        private void ProcessMessage(string msg)
        {
            AddToListBox(msg);
            PACSMessage message = PACSMessage.ParseMessage(msg);
            if (message is null) return;
            if (MessageType.VR.Equals(message.type))
            {
                ValidationMessage valMsg = (ValidationMessage)message;
                if (valMsg.result.Equals(ValidationResult.VP)) {
                    nextTab();
                }
                else if (valMsg.result.Equals(ValidationResult.AG)){
                    updateLabel(lblTitle3, "ACCESS GRANTED");
                }
                else
                {
                    updateLabel(lblTitle1, "ACCES DENIED");
                }
            }
        }

        private void LoadFileGenerator()
        {
            fileGenerator = new FileGenerator();
            fileGenerator.config = this.spaceShip.Encoding;
            fileGenerator.SumFinished += new System.EventHandler(OnSumFinished);
            fileGenerator.codifications = DatabaseHelper.GetCodifications(workflow.planetId);
        }

        private void OcultarEncabezados(TabControl tabControl1)
        {
            tabControl1.Appearance = TabAppearance.FlatButtons;
            tabControl1.ItemSize = new Size(0, 1);
            tabControl1.SizeMode = TabSizeMode.Fixed;
        }

        private void nextTab()
        {
            tabControl.Invoke((MethodInvoker)delegate
            {
                if (tabControl.SelectedIndex < tabControl.TabCount - 1)
                {
                    tabControl.SelectedIndex = tabControl.SelectedIndex + 1;
                }
            });
        }

        private void firstTab()
        {
            if (tabControl.InvokeRequired)
            {
                tabControl.Invoke((MethodInvoker)delegate
                {
                    tabControl.SelectedIndex = 0;
                });
            }
            else
            {
                tabControl.SelectedIndex = 0;
            }
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

        private void enableButton(Button btn, bool isEnable)
        {
            if (btn.InvokeRequired)
            {
                btn.Invoke((MethodInvoker)delegate
                {
                    btn.Enabled = isEnable;
                });
            }
            else
            {
                btn.Enabled = isEnable;
            }
        }

        public void OnSumFinished(object sender, EventArgs e)
        {
            long sum = ((FileGenerator.SumFinishedEventArgs)e).sum;
            AddToListBox($"Sum finished");
            ftpClient.SendMessage(sum.ToString());
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

            loadSpaceShipData();
            this.txtCodeSpaceShip.Text = spaceShip.CodeShip;
            this.ftpClient = new Client();
            this.ftpClient.messagePortL = spaceShip.MessagePort;
            this.ftpClient.filePortL = spaceShip.FilePort;
            this.ftpClient.MessageReceived += new System.EventHandler(OnMessageReceived);
            this.ftpClient.FileReceived += new System.EventHandler(OnFileReceived);
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

                ftpClient.StartListening();
                AddToListBox($"Listening for messages");
            }
            else
            {
                isListening = false;

                btnStartListening.Text = null;

                //btnStartListening.BackgroundImage = Properties.Resources.stop_listen;

                btnStartListening.BackgroundImageLayout = ImageLayout.Stretch;

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


        private void btnEnviar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtDeliveryCode.Text)){
                AddToListBox("The message cannot be sent because DeliveryCode is missing or incorrect");
                return;
            }
            this.workflow = new SpaceShipWorkflow();
            this.workflow.SpaceShipCode = spaceShip.CodeShip;
            this.workflow.SpaceShipId = spaceShip.IdShip;
            this.workflow.DeliveryCode = txtDeliveryCode.Text;
            string msg = workflow.GetEntryMessage();
            if (msg != null)
            {
                ftpClient.ipDestination = this.workflow.planetIp;
                ftpClient.messagePortS = this.workflow.planetMsgPort;
                ftpClient.filePortS = this.workflow.planetFilePort;
                ftpClient.SendMessage(msg);
                AddToListBox($"Sending ER message");
            }
            else
            {
                AddToListBox("The message cannot be sent because DeliveryCode is missing or incorrect");
            }
        }

        private void btnGenCred_Click(object sender, EventArgs e)
        {
            this.workflow.GenerateAesCredentials();
            AddToListBox("Credentials generated");
            this.workflow.EncrypKey();
            this.workflow.EncrypIV();
            AddToListBox("Credentials encrypted");
            enableButton(btnDescarregarPdf, true);
        }

        private void btnDescarregarPdf_Click(object sender, EventArgs e)
        {
            this.workflow.EncryptPDF();
            AddToListBox("PDF downloaded and encrypted");
            enableButton(btnSend2, true);
        }

        private void btnDecodificar_Click(object sender, EventArgs e)
        {
            LoadFileGenerator();
            fileGenerator.EncodeFilesAndSum();
            enableButton(btnEnviar3, true);
        }

        private void pbClose_Click(object sender, EventArgs e)
        {
            ftpClient.StopListening();
            this.Close();
        }

        private void btnSend2_Click(object sender, EventArgs e)
        {
            string encryptedKey = workflow.KeyEncripted;
            string encryptedIV = workflow.IVEncrypted;
            string encryptedPdf = workflow.PDFEncrypted;

            ftpClient.SendEncryptedCredentials(encryptedKey, encryptedIV, encryptedPdf);

            AddToListBox("Credentials and PDF sent");
        }
    }
}
