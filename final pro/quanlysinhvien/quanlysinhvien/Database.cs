using quanlysinhvien;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QL_SV
{
    public class Database
    {
        private string connetionString = @"Data Source=DESKTOP-UJ3H5CT\SQLEXPRESS;Initial Catalog=QLSV; User ID = sa; Password = 12";
        private SqlConnection conn;
        private DataTable dt;
        private SqlCommand cmd;

        public Database()
        {
            try
            {
                conn = new SqlConnection(connetionString);
                if(conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("connected failed:" + ex.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }
        public DataTable SelectData(string sql, List<CustomParameter> lstPara)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                cmd = new SqlCommand(sql, conn);// nội dung spl dc truyền vào
                cmd.CommandType = CommandType.StoredProcedure;
                foreach(var para in lstPara)
                {
                    cmd.Parameters.AddWithValue(para.key, para.value);
                }    
                dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load dữ liệu:" + ex.Message);
                return null;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }
       
        public DataRow Select(string spl)
        {
            try
            {
                conn.Open();//mở kết nối
                cmd = new SqlCommand(spl, conn);//truyền giá trị vào cmd
                dt = new DataTable();
                dt.Load(cmd.ExecuteReader());//thực thi câu lệnh
                return dt.Rows[0];
            }
            catch(Exception ex)
            {
                MessageBox.Show("Lỗi load thông tin chi tiết:" + ex.Message);
                return null;
            }
            finally
            {
                conn.Close();
            }

        }

        public int ExCute(string sql,List<CustomParameter> lstPara)
        {
            try
            {
                conn.Open();//mở kết nối
                cmd = new SqlCommand(sql, conn);//thực thi câu lệnh
                cmd.CommandType = CommandType.StoredProcedure;
                foreach (var p in lstPara)
                {
                    cmd.Parameters.AddWithValue(p.key, p.value);
                }
                var rs = cmd.ExecuteNonQuery();
                return (int)rs;//trả về kq
            }
            catch(Exception ex)
            {
                MessageBox.Show("Lỗi thực thi câu lệnh:" + ex.Message);
                return -100;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
