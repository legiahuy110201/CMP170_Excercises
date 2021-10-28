using QL_SV;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace quanlysinhvien
{
    public partial class frmDSSV : Form
    {
        public frmDSSV()
        {
            InitializeComponent();
        }
        private string tukhoa = "";
        private void frmDSSV_Load(object sender, EventArgs e)
        {
            LoadDSSV();//gọi tới hàm loadDSSV khi form đc load
            //List<string> listtimkiem = new List<string>;
            //listtimkiem.Add("ho");
            //listtimkiem.Add("ten");
        }

        private void LoadDSSV()
        {
            //load toàn bộ danh sách sv khi from đc load
            List<CustomParameter> lstPara = new List<CustomParameter>();
            lstPara.Add(new CustomParameter()
            {
                key = "@tukhoa",
                value = tukhoa  
            });
            dgvSinhVien.DataSource = new Database().SelectData("SelectAllSinhVien", lstPara);
            //đặt tên cột
            dgvSinhVien.Columns["masinhvien"].HeaderText = "Mã SV";
            dgvSinhVien.Columns["hoten"].HeaderText = "Họ tên";
            dgvSinhVien.Columns["nsinh"].HeaderText = "nsinh";
            dgvSinhVien.Columns["gt"].HeaderText = "Giới tính";
            dgvSinhVien.Columns["quequan"].HeaderText = "Quê quán";
            dgvSinhVien.Columns["diachi"].HeaderText = "Địa chỉ";
            dgvSinhVien.Columns["email"].HeaderText = "Email";
            dgvSinhVien.Columns["dienthoai"].HeaderText = "Điện thoại";
        }

        private void dgvSinhVien_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //khi double click vào sinh viên nào 
            //sẽ hiện ra form cập nhập thông tin sinh viên
            // ta cần lấy mã sinh viên để cập nhập sinh viên
            if(e.RowIndex>=0)
            {
                var msv = dgvSinhVien.Rows[e.RowIndex].Cells["masinhvien"].Value.ToString();
                //truyền mã sv này vào form sinh viên
                new frmSinhVien(msv).ShowDialog();

                // sau khi form đc đóng lại cần loag lại dssv
                LoadDSSV();
            }    
        }

        private void btnThemmoi_Click(object sender, EventArgs e)
        {
            new frmSinhVien(null).ShowDialog();//nếu thêm mới sv-> mã sv = null
            LoadDSSV();
        }
       //public void fillDataCombobox(List<string>Datasorse)
        //{
            //foreach( var item in Datasorse)
            //{
                //comboBox1.Items.Add(item);
           // }    
        //}
        private void btnTimkiem_Click(object sender, EventArgs e)
        {
            tukhoa = txtTukhoa.Text;
            LoadDSSV();
            

        }
    }
}
