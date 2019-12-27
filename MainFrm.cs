using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//
// 追加する名前空間
using System.IO;
using System.IO.Ports;
using System.Threading;
using System.Drawing.Imaging;
using System.Diagnostics;
using LP.MT.Core;

namespace LP.MT
{
    public partial class MainFrm : Form
    {
        // 内部変数
        Command cmd = new Command();

        // 通信用バッファ
        List<byte[]> packetBufferList = new List<byte[]>();
        int _i_PacketBufferSize = 0;

        // 測定時間
        DateTime _dt_meas = new DateTime();
        String _str_TimeOfStartMeas = string.Empty;
        String _str_TimeOfStarted = string.Empty;

        // ファイル保存関連
        List<int> _list_fileIndex = new List<int>();
        int _i_fileSaveListCnt = 0;

        // グラフ関連
        bool _b_ChartUpdate = true;

        int count = 0;

        //計測データ出力用
        string outputData= null;

        //
        public MainFrm()
        {
            InitializeComponent();
        }

        #region メインフォームの起動と終了に関する動作
        /// <summary>
        /// メインフォーム起動時に発生するイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainFrm_Load(object sender, EventArgs e)
        {    
            // シリアルポートを列挙
            scanSerialPorts();

            //
            //this.Ctree.ExpandAll();

            // コンボボックス系の初期設定
            this.Ctool_cbxMonitor.SelectedIndex = 0;
            this.Ccbx_Sensor_SampFreq.SelectedIndex = 6;
            this.Ccbx_Sensor_MagGain.SelectedIndex = 1;
            this.Ccbx_Sensor_WiFreq.SelectedIndex = 0;
        }

