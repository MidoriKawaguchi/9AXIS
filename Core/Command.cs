using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LP.MT.Core
{
    class Command
    {
        // 定型バイト
        byte[] BytesStopMeasurement = new byte[] { 0x55, 0x55, 0x05, 0x0e, 0x03, 0x04, 0x04, 0xAA };

        public Command()
        {
            // オブジェクト初期化直後である状態を表す。
            this.State = "load";
        }

        #region 制御コマンド～ステータス系～
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public byte[] cmdStatus(int id)
        {
            //byte[] BytesReadStatus = new byte[] { 0x55, 0x55, 0x05, 0x00, 0x01, 0x05, 0x05, 0xAA };
            byte[] BytesReadStatus = new byte[] { 0x55, 0x55, 0x05, 0x0E, 0x01, 0x05, 0x05, 0xAA };
            BytesReadStatus[4] = BitConverter.GetBytes(id)[0];

            return BytesReadStatus;
        }
        #endregion

        #region 制御コマンド～測定系～
        //ここから自己開発
        public byte[] cmdPrepMeasurement(int pid, int smid, string filename, int mode, string fileCommnet)
        {
            byte[] BytesPrepMeasurement = new byte[101];

            // Danger: Big Endian
            byte[] BytesFilename = Encoding.ASCII.GetBytes(filename.Reverse().ToArray());
            byte[] BytesFileComment = Encoding.ASCII.GetBytes(fileCommnet.Reverse().ToArray());

            byte BytePID = (byte) Convert.ToChar(pid);
            byte ByteSMID = (byte) Convert.ToChar(smid);
            byte ByteMode = (byte) Convert.ToChar(mode);

            // Product ID
            BytesPrepMeasurement[3] = BytePID;
            // Module ID
            BytesPrepMeasurement[4] = ByteSMID;
            // Filename
            //ファイル名は最後のバイトOxOOを含めて最大13バイト　ファイル名が12バイト以上の時は12、そうでない場合は現在のファイル名の長さのみバイトを確保
            Buffer.BlockCopy(BytesFilename, 0, BytesPrepMeasurement, 7, BytesFilename.Length >= 12 ? 12 : BytesFilename.Length);
            // Mode
            BytesPrepMeasurement[20] = ByteMode;

            // File Comment
            //コメントは最大64バイト　ファイル名が64バイト以上の時は64、そうでない場合は現在のコメントの長さのみバイトを確保
            Buffer.BlockCopy(BytesFileComment, 0, BytesPrepMeasurement, 25, BytesFileComment.Length >= 64 ? 64 : BytesFileComment.Length);

            Buffer.BlockCopy(new byte[] { 0x55, 0x55 }, 0, BytesPrepMeasurement, 0, 2);
            BytesPrepMeasurement[2] = 0x62;
            BytesPrepMeasurement[5] = 0x1F;

            BytesPrepMeasurement[99] = CalcParity(BytesPrepMeasurement);
            BytesPrepMeasurement[100] = 0xAA;

            return BytesPrepMeasurement;
        }

        public byte[] cmdStartCamera(int idx)
        {
            //カムの撮影開始
            // 55 55 62 28 FF 72 06 01 01 0A 09 A5 01 00 82 00 00 01 00 5A 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 0A AA

            byte[] BytesStartCamera = new byte[101];

            byte[] constant = new byte[6] { 0x55, 0x55, 0x62, 0x28, 0xff, 0x72 };
            Buffer.BlockCopy(constant, 0, BytesStartCamera, 0, 6);
            BytesStartCamera[6] = (byte) Convert.ToChar(idx);
            byte[] constant1 = new byte[] { 0x01, 0x01, 0x0A, 0x09, 0xA5, 0x01, 0x00, 0x82, 0x00, 0x00, 0x01, 0x00, 0x5A };
            Buffer.BlockCopy(constant1, 0, BytesStartCamera, 7, constant1.Length);
            BytesStartCamera[99] = CalcParity(BytesStartCamera);
            BytesStartCamera[100] = 0xAA;

            return BytesStartCamera;
            //01 01 0A 09 A5 01 00 82 00 00 01 00 5A
        }

        public byte[] cmdStopCamera(int idx)
        {
            //カムの撮影停止
            // 55 55 62 28 FF 72 07 01 01 0A 09 A5 01 00 83 00 00 01 00 5A 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 0A AA
            //
            byte[] BytesStopCamera = new byte[101];

            byte[] constant = new byte[6] { 0x55, 0x55, 0x62, 0x28, 0xff, 0x72 };
            Buffer.BlockCopy(constant, 0, BytesStopCamera, 0, 6);
            BytesStopCamera[6] = (byte)Convert.ToChar(idx);
            byte[] constant1 = new byte[] { 0x01, 0x01, 0x0A, 0x09, 0xA5, 0x01, 0x00, 0x83, 0x00, 0x00, 0x01, 0x00, 0x5A };
            Buffer.BlockCopy(constant1, 0, BytesStopCamera, 7, constant1.Length);
            BytesStopCamera[99] = CalcParity(BytesStopCamera);
            BytesStopCamera[100] = 0xAA;

            return BytesStopCamera;
            //01 01 0A 09 A5 01 00 82 00 00 01 00 5A
        }

        public byte[] cmdStartMeasurement(int id, int mode)
        {
            byte[] BytesStartMeasurement = new byte[] { 0x55, 0x55, 0x0B, 0x0e, 0x01, 0x02, 0x0A, 0x02, 0x1B, 0x16, 0x0A, 0x00, 0x0C, 0xAA };
            BytesStartMeasurement[4] = BitConverter.GetBytes(id)[0];

            DateTime dt = DateTime.Now;
            BytesStartMeasurement[6] = BitConverter.GetBytes(dt.Year - 2000)[0]; // 2000引いておかないと、dt.Yearの出力は2010(0x07DA)の下位バイトを送ってしまう。
            BytesStartMeasurement[7] = BitConverter.GetBytes(dt.Month)[0];
            BytesStartMeasurement[8] = BitConverter.GetBytes(dt.Day)[0];
            BytesStartMeasurement[9] = BitConverter.GetBytes(dt.Hour)[0];
            BytesStartMeasurement[10] = BitConverter.GetBytes(dt.Minute)[0];

            // 測定データ保存か、モニタリングか
            BytesStartMeasurement[11] = BitConverter.GetBytes(mode)[0];

            //
            BytesStartMeasurement[12] = CalcParity(BytesStartMeasurement);
            BytesStartMeasurement[13] = 0xAA;
 
            return BytesStartMeasurement;
        }

        public byte[] cmdStopMeasurement(int id)
        {
            byte[] ret = BytesStopMeasurement;
            ret[4] = BitConverter.GetBytes(id)[0];

            ret[6] = CalcParity(ret);

            return ret;
        }
        #endregion

        #region 制御コマンド～ファイル系～
        public byte[] cmdReadFileInfo(int id)
        {
            byte[] BytesReadFileInfo = new byte[] { 0x55, 0x55, 0x05, 0x0e, 0x01, 0x07, 0x07, 0xAA };
            BytesReadFileInfo[4] = BitConverter.GetBytes(id)[0];
            BytesReadFileInfo[6] = CalcParity(BytesReadFileInfo);

            return BytesReadFileInfo;
        }

        public byte[] cmdGetFileData(int id, int idx, int startPos, int length)
        {
            byte[] BytesGetFileData = new byte[17];
            BytesGetFileData.Initialize();

            //
            BytesGetFileData[0] = 0x55;
            BytesGetFileData[1] = 0x55;
            BytesGetFileData[2] = 0x0E;
            BytesGetFileData[3] = 0x0e;
            BytesGetFileData[4] = BitConverter.GetBytes(id)[0];
            BytesGetFileData[5] = 0x09;
            BytesGetFileData[6] = BitConverter.GetBytes(idx)[0];
            BytesGetFileData[7] = BitConverter.GetBytes(startPos)[3];
            BytesGetFileData[8] = BitConverter.GetBytes(startPos)[2];
            BytesGetFileData[9] = BitConverter.GetBytes(startPos)[1];
            BytesGetFileData[10] = BitConverter.GetBytes(startPos)[0];
            BytesGetFileData[11] = BitConverter.GetBytes(length)[3];
            BytesGetFileData[12] = BitConverter.GetBytes(length)[2];
            BytesGetFileData[13] = BitConverter.GetBytes(length)[1];
            BytesGetFileData[14] = BitConverter.GetBytes(length)[0];
            BytesGetFileData[15] = CalcParity(BytesGetFileData);
            BytesGetFileData[16] = 0xAA;

            return BytesGetFileData;
        }
        #endregion

        #region 制御コマンド～設定系～
        public byte[] cmdSetSamplingFreq(int id, int freq)
        {
            byte[] BytesSetSamplingFreq = new byte[10];
            BytesSetSamplingFreq.Initialize();
            
            //
            BytesSetSamplingFreq[0] = 0x55;
            BytesSetSamplingFreq[1] = 0x55;
            BytesSetSamplingFreq[2] = 0x07;
            BytesSetSamplingFreq[3] = 0x00;
            BytesSetSamplingFreq[4] = BitConverter.GetBytes(id)[0];
            BytesSetSamplingFreq[5] = 0x0B;
            BytesSetSamplingFreq[6] = BitConverter.GetBytes(freq)[1];
            BytesSetSamplingFreq[7] = BitConverter.GetBytes(freq)[0];
            BytesSetSamplingFreq[8] = CalcParity(BytesSetSamplingFreq);
            BytesSetSamplingFreq[9] = 0xAA;

            return BytesSetSamplingFreq;
        }

        public byte[] cmdSetMeasurementTime(int id, int hour, int min, int sec)
        {
            byte[] BytesSetMeasTime = new byte[11];
            BytesSetMeasTime.Initialize();

            //
            BytesSetMeasTime[0] = 0x55;
            BytesSetMeasTime[1] = 0x55;
            BytesSetMeasTime[2] = 0x07;
            BytesSetMeasTime[3] = 0x00;
            BytesSetMeasTime[4] = BitConverter.GetBytes(id)[0];
            BytesSetMeasTime[5] = 0x0C;
            BytesSetMeasTime[6] = BitConverter.GetBytes(hour)[0];
            BytesSetMeasTime[7] = BitConverter.GetBytes(min)[0];
            BytesSetMeasTime[8] = BitConverter.GetBytes(sec)[0];
            BytesSetMeasTime[9] = CalcParity(BytesSetMeasTime);
            BytesSetMeasTime[10] = 0xAA;

            return BytesSetMeasTime;
        }

        public byte[] cmdSetMagGain(int id, int gain)
        {
            byte[] BytesSetMagGain = new byte[9];
            BytesSetMagGain.Initialize();

            //
            BytesSetMagGain[0] = 0x55;
            BytesSetMagGain[1] = 0x55;
            BytesSetMagGain[2] = 0x06;
            BytesSetMagGain[3] = 0x00;
            BytesSetMagGain[4] = BitConverter.GetBytes(id)[0];
            BytesSetMagGain[5] = 0x0D;
            BytesSetMagGain[6] = BitConverter.GetBytes(gain)[0];
            BytesSetMagGain[7] = CalcParity(BytesSetMagGain);
            BytesSetMagGain[8] = 0xAA;

            return BytesSetMagGain;
        }

        public byte[] cmdSetWiFreq(int id, int wich)
        {
            byte[] BytesSetWiChannel = new byte[9];
            BytesSetWiChannel.Initialize();

            //
            BytesSetWiChannel[0] = 0x55;
            BytesSetWiChannel[1] = 0x55;
            BytesSetWiChannel[2] = 0x06;
            BytesSetWiChannel[3] = 0x00;
            BytesSetWiChannel[4] = BitConverter.GetBytes(id)[0];
            BytesSetWiChannel[5] = 0x0D;
            BytesSetWiChannel[6] = BitConverter.GetBytes(wich)[0];
            BytesSetWiChannel[7] = CalcParity(BytesSetWiChannel);
            BytesSetWiChannel[8] = 0xAA;

            return BytesSetWiChannel;
        }
        #endregion

        #region パリティチェック
        private byte CalcParity(byte[] checkBytes)
        {
            byte _by_result = 0x00;

            for (int i = 0; i < (checkBytes.Length - 7); i++)
            {
                _by_result ^= checkBytes[i + 5];
            }

            return _by_result;
        }
        #endregion

        // コマンドの状態を保持するプロパティ
        public string State { get; set; }
    }
}
