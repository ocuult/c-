using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace insertTraceId
{
    public partial class Form3 : Form
    {
        /// <summary>
        /// 定义一个filename用来存放当前窗体的openFileDialog信息
        /// </summary>
        private string filename="";
        public Form3()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Stream myStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = "c:\\Users\\Derek\\Desktop";
            openFileDialog1.Filter = "Excel2003 files (*.xls)|*.xls|Excel2007 files (*.xlsx)|*.xlsx";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = false;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        fileURL.Text = openFileDialog1.FileName;
                        filename = openFileDialog1.FileName; // 给文件赋给全局变量
                        using (myStream)
                        {
                            // Insert code to read the stream here.
                            //dataGridView1.DataSource = insertTraceId.DAL.NpoiHelp.ImportExceltoDt(openFileDialog1.FileName);

                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }
    }
}
