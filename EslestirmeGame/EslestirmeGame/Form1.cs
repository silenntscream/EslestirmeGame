using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace EslestirmeGame
{
    public partial class Form1 : Form
    {
        private readonly Timer t = new Timer();
        private readonly  Timer t2 = new Timer();
        private readonly Timer t3 = new Timer();
        private OyunYoneticisi oyun;
        private int kalansüre = 40;


        public Form1()
        {
            InitializeComponent();

            lblScore.Text = "Skor: 0"; // lblScore zaten Form tasarımında var

            oyun = new OyunYoneticisi(this, lblScore, t2, () => kalansüre);
            oyun.Göster();

            t.Interval = 2000;
            t.Tick += T_Tick;
            t.Start();

            t2.Tick += T2_Tick;

            t3.Interval = 1000;
            t3.Tick += T3_Tick;
            t3.Start();

            foreach (Control c in Controls) 
            {
                if (c is Button btn)
                    btn.Click += Buton_Click;
            }
        }

        private void T_Tick(object sender, EventArgs e)
        {
            t.Stop();
            oyun.IkonlariGizle();
        }

        private void T2_Tick(object sender, EventArgs e)
        {
            t2.Stop();
            oyun.EslesmeyenleriGizle();
        }

        public void TekrarSor()
        {
            var result = MessageBox.Show("Tekrar oynamak ister misiniz?", "Oyun Bitti", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Restart(); // Uygulamayı baştan başlatır
            }
            else
            {
                Close(); // Oyunu kapatır
            }
        }

        private void T3_Tick(object sender, EventArgs e)
        {
            kalansüre--;
            lblzaman.Text = "SÜRE : " + kalansüre;
            if (kalansüre <= 0)
            {

            
            t3.Stop();
            
            foreach (Button item in Controls.OfType<Button>())
            {
                if (item.Enabled && item.Name != "closebtn")
                {
                    MessageBox.Show("Süre bitti, kaybettiniz!");
                    TekrarSor();
                    return;
                }
            }

            TekrarSor();
        }
        }
        public void SureyiDurdur()
        {
            t3.Stop();
        }

        private void Buton_Click(object sender, EventArgs e)
        {
            oyun.Buton_Click(sender);
        }

       

        private void closebtn_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
