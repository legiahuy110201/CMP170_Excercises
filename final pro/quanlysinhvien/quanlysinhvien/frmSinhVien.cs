using QL_SV;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace quanlysinhvien
{
    public partial class frmSinhVien : Form
    {
        public frmSinhVien(string msv)
        {
            this.msv = msv;// truyền lại mã sv khi form chạy
            InitializeComponent();
        }
        private string msv;

        private void frmSinhVien_Load(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(msv))//nếu mã sv không có->thêm mới sv
            {
                this.Text = "Thêm mới sinh viên";
            }
            else
            {
                this.Text = "Cập nhập thông tin sinh viên";
                //lấy thông tin chi tiết của 1 sinh viên dựa vào msv
                var r = new Database().Select(string.Format("selectSV'" + msv + "'"));
                //MessageBox.Show(r[0].ToString());
                //set các giá trị vào component của form

                txtHo.Text = r["ho"].ToString();
                txtTendem.Text = r["tendem"].ToString();
                txtTen.Text = r["ten"].ToString();
                mtbNgaysinh.Text = r["ngsinh"].ToString();
                if (int.Parse(r["gioitinh"].ToString()) == 1)
                {
                    rbtNam.Checked = true;
                }
                else
                {
                    rbtNu.Checked = true;
                }
                txtQuequan.Text = r["quequan"].ToString();
                txtDiachi.Text = r["diachi"].ToString();
                txtDienthoai.Text = r["dienthoai"].ToString();
                txtEmail.Text = r["email"].ToString();
            }    

        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            //nút lưu sẽ sử lý 2 tình huống 
            //nếu msv không có giá trị -> thêm mới sv
            //nếu msv có giá trị - > cập nhập 
            string sql = "";
            string ho = txtHo.Text;
            string tendem = txtTendem.Text;
            string ten = txtTen.Text;
            DateTime ngaysinh;
            try
            {
                ngaysinh = DateTime.ParseExact(mtbNgaysinh.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch(Exception)
            {
                MessageBox.Show("Ngày sinh không hợp lệ");
                mtbNgaysinh.Select();//trỏ chuột về ngày sinh
                return;// không thực hiện các câu lệnh phía dưới
            }
            string gioitinh = rbtNam.Checked ? "1" : "0";
            string quequan = txtQuequan.Text;
            string dienthoai = txtDienthoai.Text;
            string diachi = txtDiachi.Text;
            string email = txtEmail.Text;

            List<CustomParameter> lstPqara = new List<CustomParameter>();
            if(string.IsNullOrEmpty(msv))
            {
                sql = "ThemMoiSV";
                
            }   
            else
            {
                sql = "updateSV";
                lstPqara.Add(new CustomParameter()
                {
                    key = "@masinhvien",
                    value = msv
                });
            }
            lstPqara.Add(new CustomParameter()
            {
                key = "@ho",
                value = ho
            });
            lstPqara.Add(new CustomParameter()
            {
                key = "@tendem",
                value = tendem
            });
            lstPqara.Add(new CustomParameter()
            {
                key = "@ten",
                value = ten
            });
            lstPqara.Add(new CustomParameter()
            {
                key = "@ngaysinh",
                value = ngaysinh.ToString("yyyy-MM-dd")
            });
            lstPqara.Add(new CustomParameter()
            {
                key = "@gioitinh",
                value = gioitinh
            });
            lstPqara.Add(new CustomParameter()
            {
                key = "@quequan",
                value = quequan
            });
            lstPqara.Add(new CustomParameter()
            {
                key = "@diachi",
                value = diachi
            });
            lstPqara.Add(new CustomParameter()
            {
                key = "@dienthoai",
                value = dienthoai
            });
            lstPqara.Add(new CustomParameter()
            {
                key = "@email",
                value = email
            });

            var rs = new Database().ExCute(sql, lstPqara);// truyền 2 tham số là câu lênh sql
            if (rs == 1)//nếu thực thi thành công
            {
                if (string.IsNullOrEmpty(msv))//nếu thêm mới
                {
                    MessageBox.Show("Thêm mới sinh viên thành công");
                }
                else//nếu cập nhập
                {
                    MessageBox.Show("Cập nhập thông tin thành công");
                }
                this.Dispose();//đóng form sau khi hoàn thành
            }
            else//nếu không thực thi được
            {
                MessageBox.Show("Thực thi thất bại");
            }    
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Application.Exit();
        }
    }
}
