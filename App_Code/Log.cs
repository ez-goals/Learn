﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using DBHelper = Microsoft.ApplicationBlocks.Data.SqlHelper;

namespace EasyGoal
{
    public class Log
    {
        private static HttpContext Current
        {
            get { return HttpContext.Current; }
        }

        private static HttpRequest Request
        {
            get
            {
                if (HttpContext.Current == null)
                    throw new NullReferenceException("System.Web.HttpContext.Current is NULL.");
                return HttpContext.Current.Request;
            }
        }

        private static HttpResponse Response
        {
            get
            {
                if (HttpContext.Current == null)
                    throw new NullReferenceException("System.Web.HttpContext.Current is NULL.");
                return HttpContext.Current.Response;
            }
        }

        private int id;
        public int Id
        {
            get { return this.id; }
        }
        private DateTime timestamp;
        public DateTime Timestamp
        {
            get { return this.timestamp; }
            set { this.timestamp = value; }
        }
        private decimal timetaken;
        public decimal Timetaken
        {
            get { return this.timetaken; }
            set { this.timetaken = value; }
        }
        private string exactURL;
        public string ExactURL
        {
            get { return this.exactURL; }
            set { this.exactURL = value; }
        }
        private string filePath;
        public string FilePath
        {
            get { return this.filePath; }
            set { this.filePath = value; }
        }
        private string method;
        public string Method
        {
            get { return this.method; }
            set { this.method = value; }
        }
        private string userAgent;
        public string UserAgent
        {
            get { return this.userAgent; }
            set { this.userAgent = value; }
        }
        private string userHost;
        public string UserHost
        {
            get { return this.userHost; }
            set { this.userHost = value; }
        }
        private string userPort;
        public string UserPort
        {
            get { return this.userPort; }
            set { this.userPort = value; }
        }
        private string httpDNT;
        public string HTTP_DNT
        {
            get { return this.httpDNT; }
            set { this.httpDNT = value; }
        }
        private string httpVia;
        public string HTTP_Via
        {
            get { return this.httpVia; }
            set { this.httpVia = value; }
        }
        private string httpXFF;
        public string HTTP_X_Forwarded_For
        {
            get { return this.httpXFF; }
            set { this.httpXFF = value; }
        }
        private string referrer;
        public string Referrer
        {
            get { return this.referrer; }
            set { this.referrer = value; }
        }
        private int statusCode;
        public int StatusCode
        {
            get { return this.statusCode; }
            set { this.statusCode = value; }
        }
        private string statusText;
        public string StatusText
        {
            get { return this.statusText; }
            set { this.statusText = value; }
        }

        private Log() { }

        public Log(int id)
        {
            this.id = id;
        }

        public Log(decimal timetaken)
        {
            this.timestamp = Current.Timestamp;
            this.timetaken = timetaken;
            this.exactURL = Request.Url.ToString();
            this.filePath = Request.FilePath;
            this.method = Request.HttpMethod;
            this.userAgent = Request.UserAgent;
            this.userHost = Request.UserHostAddress;
            this.userPort = Request.ServerVariables["REMOTE_PORT"];
            this.httpDNT = Request.ServerVariables["HTTP_DNT"];
            this.httpVia = Request.ServerVariables["HTTP_Via"];
            this.httpXFF = Request.ServerVariables["HTTP_X_Forwarded_For"];
            this.referrer = Request.UrlReferrer == null ? null : Request.UrlReferrer.ToString();
            this.statusCode = Response.StatusCode;
            this.statusText = Response.StatusDescription;
        }

        #region Insert, Delete & Update methods

