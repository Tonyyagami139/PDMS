using Common;
using Dal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace PDMS
{
    public partial class FormTicketRecord : Form
    {
        private TicketDal dal = new TicketDal(Global.DbSettingSqlserver);
        private BindingList<Ticket> ticketList = new BindingList<Ticket>();
        private Ticket selectedTicket;

        public FormTicketRecord()
        {
            InitializeComponent();
            Load += FormTicketRecord_Load;
            dataGridViewTickets.SelectionChanged += dataGridViewTickets_SelectionChanged;
            btnAdd.Click += btnAdd_Click;
            btnDelete.Click += btnDelete_Click;
            btnEdit.Click += btnEdit_Click;
            btnSearch.Click += btnSearch_Click;
            btnRefresh.Click += btnRefresh_Click;
            btnUpload.Click += btnUpload_Click;
            btnExport.Click += btnExport_Click;
        }

        private void FormTicketRecord_Load(object sender, EventArgs e)
        {
            LoadTickets();
            cbStatus.Items.AddRange(new string[] { "Open", "Closed", "Hold" });
            cbPriority.Items.AddRange(new string[] { "Low", "Medium", "High" });
        }

        private void LoadTickets()
        {
            var list = dal.GetTickets();
            ticketList = new BindingList<Ticket>(list);
            dataGridViewTickets.DataSource = ticketList;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Ticket ticket = new Ticket();
            ticket.Title = tbTitle.Text.Trim();
            ticket.Description = tbDescription.Text.Trim();
            ticket.Status = cbStatus.Text;
            ticket.Priority = cbPriority.Text;
            ticket.Assignee = tbAssignee.Text.Trim();
            ticket.Attachments = tbAttachments.Text.Trim();
            ticket.CreateUserName = Environment.UserName;
            ticket.CreateTime = DateTime.Now;
            ticket.ModifyUserName = ticket.CreateUserName;
            ticket.ModifyTime = ticket.CreateTime;
            dal.AddTicket(ticket);
            LoadTickets();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedTicket == null) return;
            dal.DeleteTicket(selectedTicket.Id, Environment.UserName);
            LoadTickets();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (selectedTicket == null) return;
            selectedTicket.Title = tbTitle.Text.Trim();
            selectedTicket.Description = tbDescription.Text.Trim();
            selectedTicket.Status = cbStatus.Text;
            selectedTicket.Priority = cbPriority.Text;
            selectedTicket.Assignee = tbAssignee.Text.Trim();
            selectedTicket.Attachments = tbAttachments.Text.Trim();
            selectedTicket.ModifyUserName = Environment.UserName;
            selectedTicket.ModifyTime = DateTime.Now;
            dal.UpdateTicket(selectedTicket);
            LoadTickets();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string desc = tbDescription.Text.Trim();
            var list = dal.GetTicketsByDescription(desc);
            ticketList = new BindingList<Ticket>(list);
            dataGridViewTickets.DataSource = ticketList;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadTickets();
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            using(OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.png;*.jpg";
                if(ofd.ShowDialog() == DialogResult.OK)
                {
                    string file = ofd.FileName;
                    tbAttachments.Text = file;
                }
            }
        }

        private void dataGridViewTickets_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewTickets.SelectedRows.Count > 0)
            {
                selectedTicket = dataGridViewTickets.SelectedRows[0].DataBoundItem as Ticket;
                if (selectedTicket != null)
                {
                    tbTitle.Text = selectedTicket.Title;
                    tbDescription.Text = selectedTicket.Description;
                    cbStatus.Text = selectedTicket.Status;
                    cbPriority.Text = selectedTicket.Priority;
                    tbAssignee.Text = selectedTicket.Assignee;
                    tbAttachments.Text = selectedTicket.Attachments;
                }
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            using(SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "CSV files|*.csv";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    using(var sw = new StreamWriter(sfd.FileName))
                    {
                        foreach(var t in ticketList)
                        {
                            sw.WriteLine($"{t.Id},{t.Title},{t.Description},{t.Status},{t.Priority},{t.Assignee}");
                        }
                    }
                }
            }
        }
    }
}
