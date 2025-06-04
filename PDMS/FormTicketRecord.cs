using Common;
using Dal;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PDMS
{
    public partial class FormTicketRecord : Form
    {
        TicketDal dal = new TicketDal(Global.DbSettingSqlserver);
        public FormTicketRecord()
        {
            InitializeComponent();
        }

        private void FormTicketRecord_Load(object sender, EventArgs e)
        {
            dal.CreateTable();
            RefreshTickets();
        }

        private void RefreshTickets()
        {
            dataGridView_tickets.DataSource = dal.GetTickets();
        }

        private void bt_add_Click(object sender, EventArgs e)
        {
            Ticket ticket = new Ticket
            {
                Title = tb_title.Text,
                Description = tb_description.Text,
                Status = tb_status.Text,
                CreateUserName = Global.UserName,
                CreateTime = DateTime.Now,
                ModifyUserName = Global.UserName,
                ModifyTime = DateTime.Now
            };
            dal.AddTicket(ticket);
            RefreshTickets();
        }

        private void bt_update_Click(object sender, EventArgs e)
        {
            if (dataGridView_tickets.CurrentRow == null) return;
            Ticket ticket = (Ticket)dataGridView_tickets.CurrentRow.DataBoundItem;
            ticket.Title = tb_title.Text;
            ticket.Description = tb_description.Text;
            ticket.Status = tb_status.Text;
            ticket.ModifyUserName = Global.UserName;
            ticket.ModifyTime = DateTime.Now;
            dal.UpdateTicket(ticket);
            RefreshTickets();
        }

        private void bt_delete_Click(object sender, EventArgs e)
        {
            if (dataGridView_tickets.CurrentRow == null) return;
            Ticket ticket = (Ticket)dataGridView_tickets.CurrentRow.DataBoundItem;
            dal.DeleteTicket(ticket.Id);
            RefreshTickets();
        }

        private void dataGridView_tickets_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView_tickets.CurrentRow == null) return;
            Ticket ticket = (Ticket)dataGridView_tickets.CurrentRow.DataBoundItem;
            tb_title.Text = ticket.Title;
            tb_description.Text = ticket.Description;
            tb_status.Text = ticket.Status;
        }
    }
}
