﻿namespace InWFExample
{
    partial class MainForm
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
            this.btnCreateAuthorization = new System.Windows.Forms.Button();
            this.txtRequestToken = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtRequestTokenSecret = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnAuthorize = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnGetAcToken = new System.Windows.Forms.Button();
            this.txtOauth_token = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtOauth_verify = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtOauth_Access_token = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtOauth_Access_tokenSecret = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtAPIResponse = new System.Windows.Forms.TextBox();
            this.btnGetProfileDetail = new System.Windows.Forms.Button();
            this.btnUpdateStatus = new System.Windows.Forms.Button();
            this.txtNewStatus = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCreateAuthorization
            // 
            this.btnCreateAuthorization.Location = new System.Drawing.Point(8, 27);
            this.btnCreateAuthorization.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCreateAuthorization.Name = "btnCreateAuthorization";
            this.btnCreateAuthorization.Size = new System.Drawing.Size(288, 28);
            this.btnCreateAuthorization.TabIndex = 1;
            this.btnCreateAuthorization.Text = "Create Authorization";
            this.btnCreateAuthorization.UseVisualStyleBackColor = true;
            this.btnCreateAuthorization.Click += new System.EventHandler(this.btnCreateAuthorization_Click);
            // 
            // txtRequestToken
            // 
            this.txtRequestToken.Location = new System.Drawing.Point(549, 23);
            this.txtRequestToken.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtRequestToken.Name = "txtRequestToken";
            this.txtRequestToken.Size = new System.Drawing.Size(309, 22);
            this.txtRequestToken.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(343, 27);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(154, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "oAuth Request Token :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(343, 70);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(195, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "oAuth Request TokenSecret :";
            // 
            // txtRequestTokenSecret
            // 
            this.txtRequestTokenSecret.Location = new System.Drawing.Point(549, 66);
            this.txtRequestTokenSecret.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtRequestTokenSecret.Name = "txtRequestTokenSecret";
            this.txtRequestTokenSecret.Size = new System.Drawing.Size(309, 22);
            this.txtRequestTokenSecret.TabIndex = 5;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnAuthorize);
            this.groupBox1.Controls.Add(this.txtRequestToken);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnCreateAuthorization);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtRequestTokenSecret);
            this.groupBox1.Location = new System.Drawing.Point(16, 26);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(880, 123);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Authorization";
            // 
            // btnAuthorize
            // 
            this.btnAuthorize.Enabled = false;
            this.btnAuthorize.Location = new System.Drawing.Point(8, 63);
            this.btnAuthorize.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAuthorize.Name = "btnAuthorize";
            this.btnAuthorize.Size = new System.Drawing.Size(288, 28);
            this.btnAuthorize.TabIndex = 7;
            this.btnAuthorize.Text = "Authorize";
            this.btnAuthorize.UseVisualStyleBackColor = true;
            this.btnAuthorize.Click += new System.EventHandler(this.btnAuthorize_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnGetAcToken);
            this.groupBox2.Controls.Add(this.txtOauth_token);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtOauth_verify);
            this.groupBox2.Location = new System.Drawing.Point(24, 178);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Size = new System.Drawing.Size(880, 123);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Authorization";
            // 
            // btnGetAcToken
            // 
            this.btnGetAcToken.Enabled = false;
            this.btnGetAcToken.Location = new System.Drawing.Point(8, 63);
            this.btnGetAcToken.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnGetAcToken.Name = "btnGetAcToken";
            this.btnGetAcToken.Size = new System.Drawing.Size(288, 28);
            this.btnGetAcToken.TabIndex = 7;
            this.btnGetAcToken.Text = "Get Access Token";
            this.btnGetAcToken.UseVisualStyleBackColor = true;
            this.btnGetAcToken.Click += new System.EventHandler(this.btnGetAcToken_Click);
            // 
            // txtOauth_token
            // 
            this.txtOauth_token.Location = new System.Drawing.Point(549, 23);
            this.txtOauth_token.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtOauth_token.Name = "txtOauth_token";
            this.txtOauth_token.Size = new System.Drawing.Size(309, 22);
            this.txtOauth_token.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(343, 70);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "oauth_verifier :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(343, 27);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 17);
            this.label4.TabIndex = 4;
            this.label4.Text = "oauth_token : ";
            // 
            // txtOauth_verify
            // 
            this.txtOauth_verify.Location = new System.Drawing.Point(549, 66);
            this.txtOauth_verify.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtOauth_verify.Name = "txtOauth_verify";
            this.txtOauth_verify.Size = new System.Drawing.Size(309, 22);
            this.txtOauth_verify.TabIndex = 5;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtOauth_Access_token);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.txtOauth_Access_tokenSecret);
            this.groupBox3.Location = new System.Drawing.Point(24, 329);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox3.Size = new System.Drawing.Size(880, 123);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Access Tokens";
            // 
            // txtOauth_Access_token
            // 
            this.txtOauth_Access_token.Location = new System.Drawing.Point(228, 30);
            this.txtOauth_Access_token.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtOauth_Access_token.Name = "txtOauth_Access_token";
            this.txtOauth_Access_token.Size = new System.Drawing.Size(623, 22);
            this.txtOauth_Access_token.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(21, 76);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(193, 17);
            this.label5.TabIndex = 6;
            this.label5.Text = "oauth_Access_tokenSecret : ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(21, 33);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(148, 17);
            this.label6.TabIndex = 4;
            this.label6.Text = "oauth_Access_token :";
            // 
            // txtOauth_Access_tokenSecret
            // 
            this.txtOauth_Access_tokenSecret.Location = new System.Drawing.Point(228, 73);
            this.txtOauth_Access_tokenSecret.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtOauth_Access_tokenSecret.Name = "txtOauth_Access_tokenSecret";
            this.txtOauth_Access_tokenSecret.Size = new System.Drawing.Size(623, 22);
            this.txtOauth_Access_tokenSecret.TabIndex = 5;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtAPIResponse);
            this.groupBox4.Controls.Add(this.btnGetProfileDetail);
            this.groupBox4.Controls.Add(this.btnUpdateStatus);
            this.groupBox4.Controls.Add(this.txtNewStatus);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Location = new System.Drawing.Point(32, 473);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox4.Size = new System.Drawing.Size(880, 261);
            this.groupBox4.TabIndex = 10;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "API Methods to Invoke";
            // 
            // txtAPIResponse
            // 
            this.txtAPIResponse.Location = new System.Drawing.Point(17, 106);
            this.txtAPIResponse.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtAPIResponse.Multiline = true;
            this.txtAPIResponse.Name = "txtAPIResponse";
            this.txtAPIResponse.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtAPIResponse.Size = new System.Drawing.Size(833, 147);
            this.txtAPIResponse.TabIndex = 7;
            // 
            // btnGetProfileDetail
            // 
            this.btnGetProfileDetail.Enabled = false;
            this.btnGetProfileDetail.Location = new System.Drawing.Point(348, 70);
            this.btnGetProfileDetail.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnGetProfileDetail.Name = "btnGetProfileDetail";
            this.btnGetProfileDetail.Size = new System.Drawing.Size(315, 28);
            this.btnGetProfileDetail.TabIndex = 6;
            this.btnGetProfileDetail.Text = "Request User Profile (GET Request Example)";
            this.btnGetProfileDetail.UseVisualStyleBackColor = true;
            this.btnGetProfileDetail.Click += new System.EventHandler(this.btnGetProfileDetail_Click);
            // 
            // btnUpdateStatus
            // 
            this.btnUpdateStatus.Enabled = false;
            this.btnUpdateStatus.Location = new System.Drawing.Point(25, 70);
            this.btnUpdateStatus.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnUpdateStatus.Name = "btnUpdateStatus";
            this.btnUpdateStatus.Size = new System.Drawing.Size(315, 28);
            this.btnUpdateStatus.TabIndex = 5;
            this.btnUpdateStatus.Text = "Send Status Update (PUT Request Example)";
            this.btnUpdateStatus.UseVisualStyleBackColor = true;
            this.btnUpdateStatus.Click += new System.EventHandler(this.btnUpdateStatus_Click);
            // 
            // txtNewStatus
            // 
            this.txtNewStatus.Location = new System.Drawing.Point(228, 30);
            this.txtNewStatus.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtNewStatus.Name = "txtNewStatus";
            this.txtNewStatus.Size = new System.Drawing.Size(623, 22);
            this.txtNewStatus.TabIndex = 3;
            this.txtNewStatus.Text = "It\'s working on windows";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(21, 33);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(149, 17);
            this.label8.TabIndex = 4;
            this.label8.Text = "Change my status to : ";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(298, 156);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(130, 32);
            this.button1.TabIndex = 11;
            this.button1.Text = "MyButton";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 748);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCreateAuthorization;
        private System.Windows.Forms.TextBox txtRequestToken;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtRequestTokenSecret;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnAuthorize;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnGetAcToken;
        private System.Windows.Forms.TextBox txtOauth_token;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtOauth_verify;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtOauth_Access_token;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtOauth_Access_tokenSecret;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox txtNewStatus;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnGetProfileDetail;
        private System.Windows.Forms.Button btnUpdateStatus;
        private System.Windows.Forms.TextBox txtAPIResponse;
        private System.Windows.Forms.Button button1;
    }
}

