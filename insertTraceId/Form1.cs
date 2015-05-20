using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using insertTraceId.DAL;
using System.Data.SQLite;
using System.IO;
using System.Data.Common;
using System.Diagnostics;
//using System.Data.SQLite;
//using System.Data;
namespace insertTraceId
{
    public partial class Form1 : Form
    {
        string FileName = "";
        public Form1()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            string sql = "select * from test";
            //string sql = "select * from sq8flyt.dbo.countrys;";
            dataGridView1.DataSource = insertTraceId.DAL.SqliteHelper.ExecuteTable(sql);
            //dataGridView1.DataSource = insertTraceId.DAL.SqlHelper.ExecuteTable(sql);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        public void button2_Click(object sender, EventArgs e)
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
                        textBox1.Text = openFileDialog1.FileName;
                        FileName = openFileDialog1.FileName; // 给文件赋给全局变量
                        using (myStream)
                        {
                            // Insert code to read the stream here.
                            dataGridView1.DataSource = insertTraceId.DAL.NpoiHelp.ImportExceltoDt(openFileDialog1.FileName);

                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string sql = "INSERT INTO [Test]([TypeID],[TraceID],[CreateUserName],[TraceType],[CheckTime])VALUES('{0}','{1}','{2}','{3}',datetime('now', 'localtime'));";
            DataTable dt = insertTraceId.DAL.NpoiHelp.ImportExceltoDt(FileName);
            int length = dt.Rows.Count;

            string typeid = cmbPosttype.Text;
            string tracetype = cmbCompany.Text;
            string CreateUserName = cmbCUN.Text;
            List<string> list = new List<string>();

                for (int i = 0; i < length; i++)
                {
                    string traceid = dt.Rows[i][0].ToString();
                    //sql参数数组
                    //SQLiteParameter[] select = {
                    //                            new SQLiteParameter("@typeid",typeid),
                    //                            new SQLiteParameter("@traceid",traceid),
                    //                            new SQLiteParameter("@tracetype",tracetype),
                    //                            new SQLiteParameter("@CreateUserName",CreateUserName)
                    //                        };
                    string xx = string.Format(sql, typeid, traceid, CreateUserName, tracetype);
                    list.Add(xx);
                }
             Stopwatch watch = new Stopwatch();
             watch.Start();
             int js=insertTraceId.DAL.SqliteHelper.ExecuteSqlTran(list);
             watch.Stop();
            string time = watch.ElapsedMilliseconds.ToString();
            MessageBox.Show(string.Format("插入:{0}行记录，用时:{1}毫秒",js.ToString(),time));
        }
    }
}
