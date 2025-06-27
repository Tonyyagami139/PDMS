using Common;
using Dal;
using Database;
using DocumentFormat.OpenXml.Bibliography;
using PDMS.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace PDMS
{
    public partial class FormLogIn : Form
    {
        public FormLogIn()
        {
            InitializeComponent();
        }

        private void FormLogIn_Load(object sender, EventArgs e)
        {
            this.BackgroundImageLayout = ImageLayout.Center;
            失效记录ToolStripMenuItem.Enabled = false;
            失效记录ToolStripMenuItem.ForeColor = Color.White;
            menuStrip1.RenderMode = ToolStripRenderMode.System;
            menuStrip1.BackColor = Color.RoyalBlue;
            menuStrip1.ForeColor = Color.White;   // 可选：改一下文字颜色以便更清晰
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MaximizeBox = false;
            DeserializeUser();
            bt_login.Focus();
        }

        private void bt_login_Click(object sender, EventArgs e)
        {
            string username = tb_userName.Text;
            string password = tb_passWord.Text;
            try
            {
                UserDal userDal = new UserDal(Global.DbSettingTym);
                if (userDal.Login(username, password))
                {
                    SerializeUser(username, password);
                    Global.UserName = username;
                    Global.UserLevel = userDal.GetAccessLevel(username);
                    HandleCommonAccess();
                    this.Text += "  User: " + username;
                    panel1.Visible = false;
                    Image image = Image.FromFile(Global.LogoPicture);
                    pictureBox1.Image = image;
                    //特殊权限处理
                    if (userDal.HaveAccess(Global.UserLevel))
                    {
                        HandleUserAccess(true);
                    }
                    else
                    {
                        HandleUserAccess(false);
                    }
                    
                }
                else
                {
                    MessageBox.Show("用户名或密码错误");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }
        private void SerializeUser(string username, string password)
        {
            User user = new User();
            user.Username = username;
            user.Password = password;
            // Serialize the object to a file
            using (FileStream stream = new FileStream(Global.UserDataPath, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, user);
            }
        }

        private void DeserializeUser()
        {
            var dir = Path.GetDirectoryName(Global.UserDataPath);
            if (!Directory.Exists(dir)) 
            { 
                Directory.CreateDirectory(dir);
            } 
            if(!File.Exists(Global.UserDataPath))
            {
                return;
            }
            // Deserialize the object from a file
            using (FileStream stream = new FileStream(Global.UserDataPath, FileMode.OpenOrCreate))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                User user = (User)formatter.Deserialize(stream);
                tb_userName.Text = user.Username;
                tb_passWord.Text = user.Password;
            }
        }
        private void HandleCommonAccess()
        {
            失效记录ToolStripMenuItem.Enabled = true;
        }

        private void HandleUserAccess(bool enable)
        {

        }

        private void 关闭ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 失效记录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormFailureRecord formFailureRecord = new FormFailureRecord();
            formFailureRecord.Show();
        }

        private void 数据模板ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                FormExcelTemplate f = new FormExcelTemplate();
                f.Show();
            }
            catch
            {
                FormExcelTemplate f = new FormExcelTemplate();
                f.Show();
            }
        }
    }
}

