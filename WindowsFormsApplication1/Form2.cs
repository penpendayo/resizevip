using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;


namespace WindowsFormsApplication1
{
    public partial class Form2 : Form
    {
        public Form1 f1;
        public Bitmap tukau;
        //コンストラクタ
        public Form2(Form1 f)
        {
            f1 = f;
            InitializeComponent();
            //解像度縦1000以上、幅2000以上なら半分にする
            if (f1.dstbitmap.Width > 2000 || f1.dstbitmap.Height > 1000)
            {
                //解像度縦1500以上、幅3000以上なら半分にする
                if (f1.dstbitmap.Width > 3000 || f1.dstbitmap.Height > 1500)
                {
                    this.Size = new Size(f1.dstbitmap.Width / 3, f1.dstbitmap.Height / 3);
                }
                else this.Size = new Size(f1.dstbitmap.Width/2, f1.dstbitmap.Height/2);
            }
            else this.Size = new Size(f1.dstbitmap.Width, f1.dstbitmap.Height);

            if (File.Exists(System.IO.Path.GetTempPath() + "\\temp.jpg") == true)
            {
                tukau = new Bitmap(System.IO.Path.GetTempPath() + "\\temp.jpg");
                this.pictureBox1.Image = tukau;
                
            }
            else
            {
                tukau = new Bitmap(f1.dstbitmap);
                this.pictureBox1.Image = tukau;
            }
 
        }
        //デストラクター
        ~Form2()
        {
            if (File.Exists(System.IO.Path.GetTempPath() + "\\temp.jpg") == true)
            {
                System.IO.File.Delete(System.IO.Path.GetTempPath() + "\\temp.jpg");
            }
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //(   ).PointToClient(Cursor.Position);でそのコントロールでのマウスのカーソルの座標的な
            Point formClientCurPos = pictureBox1.PointToClient(Cursor.Position);
            double X = formClientCurPos.X;
            double Y = formClientCurPos.Y; 
            //画像自体の比率（アスペクト比率）
            double gazouratio = (double)f1.dstbitmap.Height / (double)f1.dstbitmap.Width;
            //ウィンドウの比率
            double windowratio = (double)pictureBox1.Height / (double)pictureBox1.Width;
            bool flag = false;
            //windowratio==gazouratioのとき、ウィンドウと画像の大きさがぴったり一緒。
            
            //横にマージンがある場合
            if (gazouratio > windowratio)
            {
                double margine = ((double)pictureBox1.Width - ((double)pictureBox1.Height / (double)f1.dstbitmap.Height) * (double)f1.dstbitmap.Width) / 2;
                //MessageBox.Show("横がのびてますわ  " + margine.ToString());
                 X -= margine;
                 flag = false;
            }
            //縦にマージンがある場合
            else
            {
                double margine = ((double)pictureBox1.Height - ((double)pictureBox1.Width / (double)f1.dstbitmap.Width) * (double)f1.dstbitmap.Height) / 2;
                //MessageBox.Show("縦がのびてるお  " + margine.ToString());
                Y -= margine;
                flag = true;
            }
            //横にマージンがある場合は(double)pictureBox1.Height / (double)f1.dstbitmap.Heighが正しい比率なので、ここで分岐
            //縦の場合は(double)pictureBox1.Width / (double)f1.dstbitmap.Width;
            if (flag == false)
            {
                double a = (double)pictureBox1.Height / (double)f1.dstbitmap.Height;
                X /= a;
                double c = (double)pictureBox1.Height / (double)f1.dstbitmap.Height;
                Y /= c;
            }
            else
            {
                double a = (double)pictureBox1.Width / (double)f1.dstbitmap.Width;
                X /= a;
                double c = (double)pictureBox1.Width / (double)f1.dstbitmap.Width;
                Y /= c;
            }
            //マージン内でクリックした際に、関数を抜ける処理
            if (X < 0 || Y < 0) return;
            if (X > f1.dstbitmap.Width || Y > f1.dstbitmap.Height) return;
            //引数で使うのはintなのでintに仕方なくキャスト
            int x = (int)X;
            int y = (int)Y;



            //Graphics g = Graphics.FromHwnd(this.Handle);
            using (Graphics g = Graphics.FromImage(tukau))
            {
                Color color=Color.FromArgb(f1.trackBar1.Value, f1.trackBar2.Value, f1.trackBar3.Value, f1.trackBar4.Value);
                //g.DrawImage(dstbitmap, 0, 0, 0.5F * dstbitmap.Width, 0.5F * dstbitmap.Height);
                //int kk=f1.numericUpDown1.Value
                Brush b = new SolidBrush(color);
                f1.f = new Font("Arial", (float)f1.numericUpDown2.Value);
                //if (f1.checkBox6.Checked)
                //{
                //    f = new Font(f, FontStyle.Bold);
                //}
                //if (f1.checkBox7.Checked)
                //{
                //    f = new Font(f, FontStyle.Italic);
                //}
                //if (f1.checkBox8.Checked)
                //{
                //    f = new Font(f, FontStyle.Underline);
                //}
                int yy = 0;
                int bairitu = 0;
                f1.stArrayData = f1.textBox1.Text.Split('\n');
                foreach (string stData in f1.stArrayData)
                {
                    g.DrawString(stData, f1.f, b, x, y + bairitu * (float)f1.numericUpDown2.Value + yy);
                    bairitu++;
                    yy += 20;
                }

                //g.DrawString("アフィブログ", f1.f, b, x, y);
                //g.DrawString("転載禁止", f1.f, b, x, y + (float)f1.numericUpDown2.Value + 20);
                //g.DrawString("m9(^Д^)ﾌﾟｷﾞｬｰ", f1.f, b, x, y + (float)f1.numericUpDown2.Value * 2 + 40);
                b.Dispose();
                if (File.Exists(System.IO.Path.GetTempPath() + "\\temp.jpg") == true)
                {
                    this.pictureBox1.Image = tukau;
                }
                else
                {
                    this.pictureBox1.Image = tukau;
                }
            }

        }
        //保存ボタン
        private void button1_Click(object sender, EventArgs e)
        {
            while (File.Exists(f1.savepath))
            {
                f1.counter++;
                if (f1.zibunnde == false)
                {
                    if (f1.radioButton8.Checked == true)
                    {
                        f1.label15.Text = System.Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\vip" + f1.counter + ".jpg";
                        f1.savepath = f1.label15.Text;
                    }
                    else
                    {
                        f1.label15.Text = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\vip" + f1.counter + ".jpg";
                        f1.savepath = f1.label15.Text;
                    }
                }
                else
                {
                    //自分で指定した際のコード
                    f1.zibunndesavepath = f1.zibunndesavepath.Substring(0, f1.zibunndesavepath.LastIndexOf("\\")) + "\\vip" + f1.counter + ".jpg";
                    f1.label15.Text = f1.zibunndesavepath;
                    f1.savepath = f1.zibunndesavepath;
                }
            }
            //f1.SaveImage(f1.trackBar5.Value,true);
            //f1.dstbitmap.Save(f1.savepath,System.Drawing.Imaging.ImageFormat.Jpeg);
            pictureBox1.Image.Save(f1.savepath, System.Drawing.Imaging.ImageFormat.Jpeg);
            this.Close();
            if (File.Exists(System.IO.Path.GetTempPath() + "\\temp.jpg") == true)
            {
                System.IO.File.Delete(System.IO.Path.GetTempPath() + "\\temp.jpg");
            }
            if (MessageBox.Show("アップロードしていいですか？？", "質問", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                f1.image_upload();
            }
  
            
        }
    }
}
