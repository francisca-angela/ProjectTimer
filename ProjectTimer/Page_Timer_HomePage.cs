using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace ProjectTimer.Pages
{
	public class Page_Timer_HomePage
	{
		private IWebDriver driver;

		public Page_Timer_HomePage(IWebDriver driver)
		{
			this.driver = driver;
			PageFactory.InitElements(driver, this);
		}

		#region Elements

		[FindsBy(How = How.Id, Using = "EggTimer-start-time-input-text")]
		public IWebElement txt_TimerText { get; set; }

		[FindsBy(How = How.XPath, Using = "//button[contains(text(),'Start')]")]
		public IWebElement btn_Start { get; set; }

		[FindsBy(How = How.XPath, Using = "//div[@class='ClassicTimer-inner']/p/span")]
		public IWebElement txt_CountDown { get; set; }

		#endregion

		public void EnterTimerValue(string val)
		{
			TimerWait(txt_TimerText);
			txt_TimerText.SendKeys(val);
		}

		public void CLickonStartButton()
		{
			TimerWait(btn_Start);
			btn_Start.Click();
		}

		public string GetCountDownval()
		{
			TimerWait(txt_CountDown);
			return txt_CountDown.GetAttribute("Text");
		}

		public void TimerWait(IWebElement elementwaitignfor)
		{
			WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(50));
			wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(elementwaitignfor));
		}

	}
}
