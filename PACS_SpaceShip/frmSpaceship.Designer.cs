
namespace PACS_SpaceShip
{
    partial class frmSpaceship
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSpaceship));
            this.pnlTitle = new System.Windows.Forms.Panel();
            this.pbClose = new System.Windows.Forms.PictureBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.flpFrms = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlListBox = new System.Windows.Forms.Panel();
            this.lbxInfo = new System.Windows.Forms.ListBox();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lblDeliveryCode = new System.Windows.Forms.Label();
            this.lblSpaceShipCode = new System.Windows.Forms.Label();
            this.txtDeliveryCode = new System.Windows.Forms.TextBox();
            this.txtCodeSpaceShip = new System.Windows.Forms.TextBox();
            this.btnEnviar = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.lblTitle1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btnGenCred = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.btnDecodificar = new System.Windows.Forms.Button();
            this.btnEnviar3 = new System.Windows.Forms.Button();
            this.btnGenerarFitxer = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.btn1 = new System.Windows.Forms.Button();
            this.btn3 = new System.Windows.Forms.Button();
            this.btn2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lblListening = new System.Windows.Forms.Label();
            this.btnStopListening = new System.Windows.Forms.Button();
            this.btnStartListening = new System.Windows.Forms.Button();
            this.pnlTitle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbClose)).BeginInit();
            this.flpFrms.SuspendLayout();
            this.pnlListBox.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.pnlTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTitle
            // 
            this.pnlTitle.BackColor = System.Drawing.Color.LightGray;
            this.pnlTitle.Controls.Add(this.pbClose);
            this.pnlTitle.Controls.Add(this.lblTitle);
            this.pnlTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTitle.Location = new System.Drawing.Point(0, 0);
            this.pnlTitle.Margin = new System.Windows.Forms.Padding(2);
            this.pnlTitle.Name = "pnlTitle";
            this.pnlTitle.Size = new System.Drawing.Size(1079, 58);
            this.pnlTitle.TabIndex = 0;
            // 
            // pbClose
            // 
            this.pbClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbClose.BackColor = System.Drawing.Color.Transparent;
            this.pbClose.Image = global::PACS_SpaceShip.Properties.Resources.close;
            this.pbClose.Location = new System.Drawing.Point(1039, 0);
            this.pbClose.Margin = new System.Windows.Forms.Padding(0);
            this.pbClose.Name = "pbClose";
            this.pbClose.Size = new System.Drawing.Size(40, 40);
            this.pbClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbClose.TabIndex = 24;
            this.pbClose.TabStop = false;
            this.pbClose.Click += new System.EventHandler(this.pbClose_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(293, 15);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(310, 46);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Nom SpaceShip";
            // 
            // flpFrms
            // 
            this.flpFrms.Controls.Add(this.pnlListBox);
            this.flpFrms.Controls.Add(this.tabControl);
            this.flpFrms.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flpFrms.Location = new System.Drawing.Point(0, 263);
            this.flpFrms.Margin = new System.Windows.Forms.Padding(2);
            this.flpFrms.Name = "flpFrms";
            this.flpFrms.Size = new System.Drawing.Size(1079, 556);
            this.flpFrms.TabIndex = 1;
            // 
            // pnlListBox
            // 
            this.pnlListBox.Controls.Add(this.lbxInfo);
            this.pnlListBox.Location = new System.Drawing.Point(3, 3);
            this.pnlListBox.Name = "pnlListBox";
            this.pnlListBox.Size = new System.Drawing.Size(397, 626);
            this.pnlListBox.TabIndex = 8;
            // 
            // lbxInfo
            // 
            this.lbxInfo.Font = new System.Drawing.Font("Cambria", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbxInfo.FormattingEnabled = true;
            this.lbxInfo.HorizontalScrollbar = true;
            this.lbxInfo.ItemHeight = 20;
            this.lbxInfo.Location = new System.Drawing.Point(84, 34);
            this.lbxInfo.Margin = new System.Windows.Forms.Padding(2);
            this.lbxInfo.Name = "lbxInfo";
            this.lbxInfo.Size = new System.Drawing.Size(255, 364);
            this.lbxInfo.TabIndex = 5;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Controls.Add(this.tabPage3);
            this.tabControl.Location = new System.Drawing.Point(405, 2);
            this.tabControl.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(588, 526);
            this.tabControl.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.LightGray;
            this.tabPage1.Controls.Add(this.lblDeliveryCode);
            this.tabPage1.Controls.Add(this.lblSpaceShipCode);
            this.tabPage1.Controls.Add(this.txtDeliveryCode);
            this.tabPage1.Controls.Add(this.txtCodeSpaceShip);
            this.tabPage1.Controls.Add(this.btnEnviar);
            this.tabPage1.Controls.Add(this.button7);
            this.tabPage1.Controls.Add(this.lblTitle1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage1.Size = new System.Drawing.Size(580, 497);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            // 
            // lblDeliveryCode
            // 
            this.lblDeliveryCode.AutoSize = true;
            this.lblDeliveryCode.Font = new System.Drawing.Font("Cambria", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDeliveryCode.Location = new System.Drawing.Point(24, 124);
            this.lblDeliveryCode.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDeliveryCode.Name = "lblDeliveryCode";
            this.lblDeliveryCode.Size = new System.Drawing.Size(118, 20);
            this.lblDeliveryCode.TabIndex = 11;
            this.lblDeliveryCode.Text = "Delivery Code";
            // 
            // lblSpaceShipCode
            // 
            this.lblSpaceShipCode.AutoSize = true;
            this.lblSpaceShipCode.Font = new System.Drawing.Font("Cambria", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSpaceShipCode.Location = new System.Drawing.Point(24, 72);
            this.lblSpaceShipCode.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSpaceShipCode.Name = "lblSpaceShipCode";
            this.lblSpaceShipCode.Size = new System.Drawing.Size(131, 20);
            this.lblSpaceShipCode.TabIndex = 10;
            this.lblSpaceShipCode.Text = "SpaceShip Code";
            // 
            // txtDeliveryCode
            // 
            this.txtDeliveryCode.Font = new System.Drawing.Font("Cambria", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDeliveryCode.Location = new System.Drawing.Point(163, 122);
            this.txtDeliveryCode.Margin = new System.Windows.Forms.Padding(2);
            this.txtDeliveryCode.Name = "txtDeliveryCode";
            this.txtDeliveryCode.Size = new System.Drawing.Size(193, 27);
            this.txtDeliveryCode.TabIndex = 9;
            // 
            // txtCodeSpaceShip
            // 
            this.txtCodeSpaceShip.Font = new System.Drawing.Font("Cambria", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodeSpaceShip.Location = new System.Drawing.Point(163, 70);
            this.txtCodeSpaceShip.Margin = new System.Windows.Forms.Padding(2);
            this.txtCodeSpaceShip.Name = "txtCodeSpaceShip";
            this.txtCodeSpaceShip.ReadOnly = true;
            this.txtCodeSpaceShip.Size = new System.Drawing.Size(193, 27);
            this.txtCodeSpaceShip.TabIndex = 8;
            // 
            // btnEnviar
            // 
            this.btnEnviar.Font = new System.Drawing.Font("Cambria", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEnviar.Location = new System.Drawing.Point(194, 190);
            this.btnEnviar.Margin = new System.Windows.Forms.Padding(2);
            this.btnEnviar.Name = "btnEnviar";
            this.btnEnviar.Size = new System.Drawing.Size(162, 43);
            this.btnEnviar.TabIndex = 7;
            this.btnEnviar.Text = "Send Entry Request";
            this.btnEnviar.UseVisualStyleBackColor = true;
            this.btnEnviar.Click += new System.EventHandler(this.btnEnviar_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(334, 291);
            this.button7.Margin = new System.Windows.Forms.Padding(2);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(81, 26);
            this.button7.TabIndex = 6;
            this.button7.Text = "Next";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // lblTitle1
            // 
            this.lblTitle1.AutoSize = true;
            this.lblTitle1.Font = new System.Drawing.Font("Cambria", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle1.Location = new System.Drawing.Point(14, 16);
            this.lblTitle1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTitle1.Name = "lblTitle1";
            this.lblTitle1.Size = new System.Drawing.Size(177, 33);
            this.lblTitle1.TabIndex = 4;
            this.lblTitle1.Text = "Start delivery";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.LightGray;
            this.tabPage2.Controls.Add(this.button3);
            this.tabPage2.Controls.Add(this.button2);
            this.tabPage2.Controls.Add(this.btnGenCred);
            this.tabPage2.Controls.Add(this.button8);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage2.Size = new System.Drawing.Size(580, 497);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(106, 163);
            this.button3.Margin = new System.Windows.Forms.Padding(2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(111, 49);
            this.button3.TabIndex = 11;
            this.button3.Text = "Descarregar PDF";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(106, 227);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(83, 30);
            this.button2.TabIndex = 10;
            this.button2.Text = "Enviar";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // btnGenCred
            // 
            this.btnGenCred.Location = new System.Drawing.Point(106, 97);
            this.btnGenCred.Margin = new System.Windows.Forms.Padding(2);
            this.btnGenCred.Name = "btnGenCred";
            this.btnGenCred.Size = new System.Drawing.Size(111, 49);
            this.btnGenCred.TabIndex = 9;
            this.btnGenCred.Text = "Generar Credencials";
            this.btnGenCred.UseVisualStyleBackColor = true;
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(773, 319);
            this.button8.Margin = new System.Windows.Forms.Padding(2);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(81, 26);
            this.button8.TabIndex = 7;
            this.button8.Text = "Next";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(42, 38);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(325, 31);
            this.label6.TabIndex = 5;
            this.label6.Text = "ENVIANT MISSATGE VR";
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.LightGray;
            this.tabPage3.Controls.Add(this.btnDecodificar);
            this.tabPage3.Controls.Add(this.btnEnviar3);
            this.tabPage3.Controls.Add(this.btnGenerarFitxer);
            this.tabPage3.Controls.Add(this.label7);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage3.Size = new System.Drawing.Size(580, 497);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "tabPage3";
            // 
            // btnDecodificar
            // 
            this.btnDecodificar.Location = new System.Drawing.Point(62, 129);
            this.btnDecodificar.Margin = new System.Windows.Forms.Padding(2);
            this.btnDecodificar.Name = "btnDecodificar";
            this.btnDecodificar.Size = new System.Drawing.Size(110, 42);
            this.btnDecodificar.TabIndex = 9;
            this.btnDecodificar.Text = "Decodificar i Calcular";
            this.btnDecodificar.UseVisualStyleBackColor = true;
            // 
            // btnEnviar3
            // 
            this.btnEnviar3.Location = new System.Drawing.Point(62, 181);
            this.btnEnviar3.Margin = new System.Windows.Forms.Padding(2);
            this.btnEnviar3.Name = "btnEnviar3";
            this.btnEnviar3.Size = new System.Drawing.Size(68, 32);
            this.btnEnviar3.TabIndex = 8;
            this.btnEnviar3.Text = "Enviar";
            this.btnEnviar3.UseVisualStyleBackColor = true;
            // 
            // btnGenerarFitxer
            // 
            this.btnGenerarFitxer.Location = new System.Drawing.Point(62, 75);
            this.btnGenerarFitxer.Margin = new System.Windows.Forms.Padding(2);
            this.btnGenerarFitxer.Name = "btnGenerarFitxer";
            this.btnGenerarFitxer.Size = new System.Drawing.Size(110, 51);
            this.btnGenerarFitxer.TabIndex = 7;
            this.btnGenerarFitxer.Text = "Guardar PACS.zip";
            this.btnGenerarFitxer.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(42, 20);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(325, 31);
            this.label7.TabIndex = 4;
            this.label7.Text = "ENVIANT MISSATGE VR";
            // 
            // btn1
            // 
            this.btn1.Location = new System.Drawing.Point(263, 199);
            this.btn1.Margin = new System.Windows.Forms.Padding(2);
            this.btn1.Name = "btn1";
            this.btn1.Size = new System.Drawing.Size(67, 30);
            this.btn1.TabIndex = 2;
            this.btn1.Text = "1";
            this.btn1.UseVisualStyleBackColor = true;
            this.btn1.Click += new System.EventHandler(this.btn1_Click);
            // 
            // btn3
            // 
            this.btn3.Location = new System.Drawing.Point(608, 199);
            this.btn3.Margin = new System.Windows.Forms.Padding(2);
            this.btn3.Name = "btn3";
            this.btn3.Size = new System.Drawing.Size(67, 30);
            this.btn3.TabIndex = 6;
            this.btn3.Text = "3";
            this.btn3.UseVisualStyleBackColor = true;
            this.btn3.Click += new System.EventHandler(this.btn3_Click);
            // 
            // btn2
            // 
            this.btn2.Location = new System.Drawing.Point(437, 199);
            this.btn2.Margin = new System.Windows.Forms.Padding(2);
            this.btn2.Name = "btn2";
            this.btn2.Size = new System.Drawing.Size(67, 30);
            this.btn2.TabIndex = 7;
            this.btn2.Text = "2";
            this.btn2.UseVisualStyleBackColor = true;
            this.btn2.Click += new System.EventHandler(this.btn2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(319, 206);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 17);
            this.label1.TabIndex = 10;
            this.label1.Text = "------------------------";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(489, 206);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 17);
            this.label2.TabIndex = 11;
            this.label2.Text = "------------------------";
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.SystemColors.ControlLight;
            this.pnlTop.Controls.Add(this.lblListening);
            this.pnlTop.Controls.Add(this.btnStopListening);
            this.pnlTop.Controls.Add(this.btnStartListening);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 58);
            this.pnlTop.Margin = new System.Windows.Forms.Padding(2);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1079, 137);
            this.pnlTop.TabIndex = 1;
            // 
            // lblListening
            // 
            this.lblListening.AutoSize = true;
            this.lblListening.Font = new System.Drawing.Font("Cambria", 16.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblListening.Location = new System.Drawing.Point(32, 21);
            this.lblListening.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblListening.Name = "lblListening";
            this.lblListening.Size = new System.Drawing.Size(241, 32);
            this.lblListening.TabIndex = 10;
            this.lblListening.Text = "Listen for messages";
            // 
            // btnStopListening
            // 
            this.btnStopListening.Font = new System.Drawing.Font("Cambria", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStopListening.Location = new System.Drawing.Point(149, 72);
            this.btnStopListening.Margin = new System.Windows.Forms.Padding(2);
            this.btnStopListening.Name = "btnStopListening";
            this.btnStopListening.Size = new System.Drawing.Size(77, 30);
            this.btnStopListening.TabIndex = 9;
            this.btnStopListening.Text = "Stop";
            this.btnStopListening.UseVisualStyleBackColor = true;
            this.btnStopListening.Click += new System.EventHandler(this.btnStopListening_Click);
            // 
            // btnStartListening
            // 
            this.btnStartListening.Font = new System.Drawing.Font("Cambria", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStartListening.Location = new System.Drawing.Point(53, 72);
            this.btnStartListening.Margin = new System.Windows.Forms.Padding(2);
            this.btnStartListening.Name = "btnStartListening";
            this.btnStartListening.Size = new System.Drawing.Size(77, 30);
            this.btnStartListening.TabIndex = 8;
            this.btnStartListening.Text = "Start";
            this.btnStartListening.UseVisualStyleBackColor = true;
            this.btnStartListening.Click += new System.EventHandler(this.btnStartListening_Click);
            // 
            // frmSpaceship
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(1079, 819);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn2);
            this.Controls.Add(this.btn3);
            this.Controls.Add(this.btn1);
            this.Controls.Add(this.pnlTop);
            this.Controls.Add(this.flpFrms);
            this.Controls.Add(this.pnlTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmSpaceship";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmSpaceship_Load);
            this.pnlTitle.ResumeLayout(false);
            this.pnlTitle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbClose)).EndInit();
            this.flpFrms.ResumeLayout(false);
            this.pnlListBox.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlTitle;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.FlowLayoutPanel flpFrms;
        private System.Windows.Forms.Button btn1;
        private System.Windows.Forms.Button btn3;
        private System.Windows.Forms.Button btn2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button btnDecodificar;
        private System.Windows.Forms.Button btnEnviar3;
        private System.Windows.Forms.Button btnGenerarFitxer;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnGenCred;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label lblDeliveryCode;
        private System.Windows.Forms.Label lblSpaceShipCode;
        private System.Windows.Forms.TextBox txtDeliveryCode;
        private System.Windows.Forms.TextBox txtCodeSpaceShip;
        private System.Windows.Forms.Button btnEnviar;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Label lblTitle1;
        protected System.Windows.Forms.PictureBox pbClose;
        private System.Windows.Forms.Label lblListening;
        private System.Windows.Forms.Button btnStopListening;
        private System.Windows.Forms.Button btnStartListening;
        private System.Windows.Forms.Panel pnlListBox;
        private System.Windows.Forms.ListBox lbxInfo;
        private System.Windows.Forms.TabControl tabControl;
    }
}

