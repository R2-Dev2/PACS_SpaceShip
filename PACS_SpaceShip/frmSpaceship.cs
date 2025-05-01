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

namespace PACS_SpaceShip
{
    public partial class frmSpaceship : Form
    {
        private SpaceShip spaceShip;
        private AccesADades accesADades;
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

            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("codeSpaceShip", spaceShip.CodeShip);
            DataSet dataset = accesADades.ExecutaCerca("SpaceShips", dict);

            spaceShip.IdShip = dataset.Tables[0].Rows[0]["idSpaceShip"].ToString();
            spaceShip.PortSend = dataset.Tables[0].Rows[0]["PortSpaceShipS"].ToString();

        }


        private void OcultarEncabezados(TabControl tabControl1)
        {
            tabControl1.Appearance = TabAppearance.FlatButtons;
            tabControl1.ItemSize = new Size(0, 1);
            tabControl1.SizeMode = TabSizeMode.Fixed;
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
            tabControl1.SelectedTab = tabPage3;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage2;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage3;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmTCP form2 = new frmTCP();

            form2.Show();

            this.Hide();
        }

        private void frmSpaceship_Load(object sender, EventArgs e)
        {
            this.accesADades = new AccesADades("SecureCore");
            loadPlanetData();

            lblTitle.Text = spaceShip.CodeShip;
            OcultarEncabezados(tabControl1);
        }

        private void pbClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
