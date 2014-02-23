﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Week3
{
    public partial class ImagePreviewForm : Form
    {
        public ImagePreviewForm(Bitmap InputImage)
        {
            InitializeComponent();
            if (InputImage != null)
            {
                PreviewPictureBox.Image = InputImage;
            }
        }
    }
}
