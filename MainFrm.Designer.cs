namespace LP.MT
{
    partial class MainFrm
    {
        /// <summary>
        /// 必要なデザイナ変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナで生成されたコード

        /// <summary>
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainFrm));
            this.Cmenu = new System.Windows.Forms.MenuStrip();
            this.Cmenu_File = new System.Windows.Forms.ToolStripMenuItem();
            this.Cmenu_File_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.Cmenu_COM = new System.Windows.Forms.ToolStripMenuItem();
            this.Cmenu_COM_reScan = new System.Windows.Forms.ToolStripMenuItem();
            this.Cstatus = new System.Windows.Forms.StatusStrip();
            this.Cstatus_lblConnect = new System.Windows.Forms.ToolStripStatusLabel();
            this.Cstatus_lblCOM = new System.Windows.Forms.ToolStripStatusLabel();
            this.Ctool_Main = new System.Windows.Forms.ToolStrip();
            this.Ctool_cbxCOM = new System.Windows.Forms.ToolStripComboBox();
            this.Ctool_btnConnect = new System.Windows.Forms.ToolStripButton();
            this.Ctool_btnDisConnect = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.Ctool_btnPreMeasure = new System.Windows.Forms.ToolStripButton();
            this.Ctool_btnMeasure = new System.Windows.Forms.ToolStripButton();
            this.Ctool_btnMeasureStop = new System.Windows.Forms.ToolStripButton();
            this.Ctool_cbxMonitor = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.Ctool_cbxFreq = new System.Windows.Forms.ToolStripComboBox();
            this.SerialPort = new System.IO.Ports.SerialPort(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.Cbtn_Sensor_GetStatus = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.Cnud_dstID = new System.Windows.Forms.NumericUpDown();
            this.Ctree = new System.Windows.Forms.TreeView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.Cnud_Sensor_TimeSec = new System.Windows.Forms.NumericUpDown();
            this.Cnud_Sensor_TimeMin = new System.Windows.Forms.NumericUpDown();
            this.Cnud_Sensor_TimeHour = new System.Windows.Forms.NumericUpDown();
            this.Cbtn_SetDev_WiFreq = new System.Windows.Forms.Button();
            this.bCbtn_SetDev_MagGain = new System.Windows.Forms.Button();
            this.Cbtn_SetDev_MeasTime = new System.Windows.Forms.Button();
            this.Cbtn_SetDev_SampFreq = new System.Windows.Forms.Button();
            this.Cbtn_SetDev_ID = new System.Windows.Forms.Button();
            this.Cnud_Sensor_ID = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.Ccbx_Sensor_MagGain = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Ccbx_Sensor_SampFreq = new System.Windows.Forms.ComboBox();
            this.Ccbx_Sensor_WiFreq = new System.Windows.Forms.ComboBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.Cbtn_Sensor_FileData = new System.Windows.Forms.Button();
            this.Cbtn_Sensor_FileInfo = new System.Windows.Forms.Button();
            this.Clv_FileInfo = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.BtStartMeasurement = new System.Windows.Forms.Button();
            this.BtStopMeasurement = new System.Windows.Forms.Button();
            this.Cmenu.SuspendLayout();
            this.Cstatus.SuspendLayout();
            this.Ctool_Main.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Cnud_dstID)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Cnud_Sensor_TimeSec)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Cnud_Sensor_TimeMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Cnud_Sensor_TimeHour)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Cnud_Sensor_ID)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // Cmenu
            // 
            this.Cmenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Cmenu_File,
            this.Cmenu_COM});
            resources.ApplyResources(this.Cmenu, "Cmenu");
            this.Cmenu.Name = "Cmenu";
            // 
            // Cmenu_File
            // 
            this.Cmenu_File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Cmenu_File_Exit});
            this.Cmenu_File.Name = "Cmenu_File";
            resources.ApplyResources(this.Cmenu_File, "Cmenu_File");
            // 
            // Cmenu_File_Exit
            // 
            this.Cmenu_File_Exit.Name = "Cmenu_File_Exit";
            resources.ApplyResources(this.Cmenu_File_Exit, "Cmenu_File_Exit");
            this.Cmenu_File_Exit.Click += new System.EventHandler(this.Cmenu_File_Exit_Click);
            // 
            // Cmenu_COM
            // 
            this.Cmenu_COM.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Cmenu_COM_reScan});
            this.Cmenu_COM.Name = "Cmenu_COM";
            resources.ApplyResources(this.Cmenu_COM, "Cmenu_COM");
            // 
            // Cmenu_COM_reScan
            // 
            this.Cmenu_COM_reScan.Name = "Cmenu_COM_reScan";
            resources.ApplyResources(this.Cmenu_COM_reScan, "Cmenu_COM_reScan");
            this.Cmenu_COM_reScan.Click += new System.EventHandler(this.Cmenu_COM_reScan_Click);
            // 
            // Cstatus
            // 
            this.Cstatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Cstatus_lblConnect,
            this.Cstatus_lblCOM});
            resources.ApplyResources(this.Cstatus, "Cstatus");
            this.Cstatus.Name = "Cstatus";
            // 
            // Cstatus_lblConnect
            // 
            this.Cstatus_lblConnect.Image = global::LP.MT.Properties.Resources.transmit_blue;
            this.Cstatus_lblConnect.Name = "Cstatus_lblConnect";
            resources.ApplyResources(this.Cstatus_lblConnect, "Cstatus_lblConnect");
            // 
            // Cstatus_lblCOM
            // 
            this.Cstatus_lblCOM.Name = "Cstatus_lblCOM";
            resources.ApplyResources(this.Cstatus_lblCOM, "Cstatus_lblCOM");
            // 
            // Ctool_Main
            // 
            this.Ctool_Main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Ctool_cbxCOM,
            this.Ctool_btnConnect,
            this.Ctool_btnDisConnect,
            this.toolStripSeparator1,
            this.Ctool_btnPreMeasure,
            this.Ctool_btnMeasure,
            this.Ctool_btnMeasureStop,
            this.Ctool_cbxMonitor,
            this.toolStripSeparator2,
            this.Ctool_cbxFreq});
            resources.ApplyResources(this.Ctool_Main, "Ctool_Main");
            this.Ctool_Main.Name = "Ctool_Main";
            // 
            // Ctool_cbxCOM
            // 
            this.Ctool_cbxCOM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Ctool_cbxCOM.Name = "Ctool_cbxCOM";
            resources.ApplyResources(this.Ctool_cbxCOM, "Ctool_cbxCOM");
            // 
            // Ctool_btnConnect
            // 
            this.Ctool_btnConnect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.Ctool_btnConnect, "Ctool_btnConnect");
            this.Ctool_btnConnect.Name = "Ctool_btnConnect";
            this.Ctool_btnConnect.Click += new System.EventHandler(this.Ctool_btnConnect_Click);
            // 
            // Ctool_btnDisConnect
            // 
            this.Ctool_btnDisConnect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.Ctool_btnDisConnect, "Ctool_btnDisConnect");
            this.Ctool_btnDisConnect.Name = "Ctool_btnDisConnect";
            this.Ctool_btnDisConnect.Click += new System.EventHandler(this.Ctool_btnDisConnect_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // Ctool_btnPreMeasure
            // 
            this.Ctool_btnPreMeasure.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.Ctool_btnPreMeasure, "Ctool_btnPreMeasure");
            this.Ctool_btnPreMeasure.Name = "Ctool_btnPreMeasure";
            this.Ctool_btnPreMeasure.Click += new System.EventHandler(this.Ctool_btnPreMeasure_Click);
            // 
            // Ctool_btnMeasure
            // 
            this.Ctool_btnMeasure.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.Ctool_btnMeasure, "Ctool_btnMeasure");
            this.Ctool_btnMeasure.Name = "Ctool_btnMeasure";
            this.Ctool_btnMeasure.Click += new System.EventHandler(this.Ctool_btnMeasure_Click);
            // 
            // Ctool_btnMeasureStop
            // 
            this.Ctool_btnMeasureStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.Ctool_btnMeasureStop, "Ctool_btnMeasureStop");
            this.Ctool_btnMeasureStop.Name = "Ctool_btnMeasureStop";
            this.Ctool_btnMeasureStop.Click += new System.EventHandler(this.Ctool_btnMeasureStop_Click);
            // 
            // Ctool_cbxMonitor
            // 
            this.Ctool_cbxMonitor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Ctool_cbxMonitor.Items.AddRange(new object[] {
            resources.GetString("Ctool_cbxMonitor.Items"),
            resources.GetString("Ctool_cbxMonitor.Items1")});
            this.Ctool_cbxMonitor.Name = "Ctool_cbxMonitor";
            resources.ApplyResources(this.Ctool_cbxMonitor, "Ctool_cbxMonitor");
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // Ctool_cbxFreq
            // 
            this.Ctool_cbxFreq.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Ctool_cbxFreq.Items.AddRange(new object[] {
            resources.GetString("Ctool_cbxFreq.Items"),
            resources.GetString("Ctool_cbxFreq.Items1"),
            resources.GetString("Ctool_cbxFreq.Items2"),
            resources.GetString("Ctool_cbxFreq.Items3"),
            resources.GetString("Ctool_cbxFreq.Items4"),
            resources.GetString("Ctool_cbxFreq.Items5"),
            resources.GetString("Ctool_cbxFreq.Items6"),
            resources.GetString("Ctool_cbxFreq.Items7"),
            resources.GetString("Ctool_cbxFreq.Items8")});
            this.Ctool_cbxFreq.Name = "Ctool_cbxFreq";
            resources.ApplyResources(this.Ctool_cbxFreq, "Ctool_cbxFreq");
            // 
            // SerialPort
            // 
            this.SerialPort.BaudRate = 1000000;
            this.SerialPort.RtsEnable = true;
            this.SerialPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort_DataReceived);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.Cbtn_Sensor_GetStatus);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.Cnud_dstID);
            this.tabPage1.Controls.Add(this.Ctree);
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // Cbtn_Sensor_GetStatus
            // 
            resources.ApplyResources(this.Cbtn_Sensor_GetStatus, "Cbtn_Sensor_GetStatus");
            this.Cbtn_Sensor_GetStatus.Name = "Cbtn_Sensor_GetStatus";
            this.Cbtn_Sensor_GetStatus.UseVisualStyleBackColor = true;
            this.Cbtn_Sensor_GetStatus.Click += new System.EventHandler(this.Cbtn_Sensor_GetStatus_Click);
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // Cnud_dstID
            // 
            resources.ApplyResources(this.Cnud_dstID, "Cnud_dstID");
            this.Cnud_dstID.Maximum = new decimal(new int[] {
            254,
            0,
            0,
            0});
            this.Cnud_dstID.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Cnud_dstID.Name = "Cnud_dstID";
            this.Cnud_dstID.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // Ctree
            // 
            resources.ApplyResources(this.Ctree, "Ctree");
            this.Ctree.Name = "Ctree";
            this.Ctree.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            ((System.Windows.Forms.TreeNode)(resources.GetObject("Ctree.Nodes")))});
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.Cnud_Sensor_TimeSec);
            this.tabPage2.Controls.Add(this.Cnud_Sensor_TimeMin);
            this.tabPage2.Controls.Add(this.Cnud_Sensor_TimeHour);
            this.tabPage2.Controls.Add(this.Cbtn_SetDev_WiFreq);
            this.tabPage2.Controls.Add(this.bCbtn_SetDev_MagGain);
            this.tabPage2.Controls.Add(this.Cbtn_SetDev_MeasTime);
            this.tabPage2.Controls.Add(this.Cbtn_SetDev_SampFreq);
            this.tabPage2.Controls.Add(this.Cbtn_SetDev_ID);
            this.tabPage2.Controls.Add(this.Cnud_Sensor_ID);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.Ccbx_Sensor_MagGain);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.Ccbx_Sensor_SampFreq);
            this.tabPage2.Controls.Add(this.Ccbx_Sensor_WiFreq);
            resources.ApplyResources(this.tabPage2, "tabPage2");
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // Cnud_Sensor_TimeSec
            // 
            resources.ApplyResources(this.Cnud_Sensor_TimeSec, "Cnud_Sensor_TimeSec");
            this.Cnud_Sensor_TimeSec.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.Cnud_Sensor_TimeSec.Name = "Cnud_Sensor_TimeSec";
            // 
            // Cnud_Sensor_TimeMin
            // 
            resources.ApplyResources(this.Cnud_Sensor_TimeMin, "Cnud_Sensor_TimeMin");
            this.Cnud_Sensor_TimeMin.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.Cnud_Sensor_TimeMin.Name = "Cnud_Sensor_TimeMin";
            this.Cnud_Sensor_TimeMin.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // Cnud_Sensor_TimeHour
            // 
            resources.ApplyResources(this.Cnud_Sensor_TimeHour, "Cnud_Sensor_TimeHour");
            this.Cnud_Sensor_TimeHour.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.Cnud_Sensor_TimeHour.Name = "Cnud_Sensor_TimeHour";
            // 
            // Cbtn_SetDev_WiFreq
            // 
            resources.ApplyResources(this.Cbtn_SetDev_WiFreq, "Cbtn_SetDev_WiFreq");
            this.Cbtn_SetDev_WiFreq.Name = "Cbtn_SetDev_WiFreq";
            this.Cbtn_SetDev_WiFreq.UseVisualStyleBackColor = true;
            this.Cbtn_SetDev_WiFreq.Click += new System.EventHandler(this.Cbtn_SetDev_WiFreq_Click);
            // 
            // bCbtn_SetDev_MagGain
            // 
            resources.ApplyResources(this.bCbtn_SetDev_MagGain, "bCbtn_SetDev_MagGain");
            this.bCbtn_SetDev_MagGain.Name = "bCbtn_SetDev_MagGain";
            this.bCbtn_SetDev_MagGain.UseVisualStyleBackColor = true;
            this.bCbtn_SetDev_MagGain.Click += new System.EventHandler(this.bCbtn_SetDev_MagGain_Click);
            // 
            // Cbtn_SetDev_MeasTime
            // 
            resources.ApplyResources(this.Cbtn_SetDev_MeasTime, "Cbtn_SetDev_MeasTime");
            this.Cbtn_SetDev_MeasTime.Name = "Cbtn_SetDev_MeasTime";
            this.Cbtn_SetDev_MeasTime.UseVisualStyleBackColor = true;
            this.Cbtn_SetDev_MeasTime.Click += new System.EventHandler(this.Cbtn_SetDev_MeasTime_Click);
            // 
            // Cbtn_SetDev_SampFreq
            // 
            resources.ApplyResources(this.Cbtn_SetDev_SampFreq, "Cbtn_SetDev_SampFreq");
            this.Cbtn_SetDev_SampFreq.Name = "Cbtn_SetDev_SampFreq";
            this.Cbtn_SetDev_SampFreq.UseVisualStyleBackColor = true;
            this.Cbtn_SetDev_SampFreq.Click += new System.EventHandler(this.Cbtn_SetDev_SampFreq_Click);
            // 
            // Cbtn_SetDev_ID
            // 
            resources.ApplyResources(this.Cbtn_SetDev_ID, "Cbtn_SetDev_ID");
            this.Cbtn_SetDev_ID.Name = "Cbtn_SetDev_ID";
            this.Cbtn_SetDev_ID.UseVisualStyleBackColor = true;
            this.Cbtn_SetDev_ID.Click += new System.EventHandler(this.Cbtn_SetDev_ID_Click);
            // 
            // Cnud_Sensor_ID
            // 
            resources.ApplyResources(this.Cnud_Sensor_ID, "Cnud_Sensor_ID");
            this.Cnud_Sensor_ID.Maximum = new decimal(new int[] {
            254,
            0,
            0,
            0});
            this.Cnud_Sensor_ID.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Cnud_Sensor_ID.Name = "Cnud_Sensor_ID";
            this.Cnud_Sensor_ID.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // Ccbx_Sensor_MagGain
            // 
            this.Ccbx_Sensor_MagGain.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Ccbx_Sensor_MagGain.FormattingEnabled = true;
            this.Ccbx_Sensor_MagGain.Items.AddRange(new object[] {
            resources.GetString("Ccbx_Sensor_MagGain.Items"),
            resources.GetString("Ccbx_Sensor_MagGain.Items1"),
            resources.GetString("Ccbx_Sensor_MagGain.Items2"),
            resources.GetString("Ccbx_Sensor_MagGain.Items3"),
            resources.GetString("Ccbx_Sensor_MagGain.Items4"),
            resources.GetString("Ccbx_Sensor_MagGain.Items5"),
            resources.GetString("Ccbx_Sensor_MagGain.Items6"),
            resources.GetString("Ccbx_Sensor_MagGain.Items7")});
            resources.ApplyResources(this.Ccbx_Sensor_MagGain, "Ccbx_Sensor_MagGain");
            this.Ccbx_Sensor_MagGain.Name = "Ccbx_Sensor_MagGain";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // Ccbx_Sensor_SampFreq
            // 
            this.Ccbx_Sensor_SampFreq.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Ccbx_Sensor_SampFreq.FormattingEnabled = true;
            this.Ccbx_Sensor_SampFreq.Items.AddRange(new object[] {
            resources.GetString("Ccbx_Sensor_SampFreq.Items"),
            resources.GetString("Ccbx_Sensor_SampFreq.Items1"),
            resources.GetString("Ccbx_Sensor_SampFreq.Items2"),
            resources.GetString("Ccbx_Sensor_SampFreq.Items3"),
            resources.GetString("Ccbx_Sensor_SampFreq.Items4"),
            resources.GetString("Ccbx_Sensor_SampFreq.Items5"),
            resources.GetString("Ccbx_Sensor_SampFreq.Items6"),
            resources.GetString("Ccbx_Sensor_SampFreq.Items7"),
            resources.GetString("Ccbx_Sensor_SampFreq.Items8")});
            resources.ApplyResources(this.Ccbx_Sensor_SampFreq, "Ccbx_Sensor_SampFreq");
            this.Ccbx_Sensor_SampFreq.Name = "Ccbx_Sensor_SampFreq";
            // 
            // Ccbx_Sensor_WiFreq
            // 
            this.Ccbx_Sensor_WiFreq.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Ccbx_Sensor_WiFreq.FormattingEnabled = true;
            this.Ccbx_Sensor_WiFreq.Items.AddRange(new object[] {
            resources.GetString("Ccbx_Sensor_WiFreq.Items"),
            resources.GetString("Ccbx_Sensor_WiFreq.Items1"),
            resources.GetString("Ccbx_Sensor_WiFreq.Items2"),
            resources.GetString("Ccbx_Sensor_WiFreq.Items3"),
            resources.GetString("Ccbx_Sensor_WiFreq.Items4"),
            resources.GetString("Ccbx_Sensor_WiFreq.Items5"),
            resources.GetString("Ccbx_Sensor_WiFreq.Items6"),
            resources.GetString("Ccbx_Sensor_WiFreq.Items7"),
            resources.GetString("Ccbx_Sensor_WiFreq.Items8"),
            resources.GetString("Ccbx_Sensor_WiFreq.Items9"),
            resources.GetString("Ccbx_Sensor_WiFreq.Items10"),
            resources.GetString("Ccbx_Sensor_WiFreq.Items11"),
            resources.GetString("Ccbx_Sensor_WiFreq.Items12"),
            resources.GetString("Ccbx_Sensor_WiFreq.Items13"),
            resources.GetString("Ccbx_Sensor_WiFreq.Items14"),
            resources.GetString("Ccbx_Sensor_WiFreq.Items15")});
            resources.ApplyResources(this.Ccbx_Sensor_WiFreq, "Ccbx_Sensor_WiFreq");
            this.Ccbx_Sensor_WiFreq.Name = "Ccbx_Sensor_WiFreq";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.Cbtn_Sensor_FileData);
            this.tabPage3.Controls.Add(this.Cbtn_Sensor_FileInfo);
            this.tabPage3.Controls.Add(this.Clv_FileInfo);
            resources.ApplyResources(this.tabPage3, "tabPage3");
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // Cbtn_Sensor_FileData
            // 
            resources.ApplyResources(this.Cbtn_Sensor_FileData, "Cbtn_Sensor_FileData");
            this.Cbtn_Sensor_FileData.Name = "Cbtn_Sensor_FileData";
            this.Cbtn_Sensor_FileData.UseVisualStyleBackColor = true;
            this.Cbtn_Sensor_FileData.Click += new System.EventHandler(this.Cbtn_Sensor_FileData_Click);
            // 
            // Cbtn_Sensor_FileInfo
            // 
            resources.ApplyResources(this.Cbtn_Sensor_FileInfo, "Cbtn_Sensor_FileInfo");
            this.Cbtn_Sensor_FileInfo.Name = "Cbtn_Sensor_FileInfo";
            this.Cbtn_Sensor_FileInfo.UseVisualStyleBackColor = true;
            this.Cbtn_Sensor_FileInfo.Click += new System.EventHandler(this.Cbtn_Sensor_FileInfo_Click);
            // 
            // Clv_FileInfo
            // 
            this.Clv_FileInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Clv_FileInfo.CheckBoxes = true;
            this.Clv_FileInfo.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.Clv_FileInfo.FullRowSelect = true;
            this.Clv_FileInfo.GridLines = true;
            this.Clv_FileInfo.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.Clv_FileInfo.HideSelection = false;
            resources.ApplyResources(this.Clv_FileInfo, "Clv_FileInfo");
            this.Clv_FileInfo.Name = "Clv_FileInfo";
            this.Clv_FileInfo.UseCompatibleStateImageBehavior = false;
            this.Clv_FileInfo.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            resources.ApplyResources(this.columnHeader1, "columnHeader1");
            // 
            // columnHeader2
            // 
            resources.ApplyResources(this.columnHeader2, "columnHeader2");
            // 
            // columnHeader3
            // 
            resources.ApplyResources(this.columnHeader3, "columnHeader3");
            // 
            // columnHeader4
            // 
            resources.ApplyResources(this.columnHeader4, "columnHeader4");
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // button2
            // 
            resources.ApplyResources(this.button2, "button2");
            this.button2.Name = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // BtStartMeasurement
            // 
            resources.ApplyResources(this.BtStartMeasurement, "BtStartMeasurement");
            this.BtStartMeasurement.Name = "BtStartMeasurement";
            this.BtStartMeasurement.UseVisualStyleBackColor = true;
            this.BtStartMeasurement.Click += new System.EventHandler(this.BtStartMeasurement_Click);
            // 
            // BtStopMeasurement
            // 
            resources.ApplyResources(this.BtStopMeasurement, "BtStopMeasurement");
            this.BtStopMeasurement.Name = "BtStopMeasurement";
            this.BtStopMeasurement.UseVisualStyleBackColor = true;
            this.BtStopMeasurement.Click += new System.EventHandler(this.BtStopMeasurement_Click);
            // 
            // MainFrm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.BtStopMeasurement);
            this.Controls.Add(this.BtStartMeasurement);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.Ctool_Main);
            this.Controls.Add(this.Cstatus);
            this.Controls.Add(this.Cmenu);
            this.MainMenuStrip = this.Cmenu;
            this.Name = "MainFrm";
            this.Load += new System.EventHandler(this.MainFrm_Load);
            this.Cmenu.ResumeLayout(false);
            this.Cmenu.PerformLayout();
            this.Cstatus.ResumeLayout(false);
            this.Cstatus.PerformLayout();
            this.Ctool_Main.ResumeLayout(false);
            this.Ctool_Main.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Cnud_dstID)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Cnud_Sensor_TimeSec)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Cnud_Sensor_TimeMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Cnud_Sensor_TimeHour)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Cnud_Sensor_ID)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip Cmenu;
        private System.Windows.Forms.ToolStripMenuItem Cmenu_File;
        private System.Windows.Forms.StatusStrip Cstatus;
        private System.Windows.Forms.ToolStrip Ctool_Main;
        private System.IO.Ports.SerialPort SerialPort;
        private System.Windows.Forms.ToolStripComboBox Ctool_cbxCOM;
        private System.Windows.Forms.ToolStripButton Ctool_btnConnect;
        private System.Windows.Forms.ToolStripButton Ctool_btnDisConnect;
        private System.Windows.Forms.ToolStripMenuItem Cmenu_COM;
        private System.Windows.Forms.ToolStripStatusLabel Cstatus_lblConnect;
        private System.Windows.Forms.ToolStripStatusLabel Cstatus_lblCOM;
        private System.Windows.Forms.ToolStripMenuItem Cmenu_File_Exit;
        private System.Windows.Forms.ToolStripMenuItem Cmenu_COM_reScan;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton Ctool_btnMeasure;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TreeView Ctree;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ToolStripButton Ctool_btnPreMeasure;
        private System.Windows.Forms.ToolStripButton Ctool_btnMeasureStop;
        private System.Windows.Forms.ToolStripComboBox Ctool_cbxFreq;
        private System.Windows.Forms.ToolStripComboBox Ctool_cbxMonitor;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox Ccbx_Sensor_SampFreq;
        private System.Windows.Forms.ComboBox Ccbx_Sensor_WiFreq;
        private System.Windows.Forms.Button Cbtn_SetDev_ID;
        private System.Windows.Forms.NumericUpDown Cnud_Sensor_ID;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox Ccbx_Sensor_MagGain;
        private System.Windows.Forms.Button Cbtn_SetDev_WiFreq;
        private System.Windows.Forms.Button bCbtn_SetDev_MagGain;
        private System.Windows.Forms.Button Cbtn_SetDev_MeasTime;
        private System.Windows.Forms.Button Cbtn_SetDev_SampFreq;
        private System.Windows.Forms.NumericUpDown Cnud_Sensor_TimeSec;
        private System.Windows.Forms.NumericUpDown Cnud_Sensor_TimeMin;
        private System.Windows.Forms.NumericUpDown Cnud_Sensor_TimeHour;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ListView Clv_FileInfo;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Button Cbtn_Sensor_GetStatus;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown Cnud_dstID;
        private System.Windows.Forms.Button Cbtn_Sensor_FileData;
        private System.Windows.Forms.Button Cbtn_Sensor_FileInfo;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button BtStartMeasurement;
        private System.Windows.Forms.Button BtStopMeasurement;
    }
}

