using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
namespace Session6
{
    //connection code
    class SQLConnect
    {

        SqlConnection cn;
        SqlCommand cmd;
        SqlDataAdapter da;
        SqlDataReader dr;
        DataSet ds;

       public void connect()
        {
            cn = new SqlConnection("Server=DESKTOP-HCK5N44; Initial Catalog=Session6; Integrated Security=SSPI");
            cn.Open();
        }
        public void DisplaySingle(String sql)
        {
            connect();
            cmd = new SqlCommand(sql, cn);
            dr = cmd.ExecuteReader();
            dr.Read();
        }

        public void Modify(String sql)
        {
            connect();
            cmd = new SqlCommand(sql, cn);
            cmd.ExecuteNonQuery();
        }
        public DataSet MultipleData(String sql)
        {
            connect();
            da = new SqlDataAdapter(sql, cn);
            ds = new DataSet();
            ds.Clear();
            da.Fill(ds, "tbl");
            return ds;
        }

        public String getf1()
        {
            if (dr.HasRows)
                return dr.GetValue(0).ToString();
            else
                return "";
        }

        public String getf2()
        {
            if (dr.HasRows)
                return dr.GetValue(1).ToString();
            else
                return "";
        }

        public String getf3()
        {
            if (dr.HasRows)
                return dr.GetValue(2).ToString();
            else
                return "";
        }

        public String getf4()
        {
            if (dr.HasRows)
                return dr.GetValue(3).ToString();
            else
                return "";
        }
        public String getf5()
        {
            if (dr.HasRows)
                return dr.GetValue(4).ToString();
            else
                return "";
        }

        public String getf6()
        {
            if (dr.HasRows)
                return dr.GetValue(5).ToString();
            else
                return "";
        }

        public String getf7()
        {
            if (dr.HasRows)
                return dr.GetValue(6).ToString();
            else
                return "";
        }
        public String getf8()
        {
            if (dr.HasRows)
                return dr.GetValue(7).ToString();
            else
                return "";
        }
        public String getf9()
        {
            if (dr.HasRows)
                return dr.GetValue(8).ToString();
            else
                return "";
        }

        public String getf10()
        {
            if (dr.HasRows)
                return dr.GetValue(9).ToString();
            else
                return "";
        }


    }
}
