using System;
using System.Collections.Generic;
using System.Drawing;
using System.Security.AccessControl;
using System.Windows.Forms;

namespace EslestirmeGame
{
    public class OyunYoneticisi
    {
        private List<string> icons = new List<string>()
        {
            "🐶", "🐱", "🐭", "🐹", "🦊", "🐻", "🐼", "🐸", "🐵", "🦁",
            "🐶", "🐱", "🐭", "🐹", "🦊", "🐻", "🐼", "🐸", "🐵", "🦁"
        };

        private Random rnd = new Random();
        private Button First;
        private Button Last;
        private int score = 0;
        private Func<int> SureGetir;


        private Form1 form;
        private Label lblScore;
        private Timer t2;

        public OyunYoneticisi(Form1 form, Label lblScore, Timer t2, Func<int> sureGetir)
        {
            this.form = form;
            this.lblScore = lblScore;
            this.t2 = t2;
            SureGetir = sureGetir;
        }

        public void Göster()
        {
            List<string> iconKopya = new List<string>(icons);

            foreach (Control item in form.Controls)
            {
                if (item is Button btn && btn.Name != "closebtn")  // Kapat butonunu atla
                {
                    if (iconKopya.Count == 0)
                        break;
                    
                    int randomindex = rnd.Next(iconKopya.Count);
                    btn.Text = iconKopya[randomindex];
                    btn.ForeColor = Color.Black;
                    iconKopya.RemoveAt(randomindex);
                }
            }
        }


        public void IkonlariGizle()
        {
            foreach (Control item in form.Controls)
            {
                if (item is Button btn && btn.Name != "closebtn")
                {
                    btn.ForeColor = btn.BackColor;
                }
            }
        }

        
        public void EslesmeyenleriGizle()
        {
            if (First != null && Last != null)
            {
                First.ForeColor = First.BackColor;
                Last.ForeColor = Last.BackColor;
                First = null;
                Last = null;
                
    }
        }

        public void Buton_Click(object sender)
        {
            Button btn = sender as Button;

            if (btn.ForeColor == Color.Black || !btn.Enabled)
                return;

            if (First == null)
            {
                First = btn;
                First.ForeColor = Color.Black;
                return;
            }

            if (Last == null && btn != First)
            {
                Last = btn;
                Last.ForeColor = Color.Black;

                if (First.Text == Last.Text)
                {
                    
                    First.Enabled = false;
                    Last.Enabled = false;
                    First.BackColor = Color.Green;
                    Last.BackColor = Color.Green;
                    First = null;
                    Last = null;

                    int kalan = SureGetir.Invoke();
                    double carpan = 1.0;

                    if (kalan >= 30) carpan = 2.0;
                    else if (kalan >= 20) carpan = 1.5;
                    else if (kalan >= 10) carpan = 1.0;
                    else carpan = 0.5;

                    int artis = (int)(10 * carpan);
                    score += artis;

                    lblScore.Text = $"Skor: {score} (+{artis})";

                    KazanmaKontrol();
                }
                else
                {
                    
                    
                    t2.Start();
                    t2.Interval = 500;
                
            }
        }
        }
        public void KazanmaKontrol()
        {
            foreach (Control item in form.Controls)
            {
                if (item is Button btn && btn.Enabled && btn.Name != "closebtn")
                {
                    return;
                }
            }
            form.SureyiDurdur();
            MessageBox.Show("Tebrikler, oyunu kazandınız! Skorunuz :" +  score);
            form.Invoke(new Action(() => form.TekrarSor())); // form içinden çağır
        }



    }
}
