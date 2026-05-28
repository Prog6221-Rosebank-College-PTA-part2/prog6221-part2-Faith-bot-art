using System;
using System.Drawing;
using System.Speech.Synthesis;
using System.Windows.Forms;

namespace CybersecurityChatbot
{
    public partial class Chatbotform : Form
    {
        private ChatEngine chatbot;
        private SpeechSynthesizer speechSynthesizer;

        // Control declarations
        private RichTextBox rtxtChatDisplay;
        private TextBox txtUserInput;
        private Button btnSend;
        private Button btnClear;
        private Button btnSpeak;
        private Label lblStatus;
        private Label lblAsciiArt;

        public Chatbotform()
        {
            InitializeComponent();
            InitializeChatbot();
            InitializeVoice();
        }

        private void InitializeComponent()
        {
            // ============ FORM PROPERTIES ============
            this.Text = "Cyberbot Assistant";
            this.Size = new Size(900, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(18, 25, 45);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = true;

            // ============ ASCII ART LABEL ============
            lblAsciiArt = new Label();
            lblAsciiArt.Location = new Point(20, 15);
            lblAsciiArt.Size = new Size(850, 120);
            lblAsciiArt.Font = new Font("Consolas", 7);
            lblAsciiArt.ForeColor = Color.FromArgb(0, 255, 255);
            lblAsciiArt.TextAlign = ContentAlignment.MiddleCenter;
            lblAsciiArt.Text = @"
╔══════════════════════════════════════════════════════════════════════════════════════════════╗
║                                         CYBERBOT                                              ║
║     ██████╗██╗   ██╗██████╗ ███████╗██████╗  ██████╗ ████████╗                              ║
║    ██╔════╝╚██╗ ██╔╝██╔══██╗██╔════╝██╔══██╗██╔═══██╗╚══██╔══╝                              ║
║    ██║      ╚████╔╝ ██████╔╝█████╗  ██████╔╝██║   ██║   ██║                                 ║
║    ██║       ╚██╔╝  ██╔══██╗██╔══╝  ██╔══██╗██║   ██║   ██║                                 ║
║    ╚██████╗   ██║   ██████╔╝███████╗██║  ██║╚██████╔╝   ██║                                 ║
║     ╚═════╝   ╚═╝   ╚═════╝ ╚══════╝╚═╝  ╚═╝ ╚═════╝    ╚═╝                                 ║
║                                                                                              ║
║                           Your Personal Cyberbot Assistant                                   ║
╚══════════════════════════════════════════════════════════════════════════════════════════════╝";

            // ============ CHAT DISPLAY RICH TEXTBOX ============
            rtxtChatDisplay = new RichTextBox();
            rtxtChatDisplay.Location = new Point(20, 150);
            rtxtChatDisplay.Size = new Size(850, 400);
            rtxtChatDisplay.BackColor = Color.FromArgb(20, 25, 40);
            rtxtChatDisplay.ForeColor = Color.White;
            rtxtChatDisplay.Font = new Font("Segoe UI", 10);
            rtxtChatDisplay.ReadOnly = true;
            rtxtChatDisplay.BorderStyle = BorderStyle.FixedSingle;
            rtxtChatDisplay.WordWrap = true;

            // ============ USER INPUT TEXTBOX ============
            txtUserInput = new TextBox();
            txtUserInput.Location = new Point(20, 565);
            txtUserInput.Size = new Size(620, 27);
            txtUserInput.BackColor = Color.FromArgb(35, 40, 55);
            txtUserInput.ForeColor = Color.White;
            txtUserInput.BorderStyle = BorderStyle.FixedSingle;
            txtUserInput.Font = new Font("Segoe UI", 10);
            txtUserInput.KeyPress += TxtUserInput_KeyPress;

            // ============ SEND BUTTON ============
            btnSend = new Button();
            btnSend.Text = "SEND";
            btnSend.Location = new Point(650, 563);
            btnSend.Size = new Size(90, 32);
            btnSend.BackColor = Color.FromArgb(0, 120, 215);
            btnSend.ForeColor = Color.White;
            btnSend.FlatStyle = FlatStyle.Flat;
            btnSend.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnSend.Cursor = Cursors.Hand;
            btnSend.Click += BtnSend_Click;

            // ============ CLEAR BUTTON ============
            btnClear = new Button();
            btnClear.Text = "CLEAR";
            btnClear.Location = new Point(750, 563);
            btnClear.Size = new Size(90, 32);
            btnClear.BackColor = Color.FromArgb(200, 80, 80);
            btnClear.ForeColor = Color.White;
            btnClear.FlatStyle = FlatStyle.Flat;
            btnClear.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnClear.Cursor = Cursors.Hand;
            btnClear.Click += BtnClear_Click;

            // ============ SPEAK BUTTON (VOICE) ============
            btnSpeak = new Button();
            btnSpeak.Text = "SPEAK";
            btnSpeak.Location = new Point(20, 600);
            btnSpeak.Size = new Size(90, 32);
            btnSpeak.BackColor = Color.FromArgb(80, 140, 80);
            btnSpeak.ForeColor = Color.White;
            btnSpeak.FlatStyle = FlatStyle.Flat;
            btnSpeak.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnSpeak.Cursor = Cursors.Hand;
            btnSpeak.Click += BtnSpeak_Click;

            // ============ STATUS LABEL ============
            lblStatus = new Label();
            lblStatus.Location = new Point(120, 605);
            lblStatus.Size = new Size(750, 25);
            lblStatus.ForeColor = Color.FromArgb(150, 255, 150);
            lblStatus.Font = new Font("Segoe UI", 9);
            lblStatus.Text = "[STATUS: READY] - Ask Cyberbot about passwords, scams, privacy, or phishing";

            // ============ ADD ALL CONTROLS TO FORM ============
            this.Controls.Add(lblAsciiArt);
            this.Controls.Add(rtxtChatDisplay);
            this.Controls.Add(txtUserInput);
            this.Controls.Add(btnSend);
            this.Controls.Add(btnClear);
            this.Controls.Add(btnSpeak);
            this.Controls.Add(lblStatus);
        }

        private void InitializeChatbot()
        {
            chatbot = new ChatEngine();
            AddWelcomeMessage();
        }

        private void InitializeVoice()
        {
            try
            {
                speechSynthesizer = new SpeechSynthesizer();
            }
            catch (Exception)
            {
                AppendToChat("SYSTEM", "[Voice features not available on this system]", Color.FromArgb(255, 200, 100));
            }
        }

        private void AddWelcomeMessage()
        {
            string welcome = "═══════════════════════════════════════════════════════════════════════════════════\n" +
                            "                                    WELCOME TO CYBERBOT                                 \n" +
                            "═══════════════════════════════════════════════════════════════════════════════════\n\n" +
                            "I am your personal Cyberbot assistant. I can help you with:\n\n" +
                            "  [*] PASSWORD SECURITY - Create and manage strong passwords\n" +
                            "  [*] SCAM DETECTION - Identify and avoid online scams\n" +
                            "  [*] PRIVACY PROTECTION - Keep your personal data safe\n" +
                            "  [*] PHISHING AWARENESS - Spot fake emails and messages\n\n" +
                            "═══════════════════════════════════════════════════════════════════════════════════\n" +
                            "                                    HOW TO USE                                        \n" +
                            "═══════════════════════════════════════════════════════════════════════════════════\n\n" +
                            "  • Ask about any topic: 'Tell me about password safety'\n" +
                            "  • Tell me your name: 'My name is John'\n" +
                            "  • Tell me your interest: 'I am interested in privacy'\n" +
                            "  • Ask for more tips: 'Tell me more' or 'Another tip'\n" +
                            "  • Express your feelings: 'I am worried about scams'\n\n" +
                            "═══════════════════════════════════════════════════════════════════════════════════\n" +
                            "                         How can Cyberbot help you stay safe online today?          \n" +
                            "═══════════════════════════════════════════════════════════════════════════════════";

            AppendToChat("SYSTEM", welcome, Color.FromArgb(100, 255, 100));
        }

        private void BtnSend_Click(object sender, EventArgs e)
        {
            ProcessUserInput();
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            rtxtChatDisplay.Clear();
            AddWelcomeMessage();
            chatbot.ResetConversation();
            AppendToChat("SYSTEM", "Chat history cleared. Starting fresh conversation.", Color.FromArgb(255, 200, 100));
        }

        private void BtnSpeak_Click(object sender, EventArgs e)
        {
            string userInput = txtUserInput.Text.Trim();
            if (!string.IsNullOrWhiteSpace(userInput))
            {
                try
                {
                    speechSynthesizer?.SpeakAsync(userInput);
                }
                catch (Exception) { }
            }
        }

        private void TxtUserInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                ProcessUserInput();
                e.Handled = true;
            }
        }

