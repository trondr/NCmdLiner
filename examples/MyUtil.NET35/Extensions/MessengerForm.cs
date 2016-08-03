using System.Windows.Forms;

namespace MyUtil.Extensions
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
