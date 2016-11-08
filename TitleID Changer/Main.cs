using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace TitleID_Changer
{
    public partial class Main : Form
    {
        private const int _sizeOffset = 0x17;
        private const int _tidOffset = 0xC;
        private const int _headerSize = 0x8;
        private const int _blockSize = 0xF;

        private string _xexFile;
        private int _size;
        private int _offset;
        private bool _isTid = false;

        public Main()
        {
            InitializeComponent();
        }

        private void openXex_Click(object sender,EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                titleID.Clear();
                mediaID.Clear();
                _isTid = false;

                using (var br = new BinaryReader(File.Open(openFileDialog1.FileName,FileMode.Open,FileAccess.Read,FileShare.None)))
                {
                    var check = br.ReadBytes(4);

                    if (Encoding.ASCII.GetString(check) != "XEX2")
                    {
                        MessageBox.Show("Not a xex file");
                        return;
                    }

                    br.BaseStream.Seek(_sizeOffset,SeekOrigin.Begin);
                    _size = br.ReadByte();

                    for(int i = 0; i < _size; i++)
                    {
                        var offset = _sizeOffset + 1 + _headerSize * i;
                        br.BaseStream.Seek(offset,SeekOrigin.Begin);
                        var info = br.ReadBytes(4);

                        if (info[0] == 0x0 && info[1] == 0x4 && info[2] == 0x0 && info[3] == 0x6)
                        {
                            _isTid = true;

                            _offset = Swap(BitConverter.ToUInt32(br.ReadBytes(4), 0));

                            br.BaseStream.Seek(_offset,SeekOrigin.Begin);
                            var id = br.ReadBytes(4);
                            foreach (var byt in id)
                                mediaID.Text += String.Format("{0:X2}",byt);

                            br.BaseStream.Seek(_tidOffset-4,SeekOrigin.Current);
                            id = br.ReadBytes(4);
                            foreach (var byt in id)
                                titleID.Text += String.Format("{0:X2}",byt);
                        }
                    }

                    if (!_isTid)
                    {
                        MessageBox.Show("Can`t find TitleID");
                        titleID.Text = "00000000";
                        mediaID.Text = "00000000";
                    }

                    _xexFile = openFileDialog1.FileName;
                    addID.Enabled = !_isTid;
                    changeID.Enabled = _isTid;
                }
            }
        }

        private void addID_Click(object sender,EventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(titleID.Text,@"\A\b[0-9a-fA-F]+\b\Z") || !System.Text.RegularExpressions.Regex.IsMatch(mediaID.Text,@"\A\b[0-9a-fA-F]+\b\Z"))
            {
                MessageBox.Show("IDs must be in hex");
                return;
            }

            bool add = true;
            int tidheader;
            int offset;

            using (var br = new BinaryReader(File.Open(_xexFile,FileMode.Open,FileAccess.Read,FileShare.None)))
            {
                tidheader = _sizeOffset + 1 + _size * _headerSize;
                br.BaseStream.Seek(tidheader,SeekOrigin.Begin);
                if (BitConverter.ToUInt64(br.ReadBytes(8), 0) != 0)
                    add = false;

                br.BaseStream.Seek(_sizeOffset + 5 + (_size - 1) * _headerSize,SeekOrigin.Begin);
                offset = Swap(BitConverter.ToUInt32(br.ReadBytes(4),0)) + _blockSize + 1;

                br.BaseStream.Seek(offset,SeekOrigin.Begin);
                var block1 = BitConverter.ToUInt64(br.ReadBytes(8), 0);
                var block2 = BitConverter.ToUInt64(br.ReadBytes(8), 0);
                if (block1 != 0 || block2 != 0)
                    add = false;
            }

            if (!add)
            {
                MessageBox.Show("Can`t add titleid");
                return;
            }

            using (var bw = new BinaryWriter(File.Open(_xexFile,FileMode.Open,FileAccess.Write,FileShare.None)))
            {
                bw.Seek(_sizeOffset,SeekOrigin.Begin);
                bw.Write(_size + 1);

                bw.Seek(tidheader,SeekOrigin.Begin);
                byte[] buffer = { 0x0,0x4,0x0,0x6 };
                bw.Write(buffer);
                bw.Write(Swap((UInt32)offset));

                _offset = offset;

                bw.Seek(_offset,SeekOrigin.Begin);
                bw.Write(StringToByteArray(mediaID.Text));
                bw.Seek(_offset + _tidOffset,SeekOrigin.Begin);
                bw.Write(StringToByteArray(titleID.Text));

                _isTid = true;
                addID.Enabled = !_isTid;
                changeID.Enabled = _isTid;

                MessageBox.Show("Done!");
            }

        }

        private void changeID_Click(object sender,EventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(titleID.Text,@"\A\b[0-9a-fA-F]+\b\Z") || !System.Text.RegularExpressions.Regex.IsMatch(mediaID.Text,@"\A\b[0-9a-fA-F]+\b\Z"))
            {
                MessageBox.Show("IDs must be in hex");
                return;
            }

            using (var bw = new BinaryWriter(File.Open(_xexFile,FileMode.Open,FileAccess.Write,FileShare.None)))
            {
                bw.Seek(_offset,SeekOrigin.Begin);
                bw.Write(StringToByteArray(mediaID.Text));
                bw.Seek(_offset + _tidOffset,SeekOrigin.Begin);
                bw.Write(StringToByteArray(titleID.Text));

                MessageBox.Show("Done!");
            }
        }

        private static int Swap(UInt32 value)
        {
            return (int)((value & 0x000000FFU) << 24 | (value & 0x0000FF00U) << 8 | (value & 0x00FF0000U) >> 8 | (value & 0xFF000000U) >> 24);
        }

        public static byte[] StringToByteArray(string hex)
        {
            while (hex.Length < 8)
            {
                hex = '0' + hex;
            }

            return Enumerable.Range(0,hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x,2),16))
                             .ToArray();
        }
    }
}