        private void ProcessUserInput()
        {
            string userInput = txtUserInput.Text.Trim();
            if (string.IsNullOrWhiteSpace(userInput))
                return;

            AppendToChat("YOU", userInput, Color.FromArgb(255, 255, 150));
            txtUserInput.Clear();

            lblStatus.Text = "[STATUS: PROCESSING] - Analyzing your input...";
            lblStatus.ForeColor = Color.Yellow;
            Application.DoEvents();

            string response = chatbot.GetResponse(userInput);
            AppendToChat("CYBERBOT", response, Color.FromArgb(100, 200, 255));

            // Speak the response
            try
            {
                speechSynthesizer?.SpeakAsync(response);
            }
            catch (Exception) { }

            lblStatus.Text = "[STATUS: READY] - Ask Cyberbot about passwords, scams, privacy, or phishing";
            lblStatus.ForeColor = Color.FromArgb(150, 255, 150);
        }

        private void AppendToChat(string sender, string message, Color color)
        {
            if (rtxtChatDisplay.InvokeRequired)
            {
                rtxtChatDisplay.Invoke(new Action(() => AppendToChat(sender, message, color)));
                return;
            }

            rtxtChatDisplay.SelectionStart = rtxtChatDisplay.TextLength;
            rtxtChatDisplay.SelectionLength = 0;
            rtxtChatDisplay.SelectionColor = color;
            rtxtChatDisplay.AppendText("[" + sender + "] ");
            rtxtChatDisplay.SelectionColor = Color.White;
            rtxtChatDisplay.AppendText(message + "\n\n");
            rtxtChatDisplay.ScrollToCaret();
        }
    }
}