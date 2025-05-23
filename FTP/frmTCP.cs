﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using DataAccess;


namespace FTP
{
    public partial class frmTCP : Form
    {
        private AccesADades accesADades;
        private Client comunicador;
        private string planetCode;
        private string tableName;
        private string shipCode;

        private string ipDesti;
        private int portListen;
        private int portSend;

        public frmTCP()
        {
            InitializeComponent();
        }

        private void loadPortInfo()
        {
            Dictionary<string, string> dictShip = new Dictionary<string, string>();
            dictShip.Add("CodeSpaceShip", this.shipCode);
            DataSet dtsShip = accesADades.ExecutaCerca(this.tableName, dictShip);
            this.portSend = int.Parse(dtsShip.Tables[0].Rows[0]["PortSpaceShipS"].ToString());

            Dictionary<string, string> dictPlanet = new Dictionary<string, string>();
            dictShip.Add("CodePlanet", this.planetCode);
            DataSet dtsPlanet = accesADades.ExecutaCerca("Planets", dictPlanet);

            this.ipDesti = dtsPlanet.Tables[0].Rows[0]["IPPlanet"].ToString();
            this.portListen = int.Parse(dtsPlanet.Tables[0].Rows[0]["PortPlanetS"].ToString());
        }

        private void showValidInfo()
        {
            txb_ip2.Text = this.ipDesti;
            txb_Port2.Text = this.portListen.ToString();
            txb_port.Text = this.portSend.ToString();
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            comunicador.SendMessage(txb_message.Text);
        }

        private void btnEscoltar_Click(object sender, EventArgs e)
        {
            comunicador.StartListening();
        }

        private void btnNoEscoltar_Click(object sender, EventArgs e)
        {   
            comunicador.StopListening();
        }

        public void OnMessageReceived(object sender, EventArgs e)
        {
            string msg = ((Client.MessageEventArgs)e).msg;
            if (textBox1.InvokeRequired)
            {
                textBox1.Invoke(new Action(() => textBox1.Text = msg));
            }
            else
            {
                textBox1.Text = msg;
            }
        }

        private void frmTCP_Load(object sender, EventArgs e)
        {
            this.accesADades = new AccesADades("SecureCore");
            this.shipCode = "FC-G1SP00000"; //Se obtendrá de un xml en start up
            this.planetCode = "KASH"; //Se obtendrá de un xml en start up
            this.tableName = "SpaceShips";
            loadPortInfo();
            showValidInfo();

            comunicador = new Client();
            comunicador.messagePortL = this.portListen;
            comunicador.filePortL = this.portSend;
            comunicador.ipDestination = this.ipDesti;
            this.comunicador.MessageReceived += new System.EventHandler(OnMessageReceived);
        }

        //arxius

        /*private void btnSeleccionarArxiu_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    tbxNameArxiu.Text = ofd.FileName;
                }
            }
        }

        private void btnEnviarArxiu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbxNameArxiu.Text))
            {
                MessageBox.Show("Selecciona un fitxer abans d'enviar-lo!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txb_port.Text, out int sendPort))
            //if (!int.TryParse(txb_port.Text, out int sendPort))
            {
                MessageBox.Show("Port d'enviament invàlid!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            FileTransferHelper.SendFile(tbxNameArxiu.Text, txb_ip.Text, sendPort);
            MessageBox.Show("Arxiu enviat correctament!", "Èxit", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnRebreArxiu_Click(object sender, EventArgs e)
        {
            string saveDirectory = @"C:\Rebuts\";
            if (!Directory.Exists(saveDirectory))
            {
                Directory.CreateDirectory(saveDirectory);
            }

            if (!int.TryParse(txb_port.Text, out int listenPort))
            {
                MessageBox.Show("Port d'escolta invàlid!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            FileTransferHelper.StartFileReceiver(txb_ip.Text, listenPort, saveDirectory, (msg) =>
            {
                MessageBox.Show(msg, "Recepció d'arxius", MessageBoxButtons.OK, MessageBoxIcon.Information);
            });
        }*/
        private void btnEnviarArxiu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbxNameArxiu.Text))
            {
                MessageBox.Show("Selecciona un fitxer abans d'enviar-lo!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!File.Exists(tbxNameArxiu.Text))
            {
                MessageBox.Show("El fitxer seleccionat no existeix!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(txb_port.Text, out int port))
            {
                MessageBox.Show("Port invàlid!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // Enviar fitxer
                FileTransferHelper.SendFile(tbxNameArxiu.Text, "127.0.0.1", port);
                MessageBox.Show($"Fitxer enviat correctament: {tbxNameArxiu.Text}", "Èxit", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error en l'enviament: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void btnRebreArxiu_Click(object sender, EventArgs e)
        {
            string saveDirectory = @"C:\Rebuts\";
            if (!Directory.Exists(saveDirectory))
            {
                Directory.CreateDirectory(saveDirectory);
            }

            if (!int.TryParse(txb_port.Text, out int port))
            {
                MessageBox.Show("Port invàlid!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                FileTransferHelper.StartFileReceiver("127.0.0.1", port, saveDirectory, (message) =>
                {
                    if (string.IsNullOrWhiteSpace(message))
                    {
                        MessageBox.Show("No s'ha rebut cap fitxer!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (message.StartsWith("Error"))
                    {
                        MessageBox.Show(message, "Error durant la recepció", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show(message, "Recepció de fitxer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                });

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error en la recepció: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnArxiu_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    tbxNameArxiu.Text = ofd.FileName;
                }
            }
        }
    }
}
