using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Net.NetworkInformation;

namespace 烽火条码检测
{
    public class sqlserver
    {
        private static string pingtest()
        {
            //构造Ping实例 
            Ping pingSender = new Ping();
            //Ping 选项设置  
            PingOptions options = new PingOptions();
            options.DontFragment = true;
            //测试数据  
            string data = "test test test test test test te";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            //设置超时时间  
            int timeout = 1000;
            bool ck = false;
            bool ck1 = false;
            for (int ii = 0; ii < 4; ii++)
            {
                PingReply reply = pingSender.Send("172.16.30.2", timeout, buffer, options);
                if (reply.Status == IPStatus.Success)
                {
                    ck = true;
                }
            }
            for (int ii = 0; ii < 4; ii++)
            {
                PingReply reply = pingSender.Send("172.18.201.2", timeout, buffer, options);
                if (reply.Status == IPStatus.Success)
                {
                    ck1 = true;
                }
            }
            if (ck != true && ck1 != true)
            {
                ck = true;
                ck1 = true;
            }
            if (ck)
            {
                ConnectionString = @"server=172.16.30.2;database=oracle;uid=sa;pwd=adminsa";
                DataSet ds=sqlserver.GetDataSet("select 1");
                if(ds!=null&&ds.Tables.Count>0&&ds.Tables[0].Rows.Count>0)
                return @"server=172.16.30.2;database=oracle;uid=sa;pwd=adminsa";
            }
            if (ck1)
            {
                ConnectionString = @"server=172.18.201.2;database=oracle;uid=sa;pwd=adminsa";
                DataSet ds = sqlserver.GetDataSet("select 1");
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                return @"server=172.18.201.2;database=oracle;uid=sa;pwd=adminsa";
            }
            return "";
           
        }

        private static string ConnectionString = pingtest();       
      
