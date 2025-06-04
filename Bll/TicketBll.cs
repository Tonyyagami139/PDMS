using Common;
using Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Bll
{
    public class TicketBll
    {
        public IDatabase Db { get; set; }
        public TicketBll(string setting)
        {
            DatabaseFactory df = new DatabaseFactory();
            Db = df.CreateDatabase(setting);
        }
        public bool Connect()
        {
            return Db.Connect();
        }
        public void Disconnect()
        {
            Db.Disconnect();
        }

        public void CreateTable()
        {
            string query = @"IF NOT EXISTS(SELECT * FROM sysobjects WHERE name='Ticket' and xtype='U')
CREATE TABLE Ticket(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(100),
    Description NVARCHAR(MAX),
    Status NVARCHAR(50),
    CreateUserName NVARCHAR(50),
    CreateTime DATETIME,
    ModifyUserName NVARCHAR(50),
    ModifyTime DATETIME
);IF NOT EXISTS(SELECT * FROM sysobjects WHERE name='TicketLog' and xtype='U')
CREATE TABLE TicketLog(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    TicketId INT,
    Title NVARCHAR(100),
    Description NVARCHAR(MAX),
    Status NVARCHAR(50),
    ModifyUserName NVARCHAR(50),
    ModifyTime DATETIME
)";
            Db.ExecuteQuery(query);
        }

        public void AddTicket(Ticket ticket)
        {
            string query = "INSERT INTO Ticket (Title,Description,Status,CreateUserName,CreateTime,ModifyUserName,ModifyTime) VALUES (@Title,@Description,@Status,@CreateUserName,@CreateTime,@ModifyUserName,@ModifyTime)";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Title", ticket.Title),
                new SqlParameter("@Description", ticket.Description),
                new SqlParameter("@Status", ticket.Status),
                new SqlParameter("@CreateUserName", ticket.CreateUserName),
                new SqlParameter("@CreateTime", ticket.CreateTime),
                new SqlParameter("@ModifyUserName", ticket.ModifyUserName),
                new SqlParameter("@ModifyTime", ticket.ModifyTime)
            };
            Db.ExecuteQuery(query, parameters);
        }

        public void DeleteTicket(int id)
        {
            string query = "DELETE FROM Ticket WHERE Id=@Id";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Id", id)
            };
            Db.ExecuteQuery(query, parameters);
        }

        public void UpdateTicket(Ticket ticket)
        {
            string query = "UPDATE Ticket SET Title=@Title,Description=@Description,Status=@Status,ModifyUserName=@ModifyUserName,ModifyTime=@ModifyTime WHERE Id=@Id";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Title", ticket.Title),
                new SqlParameter("@Description", ticket.Description),
                new SqlParameter("@Status", ticket.Status),
                new SqlParameter("@ModifyUserName", ticket.ModifyUserName),
                new SqlParameter("@ModifyTime", ticket.ModifyTime),
                new SqlParameter("@Id", ticket.Id)
            };
            Db.ExecuteQuery(query, parameters);
            AddTicketLog(ticket);
        }

        public Ticket GetTicket(int id)
        {
            string query = "SELECT * FROM Ticket WHERE Id=@Id";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Id", id)
            };
            var dt = Db.ExecuteQuery(query, parameters);
            if (dt.Rows.Count == 0)
            {
                return null;
            }
            DataRow row = dt.Rows[0];
            return DataRowToTicket(row);
        }

        public List<Ticket> GetTickets(string sql="")
        {
            string query = sql;
            if (string.IsNullOrEmpty(query))
            {
                query = "SELECT * FROM Ticket ORDER BY ModifyTime DESC";
            }
            var dt = Db.ExecuteQuery(query);
            List<Ticket> list = new List<Ticket>();
            foreach(DataRow row in dt.Rows)
            {
                list.Add(DataRowToTicket(row));
            }
            return list;
        }

        public List<Ticket> GetTicketsByDescription(string desc)
        {
            if (string.IsNullOrWhiteSpace(desc))
            {
                return GetTickets();
            }
            string query = "SELECT * FROM Ticket WHERE Description LIKE @Desc ORDER BY ModifyTime DESC";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Desc", "%" + desc + "%")
            };
            var dt = Db.ExecuteQuery(query, parameters);
            List<Ticket> list = new List<Ticket>();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(DataRowToTicket(row));
            }
            return list;
        }

        private void AddTicketLog(Ticket ticket)
        {
            string query = "INSERT INTO TicketLog(TicketId,Title,Description,Status,ModifyUserName,ModifyTime) VALUES(@TicketId,@Title,@Description,@Status,@ModifyUserName,@ModifyTime)";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@TicketId", ticket.Id),
                new SqlParameter("@Title", ticket.Title),
                new SqlParameter("@Description", ticket.Description),
                new SqlParameter("@Status", ticket.Status),
                new SqlParameter("@ModifyUserName", ticket.ModifyUserName),
                new SqlParameter("@ModifyTime", ticket.ModifyTime)
            };
            Db.ExecuteQuery(query, parameters);
        }

        private Ticket DataRowToTicket(DataRow row)
        {
            Ticket ticket = new Ticket();
            ticket.Id = Convert.ToInt32(row["Id"]);
            ticket.Title = row["Title"].ToString();
            ticket.Description = row["Description"].ToString();
            ticket.Status = row["Status"].ToString();
            ticket.CreateUserName = row["CreateUserName"].ToString();
            ticket.CreateTime = Convert.ToDateTime(row["CreateTime"]);
            ticket.ModifyUserName = row["ModifyUserName"].ToString();
            ticket.ModifyTime = Convert.ToDateTime(row["ModifyTime"]);
            return ticket;
        }
    }
}
