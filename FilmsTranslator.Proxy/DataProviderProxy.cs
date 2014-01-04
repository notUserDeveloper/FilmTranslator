using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace FilmsTranslator.Proxy
{
    public class DataProviderProxy
    {
        private SqlConnection _con;
        private SqlCommand _command;
        private SqlDataReader _reader;
        private const string ConnectInit = @"Data Source=(localdb)\v11.0;Initial Catalog=FilmsTranslator.Main.Models.EntityContext;Integrated Security=True";


        public void InsertSite(string site)
        {
            using(_con = new SqlConnection(ConnectInit)){
                _con.Open();
                _command = _con.CreateCommand();
                _command.CommandText = "INSERT INTO Sites(site) VALUES('" + site + "')";
                try { _command.ExecuteNonQuery(); }
                catch { Console.WriteLine("повторка: " + site); }
            }
        }

        public Dictionary<int, string> GetSiteWithId()
        {
            using (_con = new SqlConnection(ConnectInit))
            {
                _con.Open();
                _command = _con.CreateCommand();
                _command.CommandText = "SELECT TOP 1 id,site FROM Sites where lastcheck IS NULL";
                _reader = _command.ExecuteReader();
                if (!_reader.HasRows) return null;
                _reader.Read();
                var resault = new Dictionary<int,string>{ {_reader.GetInt32(0), _reader.GetString(1)} };
                return resault;
            }
        }

        public void InsertIp(int id,string ip)
        {
            using (_con = new SqlConnection(ConnectInit))
            {
                _con.Open();
                _command = _con.CreateCommand();
                _command.CommandText = "INSERT INTO IPlist VALUES(" + id + ",'" + ip + "'," + (int)StatusIp.Unchecked + ")";
                try { _command.ExecuteNonQuery(); }
                catch { Console.WriteLine("такой IP уже есть: " + ip); }
            }
        }

        public void UpdateDate(int id, DateTime? dt)
        {
            using (_con = new SqlConnection(ConnectInit))
            {
                _con.Open();
                _command = _con.CreateCommand();
                if (dt != null)
                {
                    _command.CommandText = "UPDATE Sites SET lastcheck='" + dt.Value.ToString("MM/dd/yyyy HH:mm:ss") + "' where id=" + id;
                }
                _command.ExecuteScalar();
            }
        }

        public List<string> GetUncheckedIp(int top)
        {
            using (_con = new SqlConnection(ConnectInit))
            {
                _con.Open();
                _command = _con.CreateCommand();
                _command.CommandText = "SELECT TOP "+top+" ip from IPlist where status=" + (int)StatusIp.Unchecked;
                _reader = _command.ExecuteReader();
                if (!_reader.HasRows) return null;
                var listIp = new List<string>();
                while (_reader.Read())
                {
                    listIp.Add(_reader.GetString(0));
                }
                return listIp;
            }
        }

        public string GetGoodProxy()
        {
            using (_con = new SqlConnection(ConnectInit))
            {
                _con.Open();
                _command = _con.CreateCommand();
                _command.CommandText = "Select top 1 ip from IPlist where status="+(int)StatusIp.Good+" ORDER BY NEWID()";
                string resault = _command.ExecuteScalar().ToString();
                return resault;
            }
        }

        public void UpdateStatus(string val, StatusIp status, string column)
        {
            using (_con = new SqlConnection(ConnectInit))
            {
                _con.Open();
                _command = _con.CreateCommand();
                _command.CommandText = "UPDATE IPlist SET status=" + (int)status + " where " + column + "='" + val + "'";
                _command.ExecuteNonQuery();
            }
        }

        public void DeleteIp(string ip)
        {
            using (_con = new SqlConnection(ConnectInit))
            {
                _con.Open();
                _command = _con.CreateCommand();
                _command.CommandText = "DELETE FROM IPlist WHERE ip='" + ip + "'";
                _command.ExecuteNonQuery();
            }
        }

        public Dictionary<int,string> GetOldSites()
        {
            using (_con = new SqlConnection(ConnectInit))
            {
                _con.Open();
                _command = _con.CreateCommand();
                _command.CommandText = "SELECT id,site,lastcheck FROM Sites WHERE lastcheck<=dateadd(HOUR,-2,GETDATE())";
                _reader = _command.ExecuteReader();
                if (!_reader.HasRows) return null;
                var resault = new Dictionary<int,string>();
                while (_reader.Read())
                {
                    resault.Add(_reader.GetInt32(0), _reader.GetString(1));
                }
                return resault;
            }
        }


    }
}
