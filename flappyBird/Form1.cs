using System;
using System.Drawing; // Görseli ters çevirme işlemi için bu kütüphane şart
using System.Windows.Forms;

namespace flappyBird
{
    public partial class Form1 : Form
    {
        int pipeSpeed = 8;
        int gravity = 5;
        int score = 0;
        Random rnd = new Random();

        public Form1()
        {
            InitializeComponent();
            this.KeyPreview = true;

            flappyBird.BackColor = Color.Transparent;
            scoreText.BackColor = Color.Transparent;

            // SENİN İSTEDİĞİN GİBİ: Alt borunun resmini alıp üst boruya tepe taklak ekliyoruz!
            // (Eğer alt boruya resim yüklediysen bu kod onu kopyalayıp ters çevirir)
            if (pipeBottom.Image != null)
            {
                pipeTop.Image = (Image)pipeBottom.Image.Clone();
                pipeTop.Image.RotateFlip(RotateFlipType.RotateNoneFlipY); // Dikey eksende Y eksenini ters çevir
            }

            // İlk boruları sahneye al
            BorulariYenile();
        }

      
        private void BorulariYenile()
        {

            pipeBottom.Left = 600;
            pipeTop.Left = 600;

            // Alt boruyu ekranın biraz daha ortalarına rastgele koyuyoruz
            pipeBottom.Top = rnd.Next(100, 250);

            // Üst boruyu alt borunun TAM 500 piksel yukarısına kilitliyoruz
            pipeTop.Top = pipeBottom.Top - 300;
        }

        private void gameTimerEvent(object sender, EventArgs e)
        {
            flappyBird.Top += gravity;
            pipeBottom.Left -= pipeSpeed;
            pipeTop.Left -= pipeSpeed;
            ground.Left -= pipeSpeed;

            scoreText.Text = "Score: " + score;

            // Boru ekranın solundan çıkınca çok beklemeden (-150 yerine -80) yenisini getir
            if (pipeBottom.Left < -80)
            {
                score++;
                BorulariYenile(); // Yukarıda yazdığımız fonksiyonu çağırıyoruz
            }

            // Zemin kayma döngüsü
            if (ground.Left < -50)
            {
                ground.Left = 0;
            }

            // Çarpışma kontrolleri
            if (flappyBird.Bounds.IntersectsWith(pipeBottom.Bounds) ||
                flappyBird.Bounds.IntersectsWith(pipeTop.Bounds) ||
                flappyBird.Bounds.IntersectsWith(ground.Bounds) ||
                flappyBird.Top < -25)
            {
                endGame();
            }
        }

        private void gamekeyisdown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space || e.KeyCode == Keys.Up)
            {
                gravity = -15;
            }
        }

        private void gamekeyisup(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space || e.KeyCode == Keys.Up)
            {
                gravity = 5;
            }
        }

        private void endGame()
        {
            gameTimer.Stop();
            scoreText.Text += " - Oyun Bitti!";
        }

        private void pipeTop_Click(object sender, EventArgs e)
        {

        }
    }
}