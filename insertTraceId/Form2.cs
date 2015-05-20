using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using insertTraceId.Model;
using insertTraceId.BLL;

namespace insertTraceId
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            //设置窗体加载的时候combobox的默认值
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string typeID=comboBox1.Text.ToString(); 
            //bool state=Convert.ToBoolean(Convert.ToInt32(comboBox3.Text));
            bool state;
            switch (comboBox2.Text)
            {
                case "1": state = true; break;
                default: state = false; break;
            }
            string traceType = comboBox3.Text.ToString();
            LoadTraceIdBystate(typeID,state,traceType);
        }

        private void LoadTraceIdBystate(string typeID, bool state, string traceType)
        {
            TraceIdBLL bll=new TraceIdBLL();
            //dataGridView1.AutoGenerateColumns = false; //禁止自动加载列
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue;//奇数列
            dataGridView1.DataSource=bll.GetTraceId(typeID,state,traceType);
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;//列的宽度随字段宽度自动调整
            if (dataGridView1.RowCount>0)
            {
                dataGridView1.Rows[0].Selected = false; //不默认选中第一行 
            }
        }

        //DataGridView显示行号的方法 
        private void dataGridView1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            e.Row.HeaderCell.Value = string.Format("{0}", e.Row.Index + 1);
            //DataGridView的RowsHeaderWidthSizeMode属性设置为AutoSizeToAllHeaders
            dataGridView1.RowHeadersWidthSizeMode =DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
        }
    }
}
