using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab5
{
    public partial class Form1 : Form
    {
        private Image image = Image.FromFile("../../rpuxa.jpg");

        public Form1()
        {
            InitializeComponent();

            pictureBox1.Image = image;
            pictureBox2.Image = ConvertImage(image);
            FillDataGrid(GetBitmapFromImage(image));
        }

        private Image ConvertImage(Image image)
        {
            Bitmap bitmap = new Bitmap(GetBitmapFromImage(image));
            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    Color convertedPixel = Negative(bitmap.GetPixel(x, y));
                    bitmap.SetPixel(x, y, convertedPixel);
                }
            }
            return bitmap;
        }

        private void FillDataGrid(Bitmap bitmap)
        {
            int width = bitmap.Width;
            int height = bitmap.Height;
            string[][] rows = new string[height][];

            dataGridView1.ColumnCount = width;

            
            for (int y = 0; y < height; y++)
            {
                string[] row = new string[width];
                for (int x = 0; x < width; x++)
                {
                    row[x] = ColorToString(bitmap.GetPixel(x, y));
                }
                rows[y] = row;
            }

            Console.WriteLine(rows);
            foreach (string[] row in rows)
            {
                dataGridView1.Rows.Add(row);
            }
        }
     
        private string ColorToString(Color color)
        {
            return String.Format("{0} - {1} - {2}", color.R, color.G, color.B);
        }

        // занято
        private Color Negative(Color color)
        {
            return Color.FromArgb(255 - color.R, 255 - color.G, 255 - color.B);
        }

        private Color Absolutize(Color color)
        {
            int red = color.R;
            int green = color.G;
            int blue = color.B;
            if (blue > red && blue > green)
            {
                return Color.FromArgb(0, 0, 255);
            }
            if (red > blue && red > green)
            {
                return Color.FromArgb(255, 0, 0);
            }
            if (green > red && green > red)
            {
                return Color.FromArgb(0, 255, 0);
            }

            return color;
        }

        private Color Dark(Color color)
        {
            int value = (color.R + color.G + color.B) / 3;

            return Color.FromArgb(value, value, value);
        }



        private Bitmap GetBitmapFromImage(Image image)
        {
            return (Bitmap)image;
        }
    }
}
