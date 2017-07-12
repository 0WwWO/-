using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication2 {
	 public partial class WebForm1 : System.Web.UI.Page {
		  protected void Page_Load(object sender, EventArgs e) {

		  }

		  protected void Button1_Click(object sender, EventArgs e) {
			   usec(@"C:\Program Files (x86)\TeamViewer\TeamViewer.exe");
		  }

		  #region ====1
		  /// <summary>
		  /// 运行外部程序
		  /// </summary>
		  /// <param name="exeName">程序路径</param>
		  /// <returns>0:失败，1:成功</returns>
		  public bool RunIt(string exeName) {
			   //声明一个程序信息类
			   System.Diagnostics.ProcessStartInfo Info = new System.Diagnostics.ProcessStartInfo();
			   //设置外部程序名
			   Info.FileName = exeName;
			   //声明一个程序类
			   try {
					System.Diagnostics.Process Proc;
					Proc = System.Diagnostics.Process.Start(Info);
					return true;
			   } catch {
					return false;
			   }
		  }
		  /// <summary>
		  /// 判断是否运行
		  /// </summary>
		  /// <param name="exeName">程序名</param>
		  /// <returns>0:没运行，1:运行中</returns>
		  public bool IsRun(string exeName) {
			   string isrunning = "0";
			   Process[] myProcesses = Process.GetProcesses();
			   foreach (Process myProcess in myProcesses) {
					if (myProcess.ProcessName == exeName) {
						 isrunning = "1";
						 break;
					}
			   }
			   if (isrunning == "1") {
					return true;
			   } else {
					return false;
			   }
		  }
		  /// <summary>
		  /// 结束进程
		  /// </summary>
		  /// <param name="exeName">进程名</param>
		  /// <returns>0:失败，1:成功</returns>
		  public bool Kill(string exeName) {
			   string isrunning = "0";
			   Process[] myProcesses = Process.GetProcesses();
			   foreach (Process myProcess in myProcesses) {
					if (myProcess.ProcessName == exeName) {
						 try {
							  myProcess.Kill();
							  isrunning = "1";
						 } catch {
							  isrunning = "0";
						 }
						 break;
					}
			   }
			   if (isrunning == "1") {
					return true;
			   } else {
					return false;
			   }
		  }
		  #endregion

		  #region ====2
		  /// <summary>
		  /// 運行外部Windows程序
		  /// </summary>
		  /// <param name="userName">程序路徑</param>
		  private void usec(string userName) {
			   //声明一个程序信息类
			   System.Diagnostics.ProcessStartInfo Info = new System.Diagnostics.ProcessStartInfo();

			   //设置外部程序名
			   Info.FileName = userName;
			   //string path = System.Configuration.ConfigurationSettings.AppSettings["chash"];
			   //path = path.Replace("//", "////");
			   //设置外部程序的启动参数（命令行参数）为test.txt
			   //Info.Arguments = "/c /createauth.exe "+userName+" "+realm+" "+Pwd+" > createauth / "";

			   //设置外部程序工作目录为  C:/
			   //Info.WorkingDirectory = path;
			   //Response.Write(path);

			   //声明一个程序类
			   System.Diagnostics.Process Proc;

			   try {
					//启动外部程序
					Proc = System.Diagnostics.Process.Start(Info);
			   } catch (System.ComponentModel.Win32Exception e) {
					Response.Write("系统找不到指定的程序文/r{0}" + e);
					Response.End();
					return;
			   }
			   //打印出外部程序的开始执行时间
			   Response.Write("外部程序的开始执行时间：");
			   //等待10秒钟
			   Proc.WaitForExit(10000);

			   //如果这个外部程序没有结束运行则对其强行终止
			   if (Proc.HasExited == false) {
					Response.Write("由主程序强行终止外部程序的运行！");
					Proc.Kill();
			   } else {
					Response.Write("由外部程序正常退出！");
			   }
		  }
		  #endregion
	 }
}