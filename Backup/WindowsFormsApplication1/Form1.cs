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
    public partial class Form1 : Form
    {
        public Bitmap dstbitmap;
        double Mag = 1.0F;
        public int counter = 1;
        public string savepath;
        public Font f;
        public string[] stArrayData;
        public bool zibunnde = false;
        public string zibunndesavepath;
        public Form1()
        {
            InitializeComponent();
        }
        #region フォーム系
        private void Form1_Load(object sender, EventArgs e)
        {
            //初期値をRGBAテキストボックスに代入・プレビューのためのpictureboxにも代入
            textBox2.Text = trackBar1.Value.ToString("X2") + trackBar2.Value.ToString("X2") + trackBar3.Value.ToString("X2") + trackBar4.Value.ToString("X2");
            pictureBox2.BackColor = Color.FromArgb(trackBar1.Value, trackBar2.Value, trackBar3.Value, trackBar4.Value);
            //カラーパレット初期化
            colorDialog1.AllowFullOpen = true;
            //保存場所記入
            label15.Text = System.Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)+"\\vip"+counter+".jpg";
            savepath = label15.Text;
            //フォント設z定
            f = new Font("Arial", (float)numericUpDown2.Value);
        }
        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop) && checkBox4.Checked == false)
                e.Effect = DragDropEffects.Copy;
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string filename = ((string[])e.Data.GetData(DataFormats.FileDrop))[0];
            Bitmap srcbitmap = new Bitmap(filename);
            string profi = Environment.GetEnvironmentVariable("USERPROFILE") + "\\Documents\\vip.jpg";

            //int width = srcbitmap.Width;
            //int height = srcbitmap.Height;

            int width = (int)Math.Floor(srcbitmap.Width / Mag);
            int height = (int)Math.Floor(srcbitmap.Height / Mag);
            dstbitmap = new Bitmap(srcbitmap, width, height);




            //pictureBox1.Image = (Image)dstbitmap;
            bool ooo = false;
            if (trackBar5.Value == 100) { ooo = true; }
            if (checkBox2.Checked == true)
            {
                SaveImage(trackBar5.Value, ooo);
                Form2 preview = new Form2(this);
                preview.Show();
            }
            else
            {
                if (checkBox3.Checked == false)
                {
                    using (Graphics g = Graphics.FromImage(dstbitmap))
                    {
                        Color color = Color.FromArgb(trackBar1.Value, trackBar2.Value, trackBar3.Value, trackBar4.Value);
                        Brush b = new SolidBrush(color);
                        f = new Font("Arial", (float)numericUpDown2.Value);
                        int yy = 0;
                        int bairitu = 0;
                        stArrayData = textBox1.Text.Split('\n');
                        foreach (string stData in stArrayData)
                        {
                            g.DrawString(stData, f, b, int.Parse(textBox3.Text), int.Parse(textBox4.Text) + bairitu * (float)numericUpDown2.Value + yy);
                            bairitu++;
                            yy += 20;
                        }
                        b.Dispose();
                        f.Dispose();
                    }
                }
                while (File.Exists(savepath))
                {
                    counter++;
                    if (zibunnde == false)
                    {
                        if (radioButton8.Checked == true)
                        {
                            label15.Text = System.Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\vip" + counter + ".jpg";
                            savepath = label15.Text;
                        }
                        else 
                        { 
                            label15.Text = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\vip" + counter + ".jpg";
                            savepath = label15.Text;
                        }
                    }
                    else
                    {
                        //自分で保存場所指定した際のコード
                        zibunndesavepath=zibunndesavepath.Substring(0, zibunndesavepath.LastIndexOf("\\"))+"\\vip"+counter+".jpg";
                        label15.Text = zibunndesavepath;
                        savepath = zibunndesavepath;
                    }
                }
                //false=100以外（圧縮する）
                if (ooo == false)
                {
                    SaveImage(trackBar5.Value, ooo);
                    Bitmap temp = new Bitmap(System.IO.Path.GetTempPath() + "\\temp.jpg");
                    temp.Save(savepath);
                    temp.Dispose();
                    System.IO.File.Delete(System.IO.Path.GetTempPath() + "\\temp.jpg");
                }
                else
                {
                    dstbitmap.Save(savepath,System.Drawing.Imaging.ImageFormat.Jpeg);
                }
                //dstbitmap.Save(savepath, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            savepath = label15.Text;
        }
        #endregion

        #region サイズ変更系
        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            Mag = 1.0;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            Mag = (1 / 0.75);
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            Mag = (1 / 0.5);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Mag = (1 / 0.25);
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            Mag = (1 / (double)numericUpDown1.Value);
        }
        #endregion

        #region 文字設定系
        //A
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            previewcolor();
        }
        //R
        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            previewcolor();
        }
        //G
        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            previewcolor();
        }
        //B
        private void trackBar4_Scroll(object sender, EventArgs e)
        {
            previewcolor();
        }

        public void previewcolor()
        {
            
            textBox2.Text = trackBar1.Value.ToString("X2") + trackBar2.Value.ToString("X2") + trackBar3.Value.ToString("X2") + trackBar4.Value.ToString("X2");
            pictureBox2.BackColor = Color.FromArgb(trackBar1.Value, trackBar2.Value, trackBar3.Value, trackBar4.Value);

        }
        //黄色
        private void button2_Click(object sender, EventArgs e)
        {
            trackBar1.Value = 255;
            trackBar2.Value = 255;
            trackBar3.Value = 255;
            trackBar4.Value = 0;
            textBox2.Text = trackBar1.Value.ToString("X2") + trackBar2.Value.ToString("X2") + trackBar3.Value.ToString("X2") + trackBar4.Value.ToString("X2");
            pictureBox2.BackColor = Color.FromArgb(trackBar1.Value, trackBar2.Value, trackBar3.Value, trackBar4.Value);

        }
        //灰
        private void button3_Click(object sender, EventArgs e)
        {
            trackBar1.Value = 255;
            trackBar2.Value = 128;
            trackBar3.Value = 128;
            trackBar4.Value = 128;
            textBox2.Text = trackBar1.Value.ToString("X2") + trackBar2.Value.ToString("X2") + trackBar3.Value.ToString("X2") + trackBar4.Value.ToString("X2");
            pictureBox2.BackColor = Color.FromArgb(trackBar1.Value, trackBar2.Value, trackBar3.Value, trackBar4.Value);

        }
        //白
        private void button4_Click(object sender, EventArgs e)
        {
            trackBar1.Value = 255;
            trackBar2.Value = 255;
            trackBar3.Value = 255;
            trackBar4.Value = 255;
            textBox2.Text = trackBar1.Value.ToString("X2") + trackBar2.Value.ToString("X2") + trackBar3.Value.ToString("X2") + trackBar4.Value.ToString("X2");
            pictureBox2.BackColor = Color.FromArgb(trackBar1.Value, trackBar2.Value, trackBar3.Value, trackBar4.Value);

        }
        //黒
        private void button5_Click(object sender, EventArgs e)
        {
            trackBar1.Value = 255;
            trackBar2.Value = 0;
            trackBar3.Value = 0;
            trackBar4.Value = 0;
            textBox2.Text = trackBar1.Value.ToString("X2") + trackBar2.Value.ToString("X2") + trackBar3.Value.ToString("X2") + trackBar4.Value.ToString("X2");
            pictureBox2.BackColor = Color.FromArgb(trackBar1.Value, trackBar2.Value, trackBar3.Value, trackBar4.Value);

        }
        //カラーパレットボタン
        private void button6_Click(object sender, EventArgs e)
        {
            colorDialog1.AnyColor = true;
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox2.BackColor = colorDialog1.Color;
                textBox2.Text = colorDialog1.Color.ToArgb().ToString("X2");
                //System.Globalization.StringInfo si = new System.Globalization.StringInfo(textBox2.Text);
                //MessageBox.Show(si.SubstringByTextElements(0, 2));
                //string hexOutput = String.Format("{0:X}", textBox2.Text);
                //MessageBox.Show(hexOutput);
                trackBar1.Value = Convert.ToInt32(textBox2.Text.Substring(0, 2), 16);
                trackBar2.Value = Convert.ToInt32(textBox2.Text.Substring(2, 2), 16);
                trackBar3.Value = Convert.ToInt32(textBox2.Text.Substring(4, 2), 16);
                trackBar4.Value = Convert.ToInt32(textBox2.Text.Substring(6, 2), 16);
            }
        }
        //プレビュー画面開くかON/OFF
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox2.Checked == true)
            {
                groupBox3.Enabled = false;
            }
            else
            {
                groupBox3.Enabled = true;
            }
        }

        //文字を挿入するかしないか　ON/OFF
        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            this.SetBounds(this.Location.X, this.Location.Y, 335, this.Size.Height);
            label1.Text = "画像をドラッグ＆ドロップ(どこでも可)";

            if (this.checkBox3.Checked == true)
            {
                //ウィンドウさいずを小さく
                this.SetBounds(this.Location.X, this.Location.Y, 335, this.Size.Height);
                //ラベルに文字が入りきらないので短く
                label1.Text = "画像をドラッグ＆ドロップ(どこでも可)";
                checkBox2.Checked = false;
                textBox1.Enabled = false;
                label5.Enabled = false;
                groupBox2.Enabled = false;
                groupBox3.Enabled = false;
                // else groupBox3.Enabled = true;
                checkBox2.Enabled = false;
                this.checkBox3.Enabled = true;
            }
            else
            {
                this.SetBounds(this.Location.X, this.Location.Y, 685, this.Size.Height);
                label1.Text = "画 像 を ド ラ ッ グ ＆ ド ロ ッ プ し て く だ さ い ( ど こ で も 可 )";
                textBox1.Enabled = true;
                label5.Enabled = true;
                groupBox2.Enabled = true;
                if (checkBox2.Checked == true) groupBox3.Enabled = false;
                else groupBox3.Enabled = true;
                checkBox2.Enabled = true;
            }

        }

        //フォントのダイアログ
        private void button8_Click(object sender, EventArgs e)
        {
            // FontDialog の新しいインスタンスを生成する (デザイナから追加している場合は必要ない)
            FontDialog fontDialog1 = new FontDialog();

            // 初期選択するフォントを設定する
            fontDialog1.Font = textBox1.Font;

            // 初期選択する色を設定する
            fontDialog1.Color = textBox1.ForeColor;

            // 選択可能なフォントサイズの最大値を設定する
            fontDialog1.MaxSize = 200;

            // 選択可能なフォントサイズの最小値を設定する
            fontDialog1.MinSize = 1;

            // 存在しないフォントやスタイルを選択すると警告を表示する (初期値 false)
            fontDialog1.FontMustExist = true;

            // 色を選択できるようにする (初期値 false)
            fontDialog1.ShowColor = true;

            // 取り消し線、下線、テキストの色などのオプションを指定可能にする (初期値 true)
            fontDialog1.ShowEffects = true;

            // [ヘルプ] ボタンを表示する (初期値 false)
            fontDialog1.ShowHelp = true;

            // [適用] ボタンを表示する (初期値 false)
            fontDialog1.ShowApply = true;

            // 非 OEM 文字セット、Symbol 文字セット、ANSI 文字セットを表示する (初期値 false)
            fontDialog1.ScriptsOnly = true;

            // 固定ピッチフォント (等幅フォント) だけを表示する (初期値 false)
            fontDialog1.FixedPitchOnly = true;

            // ダイアログを表示し、戻り値が [OK] の場合は選択したフォントを textBox1 に適用する
            if (fontDialog1.ShowDialog() == DialogResult.OK)
            {
                f = fontDialog1.Font;
                // = fontDialog1.Color;
            }

            // 不要になった時点で破棄する (正しくは オブジェクトの破棄を保証する を参照)
            fontDialog1.Dispose();
        }
        #endregion

        #region 保存場所系
        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {
            zibunnde = false;
            label15.Text = System.Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\vip" + counter + ".jpg";
            savepath = label15.Text;
        }
        private void radioButton9_CheckedChanged(object sender, EventArgs e)
        {
            zibunnde = false;
            label15.Text = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\vip" + counter + ".jpg";
            savepath = label15.Text;
        }
        private void button7_Click(object sender, EventArgs e)
        {
            radioButton8.Checked = false;
            radioButton9.Checked = false;
            zibunnde = true;
            SaveFileDialog ofd = new SaveFileDialog();
            ofd.FileName = "vip.jpg";
            ofd.InitialDirectory = @"C:\";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                //OKボタンがクリックされたとき
                //選択されたファイル名を表示する
                label15.Text = ofd.FileName;
                zibunndesavepath = ofd.FileName;
            }

        }
        #endregion

        #region カスタムボタンとか
                private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            groupBox1.Enabled = true;
        }
        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            //全部ﾃﾞﾌｫ
        }
        private void button1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://heya-vip.pelolias.com/");
        }
        #endregion
 
        //画像を保存
        public void SaveImage(long quality,bool hanntei)
        {
            //もしhannteiがfalse(圧縮率100以外)なら。
            if (hanntei == false)
            {
                //EncoderParameterオブジェクトを1つ格納できる
                //EncoderParametersクラスの新しいインスタンスを初期化
                //ここでは品質のみ指定するため1つだけ用意する
                System.Drawing.Imaging.EncoderParameters eps =
                    new System.Drawing.Imaging.EncoderParameters(1);
                //品質を指定
                System.Drawing.Imaging.EncoderParameter ep =
                    new System.Drawing.Imaging.EncoderParameter(
                    System.Drawing.Imaging.Encoder.Quality, quality);
                //EncoderParametersにセットする
                eps.Param[0] = ep;

                //イメージエンコーダに関する情報を取得する
                System.Drawing.Imaging.ImageCodecInfo ici;
                ici = GetEncoderInfo("image/jpeg");

                //新しいファイルの拡張子を取得する
                string ext = ici.FilenameExtension.Split(';')[0];
                ext = System.IO.Path.GetExtension(ext).ToLower();

                //保存する

                
                dstbitmap.Save(System.IO.Path.GetTempPath() + "\\temp.jpg", ici, eps);
            }
            else
            {
                //もしhannteiがfalse(圧縮率100（圧縮しない）)なら。
               // dstbitmap.Save(savepath, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            


        }

        //MimeTypeで指定されたImageCodecInfoを探して返す
        private static System.Drawing.Imaging.ImageCodecInfo GetEncoderInfo(string mineType)
        {
            //GDI+ に組み込まれたイメージ エンコーダに関する情報をすべて取得
            System.Drawing.Imaging.ImageCodecInfo[] encs =
                System.Drawing.Imaging.ImageCodecInfo.GetImageEncoders();
            //指定されたMimeTypeを探して見つかれば返す
            foreach (System.Drawing.Imaging.ImageCodecInfo enc in encs)
                if (enc.MimeType == mineType)
                    return enc;
            return null;
        }
        //ニート
        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked == true)
            {
                groupBox1.Enabled = false;
                groupBox4.Enabled = false;
                groupBox2.Enabled = false;
                label5.Enabled = false;
                textBox1.Enabled = false;
                checkBox3.Enabled = false;
            }
            if (checkBox4.Checked == false)
            {
                groupBox1.Enabled = true;
                groupBox4.Enabled = true;
                groupBox2.Enabled = true;
                label5.Enabled = true;
                textBox1.Enabled = true;
                checkBox3.Enabled = true;
            }
        }
















    }
}
