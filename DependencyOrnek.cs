using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bildirim
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection con;
        SqlCommand cmd;
        SqlDependency dep;
        SqlDataAdapter da;
        DataTable dt;
        private void Form1_Load(object sender, EventArgs e)
        {
            RegisterDependency();

        }
        public void RegisterDependency()
        {
            con = new SqlConnection(@"Data Source=TOPRAK\STAJ;Initial Catalog=Suthanem;user id=sa;password=123456;");
            con.Open();
            cmd = new SqlCommand("Select GSM From Cari", con);
            dep = new SqlDependency(cmd);
            SqlDependency.Start(@"Data Source=TOPRAK\STAJ;Initial Catalog=Suthanem;user id=sa;password=123456;", "BildirimQueue");
            dep.OnChange += new OnChangeEventHandler(dep_OnChange);
        }
        int i = 0;
        void dep_OnChange(object sender, SqlNotificationEventArgs e)
        {
            MessageBox.Show("VERİ TABANINA KAYIT EKLENDİ"+i);
            i++;
            var info = e.Info;
            var source = e.Source; 
            var type = e.Type;
            SqlDependency dependency = sender as SqlDependency;
            dependency.OnChange -= new OnChangeEventHandler(dep_OnChange);
            RegisterDependency();

        }
    }
}
