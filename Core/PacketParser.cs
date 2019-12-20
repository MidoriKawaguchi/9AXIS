using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LP.MT.Core
{
    public class PacketParser
    {
        public List<byte[]> packetList;
        public List<byte[]> stateList;
        public byte[] residualBytes;
        public int residualSize;

        public PacketParser()
        {
            packetList = new List<byte[]>();
            stateList = new List<byte[]>();
        }

        // 残バイト配列を作成
        public void makeResidualBytes(int size)
        {
            residualSize = size;
            residualBytes = new byte[size];

            this.ResidualSize = size;
        }

        public void copyResidualBytes(byte[] data)
        {
            data.CopyTo(residualBytes, data.Length);
        }

        // コマンドの状態を保持するプロパティ
        public int ResidualSize { get; set; }
    }
}
