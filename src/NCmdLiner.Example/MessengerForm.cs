using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace NCmdLiner.Example
{
    public partial class MessengerForm : Form
    {
        public MessengerForm(string message)
        {            
            InitializeComponent();
            _messageRichTextBox.Text = message;            
        }
    }
}
