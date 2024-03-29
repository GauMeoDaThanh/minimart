﻿using ManageMiniMart.BLL;
using ManageMiniMart.Custom;
using ManageMiniMart.DAL;
using ManageMiniMart.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManageMiniMart
{
    public partial class FormBill : Form
    {
        private BillService billService;
        public FormBill()
        {
            InitializeComponent();
            billService= new BillService();
            setCBBSort1();
            setCBBSort2();
            dtpBillDate.Value = DateTime.Now;
        }

        public void setCBBSort1()
        {
            cbbSort1.Items.Add("CreatedTime");
            cbbSort1.Items.Add("Total Money");
        }
        public void setCBBSort2()
        {
            cbbSort2.Items.Add("Ascending");
            cbbSort2.Items.Add("Descending");
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string customerName = txtSearch.Text;
            dgvBill.DataSource = billService.getAllBillViewByCustomerName(customerName, dtpBillDate.Value.Date);
        }

        private void btnSortBy_Click(object sender, EventArgs e)
        {
            string s = cbbSort1.Text;
            int flag = cbbSort2.SelectedIndex;
            var list = billService.getAllBillViewSortBy(s, flag, dtpBillDate.Value.Date);

            dgvBill.DataSource = list; 
        }

        private void dgvBill_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvBill.Columns[e.ColumnIndex].Name == "ShowDetail")
            {
                int bill_id = Convert.ToInt32(dgvBill.SelectedRows[0].Cells[0].Value.ToString());
                FormBillPrint formBillPrint = new FormBillPrint(bill_id);
                formBillPrint.ShowDialog();
            }
        }

        private void dtpBillDate_ValueChanged(object sender, EventArgs e)
        {
            dgvBill.DataSource = billService.getAllBillViewByBillDate(dtpBillDate.Value.Date).ToList();
        }
    }
}