        //根据sql语句返回一个DataSet
        #region GetDataSet
        public static DataSet GetDataSet(string sql)
        {            
            SqlCommand sqlcom = new SqlCommand();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                PrepareCommand(sqlcom, conn, CommandType.Text, sql);
                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = sqlcom;
                DataSet ds = new DataSet();
                sda.Fill(ds);
                sqlcom.Parameters.Clear();
                return ds;
            }
        }
        #endregion
        //根据sql语句返回一个SqlDataReader
        #region ExecSqlDataReader
        public static SqlDataReader ExecSqlDataReader(string sql)
        {
            SqlCommand sqlcom = new SqlCommand();
            SqlConnection conn = new SqlConnection(ConnectionString);
            try
            {
                PrepareCommand(sqlcom, conn, CommandType.Text, sql);
                SqlDataReader sdr = sqlcom.ExecuteReader(CommandBehavior.CloseConnection);
                sqlcom.Parameters.Clear();
                return sdr;
            }
            catch (Exception ex)
            {
                conn.Close();
                throw ex;
            }
        }
        #endregion
        //执行sql语句，返回影响行数
        #region ExecCommand
        public static int ExecCommand(string sql)
        {            
            SqlCommand sqlcom = new SqlCommand();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                PrepareCommand(sqlcom, conn, CommandType.Text, sql);
                int rtn = sqlcom.ExecuteNonQuery();
                sqlcom.Parameters.Clear();
                return rtn;
            }
        }
        #endregion
        //执行SQL语句，无返回值
        #region ExecNon
        public void ExecNon(string sql)
        {
            SqlCommand sqlcom = new SqlCommand();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                PrepareCommand(sqlcom, conn, CommandType.Text, sql);
                sqlcom.ExecuteNonQuery();
                sqlcom.Parameters.Clear();
            }
        }
        #endregion
        //根据sql语句返回查询结果的第一行
        #region ExecuteScalar
        public static object ExecuteScalar(string sql)
        {
            SqlCommand sqlcom = new SqlCommand();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                PrepareCommand(sqlcom, conn, CommandType.Text, sql);
                object rtn = sqlcom.ExecuteScalar();
                sqlcom.Parameters.Clear();
                return rtn;
            }
        }
        #endregion
        //执行存储过程无返回值，SQL语句含有参数
        #region ExecSPCommand
        public void ExecSPCommand(string sql, System.Data.IDataParameter[] paramers)
        {
            SqlCommand sqlcom = new SqlCommand();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                PrepareCommand(sqlcom, conn, CommandType.StoredProcedure, sql);
                foreach (System.Data.IDataParameter paramer in paramers)
                {
                    sqlcom.Parameters.Add(paramer);
                }
                sqlcom.ExecuteNonQuery();
                sqlcom.Parameters.Clear();
            }
        }
        #endregion
        //执行存储过程无返回值，SQL语句不含有参数
        #region ExecSPCommand2
        public void ExecSPCommand2(string sql)
        {
            SqlCommand sqlcom = new SqlCommand();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                PrepareCommand(sqlcom, conn, CommandType.StoredProcedure, sql);
                sqlcom.ExecuteNonQuery();
                sqlcom.Parameters.Clear();
            }
        }
        #endregion
        //执行存储过程，返回SqlDataReader，SQL语句含有参数
        #region ExecSPSqlDataReader
        public SqlDataReader ExecSPSqlDataReader(string sql, System.Data.IDataParameter[] paramers)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            SqlCommand sqlcom = new SqlCommand();
            try
            {
                PrepareCommand(sqlcom, conn, CommandType.StoredProcedure, sql);
                foreach (System.Data.IDataParameter paramer in paramers)
                {
                    sqlcom.Parameters.Add(paramer);
                }
                SqlDataReader sdr = sqlcom.ExecuteReader(CommandBehavior.CloseConnection);
                sqlcom.Parameters.Clear();
                return sdr;
            }
            catch (Exception ex)
            {
                conn.Close();
                throw ex;
            }
        }
        #endregion
        //执行存储过程，返回SqlDataReader，SQL语句不含有参数
        #region ExecSPSqlDataReader2
        public SqlDataReader ExecSPSqlDataReader2(string sql)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            SqlCommand sqlcom = new SqlCommand();
            try
            {
                PrepareCommand(sqlcom, conn, CommandType.StoredProcedure, sql);
                SqlDataReader sdr = sqlcom.ExecuteReader(CommandBehavior.CloseConnection);
                sqlcom.Parameters.Clear();
                return sdr;
            }
            catch (Exception ex)
            {
                conn.Close();
                throw ex;
            }
        }
        #endregion
        //执行存储过程返回DataSet类型，SQL语句含有参数
        #region ExecSPDataSet
        public DataSet ExecSPDataSet(string sql, System.Data.IDataParameter[] paramers)
        {
            SqlCommand sqlcom = new SqlCommand();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                PrepareCommand(sqlcom, conn, CommandType.StoredProcedure, sql);
                foreach (System.Data.IDataParameter paramer in paramers)
                {
                    sqlcom.Parameters.Add(paramer);
                }
                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = sqlcom;
                DataSet ds = new DataSet();
                sda.Fill(ds);
                sqlcom.Parameters.Clear();
                return ds;
            }
        }
        #endregion
        //执行存储过程返回DataSet类型，SQL语句不含有参数
        #region ExecSPDataSet2
        public DataSet ExecSPDataSet2(string sql)
        {
            SqlCommand sqlcom = new SqlCommand();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                PrepareCommand(sqlcom, conn, CommandType.StoredProcedure, sql);
                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = sqlcom;
                DataSet ds = new DataSet();
                sda.Fill(ds);
                sqlcom.Parameters.Clear();
                return ds;
            }
        }
        #endregion
     
        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, CommandType cmdType, string cmdText)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            cmd.CommandType = cmdType;
        }

        #region 将db reader转换为Hashtable列表
        /// <summary>
        /// 将db reader转换为Hashtable列表
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public static List<Hashtable> ExecuteReaderToHash(string sql)
        {
            SqlDataReader reader = ExecSqlDataReader(sql);
            List<Hashtable> list = new List<Hashtable>();
            while (reader.Read())
            {
                Hashtable item = new Hashtable();

                for (var i = 0; i < reader.FieldCount; i++)
                {
                    var name = reader.GetName(i);
                    var value = reader[i];
                    item[name] = value;
                }
                list.Add(item);
            }
            return list;
        }      
        #endregion

        /// <summary>
        /// 获取树格式对象
        /// </summary>
        /// <param name="list">线性数据</param>
        /// <param name="id">ID的字段名</param>
        /// <param name="pid">PID的字段名</param>
        /// <returns></returns>
        public static object listToTree(List<Hashtable> list, string id, string pid)
        {
            Hashtable h = new Hashtable(); //数据索引 
            List<Hashtable> r = new List<Hashtable>(); //数据池,要返回的 
            foreach (var item in list)
            {
                if (!item.ContainsKey(id)) continue;
                h[item[id].ToString()] = item;
            }
            foreach (var item in list)
            {
                if (!item.ContainsKey(id)) continue;
                if (!item.ContainsKey(pid) || item[pid] == null || !h.ContainsKey(item[pid].ToString()))
                {
                    r.Add(item);
                }
                else
                {
                    var pitem = h[item[pid].ToString()] as Hashtable;
                    if (!pitem.ContainsKey("children"))
                        pitem["children"] = new List<Hashtable>();
                    var children = pitem["children"] as List<Hashtable>;
                    children.Add(item);
                }
            }
            return r;
        }
    }
}
