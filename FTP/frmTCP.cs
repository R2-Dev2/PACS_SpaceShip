using System;
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
            DataRow row = dtsShip.Tables[0].Rows[0];
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
            int listenPort;
            if (!int.TryParse(txb_Port2.Text, out listenPort))
            {
                MessageBox.Show($"El port d'escolta no és vàlid. S'utilitzarà el mateix port que l'enviament ({comunicador.sendPort}).", "Avís", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                listenPort = comunicador.sendPort; 
            }

            comunicador.listenPort = listenPort;
            comunicador.StartListening();
        }

        private void btnNoEscoltar_Click(object sender, EventArgs e)
        {
            comunicador.StopListening();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            comunicador.StopListening();
        }

        private void OnMessageReceived(object sender, EventArgs e)
        {
            var msg = ((MessageEventArgs)e).MsgContent;
            if (textBox1.InvokeRequired)
            {
                textBox1.Invoke(new Action(() => textBox1.AppendText(msg + Environment.NewLine)));
            }
            else
            {
                textBox1.AppendText(msg + Environment.NewLine);
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
            comunicador.listenPort = this.portListen;
            comunicador.sendPort = this.portSend;
            comunicador.MessageReceived += OnMessageReceived;
        }

        //arxius
        
        private void btnSeleccionarArxiu_Click(object sender, EventArgs e)
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
        }
    }
}
