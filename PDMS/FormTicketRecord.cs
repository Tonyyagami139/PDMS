using Common;
using Dal;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.ComponentModel;
using System.Linq;

namespace PDMS
{
    public partial class FormTicketRecord : Form
    {
        TicketDal dal = new TicketDal(Global.DbSettingSqlserver);
        private ListSortDirection sortDirection = ListSortDirection.Ascending;
        public FormTicketRecord()
        {
            InitializeComponent();
        }

        private void FormTicketRecord_Load(object sender, EventArgs e)
        {
            dal.CreateTable();
            RefreshTickets();
            dataGridView_tickets.ColumnHeaderMouseClick += dataGridView_tickets_ColumnHeaderMouseClick;
            dataGridView_tickets.CellClick += dataGridView_tickets_CellClick;
        }

        private void RefreshTickets()
        {
            dataGridView_tickets.DataSource = dal.GetTickets();
        }

        private void FillFormFromCurrentRow()
        {
            if (dataGridView_tickets.CurrentRow == null) return;
            Ticket ticket = (Ticket)dataGridView_tickets.CurrentRow.DataBoundItem;
            tb_title.Text = ticket.Title;
            tb_description.Text = ticket.Description;
            tb_status.Text = ticket.Status;
        }

        private void SearchTickets()
        {
            dataGridView_tickets.DataSource = dal.GetTicketsByDescription(tb_search.Text.Trim());
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
            FillFormFromCurrentRow();
        }

        private void dataGridView_tickets_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView_tickets_SelectionChanged(sender, e);
        }

        private void bt_search_Click(object sender, EventArgs e)
        {
            SearchTickets();
        }

        private void dataGridView_tickets_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var columnName = dataGridView_tickets.Columns[e.ColumnIndex].DataPropertyName;
            var list = dataGridView_tickets.DataSource as List<Ticket>;
            if (list == null) return;
            if (sortDirection == ListSortDirection.Ascending)
            {
                list = new List<Ticket>(list.OrderBy(t => typeof(Ticket).GetProperty(columnName).GetValue(t, null)));
                sortDirection = ListSortDirection.Descending;
            }
            else
            {
                list = new List<Ticket>(list.OrderByDescending(t => typeof(Ticket).GetProperty(columnName).GetValue(t, null)));
                sortDirection = ListSortDirection.Ascending;
            }
            dataGridView_tickets.DataSource = list;
        }
    }
}
