using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using NUnit;
using NUnit.Framework;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports;
using System;
using System.Reflection;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ProjectTimer
{
	[TestClass]
	public class BaseClass
	{
		public IWebDriver driver;
		Pages.Page_Timer_HomePage HomePage;

		public static ExtentTest test;
		public static ExtentReports extent;


		private Microsoft.VisualStudio.TestTools.UnitTesting.TestContext textContextInstance;

		public Microsoft.VisualStudio.TestTools.UnitTesting.TestContext TestContext
		{
			get { return textContextInstance; }
			set
			{
				textContextInstance = value;
			}
		}


		[TestInitialize]
		public void Initialize()
		{
			driver = new ChromeDriver();
			//string _url = GetUrl();
			test = null;

			//Integrated Extend Reports but its not working and could not fix due to time constraints
			//test = extent.CreateTest("").Info("Test Started");
			driver.Manage().Window.Maximize();
			driver.Navigate().GoToUrl("https://e.ggtimer.com/");
			//test.Log(Status.Info, "Url Launched");

			try
			{
				WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(1));
				wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(OpenQA.Selenium.By.Id("EggTimer-start-time-input-text")));
			}
			catch (Exception e)
			{
				//test.Log(Status.Fail, "Test Failed" + e);
				throw;
			}
		}

		[OneTimeSetUp]
		public void extentstart()
		{
			extent = new ExtentReports();
			var htmlreporter = new ExtentHtmlReporter(@"C:\Reports\TestResult" + DateTime.Now.ToString("_MMddyyyy_hhmmtt") + ".html");
			extent.AttachReporter(htmlreporter);
		}

		[Test]
		public void BrowserTest()
		{
			//Tried to dynamically read the xml file from Project solution to avoid hard coding but didn't work 
			//string _url = GetUrl();
			test = null;
			test = extent.CreateTest("").Info("Test Started");
			driver.Manage().Window.Maximize();
			driver.Navigate().GoToUrl("https://e.ggtimer.com/");
			test.Log(Status.Info, "Url Launched");

			try
			{
				WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(1));
				wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(OpenQA.Selenium.By.Id("EggTimer-start-time-input-text")));
			}
			catch (Exception e)
			{
				test.Log(Status.Fail, "Test Failed");
				throw;
			}
		}

		private string GetUrl()
		{

			string url = string.Empty;
			var assembly = Assembly.GetExecutingAssembly();
			var strResource = assembly.GetName().Name + @"\Config.xml";
			var rstream = assembly.GetManifestResourceStream(strResource);
			XmlReader reader = XmlReader.Create(rstream);

			XmlDocument xmldoc = new XmlDocument();
			xmldoc.LoadXml(strResource);

			int count = 0;
			count = xmldoc.ChildNodes.Item(0).FirstChild.ChildNodes.Count;
			for (int i = 0; i < count; i++)
			{
				if (xmldoc.ChildNodes.Item(0).FirstChild.ChildNodes.Item(i).Name == "url")
				{
					url = xmldoc.ChildNodes.Item(0).FirstChild.ChildNodes.Item(i).InnerText.Trim();
				}
			}

			return url;
		}

		[TestCleanup]
		public void Close()
		{
			try
			{
				driver.Close();
			}
			catch (Exception e)
			{
			}

		}
	

	}
}