        /// <summary>
        /// メインフォームが閉じられる際に発生するイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (SerialPort.IsOpen)
                {
                    SerialPort.Close();
                }
            }
            catch (IOException ioe)
            { MessageBox.Show(ioe.ToString(), "通信ポートにエラーが発生しています", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            catch (InvalidOperationException ioe)
            { MessageBox.Show(ioe.ToString(), "通信ポートにエラーが発生しています", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            finally
            {
            }
        }
        #endregion

        #region シリアルポートで得られたデータのイベント処理
        /// <summary>
        /// シリアルポートから得られたデータを元に、フォームをアップデートするデリゲート
        /// </summary>
        /// <param name="receivedData"></param>
        private delegate void UpdateFormDelegate(PacketParser receivedPacket);
        private void serialPort_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            Console.WriteLine("SerialPort data received");
            // シリアルポートに到達したバイト列を取得
            int _i_BytesToRead = this.SerialPort.BytesToRead; // シリアルポートに到達したバイト数
            if (_i_BytesToRead == 0) { return; }

            byte[] _ba_BytesToRead = new byte[_i_BytesToRead]; // バイト配列を作成
            this.SerialPort.Read(_ba_BytesToRead, 0, _i_BytesToRead);

            // 必要であればバッファを結合する。
            int _i_trueBytesToRead = _i_BytesToRead + _i_PacketBufferSize;
            byte[] _ba_trueBytesToRead = new byte[_i_trueBytesToRead];
            if (_i_PacketBufferSize > 0)
            {
                packetBufferList[0].CopyTo(_ba_trueBytesToRead, 0);
                _ba_BytesToRead.CopyTo(_ba_trueBytesToRead, _i_PacketBufferSize);
                packetBufferList.Clear();
                _i_PacketBufferSize = 0;
            }
            else
            {
                _ba_BytesToRead.CopyTo(_ba_trueBytesToRead, 0);
            }

            PacketParser tmpParsedPacket = ParseReceivedSerialData(_ba_trueBytesToRead);
            if (tmpParsedPacket.packetList.Count > 0)
            {
                BeginInvoke(new UpdateFormDelegate(UpdateFormChanged), tmpParsedPacket);
            }
            else
            { return; }
        }

        /// <summary>
        /// シリアルポートへ到達したデータパーサ
        /// 
        /// データは分割して到達したり、幾つかのパケットが結合された状態で到達する。
        /// これらを分割/結合し、正規のパケットリストとして次の処理へ進ませる。
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public PacketParser ParseReceivedSerialData(byte[] data)
        {
            PacketParser pp = new PacketParser();
            List<byte[]> returnBytes = new List<byte[]>();
            int _i_pointer = 0;
            int _i_startByte = 0;
            int _i_endByte = 0;
            int _i_tmp = 0;
            byte BytesHeader = 0x55;
            byte BytesFooter = 0xAA;
            bool _b_flag = true;

            while (_b_flag)
            {
                _i_tmp = Array.IndexOf(data, BytesHeader, _i_pointer);
                if (_i_tmp < 0)
                { 
                    _b_flag = false;
                    packetBufferList.Add(data);
                    _i_PacketBufferSize = data.Length;
                    break; 
                } // 0x55が見つからなければループを抜ける。
                else
                {
                    _i_pointer = _i_tmp + 1; // 0x55がある場合は、ポインターを一つ進める。
                    if (_i_pointer > data.Length)
                    { 
                        _b_flag = false;
                        packetBufferList.Add(data);
                        _i_PacketBufferSize = data.Length;
                        break;
                    }
                }

                _i_tmp = Array.IndexOf(data, BytesHeader, _i_pointer);
                if (_i_tmp < 0)
                { 
                    _b_flag = false;
                    packetBufferList.Add(data);
                    _i_PacketBufferSize = data.Length;
                    break;
                } // 0x55が見つからなければループを抜ける。
                else
                {
                    _i_pointer = _i_tmp + 1; // ある場合は、ポインターを一つ進める。
                    if (_i_pointer >= data.Length)
                    {
                        _b_flag = false;
                        packetBufferList.Add(data);
                        _i_PacketBufferSize = data.Length;
                        break;
                    }  
                }

                int _i_tmpSize = Convert.ToInt32(data[_i_pointer]);
                _i_pointer += _i_tmpSize;

                if (_i_pointer >= data.Length)
                {
                    _b_flag = false;
                    packetBufferList.Add(data);
                    _i_PacketBufferSize = data.Length;
                    break;
                }
                else
                {
                    if (data[_i_pointer] == BytesFooter)
                    {
                        _i_endByte = _i_pointer;
                        byte[] _b_copy = new byte[(_i_endByte - _i_startByte + 1)];
                        Array.Copy(data, _i_startByte, _b_copy, 0, (_i_endByte - _i_startByte + 1));
                        pp.packetList.Add(_b_copy);
                    }
                }

                _i_pointer++;
                if (_i_pointer >= data.Length)
                { 
                    _b_flag = false;
                    break; 
                }
                else
                {
                    _i_startByte = _i_pointer;
                }
            }

            return pp;
        }
        #endregion

        #region シリアルポート・イベント処理時のパケット振り分け
        /// <summary>
        /// 処理内容の振り分け
        /// </summary>
        /// <param name="receivedPacket"></param>
        private void UpdateFormChanged(PacketParser receivedPacket)
        {
            for (int i = 0; i < receivedPacket.packetList.Count; i++)
            {
                //応答コードによって動作
                switch (receivedPacket.packetList[i][5])
                {
                    case 0x09:
                        //追加
                        //計測開始
                        ReceivedPreMesurementAck(receivedPacket.packetList[i]);
                        break;

                    case 0x83:
                        // 計測データに関する処理をここへ
                        //計測開始後のデータパケット
                        ReceivedMesurementStartPacket(receivedPacket.packetList[i]);
                        break;
                    case 0x82:
                        ReceivedMeasurementStartAck();
                        break;



                    case 0x84:
                        //計測停止（ACKパケットのみ）
                        ReceivedMesurementStopPacket();
                        break;
                    case 0x85:
                        //各センサモジュールに設定されている情報及び状態を取得（ACKパケット）
                        ReceivedStatusAck(receivedPacket.packetList[i]);
                        break;
                    case 0x86:
                        //各センサモジュールに設定されている情報及び状態を取得（データパケット）
                        ReceivedStatusPacket(receivedPacket.packetList[i]);
                        break;
                    case 0x87:
                        //ワイヤレスセンサモジュールに搭載されたメモリに記録されているファイル情報を取得　（ACKパケット）
                        ReceivedFileInfoAck(receivedPacket.packetList[i]);
                        break;
                    case 0x88:
                        //ワイヤレスセンサモジュールに搭載されたメモリに記録されているファイル情報を取得　（データパケット）
                        ReceivedFileInfoPacket(receivedPacket.packetList[i]);
                        break;
                    case 0x89:
                        //メモリ内のファイルダウンロード　および最新ファイルデータ取得（ACKパケット）
                        ReceivedFileDataAck(receivedPacket.packetList[i]);
                        break;
                    case 0x8A:
                        //メモリ内のファイルダウンロード　および最新ファイルデータ取得（データパケット）
                        ReceivedFileDataPacket(receivedPacket.packetList[i]);
                        break;
                }
            }
        }
        #endregion

        #region シリアルポート・イベント処理時の詳細処理
        /// <summary>
        /// 
        /// </summary>
        ///
        private void ReceivedPreMesurementAck(byte[] receriveData)
        {
            Console.WriteLine("ACK Status: " + receriveData[6].ToString("X2"));
            TxState.Text = "準備完了";
        }
        private void ReceivedMesurementStartPacket(byte[] receriveData)
        {
            //自己開発
            //リアルタイムで表示する必要はないのでここでは表示しない
            //計測中であることだけ提示
            TxState.Text = "計測中";

            //計測データのcsv作成
            outputData = "magX , magY , magZ , accX , accY , accZ , angX , angY , angZ \r\n";
            //受信データパケット
            //1つのデータパケットに5回分の計測データ、1回の計測データ当たり18バイト（9チャンネル）
            for (int counter = 0; counter < 73; counter+= 18)
            {
                string magX = receriveData[9 + counter].ToString() + receriveData[10 + counter].ToString();     //地磁気X
                string magY = receriveData[11 + counter].ToString() + receriveData[12 + counter].ToString();    //地磁気Y
                string magZ = receriveData[13 + counter].ToString() + receriveData[14] + counter.ToString();    //地磁気Z

                string accY = receriveData[15 + counter].ToString() + receriveData[16 + counter].ToString();    //加速度Y　ここだけ順序違うので注意 3G/300dpsのみの仕様らしい
                string accX = receriveData[17 + counter].ToString() + receriveData[18 + counter].ToString();    //加速度X
                string accZ = receriveData[19 + counter].ToString() + receriveData[20 + counter].ToString();    //加速度Z

                string angX = receriveData[21 + counter].ToString() + receriveData[22 + counter].ToString();    //加速度Y　ここだけ順序違うので注意 3G/300dpsのみの仕様らしい
                string angY = receriveData[23 + counter].ToString() + receriveData[24 + counter].ToString();    //加速度X
                string angZ = receriveData[25 + counter].ToString() + receriveData[26 + counter].ToString();    //加速度Z

                outputData += magX + "," + magY + "," + magZ + "," + accX + "," + accY + "," + accZ + "," + angX + "," + angY + "," + angZ + "\r\n";
                Console.WriteLine("data mag:" + magX + " " + magY + " " + magZ);
                Console.WriteLine("data acc:" + accX + " " + accY + " " + accZ);
                Console.WriteLine("data ang:" + angX + " " + angY + " " + angZ);
            }

        }

        private void ReceivedMeasurementStartAck()
        {
            Console.WriteLine("Start ACK.");
        }

        private void ReceivedMesurementStopPacket()
        {
            //自己開発
            //計測を停止したことだけ提示
            TxState.Text = "計測停止";

            if(outputData != null)
            {
                File.WriteAllText("output.csv", outputData);
                Console.WriteLine("output measurement file.");
                outputData = null;
            }

        }

        private void ReceivedStatusPacket(byte[] receivedData)
        {
            this.Cnud_Sensor_ID.Value = Convert.ToDecimal(receivedData[3]);

            // 測定時間
            this.Cnud_Sensor_TimeHour.Value = Convert.ToDecimal(receivedData[7]);
            this.Cnud_Sensor_TimeMin.Value = Convert.ToDecimal(receivedData[8]);
            this.Cnud_Sensor_TimeSec.Value = Convert.ToDecimal(receivedData[9]);

            // 測定周波数
            byte[] _by_tmp_sampFreq = new byte[2];
            _by_tmp_sampFreq[0] = receivedData[11];
            _by_tmp_sampFreq[1] = receivedData[10];
            switch (BitConverter.ToUInt16(_by_tmp_sampFreq, 0))
            {
                case 1:
                    this.Ccbx_Sensor_SampFreq.SelectedIndex = 0;
                    break;
                case 5:
                    this.Ccbx_Sensor_SampFreq.SelectedIndex = 1;
                    break;
                case 10:
                    this.Ccbx_Sensor_SampFreq.SelectedIndex = 2;
                    break;
                case 20:
                    this.Ccbx_Sensor_SampFreq.SelectedIndex = 3;
                    break;
                case 50:
                    this.Ccbx_Sensor_SampFreq.SelectedIndex = 4;
                    break;
                case 100:
                    this.Ccbx_Sensor_SampFreq.SelectedIndex = 5;
                    break;
                case 200:
                    this.Ccbx_Sensor_SampFreq.SelectedIndex = 6;
                    break;
                case 500:
                    this.Ccbx_Sensor_SampFreq.SelectedIndex = 7;
                    break;
                case 1000:
                    this.Ccbx_Sensor_SampFreq.SelectedIndex = 8;
                    break;
            }

            // 地磁気センサゲイン
            //this.Ccbx_Sensor_MagGain.SelectedIndex = Convert.ToInt32(receivedData[11]);
           
            // 無線出力

            // 無線チャンネル
            this.Ccbx_Sensor_WiFreq.SelectedIndex = Convert.ToInt32(receivedData[12]) - 11;
        }
        private void ReceivedStatusAck(byte[] receivedData)
        {
            Console.WriteLine("ステータス情報取得コマンドを受信しました。");
        }

        private void ReceivedFileInfoPacket(byte[] receivedData)
        {

            // File No.
            byte _by_tmp_fileNum = receivedData[6];
            ushort _i_fileNum = Convert.ToUInt16(_by_tmp_fileNum);

            //サンプリング数
            byte[] _by_tmp_dataNum = new byte[4];
            //計測時サンプリング周波数
            byte[] _by_tmp_samplingFreq = new byte[2];
            //計測開始時刻
            string _str_tmpDateTime = string.Empty;

            // 
            try
            {
                // IDを格納
                ListViewItem _LV_tmpItem = new ListViewItem((_i_fileNum).ToString());
                // _LV_tmpItem.SubItems.Clear();
                // _LV_tmpItem.SubItems.Add((_i_sequenceNum * 5 + i).ToString());

                // サンプル数を格納
                _by_tmp_dataNum[0] = receivedData[18];
                _by_tmp_dataNum[1] = receivedData[17];
                _by_tmp_dataNum[2] = receivedData[16];
                _by_tmp_dataNum[3] = receivedData[15];
  
                _LV_tmpItem.SubItems.Add(BitConverter.ToUInt32(_by_tmp_dataNum, 0).ToString());

                _str_tmpDateTime += receivedData[10].ToString("D2") + "/";
                _str_tmpDateTime += receivedData[11].ToString("D2") + "/";
                _str_tmpDateTime += receivedData[12].ToString("D2") + " ";
                _str_tmpDateTime += receivedData[13].ToString("D2") + ":";
                _str_tmpDateTime += receivedData[14].ToString("D2");
                _LV_tmpItem.SubItems.Add(_str_tmpDateTime);
                Console.WriteLine("tmpDateTime :" + _str_tmpDateTime);
                _str_tmpDateTime = "";

                // 
                _by_tmp_samplingFreq[0] = receivedData[9];
                _by_tmp_samplingFreq[1] = receivedData[8];
                _LV_tmpItem.SubItems.Add(BitConverter.ToUInt16(_by_tmp_samplingFreq, 0).ToString());

                //
                this.Clv_FileInfo.Items.Add(_LV_tmpItem);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            
        }
        private void ReceivedFileInfoAck(byte[] receivedData)
        {
            Console.WriteLine("ファイル情報読出コマンドを受信しました。");
        }

        private void ReceivedFileDataPacket(byte[] receivedData)
        {
            Console.WriteLine("FileData Received...");



        }
        private void ReceivedFileDataAck(byte[] receivedData)
        {
            switch (receivedData[6])
            {
                case 0x21:
                    if (cmd.State != "GetFileData")
                    {
                        makeDownloadFile("tmp.csv");
                    }
                    else
                    {

                        Console.WriteLine("ファイルデータ読出コマンドを受信しました - コマンドを完了しました。");
                    }
                    break;
                case 0x60:
                    Console.WriteLine("ファイルデータ読出コマンドを受信しました - ファイルが存在しないようです。");
                    break;
            }
        }
        #endregion

        #region 計測値の算出
        /// <summary>
        /// 重力加速度の算出
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        private double CalcAccValue(byte[] values)
        {
            return (((double)(BitConverter.ToUInt16(values, 0))) / 4095 * 3.3 - 1.75) / 0.026;
        }

        /// <summary>
        /// 角速度の算出
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        private double CalcGyroValue(byte[] values)
        {
            // return (((double)(BitConverter.ToUInt16(values, 0))) / 4095 * 3.3 - 1.35) / 0.0004;
            return (((((double)(BitConverter.ToUInt16(values, 0))) / 4095 * 3.3 - 1.35) / 0.0004) / 180) * Math.PI;
        }

        /// <summary>
        /// 地磁気の算出
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        private double CalcMagnetValue(byte[] values)
        {
            return (((double)(BitConverter.ToUInt16(values, 0))) / 1300);
        }
        #endregion

        #region GUI - 各種制御コマンド発行ボタン
        private void Cbtn_Sensor_GetStatus_Click(object sender, EventArgs e)
        {
            SendStatusCmd(Convert.ToInt32(this.Cnud_dstID.Value));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ctool_btnPreMeasure_Click(object sender, EventArgs e)
        {
            //
            UpdateMeasurementButton(true);
        }

        /// <summary>
        /// 測定開始ボタン　クリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ctool_btnMeasure_Click(object sender, EventArgs e)
        {
            // 測定時間の記録
            DateTime _dt = DateTime.Now;
            _str_TimeOfStartMeas = _dt.ToString("yyMMddHHmmssfff");
            Console.WriteLine("MeasureStart = " + _str_TimeOfStartMeas);

            // 計測開始ボタン
            // 記録ありMode
            if (SendMesurementCmd(Convert.ToInt32(this.Cnud_dstID.Value), 1))
            //if (SendMesurementCmd(Convert.ToInt32(this.Cnud_dstID.Value), Convert.ToInt32(this.Ctool_cbxMonitor.SelectedIndex)))
            {
                Console.WriteLine("MeasureStart CMD sent.");
            }
            else
            {
                Ctool_btnMeasureStop_Click(sender, e);
                return;
            }

            // 測定時間の記録
            // _dt = DateTime.Now;
            // _str_TimeOfStarted = _dt.ToString("yyMMddHHmmssfff");
            // Console.WriteLine("MeasureStarted = " + _str_TimeOfStarted);
            // _dt_Started = DateTime.Now;
            // Console.WriteLine("MeasureStarted = " + _dt_Started.ToString("yyMMddHHmmssfff"));
        }
        private void Ctool_btnMeasureStop_Click(object sender, EventArgs e)
        {
            // 停止ボタンを送信
            SendMesurementStopCmd(Convert.ToInt32(this.Cnud_dstID.Value));
            Console.WriteLine("Measurement Stop.");
        }

        private void Cbtn_SetDev_ID_Click(object sender, EventArgs e)
        {

        }

        private void Cbtn_SetDev_SampFreq_Click(object sender, EventArgs e)
        {
            SendSetSamplingFreqCmd(Convert.ToInt32(this.Cnud_dstID.Value), Convert.ToInt32(this.Ccbx_Sensor_SampFreq.SelectedItem.ToString()));
        }

        private void Cbtn_SetDev_MeasTime_Click(object sender, EventArgs e)
        {
            SendSetMeasTimeCmd(Convert.ToInt32(this.Cnud_dstID.Value), Convert.ToInt32(this.Cnud_Sensor_TimeHour.Value), Convert.ToInt32(this.Cnud_Sensor_TimeMin.Value), Convert.ToInt32(this.Cnud_Sensor_TimeSec.Value));
        }

        private void bCbtn_SetDev_MagGain_Click(object sender, EventArgs e)
        {
            SendSetMagGainCmd(Convert.ToInt32(this.Cnud_dstID.Value), this.Ccbx_Sensor_MagGain.SelectedIndex);
        }

        private void Cbtn_SetDev_WiFreq_Click(object sender, EventArgs e)
        {
            SendSetWirelessFreqCmd(Convert.ToInt32(this.Cnud_dstID.Value), this.Ccbx_Sensor_WiFreq.SelectedIndex);
        }

        /// <summary>
        /// ファイル読み出し
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cbtn_Sensor_FileInfo_Click(object sender, EventArgs e)
        {
            // リストビューを初期化
            this.Clv_FileInfo.BeginUpdate();
            this.Clv_FileInfo.Items.Clear();
            this.Clv_FileInfo.EndUpdate();

            // コマンド送出
            SendReadFileInfoCmd(Convert.ToInt32(this.Cnud_dstID.Value));
        }

        private void Cbtn_Sensor_FileData_Click(object sender, EventArgs e)
        {
            if (this.Clv_FileInfo.CheckedIndices.Count > 0)
            {
                for(int i=0; i<this.Clv_FileInfo.SelectedIndices.Count; i++)
                {
                    _list_fileIndex.Add(this.Clv_FileInfo.CheckedIndices[i]);
                }

                _i_fileSaveListCnt = this.Clv_FileInfo.SelectedIndices.Count;
                SendGetFileDataCmd(Convert.ToInt32(this.Cnud_dstID.Value), this.Clv_FileInfo.CheckedIndices[0], 0, (Convert.ToInt32(this.Clv_FileInfo.CheckedItems[0].SubItems[1].Text, 10)));
            }
            else
            {
                MessageBox.Show("ファイルが選択されていないようです。");
            }
        }
        #endregion

        #region モーションセンサー制御コマンド発行
        private bool SendMesurementCmd(int id, int mode)
        {
            bool result = false;

            if (this.SerialPort.IsOpen)
            {
                byte[] _b_startMeasure = cmd.cmdStartMeasurement(id, mode);
                this.SerialPort.Write(_b_startMeasure, 0, _b_startMeasure.Length);

                // 停止ボタンを有効化
                Ctool_btnPreMeasure.Enabled = false;
                Ctool_btnMeasure.Enabled = false;
                Ctool_btnMeasureStop.Enabled = true;

                result = true;
            }
            else
            { 
                MessageBox.Show("シリアルポートが閉じています。");
                result = false;
            }

            return result;
        }

        private void SendMesurementStopCmd(int id)
        {
            if (this.SerialPort.IsOpen)
            {
                byte[] _b_stopMeasure = cmd.cmdStopMeasurement(id);
                this.SerialPort.Write(_b_stopMeasure, 0, _b_stopMeasure.Length);
                cmd.State = "Measurement";

                // 計測準備ボタンを有効化
                Ctool_btnPreMeasure.Enabled = true;
                Ctool_btnMeasure.Enabled = false;
                Ctool_btnMeasureStop.Enabled = false;
            }
            else
            { MessageBox.Show("シリアルポートが閉じています。"); }
        }

        private void SendStatusCmd(int id)
        {
            // Status読出コマンドを送信
            if (this.SerialPort.IsOpen)
            {
                byte[] _b_status = cmd.cmdStatus(id);
                this.SerialPort.Write(_b_status, 0, _b_status.Length);
                cmd.State = "Status";
            }
            else
            { MessageBox.Show("シリアルポートが閉じています。"); }
        }

        private void SendReadFileInfoCmd(int id)
        {
            // ファイル情報読出コマンドを送信
            if (this.SerialPort.IsOpen)
            {
                byte[] _b_readFileInfo = cmd.cmdReadFileInfo(id);
                this.SerialPort.Write(_b_readFileInfo, 0, _b_readFileInfo.Length);
                cmd.State = "ReadFileInfo";
            }
            else
            { MessageBox.Show("シリアルポートが閉じています。"); }
        }

        private void SendGetFileDataCmd(int id, int index, int startPos, int length)
        {
            // ファイル情報読出コマンドを送信
            if (this.SerialPort.IsOpen)
            {
                byte[] _b_getFileData = cmd.cmdGetFileData(id, index, startPos, length);
                this.SerialPort.Write(_b_getFileData, 0, _b_getFileData.Length);
                cmd.State = "GetFileData";
            }
            else
            { MessageBox.Show("シリアルポートが閉じています。"); }
        }

        private void SendSetSamplingFreqCmd(int id, int freq)
        {
            // Status読出コマンドを送信
            if (this.SerialPort.IsOpen)
            {
                byte[] _b_setSamplingFreq = cmd.cmdSetSamplingFreq(id, freq);
                this.SerialPort.Write(_b_setSamplingFreq, 0, _b_setSamplingFreq.Length);
                cmd.State = "SetSamplingFreq";
            }
            else
            { MessageBox.Show("シリアルポートが閉じています。"); }
        }

        private void SendSetMeasTimeCmd(int id, int hour, int min, int sec)
        {
            // Status読出コマンドを送信
            if (this.SerialPort.IsOpen)
            {
                byte[] _b_setMeasTime = cmd.cmdSetMeasurementTime(id, hour, min, sec);
                this.SerialPort.Write(_b_setMeasTime, 0, _b_setMeasTime.Length);
                cmd.State = "SetMeasurementTime";
            }
            else
            { MessageBox.Show("シリアルポートが閉じています。"); }
        }

        private void SendSetMagGainCmd(int id, int gain)
        {
            // Status読出コマンドを送信
            if (this.SerialPort.IsOpen)
            {
                byte[] _b_setMagGain = cmd.cmdSetMagGain(id, gain);
                this.SerialPort.Write(_b_setMagGain, 0, _b_setMagGain.Length);
                cmd.State = "SetMagnetGain";
            }
            else
            { MessageBox.Show("シリアルポートが閉じています。"); }
        }

        private void SendSetWirelessFreqCmd(int id, int wich)
        {
            // Status読出コマンドを送信
            if (this.SerialPort.IsOpen)
            {
                byte[] _b_setWiFreq = cmd.cmdSetWiFreq(id, wich);
                this.SerialPort.Write(_b_setWiFreq, 0, _b_setWiFreq.Length);
                cmd.State = "SetWirelessFrequency";
            }
            else
            { MessageBox.Show("シリアルポートが閉じています。"); }
        }
        #endregion

        #region ファイル保存
        private void makeDownloadFile(string filename)
        {
            // 
        }

        private void closeDownloadFile()
        {
        }
        #endregion

        #region シリアルポートの接続および解除（ツールバー）
        /// <summary>
        /// シリアルポートの接続ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ctool_btnConnect_Click(object sender, EventArgs e)
        {
            // COMポートが列挙されていない状態であれば、処理を進めない（エラー対策）
            if (this.Ctool_cbxCOM.Items.Count == 0)
            { return; }
            if (this.Ctool_cbxCOM.SelectedIndex < 0)
            {
                return;
            }

            try
            {
                if (this.SerialPort.IsOpen)
                {
                    this.SerialPort.Close();
                    MessageBox.Show("異なるCOMポートが開かれています。");
                    return;
                }

                Console.WriteLine(this.Ctool_cbxCOM.Items[this.Ctool_cbxCOM.SelectedIndex].ToString());
                // 
                SerialPort.PortName = this.Ctool_cbxCOM.Items[this.Ctool_cbxCOM.SelectedIndex].ToString();
                SerialPort.Open();

                // ツールバー::接続関連ボタンの状態を変更
                this.Ctool_btnConnect.Enabled = false;
                this.Ctool_btnDisConnect.Enabled = true;
                this.Ctool_btnPreMeasure.Enabled = true;

                // ステータスバーの状態を変更
                this.Cstatus_lblConnect.Image = LP.MT.Properties.Resources.transmit;
                Console.WriteLine(this.Ctool_cbxCOM.SelectedText);
                this.Cstatus_lblCOM.Text = this.Ctool_cbxCOM.SelectedText + "接続中";
            }
            catch (IOException ioe)
            { MessageBox.Show(ioe.ToString(), "通信ポートにエラーが発生しています", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            catch (InvalidOperationException ioe)
            { MessageBox.Show(ioe.ToString(), "通信ポートにエラーが発生しています", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        /// <summary>
        /// シリアルポートの接続解除ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ctool_btnDisConnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.SerialPort.IsOpen)
                {
                    this.SerialPort.Close();
                }

                // ツールバー::接続関連ボタンの状態を変更
                this.Ctool_btnConnect.Enabled = true;
                this.Ctool_btnDisConnect.Enabled = false;
                this.Ctool_btnMeasure.Enabled = false;

                // ステータスバーの状態を変更
                this.Cstatus_lblConnect.Image = LP.MT.Properties.Resources.transmit_blue;
                this.Cstatus_lblCOM.Text = "未接続";
            }
            catch (IOException ioe)
            { MessageBox.Show(ioe.ToString(), "接続エラーが発生しました", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            catch (ArgumentException)
            { return; }
        }
        #endregion

        #region シリアルポートに関するユーティリティメソッド群（ポート再検索、他）
        private void scanSerialPorts()
        {
            // すべてのシリアル・ポート名を取得
            string[] ports = SerialPort.GetPortNames();

            // 取得したシリアル・ポート名を、ツールバー内のコンボボックスへ列挙する。
            this.Ctool_cbxCOM.BeginUpdate();
            this.Ctool_cbxCOM.Items.Clear();
            foreach (string port in ports)
            {
                this.Ctool_cbxCOM.Items.Add(port);
            }
            this.Ctool_cbxCOM.EndUpdate();

            // シリアルポートが一つ以上見つかれば、コンボボックスの表示を最上位のものに設定する
            if (this.Ctool_cbxCOM.Items.Count > 0)
            { this.Ctool_cbxCOM.SelectedIndex = 0; }
        }
        #endregion

        #region メニュー関連
        // ファイル - 終了
        private void Cmenu_File_Exit_Click(object sender, EventArgs e)
        {
            try
            {
                if (!SerialPort.IsOpen)
                {
                    Application.Exit();
                }
                else
                {

                    MessageBox.Show("COMポートが接続されたままのようです。\r\n接続を切断してからアプリケーションを終了させて下さい。", "接続中", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (IOException ioe)
            { MessageBox.Show(ioe.ToString(), "通信ポートにエラーが発生しています", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            catch (InvalidOperationException ioe)
            { MessageBox.Show(ioe.ToString(), "通信ポートにエラーが発生しています", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        // シリアルポート - 再検索
        private void Cmenu_COM_reScan_Click(object sender, EventArgs e)
        {
            // シリアルポートを列挙
            scanSerialPorts();
        }
        #endregion

        private void UpdateMeasurementButton(bool state)
        {
            if (state)
            {
                // 測定開始/停止ボタンを有効化
                Ctool_btnPreMeasure.Enabled = false;
                Ctool_btnMeasure.Enabled = true;
                Ctool_btnMeasureStop.Enabled = true;

                // Status読出コマンドを送信
                if (this.SerialPort.IsOpen)
                {
                    byte[] _b_prepMeasurement = cmd.cmdPrepMeasurement(0x0e, Convert.ToInt32(this.Cnud_dstID.Value), "test", 0x01, "");
                    this.SerialPort.Write(_b_prepMeasurement, 0, _b_prepMeasurement.Length);
                    cmd.State = "PrepMeasurement";
                    TxState.Text = "準備中";
                }
                else
                { MessageBox.Show("シリアルポートが閉じています。"); }
            }
            else
            {
                // 測定開始/停止ボタンを無効化
                Ctool_btnPreMeasure.Enabled = true;
                Ctool_btnMeasure.Enabled = false;
                Ctool_btnMeasureStop.Enabled = false;
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            // Status読出コマンドを送信
            if (this.SerialPort.IsOpen)
            {
                count++;
                byte[] _b_prepMeasurement = cmd.cmdStartCamera(count);
                this.SerialPort.Write(_b_prepMeasurement, 0, _b_prepMeasurement.Length);
                cmd.State = "PrepMeasurement";
            }
            else
            { MessageBox.Show("シリアルポートが閉じています。"); }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            // Status読出コマンドを送信
            if (this.SerialPort.IsOpen)
            {
                count++;

                byte[] _b_prepMeasurement = cmd.cmdStopCamera(count);
                this.SerialPort.Write(_b_prepMeasurement, 0, _b_prepMeasurement.Length);
                cmd.State = "PrepMeasurement";
            }
            else
            { MessageBox.Show("シリアルポートが閉じています。"); }
        }

        private void BtStartMeasurement_Click(object sender, EventArgs e)
        {
            // 測定時間の記録
            DateTime _dt = DateTime.Now;
            _str_TimeOfStartMeas = _dt.ToString("yyMMddHHmmssfff");
            Console.WriteLine("MeasureStart = " + _str_TimeOfStartMeas);

            // Status読出コマンドを送信
            if (this.SerialPort.IsOpen)
            {
                count++;
                //カメラの測定開始
                byte[] _b_prepMeasurement = cmd.cmdStartCamera(count);
                this.SerialPort.Write(_b_prepMeasurement, 0, _b_prepMeasurement.Length);
                cmd.State = "PrepMeasurement";

                //センサの測定開始
                byte[] _b_startMeasure = cmd.cmdStartMeasurement(Convert.ToInt32(this.Cnud_dstID.Value), Convert.ToInt32(this.Ctool_cbxMonitor.SelectedIndex));
                this.SerialPort.Write(_b_startMeasure, 0, _b_startMeasure.Length);
                cmd.State = "Measurement";
            }
            else
            { MessageBox.Show("シリアルポートが閉じています。"); }
        }

        private void BtStopMeasurement_Click(object sender, EventArgs e)
        {
            // Status読出コマンドを送信
            if (this.SerialPort.IsOpen)
            {
                count++;

                //センサの測定停止
                byte[] _b_stopMeasure = cmd.cmdStopMeasurement(Convert.ToInt32(this.Cnud_dstID.Value));
                this.SerialPort.Write(_b_stopMeasure, 0, _b_stopMeasure.Length);
                cmd.State = "Measurement";

                //カメラの測定停止
                byte[] _b_prepMeasurement = cmd.cmdStopCamera(count);
                this.SerialPort.Write(_b_prepMeasurement, 0, _b_prepMeasurement.Length);
                cmd.State = "PrepMeasurement";
            }
            else
            { MessageBox.Show("シリアルポートが閉じています。"); }
        }
    }
}
