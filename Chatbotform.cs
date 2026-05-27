using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CyberChatbot_Part2
{
    public partial class Chatbotform : Form
    {
        public Chatbotform()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            SuspendLayout();
            // 
            // Chatbotform
            // 
            ClientSize = new Size(1165, 704);
            Name = "Chatbotform";
            ResumeLayout(false);

        }
    }
}
