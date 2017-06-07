namespace Client
{
    partial class ClientForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnConnectAndReceive = new System.Windows.Forms.Button();
            this.listBoxClient = new System.Windows.Forms.ListBox();
            this.txtSendTo = new System.Windows.Forms.RichTextBox();
            this.btnSendTextToClient = new System.Windows.Forms.Button();
            this.txtPublicKey = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnConnectAndReceive
            // 
            this.btnConnectAndReceive.AutoSize = true;
            this.btnConnectAndReceive.Location = new System.Drawing.Point(65, 84);
            this.btnConnectAndReceive.Name = "btnConnectAndReceive";
            this.btnConnectAndReceive.Size = new System.Drawing.Size(116, 23);
            this.btnConnectAndReceive.TabIndex = 1;
            this.btnConnectAndReceive.Text = "ConnectAndReceive";
            this.btnConnectAndReceive.UseVisualStyleBackColor = true;
            this.btnConnectAndReceive.Click += new System.EventHandler(this.btnConnectAndReceive_Click);
            // 
            // listBoxClient
            // 
            this.listBoxClient.FormattingEnabled = true;
            this.listBoxClient.Location = new System.Drawing.Point(12, 226);
            this.listBoxClient.Name = "listBoxClient";
            this.listBoxClient.Size = new System.Drawing.Size(597, 251);
            this.listBoxClient.TabIndex = 2;
            this.listBoxClient.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBoxClient_MouseDoubleClick);
            // 
            // txtSendTo
            // 
            this.txtSendTo.Location = new System.Drawing.Point(12, 126);
            this.txtSendTo.Name = "txtSendTo";
            this.txtSendTo.Size = new System.Drawing.Size(388, 66);
            this.txtSendTo.TabIndex = 5;
            this.txtSendTo.Text = "";
            // 
            // btnSendTextToClient
            // 
            this.btnSendTextToClient.Location = new System.Drawing.Point(428, 141);
            this.btnSendTextToClient.Name = "btnSendTextToClient";
            this.btnSendTextToClient.Size = new System.Drawing.Size(75, 23);
            this.btnSendTextToClient.TabIndex = 6;
            this.btnSendTextToClient.Text = "SendBackMessage";
            this.btnSendTextToClient.UseVisualStyleBackColor = true;
            this.btnSendTextToClient.Click += new System.EventHandler(this.btnSendTextToClient_Click);
            // 
            // txtPublicKey
            // 
            this.txtPublicKey.Location = new System.Drawing.Point(106, 2);
            this.txtPublicKey.Name = "txtPublicKey";
            this.txtPublicKey.Size = new System.Drawing.Size(465, 20);
            this.txtPublicKey.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Public Key";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Password";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(106, 40);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(324, 20);
            this.txtPassword.TabIndex = 10;
            // 
            // ClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(621, 487);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPublicKey);
            this.Controls.Add(this.btnSendTextToClient);
            this.Controls.Add(this.txtSendTo);
            this.Controls.Add(this.listBoxClient);
            this.Controls.Add(this.btnConnectAndReceive);
            this.Name = "ClientForm";
            this.Text = "ClientForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnConnectAndReceive;
        private System.Windows.Forms.ListBox listBoxClient;
        private System.Windows.Forms.RichTextBox txtSendTo;
        private System.Windows.Forms.Button btnSendTextToClient;
        private System.Windows.Forms.TextBox txtPublicKey;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPassword;
    }
}

