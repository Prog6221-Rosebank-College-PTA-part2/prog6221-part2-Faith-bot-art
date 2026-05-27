using CyberChatbot_Part2;
using System;
using System.Windows.Forms;

namespace CybersecurityChatbot
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Chatbotform());
        }
    }
}