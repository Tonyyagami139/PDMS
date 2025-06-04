using Bll;
using Common;
using System;
using System.Collections.Generic;

namespace Dal
{
    public class TicketDal
    {
        public TicketBll TicketBll { get; set; }
        public TicketDal(string setting)
        {
            TicketBll = new TicketBll(setting);
        }
        public bool Connect()
        {
            return TicketBll.Connect();
        }
        public void Disconnect()
        {
            TicketBll.Disconnect();
        }

        public void CreateTable()
        {
            TicketBll.CreateTable();
        }

        public bool AddTicket(Ticket ticket)
        {
            try
            {
                TicketBll.AddTicket(ticket);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool DeleteTicket(int id, string modifyUserName)
        {
            try
            {
                TicketBll.DeleteTicket(id, modifyUserName);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool UpdateTicket(Ticket ticket)
        {
            try
            {
                TicketBll.UpdateTicket(ticket);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public Ticket GetTicket(int id)
        {
            return TicketBll.GetTicket(id);
        }

        public List<Ticket> GetTickets(string sql="")
        {
            return TicketBll.GetTickets(sql);
        }

        public List<Ticket> GetTicketsByDescription(string desc)
        {
            return TicketBll.GetTicketsByDescription(desc);
        }
    }
}
