using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;


namespace ProjectTimer
{
	[TestClass]
	public class UnitTest1: BaseClass
	{
		Pages.Page_Timer_HomePage TimerPage1;

			//private static ChromeDriver chromeDriver;
		[TestMethod]
		public void TestMethod1()
		{

			TimerPage1 = new Pages.Page_Timer_HomePage(driver);
			TimerPage1.EnterTimerValue("25");
			TimerPage1.CLickonStartButton();


			string timeleft = string.Empty;





			for (int i = 1; i <= 25; i++)
			{
				//Wait for a second before incrementing
				System.Threading.Thread.Sleep(1000);
				timeleft = TimerPage1.GetCountDownval();
				Assert.AreEqual(i, timeleft);
				
			}
		}
	}
}