        public bool Insert()
        {
            string cmdText = "INSERT INTO [Log] ([TIMESTAMP], [TIMETAKEN], [EXACTURL], [FILEPATH], [METHOD], [USERAGENT], [USERHOST], [USERPORT], [HTTP_DNT], [HTTP_VIA], [HTTP_XFF], [REFERRER], [STATUS_CODE], [STATUS_TEXT]) ";
            cmdText += " VALUES (@Timestamp, @Timetaken, @ExactURL, @FilePath, @Method, @UserAgent, @UserHost, @UserPort, @HTTP_DNT, @HTTP_Via, @HTTP_XFF, @Referrer, @StatusCode, @StatusText)";

            var pTimestamp = new SqlParameter("@Timestamp", SqlDbType.DateTime2);
            pTimestamp.SqlValue = this.timestamp;
            var pTimetaken = new SqlParameter("@Timetaken", SqlDbType.Decimal);
            pTimetaken.SqlValue = this.timetaken;

            SqlParameter[] parameters = new SqlParameter[] { 
                pTimestamp, pTimetaken,
                new SqlParameter("@ExactURL", this.exactURL),
                new SqlParameter("@FilePath", this.filePath),
                new SqlParameter("@Method", this.method),
                new SqlParameter("@UserAgent", this.userAgent),
                new SqlParameter("@UserHost", this.userHost),
                new SqlParameter("@UserPort", this.userPort),
                new SqlParameter("@HTTP_DNT", this.httpDNT),
                new SqlParameter("@HTTP_Via", this.httpVia),
                new SqlParameter("@HTTP_XFF", this.httpXFF),
                new SqlParameter("@Referrer", this.referrer),
                new SqlParameter("@StatusCode", this.statusCode),
                new SqlParameter("@StatusText", this.statusText)
            };
            SqlConnection connection = Database.GetConnection();
            int result = DBHelper.ExecuteNonQuery(connection, CommandType.Text, cmdText, parameters);
            connection.Close();
            connection.Dispose();
            return (result > 0);
        }

        public bool Delete()
        {
            string cmdText = "DELETE FROM [Log] WHERE [ID] = @Id";
            SqlParameter parameter = new SqlParameter("@Id", this.id);
            SqlConnection connection = Database.GetConnection();
            int result = DBHelper.ExecuteNonQuery(connection, CommandType.Text, cmdText, parameter);
            connection.Close();
            connection.Dispose();
            return (result > 0);
        }

        #endregion Insert, Delete & Update methods

        private static Log PopulateEntity(IDataRecord dr)
        {
            var log = new Log();
            log.id = Convert.ToInt32(dr["ID"]);
            log.timestamp = Convert.ToDateTime(dr["TIMESTAMP"]);
            log.timetaken = Convert.ToDecimal(dr["TIMETAKEN"]);
            log.exactURL = dr["EXACTURL"].ToString();
            log.filePath = dr["FILEPATH"].ToString();
            log.method = dr["METHOD"].ToString();
            log.userAgent = dr["USERAGENT"].ToString();
            log.userHost = dr["USERHOST"].ToString();
            log.userPort = dr["USERPORT"].ToString();
            log.httpDNT = dr["HTTP_DNT"].Equals(DBNull.Value) ? null : dr["HTTP_DNT"].ToString();
            log.httpVia = dr["HTTP_VIA"].Equals(DBNull.Value) ? null : dr["HTTP_VIA"].ToString();
            log.httpXFF = dr["HTTP_XFF"].Equals(DBNull.Value) ? null : dr["HTTP_XFF"].ToString();
            log.Referrer = dr["REFERRER"].Equals(DBNull.Value) ? null : dr["REFERRER"].ToString();
            log.statusCode = Convert.ToInt32(dr["STATUS_CODE"]);
            log.statusText = dr["STATUS_TEXT"].ToString();
            return log;
        }

        public static List<Log> GetAll()
        {
            var list = new List<Log>();
            string sqlText = "SELECT * FROM [Log] ORDER BY [ID] DESC";
            using (SqlConnection connection = Database.GetConnection())
            {
                SqlDataReader dr = DBHelper.ExecuteReader(connection, CommandType.Text, sqlText);
                while (dr.Read())
                    list.Add(PopulateEntity(dr));
            }
            return list;
        }

        public static List<Log> GetAllError()
        {
            var list = new List<Log>();
            string sqlText = "SELECT * FROM [Log] WHERE [STATUS_CODE] != '200' ORDER BY [ID] DESC";
            using (SqlConnection connection = Database.GetConnection())
            {
                SqlDataReader dr = DBHelper.ExecuteReader(connection, CommandType.Text, sqlText);
                while (dr.Read())
                    list.Add(PopulateEntity(dr));
            }
            return list;
        }

    }
}