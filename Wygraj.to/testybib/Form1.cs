using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AllegroClassLibrary;
using AllegroClassLibrary.Repositories;
using AllegroClassLibrary.Utility;
using AllegroClassLibrary.Models;



using System.Data.SqlClient;

namespace testybib
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ServiceRepository repo = new ServiceRepository();
            var pass = Converter.GetHashCode(textBox2.Text);

            LoginDetails details = repo.LoginEnc(textBox1.Text, pass, Countries.Poland);
            var info = repo.GetAuctionInfo(details, 4636713245);

            textBox1.Text = repo.MakeBid(new Bid { AuctionId = 4636713245, Price = 26 }, details);

        }
    }
}
